using AutoMapper;
using Polls.Core.Helpers;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.Interfaces.IUnitOfWork;
using Polls.Domain.Models;
using Polls.Domain.ViewModel.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polls.Domain.Const;
using NToastNotify;
using Microsoft.AspNetCore.Hosting;
using Polls.Domain.ViewModel.Department;
using System.Linq.Expressions;

namespace Polls.Core.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private DateTimeHelper dateTimeHelper = new DateTimeHelper();

        public CourseServices(IUnitOfWork unit, IMapper mapper, IToastNotification toastNotification)
        {
            _unit = unit;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        public async Task<bool> CreateCourseFromViewModel(CreateCourseViewModel courseViewModel)
        {
            if (courseViewModel.Departments.Where(d => d.IsSelected).Count() == 0)
            {
                _toastNotification.AddErrorToastMessage("Please, select department");
                return false;
            }

            Course course = _mapper.Map<Course>(courseViewModel);
            
            course.Departments.Clear();
            
            foreach (var item in courseViewModel.Departments.Where(d => d.IsSelected))
            {
                Department department = await _unit.Department.GetByIdAsync(item.Id);
                course.Departments.Add(department);
            }

            course.DateTime = DateTime.Now;

            try
            {
                await _unit.Course.AddAsync(course);
                _unit.Complete();
            }
            catch
            {            
                _toastNotification.AddErrorToastMessage("Sorry, Can`t add this course");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Add Successfully");
            return true;
        }

        public async Task<int> DeleteCourseAsync(int id)
        {
            Course course = await _unit.Course.GetByIdAsync(id);

            if (course == null)
                return 404;

            try
            {
                _unit.Course.Delete(course);
                _unit.Complete();
            }
            catch
            {
                return 400;
            }

            return 200;
        }

        public async Task<List<CourseViewModel>> GetAllCourseForDropListAsync()
        {
            List<Course> courses = await _unit.Course.GetAllAsync();
            
            List<CourseViewModel> viewModels = _mapper.Map<List<CourseViewModel>>(courses);

            return viewModels;
        }

        public async Task<List<CourseIndexViewModel>> GetAllCoursesAsync()
        {
            List<Course> courses = await _unit.Course.GetAllAsync();

            List<CourseIndexViewModel> coursesViewModel = courses.Select(c => new CourseIndexViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Views = c.Views,
                Date = dateTimeHelper.GetDateFromDateTimeToString(c.DateTime),
                SessionsCount = _unit.Session.CountAsync(s => s.CourseId == c.Id).Result,
                PollsCount = _unit.Poll.CountAsync(p => p.CourseId == c.Id).Result
            }).ToList();

            return coursesViewModel.OrderByDescending(c => c.Views).ToList();
        }

        public async Task<List<CourseHomeViewModel>> GetAllCoursesForHomeAsync()
        {
            List<Course> courses = await _unit.Course.GetAllAsync(c => c.Views, OrderBy.orderbydescending);

            return courses.Select(c => new CourseHomeViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Views = c.Views,
                Description = new string(c.Description.Take(200).ToArray())
            }).ToList();
        }

        public async Task<CourseDetailsViewModel> GetCourseDetailsAsync(int id)
        {
            Course course = await _unit.Course.FindAsync(c => c.Id == id,
                new Expression<Func<Course, object>>[] { c => c.Departments, c => c.Sessions });

            if (course == null)
                return null;

            try
            {
                course.Views++;

                _unit.Course.UpDate(course);
                _unit.Complete();
            }
            catch
            {
                return null;
            }

            CourseDetailsViewModel viewModel = _mapper.Map<CourseDetailsViewModel>(course);

            int QuestionCount = await _unit.Question.CountAsync(q => q.CourseId == course.Id);

            if (QuestionCount > 0)
                viewModel.Poll = true;

            return viewModel;   
        }

        public async Task<CreateCourseViewModel> GetCourseForEditAsync(int id)
        {
            Course course = await _unit.Course.FindAsync(c => c.Id == id, 
                new Expression<Func<Course, object>>[] { c => c.Departments });

            CreateCourseViewModel viewModel = _mapper.Map<CreateCourseViewModel>(course);

            viewModel.Departments = course.Departments.Select(d => new DepartmentCheckBoxViewModel
            {
                Id = d.Id, 
                Name = d.Name, 
                IsSelected = true
            }).ToList();

            List<Department> departments = await _unit.Department.GetAllAsync();

            foreach (var item in course.Departments)
            {
                if (departments.Contains(item))
                    departments.Remove(item);
            }

            viewModel.Departments.AddRange(_mapper.Map<List<DepartmentCheckBoxViewModel>>(departments));

            return viewModel;
        }

        public async Task<List<CourseIndexViewModel>> GetCoursesInDepartmentAsync(int departmentId)
        {
            Department department = await _unit.Department.FindAsync(d => d.Id == departmentId,
                new Expression<Func<Department, object>>[] { d => d.Courses });

            List<CourseIndexViewModel> coursesViewModel = department.Courses.Select(c => new CourseIndexViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Views = c.Views,
                Date = dateTimeHelper.GetDateFromDateTimeToString(c.DateTime),
                SessionsCount = _unit.Session.CountAsync(s => s.CourseId == c.Id).Result,
                PollsCount = _unit.Poll.CountAsync(p => p.CourseId == c.Id).Result
            }).ToList();

            return coursesViewModel.OrderByDescending(c => c.Views).ToList();
        }

        public async Task<bool> UpDateCourseFromViewModelAsync(CreateCourseViewModel editCourseView)
        {
            if (editCourseView.Departments.Where(d => d.IsSelected).Count() == 0)
            {
                _toastNotification.AddErrorToastMessage("Please, select department");
                return false;
            }

            Course course = await _unit.Course.FindAsync(c => c.Id == editCourseView.Id,
                new Expression<Func<Course, object>>[] { c => c.Departments });

            if (course.Title != editCourseView.Title)
                course.Title = editCourseView.Title;

            if (course.Description != editCourseView.Description)
                course.Description = editCourseView.Description;

            course.Departments.Clear();

            foreach (var item in editCourseView.Departments.Where(d => d.IsSelected))
            {
                if(!course.Departments.Select(d => d.Id).Contains(item.Id))
                {
                    Department department = await _unit.Department.GetByIdAsync(item.Id);
                    course.Departments.Add(department);
                }
            }

            try
            {
                _unit.Course.UpDate(course);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, Can`t update this course");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Update successfully");
            return true;
        }
    }
}