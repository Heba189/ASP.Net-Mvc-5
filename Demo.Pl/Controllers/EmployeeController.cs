using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entities;
using Demo.Pl.Helpers;
using Demo.Pl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Pl.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository employeeRepository;
        //private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;

        public IUnitofWork UnitofWork { get; }

        // public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository ,IMapper mapper)
        public EmployeeController(IUnitofWork unitofWork ,IMapper mapper)
        {
            UnitofWork = unitofWork;
            //this.employeeRepository = employeeRepository;
            //this.departmentRepository = departmentRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult>  Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            { 
                var MappedEmps = mapper.Map<IEnumerable<Employee> , IEnumerable<EmployeeViewModel>>(await UnitofWork.EmployeeRepository.GetAll());
                return  View(MappedEmps);

            }
            else
            {
                
                var MappedEmps = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(await UnitofWork.EmployeeRepository.SearchEmployee(SearchValue));

                return View(MappedEmps);
            }
           
        }
        public IActionResult Create()
        {
            ViewBag.Departments = UnitofWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                //var MappedEmp = new Employee() //Manual Mapping
                //{
                //    Id = employee.Id,
                //    Name = employee.Name,
                //    Address = employee.Address,
                //    Age = employee.Age,
                //    DepartmentId = employee.DepartmentId,
                //    Email = employee.Email,
                //    HireDate = employee.HireDate,
                //    IsActive = employee.IsActive,
                //    PhoneNumber = employee.PhoneNumber,
                //    Salary = employee.Salary

                //};
               employee.ImageName = DocumentSettings.UploadFile(employee.Image, "Imags");
                var MappedEmp =  mapper.Map<EmployeeViewModel ,Employee >(employee);
                // employeeRepository.Add(employee); 
                 await UnitofWork.EmployeeRepository.Add(MappedEmp);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Departments = UnitofWork.DepartmentRepository.GetAll();
                return View(employee);
            }
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var employee =await UnitofWork.EmployeeRepository.Get(id);
           
            if (employee == null)
                return NotFound();

            var MappedEmp = mapper.Map<Employee,  EmployeeViewModel > (employee);
            return View(ViewName, MappedEmp);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            //    if (id == null)
            //        return NotFound();
            //    var Department = employeeRepository.Get(id);
            //    if (Department == null)
            //        return NotFound();
            //    return View(Department);
            ViewBag.Departments =await UnitofWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid) //server side validation
            {
                try
                {
                    var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employee);
                    await UnitofWork.EmployeeRepository.Update(MappedEmp);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {

                    return View(employee);
                }
            }
            ViewBag.Departments = UnitofWork.DepartmentRepository.GetAll();
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel employee)
        {
            if (id != employee.Id)
                return BadRequest();
            try
            {
                var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employee);
                DocumentSettings.Delete(employee.ImageName, "Imags");
                await UnitofWork.EmployeeRepository.Delete(MappedEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(employee);
            }
        }
    }
}
