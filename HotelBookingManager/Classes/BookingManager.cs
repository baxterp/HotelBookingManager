using System;
using System.Collections.Generic;

using HotelBookingManager.Interfaces;
using HotelBookingManager.DataModels;
using System.Linq;

namespace HotelBookingManager.Classes
{
    internal class BookingManager : IBookingManager
    {
        public BookingManager()
        {
            bookings = new List<BookingModel>();
            roomList = ConfigHelper.GetRoomList();
        }

        public void AddBooking(string guest, int room, DateTime date)
        {
            try
            {
                if(!IsRoomAvailable(room, date))
                {
                    throw new RoomNotAvailableException();
                }

                lock (bookings) // Thread safety
                {
                    bookings.Add(new BookingModel()
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
            List<int> bookingForToday = bookings.Where(x => x.DateBooked.Day == date.Day
                                                            && x.DateBooked.Month == date.Month
                                                            && x.DateBooked.Year == date.Year)
                                                .Select(s => s.RoomID).ToList();

            return roomList.Where(rl => !bookingForToday.Contains(rl));
        }

        public bool IsRoomAvailable(int room, DateTime date)
        {
            return bookings.Where(b => b.DateBooked.Day == date.Day
                                    && b.DateBooked.Month == date.Month
                                    && b.DateBooked.Year == date.Year
                                    && b.RoomID == room).Any() == false;
        }

        private List<BookingModel> bookings { get; }
        private List<int> roomList { get; }
    }

    internal class RoomNotAvailableException : Exception
    {
        public RoomNotAvailableException()
        {
        }
    }
}
