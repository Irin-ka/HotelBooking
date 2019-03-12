using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models;
using HotelBooking.ViewModels;
using HotelBooking.ModelBuilders.Abstract;

namespace HotelBooking.Controllers {

    public class SearchController : Controller {

        private readonly IFormBuilder formBuilder;
        private readonly IRoomSearchBuilder roomSearchBuilder;

        public SearchController(IFormBuilder formBuilder, IRoomSearchBuilder roomSearchBuilder) {
            this.formBuilder = formBuilder;
            this.roomSearchBuilder = roomSearchBuilder;
        }

        [Route("Search/SearchResults")]
        [HttpGet]
        public IActionResult SearchResults(RoomSearchModel searchModel) {
            ViewData["Cities"] = formBuilder.GetCities();
            ViewData["RoomTypes"] = formBuilder.GetRoomTypes();
            ViewData["SearchModel"] = searchModel;
            IEnumerable<Room> results;
            if (ModelState.IsValid) {
                results = roomSearchBuilder.Build(searchModel);
            } else {
                results = new List<Room>();
            }
            return View(results);
        }
    }
}