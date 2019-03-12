using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HotelBooking.ModelBuilders.Abstract;
using HotelBooking.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.ModelBuilders.Concrete {

    public class RoomModelBuilder : IRoomModelBuilder {

        private readonly WdaContext wdaContext;
        private readonly UserManager<User> userManager;

        public RoomModelBuilder(WdaContext wdaContext, UserManager<User> userManager) {
            this.wdaContext = wdaContext;
            this.userManager = userManager;
        }

        public RoomViewModel Build(int id, ClaimsPrincipal userLoged, string checkInDate, string checkOutDate) {
            const int maxRating = 5;
            var user = userManager.GetUserAsync(userLoged).Result;
            var currentRoom = wdaContext.Room.Where(room => room.RoomId == id).First();
            var reviews = wdaContext.Reviews.Include(review => review.Room).Include(review => review.User).Where(review => review.Room.Equals(currentRoom));
            Favorites favorite = null;
            bool hasBooking = !string.IsNullOrEmpty(checkInDate) && !string.IsNullOrEmpty(checkOutDate);

            if (user != null) {
                var favorites = wdaContext.Favorites.Include(fav => fav.Room).Include(fav => fav.User).AsQueryable();
                favorite = favorites.Where(fav => fav.Room.Equals(currentRoom) && fav.User.Equals(user)).FirstOrDefault();


                var bookings = wdaContext.Bookings.Include(booking => booking.Room).Include(booking => booking.User).Where(booking => booking.Room.Equals(currentRoom));

                hasBooking = bookings.Where(booking => booking.CheckInDate.CompareTo(checkOutDate) <= 0 && checkInDate.CompareTo(booking.CheckOutDate) <= 0).Any() ||
                             bookings.Where(booking => booking.User.Equals(user)).Any();
            }

            int fullStars = 0;
            int numOfReviews = 0;
            foreach (var review in reviews) {
                fullStars += review.Rate;
                numOfReviews++;
            }
            fullStars /= numOfReviews == 0 ? 1 : numOfReviews;
            int emptyStars = maxRating - fullStars;
            return new RoomViewModel(currentRoom, reviews, fullStars, emptyStars, favorite, user, hasBooking, checkInDate, checkOutDate);
        }
    }
}
