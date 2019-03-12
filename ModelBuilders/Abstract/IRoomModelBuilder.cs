using System.Security.Claims;
using HotelBooking.ViewModels;

namespace HotelBooking.ModelBuilders.Abstract {
    public interface IRoomModelBuilder {

        RoomViewModel Build(int id, ClaimsPrincipal userLoged, string checkInDate, string checkOutDate);
    }
}
