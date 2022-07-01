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
            var teacher = (from s in db.Teachers where s.Id == id select s).SingleOrDefault();
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

        [HttpGet]
        public ActionResult CourseList()
        {
            var db = new OnlineEduEntities();
            var cours = (from c in db.Courses where c.Status == 1 select c).SingleOrDefault();
            return View(cours);
        }
        [HttpPost]
        public ActionResult CourseList(Cours cours)
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            Cours course = new Cours();
            return View(course);
        }
        [HttpPost]
        public ActionResult AddCourse(Cours cours)
        {
            var Id = Int32.Parse(Session["Id"].ToString());
            var db = new OnlineEduEntities();
            var teacher = (from t in db.Teachers where t.Id == Id select t).SingleOrDefault();
            cours.Status = 0;
            cours.Enroll = 0;
            

            if (ModelState.IsValid)
            {
                try
                {
                    teacher.Courses.Add(cours);
                    db.SaveChanges();
                    return RedirectToAction("CourseList", "Teacher");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}