using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly DepartmentRepository _departmentRepository;

        public EmployeeController(EmployeeRepository employeeRepository, DepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult AllEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();
            return View(employees);
        }
        public IActionResult AddEmployeeForm()
        {
            var departments = _departmentRepository.GetAllDepartments();
            return View(departments);
        }


        [HttpPost]
        public IActionResult AddEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.AddEmployee(model.DepartmentId, model.Name, model.Position, model.Phone, model.Email);
                return RedirectToAction("AllEmployees");
            }
            return View(model);
        }

        public IActionResult DeleteEmployeeForm()
        {
            var employees = _employeeRepository.GetAllEmployees();
            return View("DeleteEmployeeForm", employees);
        }


        [HttpPost]
        public IActionResult DeleteEmployee(int employeeId)
        {
            _employeeRepository.DeleteEmployee(employeeId);
            return RedirectToAction("AllEmployees");
        }

        public IActionResult EditEmployeeForm(int employeeId)
        {
            var employee = _employeeRepository.GetEmployeeById(employeeId);
            return View("EditEmployeeForm", employee);
        }


        [HttpPost]
        public IActionResult EditEmployee(Employee model)
        {
            _employeeRepository.UpdateEmployee(model);
            return RedirectToAction("AllEmployees");
        }

        public IActionResult SelectEmployeeForEdit()
        {
            var employees = _employeeRepository.GetAllEmployees();
            return View("SelectEmployeeForEdit", employees);
        }

        public IActionResult SearchEmployees(string searchString)
        {
            var employees = _employeeRepository.SearchEmployees(searchString);
            return View("SearchEmployees", employees);
        }

        public IActionResult ChooseDepartmentForm()
        {
            var departments = _departmentRepository.GetAllDepartments();
            return View(departments);
        }

        public IActionResult EmployeesByDepartment(int departmentId)
        {
            var employees = _employeeRepository.GetEmployeesByDepartmentId(departmentId);
            return View(employees);
        }


    }
}
