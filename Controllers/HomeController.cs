using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HotelBooking.Models;
using HotelBooking.ModelBuilders.Abstract;
using HotelBooking.ViewModels;


namespace HotelBooking.Controllers {
    public class HomeController : Controller {

        private readonly IProfileModelBuilder profileModelBuilder;
        private readonly IFormBuilder formBuilder;

        public HomeController(WdaContext wdaContext, SignInManager<User> signInManager, IProfileModelBuilder profileModelBuilder, IFormBuilder formBuilder) {
            this.profileModelBuilder = profileModelBuilder;
            this.formBuilder = formBuilder;
        }

        public IActionResult Index() {
            ViewData["Cities"] = formBuilder.GetCities();
            ViewData["RoomTypes"] = formBuilder.GetRoomTypes();
            return View();
        }

        [Route("Home/Profile")]
        public IActionResult Profile() {
            var profileViewModel = profileModelBuilder.BuildModel(HttpContext.User);
            return View(profileViewModel);
        }


        [HttpPost]
        public IActionResult Index(RoomSearchModel roomSearchModel) {
            if (!ModelState.IsValid) {
                ViewData["Cities"] = formBuilder.GetCities();
                ViewData["RoomTypes"] = formBuilder.GetRoomTypes();
                return View(roomSearchModel);
            }
            return RedirectToAction("SearchResults", "Search", roomSearchModel);
        }
    }
}