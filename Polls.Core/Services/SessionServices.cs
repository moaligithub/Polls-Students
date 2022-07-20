using AutoMapper;
using NToastNotify;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.Interfaces.IUnitOfWork;
using Polls.Domain.Models;
using Polls.Domain.ViewModel.Course;
using Polls.Domain.ViewModel.Instructor;
using Polls.Domain.ViewModel.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Core.Services
{
    public class SessionServices : ISessionServices
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public SessionServices(IUnitOfWork unit, IMapper mapper, IToastNotification toastNotification)
        {
            _unit = unit;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        public async Task<bool> CreateSessionFromViewModelAsync(CreateSessionViewModel sessionViewModel)
        {
            Session session = _mapper.Map<Session>(sessionViewModel);

            try
            {
                await _unit.Session.AddAsync(session);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, Can`t add this session");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Create Successfully");
            return true;
        }

        public async Task<int> DeleteSessionAsync(int id)
        {
            Session session = await _unit.Session.GetByIdAsync(id);

            if (session is null)
                return 404;

            try
            {
                _unit.Session.Delete(session);
                _unit.Complete();
            }
            catch
            {
                return 400;
            }

            return 200;
        }

        public async Task<AllSessionsIndexViewModel> GeAllSessionsAsync()
        {
            List<Session> sessions = await _unit.Session.GetAllAsync(
                new System.Linq.Expressions.Expression<Func<Session, object>>[] { s => s.Course, s => s.Instructor });
            
            List<SessionIndexViewModel> sessionsIndex = _mapper.Map<List<SessionIndexViewModel>>(sessions);

            AllSessionsIndexViewModel viewModel = new AllSessionsIndexViewModel
            {
                Sessions = sessionsIndex,
                Courses = _mapper.Map<List<CourseViewModel>>(await _unit.Course.GetAllAsync()),
                Instructors = _mapper.Map<List<InstructorIndexViewModel>>(await _unit.Instructor.GetAllAsync())
            };

            return viewModel;
        }

        public async Task<AllSessionsIndexViewModel> GeAllSessionsInCourseAsync(int courseId)
        {
            List<Session> sessions = await _unit.Session.FindAllAsync(s => s.CourseId == courseId, 
                new System.Linq.Expressions.Expression<Func<Session, object>>[] { s => s.Course, s => s.Instructor });

            List<SessionIndexViewModel> sessionsIndex = _mapper.Map<List<SessionIndexViewModel>>(sessions);

            AllSessionsIndexViewModel viewModel = new AllSessionsIndexViewModel
            {
                Sessions = sessionsIndex,
                Courses = _mapper.Map<List<CourseViewModel>>(await _unit.Course.GetAllAsync()),
                Instructors = _mapper.Map<List<InstructorIndexViewModel>>(await _unit.Instructor.GetAllAsync())
            };

            return viewModel;
        }

        public async Task<bool> UpDateSessionFromViewModelAsync(EditSessionViewModel sessionViewModel)
        {
            if (!await _unit.Session.AnyAsync(s => s.Id == sessionViewModel.Id))
            {
                _toastNotification.AddErrorToastMessage("this session is not found!");
                return false;
            }

            Session session = _mapper.Map<Session>(sessionViewModel);

            try
            {
                _unit.Session.UpDate(session);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, Can`t Update this session");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Update Successfully");
            return true;
        }
    }
}