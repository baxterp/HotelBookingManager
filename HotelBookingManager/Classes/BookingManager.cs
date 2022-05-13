using System;
using System.Collections.Generic;
using System.Linq;

using HotelBookingManager.Interfaces;
using HotelBookingManager.DataModels;

namespace HotelBookingManager.Classes
{
    public class BookingManager : IBookingManager
    {
        private SynchronizedCollection<BookingModel> Bookings { get; }
        private List<int> RoomList { get; }
        private readonly object lockObject = new();

        public BookingManager()
        {
            Bookings = new SynchronizedCollection<BookingModel>();
            RoomList = ConfigHelper.GetRoomList();
        }

        public void AddBooking(string guest, int room, DateTime date)
        {
            try
            {
                if(!IsRoomAvailable(room, date))
                {
                    throw new RoomNotAvailableException();
                }

                lock (lockObject) // Extra thread safety
                {
                    Bookings.Add(new BookingModel
                    {
                        GuestName = guest,
                        DateBooked = date,
                        RoomID = room,
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<int> GetAvailableRooms(DateTime date)
        {
            List<int> bookingForToday = Bookings.Where(x => x.DateBooked.Day == date.Day
                                                            && x.DateBooked.Month == date.Month
                                                            && x.DateBooked.Year == date.Year)
                                                .Select(s => s.RoomID).ToList();

            return RoomList.Where(rl => !bookingForToday.Contains(rl));
        }

        public bool IsRoomAvailable(int room, DateTime date)
        {
            return Bookings.Where(b => b.DateBooked.Day == date.Day
                                    && b.DateBooked.Month == date.Month
                                    && b.DateBooked.Year == date.Year
                                    && b.RoomID == room).Any() == false;
        }
    }

    internal class RoomNotAvailableException : Exception
    {
        public RoomNotAvailableException()
        {
        }
    }
}
