using AjaxDemowithRashmi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxDemowithRashmi.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetEmployees());
        }

        IEnumerable<Employee> GetEmployees()
        {
            using (AjaxDemoEntities db = new AjaxDemoEntities())
            {
                return db.Employees.ToList<Employee>();
            }
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            Employee emp = new Employee();
            if (id != 0)
            {
                using (AjaxDemoEntities db = new AjaxDemoEntities())
                {
                    emp = db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault<Employee>();
                }
            }
            return View(emp);
        }
        [HttpPost]
        public JsonResult AddOrEdit(Employee emp)
        {
            using (AjaxDemoEntities db = new AjaxDemoEntities()) { 
                db.Employees.Add(emp);
                db.SaveChanges();
            }
            //return RedirectToAction("ViewAll");
            return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetEmployees()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (AjaxDemoEntities db = new AjaxDemoEntities())
                {
                    Employee emp = db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault<Employee>();
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetEmployees()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
    
}