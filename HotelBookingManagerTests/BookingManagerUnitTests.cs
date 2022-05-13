using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelBookingManager.Classes;
using HotelBookingManager.Interfaces;
using HotelBookingManager.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingManagerTests
{
    [TestClass]
    public class BookingManagerUnitTests
    {
        [TestMethod]
        public void AddBookingTest()
        {
            IBookingManager bookingManager = new BookingManager();
            DateTime dateBooked = new System.DateTime(2032, 1, 1);
            string guestName = "Joe Bloggs";
            int roomID = 101;

            try
            {
                bookingManager.AddBooking(guestName, roomID, dateBooked);
                Assert.IsTrue(true); // No Asset.Pass available in MS Test Projects
            }
            catch (Exception)
            {
                Assert.Fail("Exception experienced");
            }
        }

        [TestMethod]
        public async Task AddBookingMultiThreadedTest()
        {
            IBookingManager bookingManager = new BookingManager();
            DateTime dateBooked = new System.DateTime(2032, 1, 1);
            string guestName = "Joe Bloggs";
            int roomID1 = 101;
            int roomID2 = 102;
            int totalRoomsAvailable = ConfigHelper.GetRoomList().Count;

            Task testTask1 = new Task(() =>
            {
                bookingManager.AddBooking(guestName, roomID1, dateBooked);
            });

            Task testTask2 = new Task(() =>
            {
                bookingManager.AddBooking(guestName, roomID2, dateBooked);
            });

            testTask1.Start();
            testTask2.Start();
            await Task.WhenAll(testTask1, testTask2);

            List<int> availableRooms = bookingManager.GetAvailableRooms(dateBooked).ToList();

            Assert.AreEqual(availableRooms.Count, totalRoomsAvailable - 2);   
        }

        [TestMethod]
        public void GetAvailbaleRoomsTest()
        {
            IBookingManager bookingManager = new BookingManager();
            DateTime dateBooked = new System.DateTime(2032, 1, 1);
            int totalRoomsAvailable = ConfigHelper.GetRoomList().Count;

            List<int> roomNumber = bookingManager.GetAvailableRooms(dateBooked).ToList();
            Assert.AreEqual(roomNumber.Count, totalRoomsAvailable);
        }

        [TestMethod]
        public void GetAvailbaleRoomsWithAddTest()
        {
            IBookingManager bookingManager = new BookingManager();
            int totalRoomsAvailable = ConfigHelper.GetRoomList().Count;

            DateTime dateBooked = new System.DateTime(2032, 1, 1);
            string guestName = "Joe Bloggs";
            int roomID = 101;

            bookingManager.AddBooking(guestName, roomID, dateBooked);
            List<int> roomNumber = bookingManager.GetAvailableRooms(dateBooked).ToList();

            Assert.AreEqual(roomNumber.Count, totalRoomsAvailable - 1);
        }

        [TestMethod]
        public void IsRoomAvailableTest()
        {
            IBookingManager bookingManager = new BookingManager();
            DateTime dateBooked = new System.DateTime(2032, 1, 1);
            int roomID = 101;

            bool roomAvailable = bookingManager.IsRoomAvailable(roomID, dateBooked);

            Assert.IsTrue(roomAvailable);
        }

        [TestMethod]
        public void IsRoomAvailableWithAddTest()
        {
            IBookingManager bookingManager = new BookingManager();
            DateTime dateBooked = new System.DateTime(2032, 1, 1);
            string guestName = "Joe Bloggs";
            int roomID = 101;

            bookingManager.AddBooking(guestName, roomID, dateBooked);

            bool roomAvailable = bookingManager.IsRoomAvailable(roomID, dateBooked);
            Assert.IsFalse(roomAvailable);
        }
    }
}
