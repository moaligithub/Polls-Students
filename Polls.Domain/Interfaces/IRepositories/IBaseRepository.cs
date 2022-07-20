using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Interfaces.IRepositories
{
    /// <summary>
    ///     This interface it Working As Repository for access on the database by Entity Framework.
    ///     And Is Generic interface
    /// </summary>
    /// <typeparam name="T">T it expresses class model</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        // Sync Methods

        /// <summary>
        ///     This method it working on get one entity from database by entity key. 
        /// </summary>
        /// <param name="Id">Id is entity key</param>
        /// <returns>the entity found , or null</returns>
        T GetById(object Id);

        /// <summary>
        ///     This method it working on add one entity in database.
        /// </summary>
        /// <param name="entity">entity is entity that being added</param>
        void Add(T entity);

        /// <summary>
        ///     This method it working on update one entity in database
        /// </summary>
        /// <param name="entity">entity is a new entity that being Updated on old entity in database</param>
        void UpDate(T entity);

        /// <summary>
        ///     This method it working on delete one entity from database.
        /// </summary>
        /// <param name="entity">entity is the entity that being deleted from database.</param>
        void Delete(T entity);

        /// <summary>
        ///     This method it working on delete multiple entities from database.
        /// </summary>
        /// <param name="entity">entity is the entity that being deleted from database.</param>
        void DeleteRange(IEnumerable<T> entities);

        // Overloads For Method => GitAll()

        /// <summary>
        ///     This method it working on get all entities from database.
        /// </summary>
        /// <returns>all existing entities , or null</returns>
        List<T> GetAll();

        /// <summary>
        ///     This method it working on get all entities from database,
        ///     with included special more data for this entities.
        /// </summary>
        /// <param name="Include">is Lambda Expression, expresses the object that being included with base data.</param>
        /// <returns>all existing entities with data included for this entities, or null</returns>
        List<T> GetAll(Expression<Func<T, object>>[] Include);

        /// <summary>
        ///     this method it working on get all entities from database and arranged this entities.
        /// </summary>
        /// <param name="Order"> Order is Lambda Expression, expresses the object that being entities arrange by it</param>
        /// <param name="By">this parameter of a string data type And expresses the order form</param>
        /// <returns>all existing entities After arranged, or null</returns>
        List<T> GetAll(Expression<Func<T, object>> Order, string By);

        /// <summary>
        ///     This method it working on get all entities from database with arrange this entities And included special more data for this entities.
        /// </summary>
        /// <param name="Include">is lambda Expression, expresses the object that being included with base data. </param>
        /// <param name="Order">is lambda Expression, expresses the object that being by it entities arrange.</param>
        /// <param name="By">this parameter of a string data type and expresses the order form.</param>
        /// <returns>all existing entities After arranged with data included for this entities, or null</returns>
        List<T> GetAll(Expression<Func<T, object>>[] Include, Expression<Func<T, object>> Order, string By);

        // Overloads For Method => Find()

        /// <summary>
        ///     This method it working on get one entity from database by condition.  
        /// </summary>
        /// <param name="match">is lambda Expression , expresses the condition</param>
        /// <returns>one entity, or null</returns>
        T Find(Expression<Func<T, bool>> match);

        /// <summary>
        ///     This method it working on get one entity from database by condition, and included more data  
        /// </summary>
        /// <param name="match">is lambda Expression , expresses the condition</param>
        /// <param name="Include">is lambda Expression , expresses the object that being by it included more data.</param>
        /// <returns>one entity with Include more data that special this entity, or null</returns>
        T Find(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include);

        // Overloads For Method => FindAll()

        /// <summary>
        ///     This method it working on get collection of entities from database by condition. 
        /// </summary>
        /// <param name="match">is lambda Expression, expresses the condition</param>
        /// <returns>List of entities, or null</returns>
        List<T> FindAll(Expression<Func<T, bool>> match);

        /// <summary>
        ///     This method it wokimg on get collection of entities from database by condition,
        ///     and Include more data special for this entities.
        /// </summary>
        /// <param name="match">is lambda Expression, expresses the condition</param>
        /// <param name="Include">is lambda Expression , expresses the object that being by it included more data.</param>
        /// <returns>List of entities with Include more data that special this entity, or null</returns>
        List<T> FindAll(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include);

        /// <summary>
        ///     This method it wokimg on get collection of entities from database by condition with arrange this entities,
        /// </summary>
        /// <param name="match">is lambda Expression, expresses the condition</param>
        /// <param name="Order">is lambda Expression, expresses the object that being by it entities arrange.</param>
        /// <param name="By">this parameter of a string data type And expresses the order form</param>
        /// <returns>List of entities arranged, or null</returns>
        List<T> FindAll(Expression<Func<T, bool>> match, Expression<Func<T, object>> Order, string By);

        /// <summary>
        ///     This method it wokimg on get collection of entities from database by condition with arrange this entities,
        ///     and Include more data special for this entities.
        /// </summary>
        /// <param name="match">is lambda Expression, expresses the condition</param>
        /// <param name="Include">is lambda Expression , expresses the object that being by it included more data.</param>
        /// <param name="Order">is lambda Expression, expresses the object that being by it entities arrange.</param>
        /// <param name="By">this parameter of a string data type And expresses the order form</param>
        /// <returns>List of entities arranged with special more data for this entities.</returns>
        List<T> FindAll(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include, Expression<Func<T, object>> Order, string By);

        /// <summary>
        ///     This method it works for checking the existence of a value.  
        /// </summary>
        /// <param name="expression">this expresses the key of this value</param>
        /// <returns>True or False</returns>
        bool Any(Expression<Func<T, bool>> expression);

        // Overloads For Method => Count()

        /// <summary>
        ///     This method working on Get The items Count from database.
        /// </summary>
        /// <returns>The items Count of int data type</returns>
        int Count();

        /// <summary>
        ///     This method working on Get The items Count by condition from database.
        /// </summary>
        /// <returns>The items Count of int data type</returns>
        int Count(Expression<Func<T, bool>> math);

        // Async Methods

        /// <summary>
        ///     This Async method it working on get one entity from database by entity key. 
        /// </summary>
        /// <param name="Id">Id is entity key</param>
        /// <returns>the entity found , or null</returns>
        Task<T> GetByIdAsync(object Id);

        /// <summary>
        ///     This Async method it working on add one entity in database.
        /// </summary>
        /// <param name="entity">entity is entity that being added</param>
        Task AddAsync(T entity);

        /// <summary>
        ///     This Async method it working on add one entity in database.
        /// </summary>
        /// <param name="entities">entities is List of entity that being added</param>
        Task AddRangeAsync(List<T> entities);

        // Overloads For Method => GitAllAsync()

        /// <summary>
        ///     This Async method it working on get all entities from database.
        /// </summary>
        /// <returns>all existing entities , or null</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        ///     This Async method it working on get all entities from database.
        /// </summary>
        /// <returns>all existing entities , or null</returns>
        Task<List<T>> GetAllAsync(int take = 0, int skip = 0);

        /// <summary>
        ///     This Async method it working on get all entities from database,
        ///     with included special more data for this entities.
        /// </summary>
        /// <param name="Include">is Lambda Expression, expresses the object that being included with base data.</param>
        /// <returns>all existing entities with data included for this entities, or null</returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, object>>[] Include);

        /// <summary>
        ///     this Async method it working on get all entities from database and arranged this entities.
        /// </summary>
        /// <param name="Order"> Order is Lambda Expression, expresses the object that being entities arrange by it</param>
        /// <param name="By">this parameter of a string data type And expresses the order form</param>
        /// <returns>all existing entities After arranged, or null</returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, object>> Order, string By, int take = 0, int skip = 0);

        /// <summary>
        ///     This Async method it working on get all entities from database with arrange this entities And included special more data for this entities.
        /// </summary>
        /// <param name="Include">is lambda Expression, expresses the object that being included with base data. </param>
        /// <param name="Order">is lambda Expression, expresses the object that being by it entities arrange.</param>
        /// <param name="By">this parameter of a string data type and expresses the order form.</param>
        /// <returns>all existing entities After arranged with data included for this entities, or null</returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, object>>[] Include, Expression<Func<T, object>> Order, string By, int take = 0, int skip = 0);

        // Overloads For Method => FindAsync()
        
        /// <summary>
        ///     This Async method it working on get one entity from database by condition.  
        /// </summary>
        /// <param name="match">is lambda Expression , expresses the condition</param>
        /// <returns>one entity, or null</returns>
        Task<T> FindAsync(Expression<Func<T, bool>> match);

        /// <summary>
        ///     This Async method it working on get one entity from database by condition, and included more data  
        /// </summary>
        /// <param name="match">is lambda Expression , expresses the condition</param>
        /// <param name="Include">is lambda Expression , expresses the object that being by it included more data.</param>
        /// <returns>one entity with Include more data that special this entity, or null</returns>
        Task<T> FindAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include);

        // Overloads For Method => FindAllAsync()

        /// <summary>
        ///     This Async method it working on get collection of entities from database by condition. 
        /// </summary>
        /// <param name="match">is lambda Expression, expresses the condition</param>
        /// <returns>List of entities, or null</returns>
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match);

        /// <summary>
        ///     This Async method it wokimg on get collection of entities from database by condition,
        ///     and Include more data special for this entities.
        /// </summary>
        /// <param name="match">is lambda Expression, expresses the condition</param>
        /// <param name="Include">is lambda Expression , expresses the object that being by it included more data.</param>
        /// <returns>List of entities with Include more data that special this entity, or null</returns>
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include);

        /// <summary>
        ///     This Async method it wokimg on get collection of entities from database by condition with arrange this entities,
        /// </summary>
        /// <param name="match">is lambda Expression, expresses the condition</param>
        /// <param name="Order">is lambda Expression, expresses the object that being by it entities arrange.</param>
        /// <param name="By">this parameter of a string data type And expresses the order form</param>
        /// <returns>List of entities arranged, or null</returns>
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> Order, string By);

        /// <summary>
        ///     This Async method it wokimg on get collection of entities from database by condition with arrange this entities,
        ///     and Include more data special for this entities.
        /// </summary>
        /// <param name="match">is lambda Expression, expresses the condition</param>
        /// <param name="Include">is lambda Expression , expresses the object that being by it included more data.</param>
        /// <param name="Order">is lambda Expression, expresses the object that being by it entities arrange.</param>
        /// <param name="By">this parameter of a string data type And expresses the order form</param>
        /// <returns>List of entities arranged with special more data for this entities.</returns>
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include, Expression<Func<T, object>> Order, string By);
        
        /// <summary>
        ///     This Async method it works for checking the existence of a value.  
        /// </summary>
        /// <param name="expression">this expresses the key of this value</param>
        /// <returns>True or False</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        // Overloads For Method => CountAsync()

        /// <summary>
        ///     This Async method working on Get The items Count from database.
        /// </summary>
        /// <returns>The items Count of int data type</returns>
        Task<int> CountAsync();

        /// <summary>
        ///     This Async method working on Get The items Count by condition from database.
        /// </summary>
        /// <returns>The items Count of int data type</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> math);

        /// <summary>
        ///     This method it working on update list of entities in database
        /// </summary>
        /// <param name="entities">entities is a new entities that being Updated on old entity in database</param>
        void UpDateRange(List<T> entities);
    }
}