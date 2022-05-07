using System;

namespace HotelBookingManager.DataModels
{
    internal class BookingModel
    {
        public int RoomID { get; set; }
        public string GuestName { get; set; }
        public DateTime DateBooked { get; set; }
    }
}
