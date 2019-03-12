using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HotelBooking.ControllerHelpers.RoomControllerHelpers.Abstract;
using HotelBooking.Models;

namespace HotelBooking.ControllerHelpers.RoomControllerHelpers.Concrete {
    public class BookingHandler : IBookingHandler {

        private readonly WdaContext wdaContext;
        private readonly UserManager<User> userManager;

        public BookingHandler(WdaContext wdaContext, UserManager<User> userManager) {
            this.wdaContext = wdaContext;
            this.userManager = userManager;
        }

        public bool TryMakeBooking(ClaimsPrincipal claimsPrincipal, int roomId, string checkInDate, string checkOutDate) {
            var room = wdaContext.Room.Where(r => r.RoomId == roomId).First();
            
            bool isBooked = wdaContext.Bookings.Include(booking => booking.Room).Where(booking => booking.Room.Equals(room) &&
                                             booking.CheckInDate.CompareTo(checkOutDate) <= 0 &&
                                             checkInDate.CompareTo(booking.CheckOutDate) <= 0).Any();

            if (!isBooked) {
                var booking = new Bookings {
                    UserId = userManager.GetUserAsync(claimsPrincipal).Result.UserId,
                    RoomId = roomId,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    DateCreated = DateTime.Now
                };

                wdaContext.Bookings.Add(booking);
                wdaContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
