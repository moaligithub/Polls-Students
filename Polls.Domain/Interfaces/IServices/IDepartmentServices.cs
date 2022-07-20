using Polls.Domain.ViewModel.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Interfaces.IServices
{
    public interface IDepartmentServices
    {
        /// <summary>
        ///     Get all department for department index. 
        /// </summary>
        /// <returns>List of department type of DepartmentIndexViewModel.</returns>
        Task<List<DepartmentIndexViewModel>> GetAllDepartmentAsync();

        /// <summary>
        ///     Create a new department from CreateDepartmentViewModel.
        /// </summary>
        /// <param name="createDepartment">Expresses CreateDepartmentViewModel.</param>
        /// <returns>True in case create successfully, or False in case create failed.</returns>
        Task<bool> CreateDepartmentFromViewModelAsync(CreateDepartmentViewModel createDepartment);

        /// <summary>
        ///     Edit Department Name by department id.
        /// </summary>
        /// <param name="id">Expresses department id.</param>
        /// <param name="name">Expresses department name</param>
        /// <returns>200 in case update successfully, or 400 in BadRequest or 404 in NotFound.</returns>
        Task<int> EditDepartmentNameAsync(int id, string name);

        /// <summary>
        ///     Delete department by department id.
        /// </summary>
        /// <param name="id">Expresses department id.</param>
        /// <returns>200 in case delete successfully, or 404 in case not found, or 400 in case bad request.</returns>
        Task<int> DeleteAsync(int id);

        /// <summary>
        ///     Get all department for display in drop list.
        /// </summary>
        /// <returns>List of department type of DepartmentViewModel.</returns>
        Task<List<DepartmentViewModel>> GetAllDepartmentForDropListAsync();

        /// <summary>
        ///     Get all department for display in Check Box.
        /// </summary>
        /// <returns>List of department type of DepartmentCheckBoxViewModel.</returns>
        Task<List<DepartmentCheckBoxViewModel>> GetAllDepartmentCheckBoxAsync();
    }
}
