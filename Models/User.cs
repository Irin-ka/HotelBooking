using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HotelBooking.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
