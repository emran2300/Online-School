using S.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S.Controllers
{
    public class RegistationController : Controller
    {
        
        // GET: Registation
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TeacherRegistation()
        {
            Teacher t = new Teacher();
            return View(t);
        }
        [HttpPost]
        public ActionResult TeacherRegistation(Teacher teacher)
        {
            var db = new OnlineEduEntities();
            teacher.AccountStatus = 0;
            teacher.Type = "teacher";
                       
            if (ModelState.IsValid)
            {
                try
                {
                    db.Teachers.Add(teacher);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                catch (Exception)
                {
                    TempData["msg"] ="Email already exist.";

                    return View();
                }
                
            }
            return View();
        }

        [HttpGet]
        public ActionResult StudentRegistation()
        {
            Student s = new Student();
            return View(s);
        }
        [HttpPost]
        public ActionResult StudentRegistation(Student student)
        {
            var db = new OnlineEduEntities();
            student.Type = "student";

            if (ModelState.IsValid)
            {
                try
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                catch (Exception)
                {
                    TempData["msg"] = "Email already exist.";

                    return View();
                }

            }
            return View();
        }

        [HttpGet]
        public ActionResult LoginRegistation()
        {
            LoginMatch loginMatch = new LoginMatch();
            return View(loginMatch);
        }

        [HttpPost]
        public ActionResult LoginRegistation(LoginMatch loginMatch)
        {
            var db = new OnlineEduEntities();
            var login = (from l in db.LoginMatches 
                         where l.Email.Equals(loginMatch.Email)
                         && l.Password.Equals(loginMatch.Password)
                         select l).SingleOrDefault();
            if( login != null)
            {
                if(login.type.Equals("admin"))
                {
                    var admin = (from a in db.Admins where a.Email.Equals(login.Email) select a).SingleOrDefault();
                    Session["type"]=login.type;
                    Session["id"]=admin.Id;
                    return RedirectToAction("Index", "Admin");
                }
                else if (login.type.Equals("teacher"))
                {
                    var teacher = (from a in db.Teachers where a.Email.Equals(login.Email) select a).SingleOrDefault();
                    Session["type"] = login.type;
                    Session["id"]=teacher.Id;
                    return RedirectToAction("Index", "Teacher");
                }
                else if (login.type.Equals("student"))
                {
                    var student = (from s in db.Students where s.Email.Equals(loginMatch.Email) select s).SingleOrDefault();
                    Session["type"] = login.type;
                    Session["id"] = student.Id;
                    return RedirectToAction("Index", "Student");
                }
            }
            return View();
        }
        
    }
}