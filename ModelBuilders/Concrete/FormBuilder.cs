using System.Collections.Generic;
using System.Linq;
using HotelBooking.ModelBuilders.Abstract;
using HotelBooking.Models;

namespace HotelBooking.ModelBuilders.Concrete {
    public class FormBuilder : IFormBuilder {

        private readonly WdaContext wdaContext;

        public FormBuilder(WdaContext wdaContext) {
            this.wdaContext = wdaContext;
        }

        public IEnumerable<string> GetCities() {
            return wdaContext.Room.Select(room => room.City).Distinct().AsEnumerable();
        }

        public IEnumerable<RoomType> GetRoomTypes() {
            return wdaContext.RoomType.AsEnumerable();
        }
    }
}
