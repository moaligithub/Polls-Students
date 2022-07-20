using AutoMapper;
using Polls.Domain.Models;
using Polls.Domain.ViewModel.Contact;
using Polls.Domain.ViewModel.Course;
using Polls.Domain.ViewModel.Department;
using Polls.Domain.ViewModel.Instructor;
using Polls.Domain.ViewModel.Poll;
using Polls.Domain.ViewModel.Question;
using Polls.Domain.ViewModel.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Instructor
            CreateMap<CreateInstructorViewModel, Instructor>();
            CreateMap<EditInstructorViewModel, Instructor>().ReverseMap();
            CreateMap<Instructor, InstructorIndexViewModel>();
            CreateMap<Instructor, InstructorHomeViewModel>();
            CreateMap<Instructor, InstructorReviewsViewModel>();
            CreateMap<InstructorReview, InstructorReviewViewModel>();
            CreateMap<CreateInstructorReview, InstructorReview>();

            // Contact
            CreateMap<ContactViewModel, Contact>().ReverseMap();
            CreateMap<EditContactViewModel, Contact>().ReverseMap();
            
            // Department
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<Department, DepartmentCheckBoxViewModel>().ReverseMap();
           
            // Course
            CreateMap<CreateCourseViewModel, Course>().ReverseMap();
            CreateMap<Course, CourseViewModel>();
            CreateMap<Course, CourseHomeViewModel>();
            CreateMap<Course, CourseIndexViewModel>();
            CreateMap<Course, CourseDetailsViewModel>()
                .ForMember(dest => dest.Departments, src => src.MapFrom(src => src.Departments.Select(d => d.Name)));
            CreateMap<Course, BasePollForIndexViewModel>()
                .ForMember(dest => dest.CourseName, src => src.MapFrom(src => src.Title))
                .ForMember(dest => dest.QuestionsCount, src => src.MapFrom(src => src.Questions.Count()))
                .ForMember(dest => dest.PollsStudentCount, src => src.MapFrom(src => src.Polls.Count))
                .ForMember(dest => dest.CourseId, src => src.MapFrom(src => src.Id));

            // Session
            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, SessionViewModel>();
            CreateMap<EditSessionViewModel, Session>().ReverseMap();
            CreateMap<Session, SessionIndexViewModel>()
                .ForMember(dest => dest.CourseName, src => src.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.InstructorName, src => src.MapFrom(src 
                    => src.Instructor.FirstName + " " + src.Instructor.LastName));

            // Question
            CreateMap<string, Question>()
                .ForMember(dest => dest.Text, src => src.MapFrom(src => src));
            CreateMap<Question, EditQuestionViewModel>().ReverseMap();

            // QuestionPoll
            CreateMap<QuestionStudentViewModel, QuestionPoll>().ReverseMap();

            // Poll
            CreateMap<Poll, PollStudentViewModel>();
            CreateMap<Poll, PollStudentDetailsViewModel>()
                .ForMember(dest => dest.CourseName, src => src.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.CourseId, src => src.MapFrom(src => src.Course.Id));
        }
    }
}
