using S.EF;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S.Controllers
{
    //[Admin]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var admin = Session["teacher"];
            return View(admin);
        }
        [HttpGet]
        public ActionResult Profile()
        {
            int id = Int32.Parse(Session["id"].ToString());
            var db = new OnlineEduEntities();
            var admin = (from a in db.Admins where a.Id == id select a).SingleOrDefault();
            return View(admin);
        }
        [HttpPost]
        public ActionResult Profile(Admin admin)
        {
            return View();
        }
        [HttpGet]
        public ActionResult TeacherList()
        {
            var db = new OnlineEduEntities();
            var teachers = db.Teachers.ToList();
            return View(teachers);
        }
        [HttpPost]
        public ActionResult TeacherList(Teacher teacher)
        {
            var db = new OnlineEduEntities();
            var teachers = db.Teachers.ToList();
            return View(teachers);
        }

        [HttpGet]
        public ActionResult StudentList()
        {
            var db = new OnlineEduEntities();
            var students = db.Students.ToList();
            return View(students);
        }
        [HttpPost]
        public ActionResult StudentList(Student student)
        {
            return View();
        }
        [HttpGet]
        public ActionResult CourseList()
        {
            var db = new OnlineEduEntities();
            var cours = db.Courses.ToList();
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
            var db = new OnlineEduEntities();
            var teacher = db.Teachers.ToList();
            return View(teacher);
        }
        [HttpPost]
        public ActionResult AddCourse(Cours cours)
        {
            var db = new OnlineEduEntities();
            cours.Status = 1;
            cours.Enroll = 0;

            if (ModelState.IsValid)
            {
                try
                {
                    db.Courses.Add(cours);
                    db.SaveChanges();
                    return RedirectToAction("CourseList", "Admin");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View(cours);
        }
        [HttpGet]
        public ActionResult UpdateCourse(int id)
        {
            var db = new OnlineEduEntities();
            var cours = db.Courses.Find(id);
            ViewBag.Teacher = db.Teachers.ToList(); 
            return View(cours);
        }
        [HttpPost]
        public ActionResult UpdateCourse(Cours c)
        {
            var db = new OnlineEduEntities();

            var course = db.Courses.Find(c.Id);
            course.Status = 1;
            course.Enroll = 0;
            course.Name = c.Name;
            course.Description = c.Description;
            course.Capacity = c.Capacity;
            course.Cost = c.Cost;
            course.Teacher_Id = c.Teacher_Id;
            db.SaveChanges();
            return RedirectToAction("CourseList");
        }

        public ActionResult StatusChange()
        {
            return View();
        }

        public ActionResult DeleteCourse(int id)
        {
            var db = new OnlineEduEntities();
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
            db.SaveChanges();

            return RedirectToAction("CourseList", "Admin");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}