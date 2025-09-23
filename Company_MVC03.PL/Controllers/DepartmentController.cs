using Company_MVC03.BLL.Interfaces;
using Company_MVC03.BLL.Repositories;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace Company_MVC03.PL.Controllers
{
    // V07
    // IN Dependances -> Add Project Reference -> Company_MVC03.BLL

    // MVC Controller 
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        // ASK CLR Create object From DepartmentRepository 

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /*
        public DepartmentController()
        {
            _departmentRepository = new DepartmentRepository();
        }
        */

        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
            // DepartmentRepository departmentRepository = new DepartmentRepository();
            var departments = _departmentRepository.GetAll();

            return View(departments);
        }

        [HttpGet] // GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var department = new Department()
                {
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt,

                };
                var count = _departmentRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

    }
}
