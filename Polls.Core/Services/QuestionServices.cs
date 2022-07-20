using AutoMapper;
using NToastNotify;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.Interfaces.IUnitOfWork;
using Polls.Domain.Models;
using Polls.Domain.ViewModel.Poll;
using Polls.Domain.ViewModel.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Core.Services
{
    public class QuestionServices : IQuestionServices
    {
        private readonly IUnitOfWork _unit;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;

        public QuestionServices(IUnitOfWork unit, IToastNotification toastNotification, IMapper mapper)
        {
            _unit = unit;
            _toastNotification = toastNotification;
            _mapper = mapper;
        }

        public async Task<bool> CreateQuestionsForCourseAsync(CreateQuestionsViewModel viewModel)
        {
            bool CourseExist = await _unit.Course.AnyAsync(c => c.Id == viewModel.CourseId);

            if (!CourseExist)
            {
                _toastNotification.AddErrorToastMessage("This course is not found");
                return false;
            }

            List<Question> questions = viewModel.Questions.Select(x => new Question
            {
                Text = x,
                CourseId = viewModel.CourseId
            }).ToList();

            try
            {
                await _unit.Question.AddRangeAsync(questions);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, Can`t create this questions");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Create Successfully");
            return true;
        }

        public async Task<bool> CreateQuestionsForCoursesAsync(AllCreateQuestionsViewModel viewModel)
        {
            List<int> coursesId = viewModel.Courses.Where(c => c.IsSelected).Select(c => c.Id).ToList();

            if (coursesId.Count() == 0)
            {
                _toastNotification.AddErrorToastMessage("Please, Select course");
                return false;
            }

            List<string> questions = viewModel.Questions.Where(x => x != null).ToList();

            if (questions.Count == 0)
            {
                _toastNotification.AddErrorToastMessage("Please, Add Question");
                return false;
            }

            List<Question> AllQuestions = new List<Question>();

            for (int i = 0; i < coursesId.Count(); i++)
                AllQuestions.AddRange(questions.Select(q => new Question
                {
                    Text = q,
                    CourseId = coursesId[i]
                }).ToList());

            try
            {
                await _unit.Question.AddRangeAsync(AllQuestions);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Can`t, Add this poll");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Create Successfully");
            return true;
        }

        public async Task<int> DeleteOneQuestionAsync(int questionId)
        {
            Question question = await _unit.Question.GetByIdAsync(questionId);

            if (question == null)
                return 404;

            try
            {
                _unit.Question.Delete(question);
                _unit.Complete();
            }
            catch
            {
                return 400;
            }

            return 200;
        }

        public async Task<int> DeleteQuestionInCourseAsync(int courseId)
        {
            List<Question> questions = await _unit.Question.FindAllAsync(q => q.CourseId == courseId);

            if (questions.Count() == 0)
                return 404;

            try
            {
                _unit.Question.DeleteRange(questions);
                _unit.Complete();
            }
            catch
            {
                return 400;
            }

            return 200;
        }

        public async Task<CreatePollStudentViewModel> GetQuestionForCreatePollAsync(int courseId)
        {
            Course course = await _unit.Course.FindAsync(c => c.Id == courseId,
                new Expression<Func<Course, object>>[] { c => c.Questions });

            if (course == null || course.Questions.Count() == 0)
                return null;

            CreatePollStudentViewModel viewModel = new CreatePollStudentViewModel
            {
                CourseId = courseId,
                CourseName = course.Title,
                Questions = course.Questions.Select(q => new QuestionStudentViewModel
                {
                    Text = q.Text,
                    Rating = string.Empty
                }).ToList()
            };

            return viewModel;
        }

        public async Task<List<EditQuestionViewModel>> GetQuestionsForEditAsync(int courseId)
        {
            List<Question> questions = await _unit.Question.FindAllAsync(q => q.CourseId == courseId);

            return _mapper.Map<List<EditQuestionViewModel>>(questions);
        }

        public bool UpDateQuestionsFromViewModel(List<EditQuestionViewModel> editQuestions)
        {
            List<EditQuestionViewModel> editQuestionViewModels = editQuestions.Where(q => q.IsSelected).ToList();

            if (editQuestionViewModels.Count == 0 )
            {
                _toastNotification.AddErrorToastMessage("Please, select question");
                return false;
            }

            List<Question> questions = _mapper.Map<List<Question>>(editQuestionViewModels);

            try
            {
                _unit.Question.UpDateRange(questions);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, Can`t update this questions");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Update Successfully");
            return true;
        }
    }
}
