﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route_C41_G02_BLL.Interfaces;
using Route_C41_G02_DAL.Models;
using System;

namespace Route_C41_G02_PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepository , IWebHostEnvironment env)
        {
            _employeeRepo = employeeRepository;
            _env = env;
        }

        public IActionResult Index()
        {
            var employee = _employeeRepo.GetAll();
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                var count = _employeeRepo.Add(employee);
                if(count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }

        public IActionResult Details(int? id , string ViewName = "Details")
        {
            if(!id.HasValue)
            {
                return BadRequest();
            }

            var employee= _employeeRepo.Get(id.Value);

            if(employee is null)
                return NotFound();  

            return View(employee); 
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id , Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                _employeeRepo.Update(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);

                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured during Updating The Employee");

                return View(employee);

            }
        }


        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepo.Delete(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if(_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else 
                    ModelState.AddModelError(string.Empty , "An Error Has Occured during Updating The Employee")
            }

            return View(employee); 
        }

    }
}
