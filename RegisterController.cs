using ASPNETMVC_BootstrapCustomThemeDemo.Models;
using ASPNETMVC_BootstrapCustomThemeDemo.Service;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using ASPNETMVC_BootstrapCustomThemeDemo.Service;
using System.Web.UI;
//using ASPNETMVC_BootstrapCustomThemeDemo.Models;

namespace ASPNETMVC_BootstrapCustomThemeDemo.Controllers
{
   
    public class RegisterController : Controller
    {
        DBContext db = new DBContext();
        // GET: Register
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Registration");
        }

        [HttpGet]
         public ActionResult Registration()
         {
             return View();
         }
        /*  public JsonResult RegisterUser(userModel um)
          {

              SameSiteMode ss = new SameSiteMode();
              db.UserName = um.UserName;
              db.Password = um.Password;
              db.e


          }*/

        [HttpPost]
      public ActionResult Registration(userModel um)
        {
            if(ModelState.IsValid)
            {
                if (!db.IsUserExists(um.Email))
                {
                    if (db.Add(um))
                    {
                        TempData["Insertmsg"] = "<script>alert('Registration of User Data Successfull!!!')</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["InsertErrormsg"] = "<script>alert('Registration Of User Data Not Saved!!!')</script>";
                    }

                    

                }
                ModelState.AddModelError("", "User with same email already exist!");
                TempData["checkEmail"] = "<script>alert('ALERT:-Duplicate Email!!! Same Email Already Registered in the system!!!')</script>";
                ModelState.Clear();  //After Message of DuplicateEmail it Clears the Register Forms
                //return View();
            }
            return View();

            /*
            if (db.Add(um))
            {
                TempData["Insertmsg"] = "<script>alert('Registration of User Data Successfull!!!')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["InsertErrormsg"] = "<script>alert('Registration Of User Data Not Saved!!!')</script>";
            }

            return View(); */
        }///END OF Registration EntryData.......

        [HttpGet]
        public ActionResult Login() { 
            return View(); 
                }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if(db.IsValidUser(lvm.Email,lvm.Password))
            {
                HttpContext.Session["username"] = lvm.Email;
                FormsAuthentication.SetAuthCookie(lvm.Email, false);
                return RedirectToAction("UserInfo", "Register");


            }
            else
            {
                ModelState.AddModelError("", "Your Email and Passowrd is Incorrect!!");
            }
            return View(lvm);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home"); ///WHEN Logout Menu is clicked it goes to HomeController to LogOut function to redirect to Index page
        }

        //[Authorize]
        [HttpGet]
        public ActionResult UserInfo()
        {
            
            return View();
        }

    }
}