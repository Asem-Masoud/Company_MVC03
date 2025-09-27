using Company_MVC03.BLL.Interfaces;
using Company_MVC03.BLL.Repositories;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new
            {
                StatusCode = 404,
                message = $"Department With Id : {id} is not found"
            });
            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            /*
            if (id is null) return BadRequest("Invalid Id"); //400
            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id : {id} is not found" });

            return View(department);
            */
            return Details(id, "Edit");

        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest(); //400

                var count = _departmentRepository.Update(department);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(department);
        }

        /*
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id = id,
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt,
                };
                var count = _departmentRepository.Update(department);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(model);
        }*/

        public IActionResult Delete(int? id)
        {
            /*
            if (id is null) { return BadRequest("id required "); }

            var department = _departmentRepository.Get(id.Value);
            if (department is null) { return NotFound($"no department with this id {id}"); }

            return View(department);
            */

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest(); //400

                var count = _departmentRepository.Delete(department);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(department);


        }

    }
}
