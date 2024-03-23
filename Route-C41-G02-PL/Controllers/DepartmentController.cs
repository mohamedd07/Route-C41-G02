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

namespace Route_C41_G02_PL.Controllers
{
    public class DepartmentController : Controller
    {
        // Inheritance : Is a Controller
        // Composition : Has a DepartmentRepository

        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepo = departmentRepository;
        }

        // /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAll();

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
                var count = _departmentRepo.Add(department);
                if(count>0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _departmentRepo.Get(id.Value);

            if (department is null)
                return NotFound();

            return View(department);

        }
    }
}
