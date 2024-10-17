using ThuvienMvc.Dtos.UserDtos;
using ThuvienMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ThuvienMvc.Models.Authentications;
using ThuvienMvc.Models;
using Azure;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using ThuvienMvc.Services.Implements;
namespace ThuvienMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService servie)
        {
            _service = servie;
        }

        [Authentication]
        public IActionResult Index()
        {
            var id = (int)HttpContext.Session.GetInt32("UserId");
            var user = _service.GetById(id);
            return View(user);
        }

        [AuthenAdmin]
        public IActionResult Adminedit()
        {
            var id = (int)HttpContext.Session.GetInt32("UserId");
            var admin = _service.GetAdmin(id);
            return View(admin);
        }
        [AuthenAdmin]
        public IActionResult IndexAdmin(string name , int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 7;


            IPagedList<User> users = _service.GetPagedUser(name, page, pageSize);
            if (!string.IsNullOrEmpty(name) && (users == null || !users.Any()))
            {
                ViewBag.Message = "Không tồn tại người dùng nào theo kết quả tìm kiếm.";
            }
            ViewData["SearchName"] = name;
            return View(users);
        }


        [Authentication]
        public IActionResult Edit()
        {
            var id = (int)HttpContext.Session.GetInt32("UserId");
            var user = _service.GetById(id);
            return View(user);
        }

        [HttpPost]
        [Authentication]
        public IActionResult Edit(UserDto model)
        {
            if (ModelState.IsValid)
            {
               
                _service.Update(model); 
                return RedirectToAction("Index"); 
            }
            return View(model); 
        }

        [HttpPost]
        [AuthenAdmin]
        public IActionResult EditAdmin(Admin model)
        {
            if (ModelState.IsValid)
            {

                _service.UpdateAdmin(model);
                return RedirectToAction("IndexAdmin");
            }
            return View(model);
        }

        [HttpGet]
        [AuthenAdmin]  
        public IActionResult EditUser(int id)
        {
            var user = _service.GetById(id); 
            if (user == null)
            {
                return NotFound(); 
            }
            return View(user); 
        }
        [HttpPost]
        [AuthenAdmin]
        public IActionResult EditUser(UserDto model)
        {
            if (ModelState.IsValid)
            {

                _service.Update(model);
                return RedirectToAction("IndexAdmin");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
           
             if(HttpContext.Session.GetString("UserRole") == "user")
            {
                return RedirectToAction("Index");
            }
            else if (HttpContext.Session.GetString("UserRole") == "admin")
            {
                return RedirectToAction("IndexAdmin");
            }
            else
            {
                return View(); 
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInUserDto input)
        {
           
            if (!ModelState.IsValid)
            {
                return View(input); 
            }

            var result = await _service.SignIn(input);

            if (result == "User")
            {

                HttpContext.Session.SetString("UserRole", "user");
                HttpContext.Session.SetInt32("UserId", input.IdSign);
                HttpContext.Session.SetString("UserName" , input.UserName);
                return RedirectToAction("Index", "Home");
            }
            else if (result == "Admin")
            {
                HttpContext.Session.SetString("UserRole","admin");
                HttpContext.Session.SetInt32("UserId", input.IdSign);
                HttpContext.Session.SetString("UserName", input.UserName);
                return RedirectToAction("IndexAdmin", "Book");
            }
            else
            {
                
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(input);
            }
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(CreateUserDto input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }
            
            var result = await _service.SignUp(input);
            if(result == "User signed up successfully.")
            {
                return RedirectToAction("SignIn", "User");
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(input);
            }

        }
       
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName"); 
            return RedirectToAction("SignIn");
        }


    }
}
