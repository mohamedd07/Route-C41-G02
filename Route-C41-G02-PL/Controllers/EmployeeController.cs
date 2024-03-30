using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route_C41_G02_BLL.Interfaces;
using Route_C41_G02_BLL.Repositories;
using Route_C41_G02_DAL.Models;
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

        //private readonly IEmployeeRepository _employeeRepo;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IUnitOfWork unitOfWork,/*IEmployeeRepository employeeRepository */ IWebHostEnvironment env /*,IDepartmentRepository departmentRepo*/,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepo = employeeRepository;
            _env = env;
            _mapper = mapper;
            //_departmentRepo = departmentRepo;
        }

        public IActionResult Index(string searchInp)
        {
            // Binding Through View's Dictionary --> Transfer data from action to view [One Way]
            // 1. ViewData -->.Net 3.5
            //ViewData["Message"] = "Hello ViewData";

            // 2. ViewBag --> .Net 4.0 [Based on Dynamic Keyword] --> CLR will Detect the dataType on run time
            //ViewBag.Message = "Hello ViewBag"; // --> The same key [Message] the viewbag will override the Value of Message.
            var employee = Enumerable.Empty<Employee>();


            if (string.IsNullOrEmpty(searchInp))
            {
                //var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);
                employee = _unitOfWork.EmployeeRepository.GetAll();
            }

            else
            {
                employee = _unitOfWork.EmployeeRepository.SearchByName(searchInp.ToLower());
                //var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);
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
            // To Transfer data to the next request of the current request we USE:
            // 3. TempData --> Call another dictionary [Not like ViewBag & ViewData] 

            var MappedEmployee = _mapper.Map<EmployeeViewModel , Employee>(employee);
            if(ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Add(MappedEmployee);

                var count = _unitOfWork.Complete();


                if(count > 0)
                     TempData["Message"] = "Employee is Created Successfully";
                
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

            var employee= _unitOfWork.EmployeeRepository.Get(id.Value);
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);


            if (employee is null)
                return NotFound();  

            return View(MappedEmployee); 
        }

        public IActionResult Edit(int? id)
        {
            //ViewBag.Department = _departmentRepo.GetAll();

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
            var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);

            try
            {
                _unitOfWork.EmployeeRepository.Update(MappedEmployee);
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
            var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);

            try
            {
                _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
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
