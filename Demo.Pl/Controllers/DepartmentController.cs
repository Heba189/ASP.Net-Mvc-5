using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.Pl.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            //One Way Binding
            //1- ViewData is A dictionary object (intoduction in asp.net framework 3.5)
            // => It helps us to transfer the data from controller to view
            ViewData["Message"] = "Hello View Data";


            //2- viewBag is a Dynamic Property (introduction in asp.net framework 4.0 based on dynamic feature)
            // => It helps us to transfer the data from controller to view
              ViewBag.Message = "Hello View Bag";

            //3- TempData is A dictionary object (intoduction in asp.net framework 3.5)
            // is used to pass data between two consecutive requests
            //only works during the current and subsequent request
            

            return View(departmentRepository.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                departmentRepository.Add(department);
                TempData["Message"] = "Department is Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(department);
            }
        }

        public IActionResult Details(int? id , string ViewName= "Details")
        {
            if (id == null)
                return NotFound();
            var Department = departmentRepository.Get(id);
            if (Department == null)
                return NotFound();
            return View(ViewName , Department);
        }

       public IActionResult Edit(int? id)
        {
            //    if (id == null)
            //        return NotFound();
            //    var Department = departmentRepository.Get(id);
            //    if (Department == null)
            //        return NotFound();
            //    return View(Department);
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id ,Department department)
        {
            if(id != department.Id)
                return BadRequest();
            if(ModelState.IsValid) //server side validation
            {
                try
                {
                    departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    
                    return View(department);
                }
            }
            return View(department);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int? id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            try
            {
                departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(department);
            }
        }
    }
}
