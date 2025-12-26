using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.Models.DTOs;
using ShoppingCartApp.Models.Repositories;

namespace ShoppingCartApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationRepository _repository;
        public AuthenticationController(IAuthenticationRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = _repository.Authenticate(loginDTO);

            if (user == null) 
            {
                ViewBag.LoginMessage = "Username or Password Incorrect";
                return View(loginDTO);
            }

            HttpContext.Session.SetString("Authenticated", "true");
            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index","Home");
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.SetString("Authenticated", "false");
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserDTO userDTO)
        {
            //Check if the users provided password and confiration match
            if (userDTO.Password.Equals(userDTO.PasswordConfirmation) == false)
            {
                //Generate an error message for the user and return to the form.
                ViewBag.CreateUserError = "Password and Confirmaion does not match!";
                return View(userDTO);
            }
            //Check that the data meets any rules oulined in the model such as
            //password rules or username restrictions.
            if (ModelState.IsValid ==  false)
            {
                return View(userDTO);
            }
            //Send the form data to the repository to be saved
            var user = _repository.CreateUser(userDTO);
            //If no user is retunred, indicating the username is already taked
            if (user == null)
            {
                //Generate an error message for the user and return to the form.
                ViewBag.CreateUserError = "Username already exists. Try a different name.";
                return View(userDTO);
            }
            //Send the user to the Login page to test their new account
            return RedirectToAction("Login");
        }

        // GET: AuthenticationController/AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
