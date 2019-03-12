using System.Collections.Generic;
using HotelBooking.Models;

namespace HotelBooking.ModelBuilders.Abstract {
    public interface IFormBuilder {

        IEnumerable<string> GetCities();

        IEnumerable<RoomType> GetRoomTypes();
    }
}
