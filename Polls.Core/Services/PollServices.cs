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
    public class PollServices : IPollServices
    {
        private readonly IUnitOfWork _unit;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;

        public PollServices(IUnitOfWork unit, IToastNotification toastNotification, IMapper mapper)
        {
            _unit = unit;
            _toastNotification = toastNotification;
            _mapper = mapper;
        }

        public async Task<bool> CreatePollStudentAsync(CreatePollStudentViewModel createPollStudent, string StudentName, string StudentId, string StudentDepartment)
        {
            bool PollExist = await _unit.Poll.AnyAsync(p => p.StudentId == StudentId 
                && p.CourseId == createPollStudent.CourseId);

            if (PollExist)
            {
                _toastNotification.AddErrorToastMessage("You can`t Submit this Poll");
                return false;
            }

            bool AnswerisNull = createPollStudent.Questions.Any(q => q.Rating == null);

            if (AnswerisNull)
            {
                _toastNotification.AddErrorToastMessage("Select All Questions");
                return false;
            }

            Poll poll = new Poll
            {
                CourseId = createPollStudent.CourseId,
                Questions = _mapper.Map<List<QuestionPoll>>(createPollStudent.Questions),
                StudentName = StudentName,
                StudentId = StudentId,
                StudentDepartment = StudentDepartment
            };

            try
            {
                await _unit.Poll.AddAsync(poll);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, Can`t Submit this poll");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Submit Successfully");
            return true;
        }

        public async Task<List<BasePollForIndexViewModel>> GetAllBasePollAsync()
        {
            List<Course> courses = await _unit.Course.FindAllAsync(c => c.Questions.Count() != 0,
                new System.Linq.Expressions.Expression<Func<Course, object>>[] { c 
                => c.Questions, c => c.Polls });

            return _mapper.Map<List<BasePollForIndexViewModel>>(courses);
        }

        public async Task<PollStudentDetailsViewModel> GetPollStudentDetailsAsync(int pollId)
        {
            Poll poll = await _unit.Poll.FindAsync(p => p.Id == pollId,
                new Expression<Func<Poll, object>>[] { p => p.Questions, p => p.Course });

            if (poll == null)
                return null;

            return _mapper.Map<PollStudentDetailsViewModel>(poll);
        }

        public async Task<List<PollStudentViewModel>> GetStudentsPollsInCourseAsync(int courseId)
        {
            List<Poll> polls = await _unit.Poll.FindAllAsync(p => p.CourseId == courseId);

            if (polls.Count == 0)
                return null;

            return _mapper.Map<List<PollStudentViewModel>>(polls).OrderByDescending(p => p.Id).ToList();
        }
    }
}
