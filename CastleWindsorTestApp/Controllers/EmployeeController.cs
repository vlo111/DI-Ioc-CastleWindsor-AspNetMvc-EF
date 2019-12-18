using CastleWindsorTestApp.Helpers;
using CastleWindsorTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MvcPaging;


namespace CastleWindsorTestApp.Controllers
{
    public class EmployeeController : Controller
    {
        private IEnumerable<Employee> employeers;
        private IEnumerable<Department> departments;
        private const int defaultPageSize = 5;

        public EmployeeController()
        {

            // if there is no database, create and add data
            if (!System.Data.Entity.Database.Exists("CustleWindsorContext"))
            {
                GetRepo.EmployeeRepository.AddRange(NewEmployers.employers);
            }

            // Get all departmnet 
            departments = GetRepo.DepartmentRepository.GetAll();

            // Department list for create page, it's accepted value for dropdown
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");

            // Data Caching
            #region Cache Manager

            if (!MemoryCache.Default.Contains("CacheKey"))
            {
                employeers = GetRepo.EmployeeRepository.GetWithInclude();
                // Initialize the employee's department object
                foreach (var department in departments)
                    foreach (var employee in employeers)
                        if (department.DepartmentId == employee.DepartmentId)
                            employee.Department = department;
                CacheManager.List = employeers;
            }

            #endregion


        }

        // GET: Employee
        public ActionResult Index(int? page)
        {
            ViewData["emp_name"] = "";
            int currentPageIndex = page.HasValue ? page.Value : 1;
            return View(((List<Employee>)CacheManager.List).ToPagedList(currentPageIndex, defaultPageSize));
        }

        public ActionResult Create()
        {
            return PartialView("~/views/employee/partial_views/_create.cshtml", new Employee());
        }
        [HttpPost]
        public JsonResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    // add employee to database 
                    GetRepo.EmployeeRepository.Create(employee);

                    // add employee to cache 
                    var new_employee = new Employee
                    {
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.EmployeeName,
                        DepartmentId = employee.DepartmentId,
                        JobName = employee.JobName,
                        Salary = employee.Salary,
                        Department = departments.Where(p => p.DepartmentId == employee.DepartmentId).SingleOrDefault()
                    };
                    ((List<Employee>)CacheManager.List).Add(new_employee);


                    return Json(new { success = "created" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { error = "Model is not valid" }, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception e)
            {
                return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Update(int id)
        {
            var employee = ((IEnumerable<Employee>)CacheManager.List).Where(p => p.EmployeeId == id).FirstOrDefault();
            return PartialView("~/views/employee/partial_views/_update.cshtml", employee);
        }
        [HttpPost]
        public async Task<JsonResult> Update(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employee.Department = departments.Where(p => p.DepartmentId == employee.DepartmentId).FirstOrDefault();

                    var index = ((List<Employee>)CacheManager.List).FindIndex(p => p.EmployeeId == employee.EmployeeId);
                    ((List<Employee>)CacheManager.List)[index] = employee;

                    await GetRepo.EmployeeRepository.Update(employee);

                    return Json(new { success = "changed" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { error = "Model is not valid" }, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception e)
            {
                return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            var employee = ((IEnumerable<Employee>)CacheManager.List).Where(p => p.EmployeeId == id).FirstOrDefault();

            return PartialView("~/views/employee/partial_views/_delete.cshtml", employee);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //employee.Department = departments.Where(p => p.DepartmentId == employee.DepartmentId).SingleOrDefault();
                    //// remove from db
                    await GetRepo.EmployeeRepository.Remove(employee.EmployeeId);

                    // find by id product from cach
                    var index = ((List<Employee>)CacheManager.List).FindIndex(p => p.EmployeeId == employee.EmployeeId);
                    ((IList<Employee>)CacheManager.List)[index] = employee;
                    // remove from cache
                    ((IList<Employee>)CacheManager.List).RemoveAt(index);

                    return Json(new { success = "removed" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { error = "Model is not valid" }, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception e)
            {
                return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}