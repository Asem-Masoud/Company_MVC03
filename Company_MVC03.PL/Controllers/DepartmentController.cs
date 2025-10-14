using AutoMapper;
using Company_MVC03.BLL.Interfaces;
using Company_MVC03.BLL.Repositories;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company_MVC03.PL.Controllers
{
    // V07
    // IN Dependances -> Add Project Reference -> Company_MVC03.BLL

    // MVC Controller 
    [Authorize]
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;

        private readonly IUnitOfWork _unitOfWork;

        // ASK CLR Create object From DepartmentRepository 

        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
            // _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        /*
        public DepartmentController()
        {
            _departmentRepository = new DepartmentRepository();
        }
        */

        [HttpGet] // GET: /Department/Index
        public async Task<IActionResult> Index()
        {
            // DepartmentRepository departmentRepository = new DepartmentRepository();
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();

            return View(departments);
        }

        [HttpGet] // GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var department = new Department()
                {
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt,
                };
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = await _unitOfWork.CompleteAsync(); // Save Changes
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id : {id} is not found" });
            return View(viewName, department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            /*
            if (id is null) return BadRequest("Invalid Id"); //400
            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id : {id} is not found" });

            return View(department);
            */
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest(); //400

                _unitOfWork.DepartmentRepository.Update(department);
                var count = await _unitOfWork.CompleteAsync(); // Save Changes

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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest(); //400

                _unitOfWork.DepartmentRepository.Delete(department);
                var count = await _unitOfWork.CompleteAsync(); // Save Changes

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
    }
}