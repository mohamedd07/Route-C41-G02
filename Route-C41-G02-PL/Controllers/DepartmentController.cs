using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Route_C41_G02_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Route_C41_G02_BLL.Repositories;
using Route_C41_G02_BLL.Interfaces;
using Route_C41_G02_DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Route_C41_G02_PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // Inheritance : Is a Controller
        // Composition : Has a DepartmentRepository

        //private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IUnitOfWork unitOfWork/*IDepartmentRepository departmentRepository ,*/ ,IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            //_departmentRepo = departmentRepository;
            _env = env;
        }

        // /Department/Index
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid) //Server Side Validation
            {
                _unitOfWork.DepartmentRepository.Add(department);

                var count = _unitOfWork.Complete();
                if (count>0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }

        public IActionResult Details(int? id , string ViewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

            if (department is null)
                return NotFound();

            return View(ViewName,department);

        }

        public IActionResult Edit(int? id)
        {
            //if(!id.HasValue)
            //    return BadRequest();

            //var department = _departmentRepo.Get(id.Value);

            //if(department is null)
            //    return NotFound();

            //return View(department);

            return Details(id , "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Department department)
        {
            if(id != department.Id)
                return BadRequest();

            if(!ModelState.IsValid)
                return View(department);

            try
            {
                _unitOfWork.DepartmentRepository.Update(department);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // 1- Log Exeption
                // 2- Friendly Message

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);

                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured during Updating The Department");

                return View(department);

            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);

                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured during Updating The Department");
            }

            return View(department);
        }


    }
}
