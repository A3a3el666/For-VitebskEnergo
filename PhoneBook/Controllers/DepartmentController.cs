using Microsoft.AspNetCore.Mvc;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IActionResult AllDepartments()
        {
            var departments = _departmentRepository.GetAllDepartments();
            return View(departments);
        }

        public IActionResult AddDepartmentForm()
        {
            var departments = _departmentRepository.GetAllDepartments();
            ViewData["Departments"] = departments;
            return View();
        }

        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.AddDepartment(department);
                return RedirectToAction("AllDepartments");
            }
                return View(department);
        }

        public IActionResult DeleteDepartmentForm()
        {
            var departments = _departmentRepository.GetAllDepartments();
            return View("DeleteDepartmentForm", departments);
        }

        [HttpPost]
        public IActionResult DeleteDepartment(int departmentId)
        {
            _departmentRepository.DeleteDepartmentAndDescendantsRecursive(departmentId);
            return RedirectToAction("AllDepartments");
        }

        public IActionResult EditDepartmentForm(int departmentId)
        {
            var department = _departmentRepository.GetDepartmentById(departmentId);
            var parentDepartments = _departmentRepository.GetAllDepartments();
            ViewBag.ParentDepartments = parentDepartments;
            return View("EditDepartmentForm", department);
        }


        [HttpPost]
        public IActionResult EditDepartment(Department model)
        {
            _departmentRepository.UpdateDepartment(model);
            return RedirectToAction("AllDepartments");
        }

        public IActionResult SelectDepartmentForEdit()
        {
            var departments = _departmentRepository.GetAllDepartments();
            return View("SelectDepartmentForEdit", departments);
        }

    }
}
