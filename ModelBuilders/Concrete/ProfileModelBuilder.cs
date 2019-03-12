using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HotelBooking.Models;
using HotelBooking.ModelBuilders.Abstract;
using HotelBooking.ViewModels;

namespace HotelBooking.ModelBuilders.Concrete {
    public class ProfileModelBuilder : IProfileModelBuilder {

        private readonly WdaContext wdaContext;
        private readonly UserManager<User> userManager;

        public ProfileModelBuilder(WdaContext wdaContext, UserManager<User> userManager) {
            this.wdaContext = wdaContext;
            this.userManager = userManager;
        }

        public ProfileViewModel BuildModel(ClaimsPrincipal userLoged) {
            var user = userManager.GetUserAsync(userLoged).Result;
            var bookings = wdaContext.Bookings.Include(booking => booking.User).Include(booking => booking.Room).Include(booking => booking.Room.RoomType).Where(booking => booking.User.Equals(user));
            var reviews = wdaContext.Reviews.Include(review => review.User).Include(review => review.Room).Where(review => review.User.Equals(user));
            var favorites = wdaContext.Favorites.Include(favorite => favorite.User).Include(favorite => favorite.Room).Where(favorite => favorite.User.Equals(user) && favorite.Status != 0);
            return new ProfileViewModel(user, bookings, reviews, favorites);
        }
    }
}
