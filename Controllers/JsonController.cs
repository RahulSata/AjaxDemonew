using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AjaxDemowithRashmi.Controllers
{
    public class JsonController : Controller
    {
        // GET: Json
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Index1() {
            List<EmployeeClass> EmpList = new List<EmployeeClass>() {
                new EmployeeClass{Id= 1,name ="Rahul" },
                new EmployeeClass{Id=2,name="Rashmi"}
            };
            return Json(EmpList, JsonRequestBehavior.AllowGet);
        }
    }
}