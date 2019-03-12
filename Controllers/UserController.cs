using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using HotelBooking.ViewModels;
using HotelBooking.Models;
using HotelBooking.ControllerHelpers.UserControllerHelpers.Abstract;

namespace HotelBooking.Controllers {
    public class UserController : Controller {

        private readonly SignInManager<User> signInManager;
        private readonly IRegisterHandler registerHandler;

        public UserController(SignInManager<User> signInManager, IRegisterHandler registerHandler) {
            this.signInManager = signInManager;
            this.registerHandler = registerHandler;
        }

        [Route("User/Login")]
        public IActionResult Login() {
            return View();
        }

        [Route("User/Register")]
        public IActionResult Register() {
            return View();
        }

        [Route("User/Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel) {
            if (!ModelState.IsValid) {
                return View(registerViewModel);
            }
            ViewData["result"] = registerHandler.TryRegister(registerViewModel);
            return View("RegisterResult");
        }


        [Route("User/Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var result = signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false).Result;
            if (result.Succeeded) {
                return RedirectToAction("Profile", "Home");
            }
            return View(model);
        }

        [Route("User/Logout")]
        public async Task<IActionResult> Logout() {
            if (User.Identity.IsAuthenticated) {
                await signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}