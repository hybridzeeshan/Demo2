using MvcUserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcUserManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return View();
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(userLogin users)
        {
            userLogin login = new userLogin();
            if (ModelState.IsValid)
            {
                using (userContext db = new userContext())
                {
                    var user = db.userLogins.Where(x => x.uName.Equals(users.uName) && x.Password.Equals(users.Password)).FirstOrDefault();

                    if (user != null)
                    {
                        //Session["Username"] = users.uName;
                        Session.Add("UserName", users.uName);
                        FormsAuthentication.SetAuthCookie(users.uName,true);
                        
                        ViewBag.LoginSuccess = "Login Successfully";
                       return RedirectToAction("Index", "Profile");
                    }
                    else
                    {
                        ViewBag.LoginMessage = "Login Failed Username/Password incorrect";
                        return View();
                    }
                                    
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Register")]
        public ActionResult RegisterPost(userLogin user)
        {
            if (ModelState.IsValid)
            {
                if (user!=null)
                {
                    using (userContext db = new userContext())
                    {
                        var users = db.userLogins.Where(x => x.uName.Equals(user.uName) && x.Password.Equals(user.Password)).FirstOrDefault();
                        if (users==null)
                        {
                            db.userLogins.Add(user);
                            db.SaveChanges();
                            ViewBag.SuccessMessage = "User Added"; 
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "User Already Exists";
                        }
                    } 
                }
            }
            return View();
        }

      
    }
}