using AutoMapper;
using Company_MVC03.BLL.Interfaces;
using Company_MVC03.BLL.Repositories;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company_MVC03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        // ASK CLR Create object From EmployeeRepository
        public EmployeeController(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IMapper mapper
            )
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;  // RelationShip
            _mapper = mapper;
        }

        [HttpGet] // GET : /Department/Index
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _employeeRepository.GetAll();
            }
            else
            {
                employees = _employeeRepository.GetByName(SearchInput);
            }

            #region S04V02
            //// Dictionary : 3 Property
            //// 1. ViewData : Transfer Extra Information From Controller (Action) To View
            ////ViewData["Message"] = "Hello From ViewData";
            //// 2. ViewBag : Transfer Extra Information From Controller (Action) To View
            //ViewBag.Message = "Hello From ViewBag";
            ////// 3. TempData
            #endregion

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.GetAll(); // RelationShip
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Manual Mapping -> S05V06
                    // -> Install in /Company_MVC03.PL / Dependencies / Manage NuGet Packages / AutoMapper / Install

                    /*
                    var employee = new Employee
                    {
                        Name = model.Name,
                        Address = model.Address,
                        Age = model.Age,
                        CreateAt = model.CreateAt,
                        HiringDate = model.HiringDate,
                        Email = model.Email,
                        IsActive = true,
                        IsDeleted = model.IsDeleted,
                        Phone = model.Phone,
                        Salary = model.Salary,
                        DepartmentId = model.DepartmentId,

                    };
                    */

                    var employee = _mapper.Map<Employee>(model);

                    var count = _employeeRepository.Add(employee);
                    if (count > 0)
                    {
                        TempData["Message"] = "Employee is Created !! ";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id : {id} is not found" });


            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id"); //400
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id : {id} is not found" });
            var employeeDto = new CreateEmployeeDto // PartialView
            {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                CreateAt = employee.CreateAt,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                IsActive = true,
                IsDeleted = employee.IsDeleted,
                Phone = employee.Phone,
                Salary = employee.Salary
            };

            var dto = _mapper.Map<CreateEmployeeDto>(employee);


            //return View(employeeDto);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,
          //Employee
          CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                // if (id != model.Id) return BadRequest(); //400
                var employee = new Employee
                {
                    Id = id,
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Email = model.Email,
                    IsActive = true,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Salary = model.Salary
                };
                var count = _employeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest(); //400
                var count = _employeeRepository.Delete(model);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}
