using AutoMapper;
using Company_MVC03.BLL.Interfaces;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.Dtos;
using Company_MVC03.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company_MVC03.PL.Controllers
{
    // [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // private readonly IEmployeeRepository _employeeRepository; // UnitOfWork
        // private readonly IDepartmentRepository _departmentRepository; //Comment, because we used it only once in Create Get
        private readonly IMapper _mapper;

        // ASK CLR Create object From EmployeeRepository
        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            // _employeeRepository = employeeRepository;
            // _departmentRepository = departmentRepository;  // RelationShip
        }

        [HttpGet] // GET : /Department/Index
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }

            return View(employees);

            #region S04V02
            //// Dictionary : 3 Property
            //// 1. ViewData : Transfer Extra Information From Controller (Action) To View
            ////ViewData["Message"] = "Hello From ViewData";
            //// 2. ViewBag : Transfer Extra Information From Controller (Action) To View
            //ViewBag.Message = "Hello From ViewBag";
            ////// 3. TempData
            #endregion
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync(); // RelationShip
            ViewData["departments"] = departments;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
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

                    if (model.Image is not null)
                    {
                        model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                    }

                    var employee = _mapper.Map<Employee>(model);
                    await _unitOfWork.EmployeeRepository.AddAsync(employee);

                    var count = await _unitOfWork.CompleteAsync();
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
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsyncGet(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id : {id} is not found" });

            var dto = _mapper.Map<CreateEmployeeDto>(employee);

            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id"); //400

            //var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

            //if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id : {id} is not found" });

            /*
            var employeeDto = new CreateEmployeeDto // PartialView
            {
                EmpName = employee.Name,
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
            */

            //  var dto = _mapper.Map<CreateEmployeeDto>(employee);


            //return View(employeeDto);
            // return View(viewName, dto);

            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, /*Employee*/ CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {

                //if (id != model.Id) return BadRequest(); //400
                /*
                var employee = new Employee
                {
                    Id = id,
                    Name = model.EmpName,
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
                */

                if (model.ImageName is not null && model.Image is not null)
                {
                    DocumentSetting.DeleteFile(model.ImageName, "images");
                }

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                }

                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                //if (id != model.Id) return BadRequest(); //400
                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSetting.DeleteFile(model.ImageName, "images");
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

    }
}
