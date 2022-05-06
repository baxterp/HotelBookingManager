using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingManager.Interfaces;

namespace HotelBookingManager.Classes
{
    internal class BookingManager : IBookingManager
    {
        public void AddBooking(string guest, int room, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetAvailableRooms(DateTime date)
        {
            throw new NotImplementedException();
        }

        public bool IsRoomAvailable(int room, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
