using Polls.Domain.Interfaces.IServices;
using Polls.Domain.Interfaces.IUnitOfWork;
using Polls.Domain.Models;
using Polls.Domain.ViewModel.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polls.Domain.Const;
using Polls.Core.Helpers;
using NToastNotify;
using AutoMapper;
using System.Linq.Expressions;

namespace Polls.Core.Services
{
    public class InstructorServices : IInstructorServices
    {
        private readonly IUnitOfWork _unit;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;
        private readonly FileHelper _fileHelper = new FileHelper();

        public InstructorServices(IUnitOfWork unit, IToastNotification toastNotification, 
            IMapper mapper)
        {
            _unit = unit;
            _toastNotification = toastNotification;
            _mapper = mapper;
        }

        public async Task<bool> CreateInstructorFromViewModelAsync(CreateInstructorViewModel instructorViewModel)
        {
            if (instructorViewModel.File != null)
            {
                string[] extensions = { Extensions.PNG, Extensions.JPG };

                if (!_fileHelper.ValidExtension(instructorViewModel.File, extensions))
                {
                    _toastNotification.AddErrorToastMessage("Allow JPG, PNG Extensions Qnly");
                    return false;
                }

                if (!_fileHelper.ValidSize(instructorViewModel.File, FilesSize.MB1))
                {
                    _toastNotification.AddErrorToastMessage("Max Size is 1MB");
                    return false;
                }
            }

            Instructor instructor = _mapper.Map<Instructor>(instructorViewModel); 

            if (instructorViewModel.File != null)
                instructor.ProfilePicture = _fileHelper.UpLoadeToDatabase(instructorViewModel.File);

            try
            {
                await _unit.Instructor.AddAsync(instructor); 
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, Can`t add this instructor");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Create Successfully");
            return true;
        }

        public async Task<bool> CreateInstructorReviewAsync(CreateInstructorReview review, string studentId)
        {
            bool IsExist = await _unit.InstructorReview.AnyAsync(r => r.InstructorId == review.InstructorId
                && r.StudentId == studentId);
            
            if (IsExist)
            {
                _toastNotification.AddErrorToastMessage("You can`t submit your rating");
                return false;
            }

            if (review.Review == 0)
            {
                _toastNotification.AddErrorToastMessage("Choose a review");
                return false;
            }
            
            InstructorReview instructorReview = _mapper.Map<InstructorReview>(review);  
            instructorReview.StudentId = studentId;

            int ReviweCount = await _unit.InstructorReview.CountAsync(ins => ins.InstructorId == review.InstructorId);

            Instructor instructor = await _unit.Instructor.GetByIdAsync(review.InstructorId);

            instructor.TotalReview = (((instructor.TotalReview * ReviweCount) + review.Review) / (ReviweCount + 1));

            try
            {
                await _unit.InstructorReview.AddAsync(instructorReview);
                _unit.Instructor.UpDate(instructor);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry , can`t submit your rating");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Submit Successfully");
            return true;
        }

        public async Task<int> DeleteAsync(int id)
        {
            Instructor instructor = await _unit.Instructor.GetByIdAsync(id);

            if (instructor is null)
                return 404;

            try
            {
                _unit.Instructor.Delete(instructor);
                _unit.Complete();
            }
            catch
            {
                return 400;
            }

            return 200;
        }

        public async Task<List<InstructorIndexViewModel>> GetAllAsync()
        {
            List<Instructor> instructors = await _unit.Instructor.GetAllAsync();

            return instructors.Select(ins => new InstructorIndexViewModel
            {
                Id = ins.Id,
                FirstName = ins.FirstName,
                LastName = ins.LastName,
                JobTitle = ins.JobTitle
            }).ToList();
        }

        public async Task<List<InstructorHomeViewModel>> GetAllInstructorsForHomeAsync()
        {
            List<Instructor> instructors = await _unit.Instructor.GetAllAsync(
                new System.Linq.Expressions.Expression<Func<Instructor, object>>[] { ins
                => ins.Contact }, ins => ins.TotalReview, OrderBy.orderbydescending);

            return _mapper.Map<List<InstructorHomeViewModel>>(instructors);
        }

        public async Task<EditInstructorViewModel> GetInstructorForEditAsync(int id)
        {
            Instructor instructor = await _unit.Instructor.FindAsync(x => x.Id == id,
                new System.Linq.Expressions.Expression<Func<Instructor, object>>[] { x => x.Contact });

            return _mapper.Map<EditInstructorViewModel>(instructor);
        }

        public async Task<InstructorReviewsViewModel> GetInstructorReviewsAsync(int instructorId, string studentId)
        {
            Instructor instructor = await _unit.Instructor.FindAsync(ins => ins.Id == instructorId,
                new Expression<Func<Instructor, object>>[] { ins => ins.Contact, ins => ins.Reviews });
        
            if (instructor is null)
                return null;

            InstructorReviewsViewModel viewModel = _mapper.Map<InstructorReviewsViewModel>(instructor);
            viewModel.InstructorReviews = _mapper.Map<List<InstructorReviewViewModel>>(instructor.Reviews);

            if (studentId == null)
                return viewModel;

            InstructorReview Review = instructor.Reviews.Where(r => r.InstructorId == instructorId
            && r.StudentId == studentId).SingleOrDefault();

            viewModel.StudentReview = _mapper.Map<InstructorReviewViewModel>(Review);

            return viewModel;
        }

        public bool UpDateInstructorFromViewModel(EditInstructorViewModel editInstructor)
        {
            if (editInstructor.File != null)
            {
                string[] extensions = { Extensions.PNG, Extensions.JPG };

                if (!_fileHelper.ValidExtension(editInstructor.File, extensions))
                {
                    _toastNotification.AddErrorToastMessage("Allow JPG, PNG Extensions Qnly");
                    return false;
                }

                if (!_fileHelper.ValidSize(editInstructor.File, FilesSize.MB1))
                {
                    _toastNotification.AddErrorToastMessage("Max Size is 1MB");
                    return false;
                }
            }

            Instructor instructor = _mapper.Map<Instructor>(editInstructor);

            if (editInstructor.File != null)
                instructor.ProfilePicture = _fileHelper.UpLoadeToDatabase(editInstructor.File);

            try
            {
                _unit.Instructor.UpDate(instructor);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, Can`t Update this Instructor");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Update Successfully");
            return true;
        }

        public async Task<bool> UpDateInstructorReviewAsync(CreateInstructorReview review, string studentId)
        {
            InstructorReview instructorReview = await _unit.InstructorReview.FindAsync(r
                => r.InstructorId == review.InstructorId && r.StudentId == studentId, 
                new Expression<Func<InstructorReview, object>>[] { r => r.Instructor });

            if (instructorReview.Description != review.Description)
                instructorReview.Description = review.Description;

            if (instructorReview.Review != review.Review)
            {
                int CountReviews = await _unit.InstructorReview.CountAsync(ins => ins.InstructorId == review.InstructorId);
                var totalReviews = instructorReview.Instructor.TotalReview * CountReviews;
                totalReviews -= instructorReview.Review;
                totalReviews += review.Review;
                instructorReview.Instructor.TotalReview = totalReviews / CountReviews;
                
                instructorReview.Review = review.Review;
            }

            try
            {
                _unit.InstructorReview.UpDate(instructorReview);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, can`t submit your rating");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("UpDate Successfully");
            return true;
        }
    }
}