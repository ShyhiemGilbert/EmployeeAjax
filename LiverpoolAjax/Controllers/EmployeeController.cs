﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
			if (id != 0)
			{
				using (EmployeesEntities db = new EmployeesEntities())
				{
					emp = db.EmployeeTbls.Where(x => x.EmployeeId == id).FirstOrDefault<EmployeeTbl>();
				}
			}
			return View(emp);
		}

		[HttpPost]
		public ActionResult AddOrEdit(EmployeeTbl emp)
		{
			try
			{
				if (emp.ImageUpload != null)
				{
					string fileName = Path.GetFileNameWithoutExtension(emp.ImageUpload.FileName);
					string extention = Path.GetExtension(emp.ImageUpload.FileName);
					fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
					emp.Image = "~/AppFiles/Images/" + fileName;
					emp.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
				}

				using (EmployeesEntities db = new EmployeesEntities())
				{
					if (emp.EmployeeId == 0)
					{
						db.EmployeeTbls.Add(emp);
						db.SaveChanges();
					}
					else
					{
						db.Entry(emp).State = EntityState.Modified;
						db.SaveChanges();
					}
				}

				return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "Submitted Successfully" },
					JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{

				return Json(new { success = false, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = ex.Message },
					JsonRequestBehavior.AllowGet);
			}

		}

		public ActionResult Delete(int id)
		{
			try
			{
				using (EmployeesEntities db = new EmployeesEntities())
				{
					EmployeeTbl emp = db.EmployeeTbls.Where(x => x.EmployeeId == id).FirstOrDefault<EmployeeTbl>();
					db.EmployeeTbls.Remove(emp);
					db.SaveChanges();
				}

				return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "Deleted Successfully" },
					JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = ex.Message },
					JsonRequestBehavior.AllowGet);
			}
		}
	}
}