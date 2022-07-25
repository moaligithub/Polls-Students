using Polls.Domain.Interfaces.IRepositories;
using Polls.Domain.Interfaces.IUnitOfWork;
using Polls.Domain.Models;
using Polls.EF.Data;
using Polls.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.EF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IBaseRepository<Department> _department;
        private IBaseRepository<Contact> _contact;
        private IBaseRepository<Course> _course;
        private IBaseRepository<Instructor> _instructor;
        private IBaseRepository<InstructorReview> _instructorReview;
        private IBaseRepository<Poll> _poll;
        private IBaseRepository<Question> _question;
        private IBaseRepository<QuestionPoll> _questionPoll;
        private IBaseRepository<Session> _session;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<Department> Department
        {
            get { return _department ?? (_department = new BaseRepository<Department>(_context)); } 
        }

        public IBaseRepository<Contact> Contact
        {
            get { return _contact ?? (_contact = new BaseRepository<Contact>(_context)); }
        }

        public IBaseRepository<Course> Course
        {
            get { return _course ?? (_course = new BaseRepository<Course>(_context)); }
        }

        public IBaseRepository<Instructor> Instructor
        {
            get { return _instructor ?? (_instructor = new BaseRepository<Instructor>(_context)); } 
        }

        public IBaseRepository<Poll> Poll
        {
            get { return _poll ?? (_poll = new BaseRepository<Poll>(_context)); }
        }

        public IBaseRepository<Question> Question
        {
            get { return _question ?? (_question = new BaseRepository<Question>(_context)); }
        }

        public IBaseRepository<Session> Session
        {
            get { return _session ?? (_session = new BaseRepository<Session>(_context)); }
        }

        public IBaseRepository<InstructorReview> InstructorReview
        {
            get { return _instructorReview ?? (_instructorReview = new BaseRepository<InstructorReview>(_context)); }
        }

        public IBaseRepository<QuestionPoll> QuestionPolls
        {
            get { return _questionPoll ?? (_questionPoll = new BaseRepository<QuestionPoll>(_context)); }
        }

        public void Complete()
            => _context.SaveChanges();

        public void Dispose()
            => _context.Dispose();
    }
}