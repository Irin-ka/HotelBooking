using System.Security.Claims;

namespace HotelBooking.ControllerHelpers.RoomControllerHelpers.Abstract {
    public interface IReviewHandler {

        void AddReview(ClaimsPrincipal claimsPrincipal, int roomId, int rating, string text);
    }
}
