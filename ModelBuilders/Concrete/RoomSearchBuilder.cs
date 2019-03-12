using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HotelBooking.ViewModels;
using HotelBooking.Models;
using HotelBooking.ModelBuilders.Abstract;

namespace HotelBooking.ModelBuilders.Concrete {
    public class RoomSearchBuilder : IRoomSearchBuilder {

        private readonly WdaContext wdaContext;

        public RoomSearchBuilder(WdaContext wdaContext) {
            this.wdaContext = wdaContext;
        }

        public IEnumerable<Room> Build(RoomSearchModel roomSearchModel) {
            IEnumerable<Room> results;

            if (roomSearchModel != null) {

                results = wdaContext.Room.Include(room => room.RoomType).AsQueryable();
                results = results.Where(room => room.City.Equals(roomSearchModel.City));
                results = results.Where(room => room.RoomTypeId.Equals(roomSearchModel.RoomType));

                var bookings = wdaContext.Bookings.Where(booking => booking.CheckInDate.CompareTo(roomSearchModel.CheckOutDate) <= 0 &&
                                                                    roomSearchModel.CheckInDate.CompareTo(booking.CheckOutDate) <= 0);

                results = results.Where(r => !bookings.Any(b => b.RoomId.Equals(r.RoomId)));

                if (roomSearchModel.GuestsCount != null) {
                    results = results.Where(room => room.CountOfGuests >= roomSearchModel.GuestsCount);
                }

                if (roomSearchModel.PriceMin != null && roomSearchModel.PriceMin != 0) {
                    results = results.Where(room => room.Price >= roomSearchModel.PriceMin);
                }

                if (roomSearchModel.PriceMax != null && roomSearchModel.PriceMax != 5000) {
                    results = results.Where(room => room.Price <= roomSearchModel.PriceMax);
                }
            } else {
                results = wdaContext.Room;
            }

            results = results.OrderBy(room => room.Price);
            return results;
        }
    }
}
