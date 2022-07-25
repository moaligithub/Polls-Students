using Polls.Domain.Interfaces.IRepositories;
using Polls.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     This Property expresses Department repository. 
        /// </summary>
        public IBaseRepository<Department> Department { get; }

        /// <summary>
        ///     This Property expresses Contact repository. 
        /// </summary>
        public IBaseRepository<Contact> Contact { get; }

        /// <summary>
        ///     This Property expresses Course repository. 
        /// </summary>
        public IBaseRepository<Course> Course { get; }

        /// <summary>
        ///     This Property expresses Instructor repository. 
        /// </summary>
        public IBaseRepository<Instructor> Instructor { get; }

        /// <summary>
        ///     This Property expresses InstructorReview repository. 
        /// </summary>
        public IBaseRepository<InstructorReview> InstructorReview { get; }

        /// <summary>
        ///     This Property expresses Poll repository. 
        /// </summary>
        public IBaseRepository<Poll> Poll { get; }

        /// <summary>
        ///     This Property expresses Question repository. 
        /// </summary>
        public IBaseRepository<Question> Question { get; }

        /// <summary>
        ///     This Property expresses QuestionPolls repository. 
        /// </summary>
        public IBaseRepository<QuestionPoll> QuestionPolls { get; }

        /// <summary>
        ///     This Property expresses Session repository. 
        /// </summary>
        public IBaseRepository<Session> Session { get; }

        /// <summary>
        ///     This Complete method it expresses Save Changes
        /// </summary>
        void Complete();
    }
}
