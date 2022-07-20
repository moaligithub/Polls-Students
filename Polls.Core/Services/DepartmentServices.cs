using AutoMapper;
using NToastNotify;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.Interfaces.IUnitOfWork;
using Polls.Domain.Models;
using Polls.Domain.ViewModel.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Core.Services
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork _unit;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;

        public DepartmentServices(IUnitOfWork unit, IToastNotification toastNotification, 
            IMapper mapper)
        {
            _unit = unit;
            _toastNotification = toastNotification;
            _mapper = mapper;
        }

        public async Task<bool> CreateDepartmentFromViewModelAsync(CreateDepartmentViewModel createDepartment)
        {
            Department department = new Department
            {
                Name = createDepartment.Name,
            };

            try
            {
                await _unit.Department.AddAsync(department);
                _unit.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, can`t add this Department");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Add Successfully");
            return true;
        }

        public async Task<int> DeleteAsync(int id)
        {
            Department department = await _unit.Department.GetByIdAsync(id);

            if (department == null)
                return 404;

            try
            {
                _unit.Department.Delete(department);
                _unit.Complete();
            }
            catch 
            {
                return 400;
            }

            return 200;
        }

        public async Task<int> EditDepartmentNameAsync(int id, string name)
        {
            if (name is null)
                return 400;

            Department department = await _unit.Department.GetByIdAsync(id);

            if (department is null)
                return 404;

            department.Name = name;

            try
            {
                _unit.Department.UpDate(department);
                _unit.Complete();
            }
            catch
            {
                return 400;
            }

            return 200;
        }

        public async Task<List<DepartmentIndexViewModel>> GetAllDepartmentAsync()
        {
            List<Department> departments = await _unit.Department
                .GetAllAsync(new System.Linq.Expressions.Expression<Func<Department, object>>[] { d
                => d.Courses });

            return departments.Select(d => new DepartmentIndexViewModel
            {
                Id = d.Id,
                Name = d.Name,
                CoursesCount = d.Courses.Count()
            }).ToList();
        }

        public async Task<List<DepartmentCheckBoxViewModel>> GetAllDepartmentCheckBoxAsync()
        {
            List<Department> departments = await _unit.Department.GetAllAsync();

            return _mapper.Map<List<DepartmentCheckBoxViewModel>>(departments);
        }

        public async Task<List<DepartmentViewModel>> GetAllDepartmentForDropListAsync()
        {
            List<Department> departments = await _unit.Department.GetAllAsync();

            return _mapper.Map<List<DepartmentViewModel>>(departments);
        }
    }
}