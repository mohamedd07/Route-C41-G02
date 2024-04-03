using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route_C41_G02_BLL.Interfaces;
using Route_C41_G02_BLL.Repositories;
using Route_C41_G02_DAL.Models;
using Route_C41_G02_PL.Helpers;
using Route_C41_G02_PL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Route_C41_G02_PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment env ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
        }

        public IActionResult Index(string searchInp)
        {
            var employee = Enumerable.Empty<Employee>();
            var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;

            if (string.IsNullOrEmpty(searchInp))
            {
                employee = employeeRepo.GetAll();
            }

            else
            {
                employee = employeeRepo.SearchByName(searchInp.ToLower());
            }
            return View(_mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee));

        }

        public IActionResult Create()
        {
            //ViewData["Department"] = _departmentRepo.GetAll();
            //ViewBag.Department = _departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            
            if(ModelState.IsValid)
            {
                employee.ImageName= DocumentSettings.UploadFile(employee.Image, "images");





                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);

                //MappedEmployee.ImageName = fileName;

                _unitOfWork.Repository<Employee>().Add(MappedEmployee);

                var count = _unitOfWork.Complete();


                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created Successfully";
                }
                else
                    TempData["Message"] = "AN Error Has Occured While Employee is Created";

                return RedirectToAction("Index");

            }
            return View(employee);
        }

        public IActionResult Details(int? id , string ViewName = "Details")
        {
            if(!id.HasValue)
            {
                return BadRequest();
            }

            var employee= _unitOfWork.Repository<Employee>().Get(id.Value);

            if (employee is null)
                return NotFound();

            if(ViewName.Equals("Delete" , StringComparison.OrdinalIgnoreCase))
                TempData["ImageName"] = employee.ImageName;

            return View(ViewName,_mapper.Map<Employee,EmployeeViewModel>(employee)); 
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id , EmployeeViewModel employee)
        {
            if (id != employee.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);

                _unitOfWork.Repository<Employee>().Update(MappedEmployee);
                _unitOfWork.Complete();
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
        public IActionResult Delete(EmployeeViewModel employee)
        {

            try
            {
                employee.ImageName = TempData["ImageName"] as string;
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);

                _unitOfWork.Repository<Employee>().Delete(MappedEmployee);
                var count = _unitOfWork.Complete();
                if(count > 0)
                {
                    DocumentSettings.DeleteFile(employee.ImageName, "images");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured during Updating The Employee");
            }

            return View(employee); 
        }

    }
}
