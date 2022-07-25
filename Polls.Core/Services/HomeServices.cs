using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Polls.Domain.Const;
using Polls.Domain.Consts;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.Interfaces.IUnitOfWork;
using Polls.Domain.Models;
using Polls.Domain.ViewModel.Course;
using Polls.Domain.ViewModel.Home;
using Polls.Domain.ViewModel.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Core.Services
{
    public class HomeServices : IHomeServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        private readonly IQuestionServices _questionServices;
        private readonly IJsonHelper _jsonHelper;

        public HomeServices(IMapper mapper, IUnitOfWork unit, IQuestionServices questionServices, 
            IJsonHelper jsonHelper)
        {
            _mapper = mapper;
            _unit = unit;
            _questionServices = questionServices;
            _jsonHelper = jsonHelper;
        }

        public async Task<DashboardHomeViewModel> GetDashboardHomeAsync()
        {
            List<Course> courses = await _unit.Course.FindAllAsync(c => c.Polls.Count() != 0, c => c.Polls.Count(), OrderBy.orderbydescending);
            List<Instructor> instructors = await _unit.Instructor.GetAllAsync(ins
                => ins.TotalReview, OrderBy.orderbydescending, 3);

            DashboardHomeViewModel viewModel = new DashboardHomeViewModel()
            {
                Courses = _mapper.Map<List<CourseViewModel>>(courses),
                Instructors = _mapper.Map<List<InstructorIndexViewModel>>(instructors),

                CoursesCount = await _unit.Course.CountAsync(),
                InstructorsCount = await _unit.Instructor.CountAsync(),
                SessionsCount = await _unit.Session.CountAsync(),
                StudentsPollsCount = await _unit.Poll.CountAsync()
            };          

            return viewModel;
        }

        public async Task<HomeIndexViewModel> GetHomeIndexAsync(int InstructorsCount, int CoursesCount)
        {
            List<Course> courses = await _unit.Course.GetAllAsync(c => c.Views, OrderBy.orderbydescending, CoursesCount);
            
            List<Instructor> instructors = await _unit.Instructor.GetAllAsync(
                new System.Linq.Expressions.Expression<Func<Instructor, object>>[] { ins
                => ins.Contact }, ins => ins.TotalReview, OrderBy.orderbydescending, InstructorsCount);

            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                Courses = courses.Select(c => new CourseHomeViewModel 
                {
                    Id = c.Id,
                    Title = c.Title,
                    Views = c.Views,
                    Description = new string(c.Description.Take(200).ToArray())
                }).ToList(),
                Instructors = _mapper.Map<List<InstructorHomeViewModel>>(instructors),
                SessionsCount = await _unit.Session.CountAsync(),
                CoursesCount = await _unit.Course.CountAsync(),
                InstructorsCount = await _unit.Instructor.CountAsync(),
                BasePollsCount = await _unit.Course.CountAsync(c => c.Questions.Count != 0)
            };

            return viewModel;
        }
    }
}
