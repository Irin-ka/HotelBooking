using System.Security.Claims;

namespace HotelBooking.ControllerHelpers.RoomControllerHelpers.Abstract {
    public interface IFavoriteChanger {

        void ChangeFavorite(ClaimsPrincipal claimsPrincipal, int roomId);
    }
}
