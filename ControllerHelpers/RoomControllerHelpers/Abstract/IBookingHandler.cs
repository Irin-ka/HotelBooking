using System.Security.Claims;

namespace HotelBooking.ControllerHelpers.RoomControllerHelpers.Abstract {
    public interface IBookingHandler {

        bool TryMakeBooking(ClaimsPrincipal claimsPrincipal, int roomId, string checkInDate, string checkOutDate);
    }
}
