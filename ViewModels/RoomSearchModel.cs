using System.ComponentModel.DataAnnotations;

namespace HotelBooking.ViewModels {
    public class RoomSearchModel {

        [Required]
        public string City { get; set; }

        [Required]
        public int? RoomType { get; set; }

        [Required]
        public string CheckInDate { get; set; }

        [Required]
        public string CheckOutDate { get; set; }

        public int? GuestsCount { get; set; }

        public int? PriceMin { get; set; }

        public int? PriceMax { get; set; }
    }
}
