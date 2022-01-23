using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

 

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Member member,string command)
        {
            if (string.IsNullOrEmpty(member.Username) || (string.IsNullOrEmpty(member.Password)))
            {
                ModelState.AddModelError("", "Plesse enter Username or Password.");
                return View();
            }
            else
            {
                LibraryMgmtContext db = new LibraryMgmtContext();
                var usr = db.members.Where(u => u.Username == member.Username && u.Password == member.Password).FirstOrDefault();
                if (usr != null)
                {
                    if (member.Username.Equals("admin") && command.Equals("adminlogin"))
                        return RedirectToAction("Index", "Home");
                    else
                        return RedirectToAction("Member_Index", "Home");
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                    return View();
                }                
            }         
        }
            public ActionResult Member_Index()
        {
            return View();
        }

    }
}