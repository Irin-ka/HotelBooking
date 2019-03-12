using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using HotelBooking.ControllerHelpers.RoomControllerHelpers.Abstract;
using HotelBooking.Models;

namespace HotelBooking.ControllerHelpers.RoomControllerHelpers.Concrete {
    public class ReviewHandler : IReviewHandler {

        private readonly WdaContext wdaContext;
        private readonly UserManager<User> userManager;

        public ReviewHandler(WdaContext wdaContext, UserManager<User> userManager) {
            this.wdaContext = wdaContext;
            this.userManager = userManager;
        }

        public void AddReview(ClaimsPrincipal claimsPrincipal, int roomId, int rating, string text) {
            wdaContext.Reviews.Add(new Reviews {
                UserId = userManager.GetUserAsync(claimsPrincipal).Result.UserId,
                RoomId = roomId,
                Rate = rating,
                Text = text,
                DateCreated = DateTime.Now
            });
            wdaContext.SaveChanges();
        }
    }
}
