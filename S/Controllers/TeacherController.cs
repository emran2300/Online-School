using S.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S.Controllers
{
    [TeacherAuth]
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            var teacher = Session["teacher"];
            return View(teacher);
        }
        [HttpGet]
        public ActionResult Profile()
        {
            int id = Int32.Parse(Session["id"].ToString());
            var db = new OnlineEduEntities();
            Teacher teacher = (from s in db.Teachers where s.Id == id select s).SingleOrDefault();
            return View(teacher);
        }
        [HttpPost]
        public ActionResult Profile(Teacher teacher)
        {
            int id = Int32.Parse(Session["id"].ToString());
            var db = new OnlineEduEntities();
            var tc = (from tch in db.Teachers where tch.Id == id select tch).SingleOrDefault();
            
            db.SaveChanges();

            return RedirectToAction("Profile", "Student");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}