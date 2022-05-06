using System;
using System.Collections.Generic;
using System.Linq;
using HotelBookingManager.Classes;
using HotelBookingManager.Interfaces;

namespace HotelBookingManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string optionSelected = string.Empty;
            List<string> optionsToSelect = new List<string> { "G", "A", "I", "X" };
            IBookingManager bookingManager = new BookingManager();

            do
            {
                do
                {
                    Console.WriteLine("Hello, please enter one of the following options");
                    Console.WriteLine(string.Empty);
                    Console.WriteLine("G : Get available rooms");
                    Console.WriteLine("A : Add booking");
                    Console.WriteLine("I : Is room available");
                    Console.WriteLine("X : Exit");
                    optionSelected = Console.ReadKey().KeyChar.ToString().ToUpper();

                } while (!optionsToSelect.Contains(optionSelected.ToUpper()));

                Console.WriteLine(string.Empty);
                Console.WriteLine(string.Empty);

                string dateString = string.Empty;
                DateTime proposedDate = DateTime.Now;
                switch (optionSelected)
                {
                    case "G":
                        Console.WriteLine("Get avaliable rooms");
                        Console.WriteLine(string.Empty);
                        proposedDate = GetValidDate();

                        Console.WriteLine("The available rooms for date : " + proposedDate.ToString("dd/MM/yyy") + ", are :");
                        List<int> availableRooms = bookingManager.GetAvailableRooms(proposedDate).ToList();
                        availableRooms.ForEach(ar => Console.WriteLine(ar));

                        break;

                    case "A":
                        Console.WriteLine("Add booking");
                        Console.WriteLine(string.Empty);

                        proposedDate = GetValidDate();
                        int roomNumber = GetValidRoomNumber();

                        Console.WriteLine("Name of booking");
                        string guestName = Console.ReadLine();

                        try
                        {
                            bookingManager.AddBooking(guestName, roomNumber, proposedDate);
                            Console.WriteLine("Booking added successfully");
                        }
                        catch (Exception ex)
                        {
                            // Error handling
                            throw;
                        }

                        break;
                    case "I":
                        Console.WriteLine("Is room available");
                        Console.WriteLine(string.Empty);

                        break;
                    case "X":
                        Console.WriteLine("Exit");
                        Console.WriteLine(string.Empty);

                        break;
                    default:
                        break;
                }

            } while (optionSelected != "X");
        }

        private static DateTime GetValidDate()
        {
            bool dateNotValid = false;
            bool dateInThePast = false;
            DateTime proposedDate = DateTime.Now;

            do
            {
                if (dateNotValid)
                    Console.WriteLine("** Date entered not valid **");

                if (dateInThePast)
                    Console.WriteLine("** Date entered in the past **");

                Console.WriteLine("Please enter date in the form dd/mm/yyy");
                string dateString = Console.ReadLine();
                try
                {
                    proposedDate = DateTime.ParseExact(dateString, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                    dateNotValid = false;
                    if (proposedDate < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)) // Only test for a date with time of 00:00
                    {
                        dateInThePast = true;
                    }
                }
                catch (FormatException)
                {
                    dateNotValid = true;
                }
            } while (dateNotValid || dateInThePast);

            return proposedDate;
        }

        private static int GetValidRoomNumber()
        {
            bool roomNumberNotValid = false;
            bool roomNumberNotInList = false;
            int proposedRoomNumber;
            List<int> roomList = new() { 101, 102, 201, 203 }; // Room list defined twice, bad pratice,
                                                               // but sticking to Interface defination given in the task description

            do
            {
                if (roomNumberNotValid)
                    Console.WriteLine("** room number entered not valid **");

                if (roomNumberNotInList)
                    Console.WriteLine("** room number entered not in list **");

                Console.WriteLine("Please enter one of the following room numbers");
                roomList.ForEach(rl => Console.WriteLine(rl));

                string roomNumberString = Console.ReadLine();
                roomNumberNotValid = !int.TryParse(roomNumberString, out proposedRoomNumber);
                roomNumberNotInList = !roomList.Contains(proposedRoomNumber);

            } while (roomNumberNotValid || roomNumberNotInList);

            return proposedRoomNumber;
        }
    }
}
