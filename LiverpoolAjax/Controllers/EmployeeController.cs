using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiverpoolAjax.Models;


namespace LiverpoolAjax.Controllers
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
			return View(GetAllEmployee());
		}

		IEnumerable<EmployeeTbl> GetAllEmployee()
		{
			using (EmployeesEntities db = new EmployeesEntities())
			{
				return db.EmployeeTbls.ToList<EmployeeTbl>();
			}

		}

		public ActionResult AddOrEdit(int id = 0)
		{
			EmployeeTbl emp = new EmployeeTbl();
			return View(emp);
		}

		[HttpPost]
		public ActionResult AddOrEdit()
		{
			return View();
		}
	}
}