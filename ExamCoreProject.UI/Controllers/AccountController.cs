using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamProjectCore.Business.Abstract;
using ExamProjectCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamCoreProject.UI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
     

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var login = _userService.GetAll().Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
            if (login != null)
            {           
                return RedirectToAction("CreateExam", "Exam");
            }
            else
            {
                ViewBag.Message = "Username or password is wrong.Please try again.";
                return View();
            }
        }

        
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (user != null)
            {
                _userService.Create(user);
                ViewBag.Message = "Success";
                return View("Login");
            }
            else
            {
                ViewBag.Message = "Error";
                return View("Login");
            }

        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}