using System.Collections.Generic;
using HotelBooking.Models;

namespace HotelBooking.ViewModels {
    public class RoomViewModel {
        public Room Room { get; set; }
        public IEnumerable<Reviews> Reviews { get; set; }
        public int FullStars { get; set; }
        public int EmptyStars { get; set; }
        public Favorites Favorites { get; set; }
        public User UserLoged { get; set; }
        public bool HasBooking { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }


        public RoomViewModel(Room room, IEnumerable<Reviews> reviews, int fullStars, int emptyStars, Favorites favorites, User userLoged, bool hasBooking, string checkInDate = null, string checkOutDate = null) {
            Room = room;
            Reviews = reviews;
            FullStars = fullStars;
            EmptyStars = emptyStars;
            Favorites = favorites;
            UserLoged = userLoged;
            HasBooking = hasBooking;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }
    }
}
