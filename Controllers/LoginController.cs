using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class LoginController : Controller
    {
        LoginSignupEntities db = new LoginSignupEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User u)
        {
            var user = db.Users.Where(model => model.Username == u.Username && model.Password == u.Password).FirstOrDefault();
            if(user!=null)
            {
                Session["UserID"] = u.Id.ToString();
                Session["Username"] = u.Username.ToString();
                TempData["LoginSuccessMessage"] = "<script>alert('Login Success !')</script>";
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.ErrorMessage = "<script>alert('Username or password incorrect')</script>";
                return View();
            }
            
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User u)
        {
            if(ModelState.IsValid==true)
            {
                db.Users.Add(u);
                int a=db.SaveChanges();
                if(a>0)
                {
                    ViewBag.InsertMessage = "<script>alert('Registered !')</script>";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.InsertMessage = "<script>alert('Registration failed !')</script>";
                }
            }
            return View();
        }
    }
}