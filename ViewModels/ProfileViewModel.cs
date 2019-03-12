using System.Collections.Generic;
using HotelBooking.Models;

namespace HotelBooking.ViewModels {
    public class ProfileViewModel {
        public User User { get; set; }
        public IEnumerable<Bookings> Bookings { get; set; }
        public IEnumerable<Reviews> Reviews { get; set; }
        public IEnumerable<Favorites> Favorites { get; set; }

        public ProfileViewModel(User user, IEnumerable<Bookings> bookings, IEnumerable<Reviews> reviews, IEnumerable<Favorites> favorites) {
            User = user;
            Bookings = bookings;
            Reviews = reviews;
            Favorites = favorites;
        }
    }
}
