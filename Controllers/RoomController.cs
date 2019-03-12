using System;
using Microsoft.AspNetCore.Mvc;
using HotelBooking.ViewModels;
using HotelBooking.ModelBuilders.Abstract;
using HotelBooking.ControllerHelpers.RoomControllerHelpers.Abstract;

namespace HotelBooking.Controllers {

    public class RoomController : Controller {

        private readonly IRoomModelBuilder roomModelBuilder;
        private readonly IFavoriteChanger favoriteChanger;
        private readonly IBookingHandler bookingHandler;
        private readonly IReviewHandler reviewHandler;

        public RoomController(IRoomModelBuilder roomModelBuilder, IFavoriteChanger favoriteChanger, IBookingHandler bookingHandler, IReviewHandler reviewHandler) {
            this.roomModelBuilder = roomModelBuilder;
            this.favoriteChanger = favoriteChanger;
            this.bookingHandler = bookingHandler;
            this.reviewHandler = reviewHandler;
        }

        [Route("Room/{id}")]
        public IActionResult Room(int roomId, string checkInDate = null, string checkOutDate = null) {
            if (!ModelState.IsValid) {
                throw new ApplicationException("Invalid room model");
            }
            RoomViewModel roomViewModel = roomModelBuilder.Build(roomId, HttpContext.User, checkInDate, checkOutDate);
            return View(roomViewModel);
        }

        [Route("Room/Booking")]
        public IActionResult MakeBooking(int roomId, string checkInDate, string checkOutDate) {
            if (!ModelState.IsValid) {
                throw new ApplicationException("Invalid booking model");
            }
            var success = bookingHandler.TryMakeBooking(HttpContext.User, roomId, checkInDate, checkOutDate);



            return RedirectToAction("BookingResult", new {
                roomId = roomId,
                success = success
            });
        }

        [Route("Room/BookingResult")]
        public IActionResult BookingResult(int roomId, bool success) {
            string roomName = roomModelBuilder.Build(roomId, HttpContext.User, null, null).Room.Name;
            string msg = success ? "Booking completed!" : "Oops, room is already booked";
            var model = new BookingResultViewModel(roomId, roomName, msg);
            return View(model);
        }

        [Route("Room/ChangeFavorite")]
        public IActionResult ChangeFavorite(int roomId, string checkInDate, string checkOutDate) {
            if (!ModelState.IsValid) {
                throw new ApplicationException("Invalid favorite model");
            }
            favoriteChanger.ChangeFavorite(HttpContext.User, roomId);
            return RedirectToAction("Room", new {
                roomId = roomId,
                checkInDate = checkInDate,
                checkOutDate = checkOutDate
            });
        }

        [Route("Room/SubmitReview")]
        [HttpPost]
        public IActionResult SubmitReview(int roomId, int rating, string text, string checkInDate, string checkOutDate) {
            if (!ModelState.IsValid) {
                throw new ApplicationException("Invalid review model");
            }
            reviewHandler.AddReview(HttpContext.User, roomId, rating, text);
            return RedirectToAction("Room", new {
                roomId = roomId,
                checkInDate = checkInDate,
                checkOutDate = checkOutDate
            });
        }

    }
}