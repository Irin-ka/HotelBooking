﻿using System;
using System.Collections.Generic;

namespace HotelBooking.Models
{
    public partial class Bookings
    {
        public int BookingId { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
