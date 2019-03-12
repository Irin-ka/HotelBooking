using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using HotelBooking.ControllerHelpers.RoomControllerHelpers.Abstract;
using HotelBooking.Models;

namespace HotelBooking.ControllerHelpers.RoomControllerHelpers.Concrete {
    public class FavoriteChanger : IFavoriteChanger {

        private readonly WdaContext wdaContext;
        private readonly UserManager<User> userManager;

        public FavoriteChanger(WdaContext wdaContext, UserManager<User> userManager) {
            this.wdaContext = wdaContext;
            this.userManager = userManager;
        }

        public void ChangeFavorite(ClaimsPrincipal claimsPrincipal, int roomId) {
            var user = userManager.GetUserAsync(claimsPrincipal).Result;
            var favorite = wdaContext.Favorites.Where(fav => fav.RoomId.Equals(roomId) && fav.UserId.Equals(user.UserId)).FirstOrDefault();

            if (favorite == null) {
                favorite = new Favorites {
                    RoomId = roomId,
                    UserId = user.UserId,
                    Status = 1,
                    DateCreated = DateTime.Now
                };
                wdaContext.Favorites.Add(favorite);
            } else {
                favorite.Status = favorite.Status == 0 ? 1 : 0;
                wdaContext.Favorites.Update(favorite);
            }

            wdaContext.SaveChanges();
        }
    }
}
