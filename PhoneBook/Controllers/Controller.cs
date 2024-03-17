using Microsoft.AspNetCore.Mvc;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class BaseController : Controller
    {
        protected readonly DepartmentRepository _departmentRepository;
        protected readonly EmployeeRepository _employeeRepository;

        public BaseController()
        {
        }

        public BaseController(DepartmentRepository departmentRepository, EmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }
    }
}
