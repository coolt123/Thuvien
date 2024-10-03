using ThuvienMvc.Dtos.UserDtos;
using ThuvienMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace ThuvienMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService servie)
        {
            _service = servie;
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
                return RedirectToAction("Index", "Home");
            }
            else if (result == "Admin")
            {
                HttpContext.Session.SetString("UserRole","admin");
                HttpContext.Session.SetInt32("UserId", input.IdSign);
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
