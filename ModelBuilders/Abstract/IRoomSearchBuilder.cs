using System.Collections.Generic;
using HotelBooking.Models;
using HotelBooking.ViewModels;

namespace HotelBooking.ModelBuilders.Abstract {
    public interface IRoomSearchBuilder {

        IEnumerable<Room> Build(RoomSearchModel roomSearchModel);
    }
}
