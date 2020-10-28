using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingTests
{
    [TestClass]
    public class IBookingManagerTests
    { 
        HotelBookingExample.BookingManager bm = new HotelBookingExample.BookingManager();

        [TestMethod]
        public void Task1_IsRoomAvailable_GoodBooking_ReturnsTrue()
        {
            int roomNumber = 201; // Avoid magic strings
            DateTime date = new DateTime(2020, 10, 27);
            Assert.IsTrue(bm.IsRoomAvailable(roomNumber, date));
        }

        [TestMethod]
        public void Task1_IsRoomAvailable_BadBooking_ReturnsTrue()
        {
            int roomNumber = 101;
            DateTime date = new DateTime(2020, 10, 27);
            Assert.IsFalse(bm.IsRoomAvailable(roomNumber, date));
        }

        [TestMethod]
        public void Task1_AddBooking_ExistingBookingForGuest_ReturnsErrorGuestAlreadyBooked()
        {
            string guest = "Lally";
            int roomNumber = 101;
            DateTime date = new DateTime(2020, 10, 27);
            try
            {
                bm.AddBooking(guest, roomNumber, date);
            }
            catch (Exception ex)
            {
                if (ex.Message != "Already booked for this guest")
                    throw new Exception("Unexpected error in unit test: " + ex.Message);
            }
        }

        [TestMethod]
        public void Task1_AddBooking_ExistingBooking_ReturnsErrorNonGuestBooked()
        {
            string guest = "Page";
            int roomNumber = 101;
            DateTime date = new DateTime(2020, 10, 27);
            try
            {
                bm.AddBooking(guest, roomNumber, date);
            }
            catch (Exception ex)
            {
                if (ex.Message != "Already booked on this date")
                    throw new Exception("Unexpected error in unit test: " + ex.Message);
            }
        }

        [TestMethod]
        public void Task1_AddBooking_NewBooking_AddsBooking()
        {
            string guest = "Page";
            int roomNumber = 101;
            DateTime date = new DateTime(2020, 10, 30);
            bm.AddBooking(guest, roomNumber, date);

            try
            {
                bm.AddBooking(guest, roomNumber, date);
            }
            catch (Exception ex)
            {
                if (ex.Message != "Already booked for this guest")
                    throw new Exception("Unexpected error in unit test: " + ex.Message);
            }
        }

        /// <summary>
        /// Run this test in Debug mode only
        /// </summary>
        [TestMethod]
        public void Task1_DebugThreadSafetyTest_CreateThreadPoolAndTestsLocking()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.For(0, 10, i =>
            {
                System.Diagnostics.Debug.WriteLine("Thread {0} started at {1}", i, DateTime.Now.ToLongTimeString());
                string guest = "Page";
                int roomNumber = 101;
                DateTime date = new DateTime(2020, 11, i+1); // change the date per thread to ensure uniqueness
                bm.AddBooking(guest, roomNumber, date);
                System.Diagnostics.Debug.WriteLine("Thread {0} finished at {1}", i, DateTime.Now.ToLongTimeString());
            });

            stopwatch.Stop();

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Assert.IsTrue(stopwatch.ElapsedMilliseconds > 10000); //Greater than 10 seconds
            }
            else
            {
                Assert.Inconclusive();
            }
        }

        [TestMethod]
        public void Task2_GetAvailableRooms_CheckInitialRooms_ReturnInitialCount()
        {
            DateTime date = new DateTime(2020, 10, 27);
            IEnumerable<int> rooms = bm.GetAvailableRooms(date);
            Assert.IsTrue(rooms.Count() == 1);

            date = new DateTime(2020, 10, 28);
            rooms = bm.GetAvailableRooms(date);
            Assert.IsTrue(rooms.Count() == 2);

            date = new DateTime(2020, 10, 29);
            rooms = bm.GetAvailableRooms(date);
            Assert.IsTrue(rooms.Count() == 0);

            date = new DateTime(2020, 10, 30);
            rooms = bm.GetAvailableRooms(date);
            Assert.IsTrue(rooms.Count() == 4);

            this.Task1_AddBooking_NewBooking_AddsBooking(); // Add a user
            date = new DateTime(2020, 10, 30);
            rooms = bm.GetAvailableRooms(date);
            Assert.IsTrue(rooms.Count() == 3);
        }
    }
}
