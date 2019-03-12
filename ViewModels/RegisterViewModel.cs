using System.ComponentModel.DataAnnotations;

namespace HotelBooking.ViewModels {
    public class RegisterViewModel {

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
