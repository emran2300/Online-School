using S.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S.Controllers
{
    [StudentAuth]
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Profile()
        {
            int id = Int32.Parse(Session["id"].ToString());
            var db = new OnlineEduEntities();
            Student student = db.Students.Find(id);/*(from s in db.Students where s.Id == id select s).SingleOrDefault();*/
            return View(student);
        }
        [HttpPost]
        public ActionResult Profile(Student s)
        {
            int id = Int32.Parse(Session["id"].ToString());
            var db = new OnlineEduEntities();
            var st = (from stu in db.Students where stu.Id == id select stu).SingleOrDefault();
            st.Name = s.Name;
            st.Address = s.Address;
            st.Phone = s.Phone;
            st.DateOfBirth = s.DateOfBirth;
            db.SaveChanges();

            return RedirectToAction("Profile", "Student");
        }

        public ActionResult CourseList()
        {
            int id = Int32.Parse(Session["id"].ToString());
            var db = new OnlineEduEntities();
            var cours = (from c in db.Courses where c.Status == 1 && c.Teacher_Id != null && 
                          !(from s in db.CourseStudentMaps where s.StudentId == id select s.CourseId).Contains(c.Id)  select c).ToList();
            return View(cours);
        }

        public ActionResult EnrollCourse(int id)
        {
            var db = new OnlineEduEntities();
            var cours = db.Courses.Find(id);
            if(cours.Capacity != cours.Enroll)
            {
                var student = db.Students.Find(Int32.Parse(Session["id"].ToString()));
                CourseStudentMap cs = new CourseStudentMap();
                cs.StudentId = student.Id;
                cs.CourseId = id;
                db.CourseStudentMaps.Add(cs);
                cours.Enroll++;
                db.SaveChanges();
                return RedirectToAction("MyCourse");
            }
            return View();
        }
        public ActionResult MyCourse()
        {
            int id = Int32.Parse(Session["id"].ToString());
            var db = new OnlineEduEntities();
            var cs = (from c in db.CourseStudentMaps where c.StudentId == id select c).ToList();
            return View(cs);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}