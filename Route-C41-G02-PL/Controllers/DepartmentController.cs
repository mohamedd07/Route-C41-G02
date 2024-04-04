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
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IUnitOfWork unitOfWork ,IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        // /Department/Index
        public IActionResult Index()
        {
            var departments = _unitOfWork.Repository<Department>().GetAllAsync();

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if(ModelState.IsValid) //Server Side Validation
            {
                _unitOfWork.Repository<Department>().Add(department);

                var count =await _unitOfWork.Complete();
                if (count>0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Details(int? id , string ViewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();

            var department =await _unitOfWork.Repository<Department>().GetAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(ViewName,department);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id , "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id,Department department)
        {
            if(id != department.Id)
                return BadRequest();

            if(!ModelState.IsValid)
                return View(department);

            try
            {
                _unitOfWork.Repository<Department>().Update(department);
                await _unitOfWork.Complete();
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

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Department department)
        {
            try
            {
                _unitOfWork.Repository<Department>().Delete(department);
                await _unitOfWork.Complete();
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
