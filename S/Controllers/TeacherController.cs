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
            tc.Name = teacher.Name;
            tc.Address = teacher.Address;   
            tc.Bio = teacher.Bio;   
            tc.ExpartArea = teacher.ExpartArea;
            tc.Phone = teacher.Phone;
            db.SaveChanges();

            return RedirectToAction("Profile", "Teacher");
        }

        [HttpGet]
        public ActionResult AvailableCourseList()
        {
            var db = new OnlineEduEntities();
            var cours = (from c in db.Courses where c.Status == 1 && c.Teacher_Id == null select c).ToList();
            return View(cours);
        }
        [HttpPost]
        public ActionResult CourseList(Cours cours)
        {
            return View();
        }

        public ActionResult MyCourseList()
        {
            int id = Int32.Parse(Session["id"].ToString());
            var db = new OnlineEduEntities();
            var cours = (from c in db.Courses where c.Status == 1 && c.Teacher_Id == id select c).ToList();
            return View(cours);
        }
        public ActionResult PendingCourseList()
        {
            int id = Int32.Parse(Session["id"].ToString());
            var db = new OnlineEduEntities();
            var cours = (from c in db.Courses where c.Status == 0 && c.Teacher_Id == id select c).ToList();
            return View(cours);
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
            cours.Status = 0;
            cours.Enroll = 0;
            cours.Teacher_Id= Id;

            if (ModelState.IsValid)
            {
                try
                {
                    db.Courses.Add(cours);
                    db.SaveChanges();
                    return RedirectToAction("PendingCourseList", "Teacher");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult UpdateCourse(int id)
        {
            var db = new OnlineEduEntities();
            Cours cours = db.Courses.Find(id);
            return View(cours);
        }
        [HttpPost]
        public ActionResult UpdateCourse(Cours c)
        {
            var db = new OnlineEduEntities();
            var course = db.Courses.Find(c.Id);
            course.Status = 0;
            course.Enroll = 0;
            course.Name = c.Name;
            course.Description = c.Description;
            course.Capacity = c.Capacity;
            course.Cost = c.Cost;

            db.SaveChanges();
            return RedirectToAction("PendingCourseList", "Teacher");
        }

        public ActionResult DeleteCourse(int id)
        {
            var db = new OnlineEduEntities();
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
            db.SaveChanges();

            return RedirectToAction("PendingCourseList", "Teacher");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult EnrollICourse(int id)
        {
            var db = new OnlineEduEntities();
            var course = db.Courses.Find(id);
            course.Status = 0;
            course.Teacher_Id = Int32.Parse(Session["id"].ToString());
            db.SaveChanges();
            return RedirectToAction("PendingCourseList", "Teacher");
        }
    }
}