using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingManager.DataModels
{
    internal class BookingModel
    {
        public int RoomID { get; set; }
        public string GuestName { get; set; }
        public DateTime DateBooked { get; set; }
    }
}
