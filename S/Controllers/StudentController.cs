using S.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S.Controllers
{
    //[StudentAuth]
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
            Student student = (from s in db.Students where s.Id == id select s).SingleOrDefault();
            return View(student);
        }
        [HttpPost]
        public ActionResult Profile(Student s)
        {
            int id = Int32.Parse(Session["id"].ToString());
            var db = new OnlineEduEntities();
            var st =(from stu in db.Students where stu.Id==id select stu).SingleOrDefault();
            st.Name = s.Name;
            st.Address = s.Address;
            st.Phone = s.Phone;
            st.DateOfBirth = s.DateOfBirth;
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