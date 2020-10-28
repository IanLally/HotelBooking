using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingExample
{
    public class BookingManager : IBookingManager
    {
        static readonly object _object = new object();
        internal Bookings bookings = new Bookings();

        /// <summary>Adds a booking for the given guest in the given room on the given date.
        /// If the room is not available, throw a suitable Exception.
        /// </summary>
        /// <param name="guest">The guest surname</param>
        /// <param name="room">The room requested</param>
        /// <param name="date">The date</param>
        public void AddBooking(string guest, int room, DateTime date)
        {
            System.Diagnostics.Debug.WriteLine("Thread waiting at " + DateTime.Now.ToLongTimeString());
            Monitor.Enter(_object); // Ensure thread safety
            System.Diagnostics.Debug.WriteLine("Thread locked at " + DateTime.Now.ToLongTimeString());
            
            if (System.Diagnostics.Debugger.IsAttached) // Purely for demonstration purposes to show the thread safety
            {
                Thread.Sleep(1000);
            }

            try
            {
                // Check if a booking existss
                foreach (Booking booking in bookings)
                {
                    if ((booking.Date.Date == date.Date) && (booking.Room == room))
                    {
                        if (booking.Guest == guest)
                            throw new Exception("Already booked for this guest");
                        else
                            throw new Exception("Already booked on this date");
                    }
                }

                // If no exception thrown it's safe to add the booking safely
                bookings.BookingList.Add(new Booking(room, guest, date.Date));
            }
            finally // to prevent a deadlock if an exception is thrown
            {
                Monitor.Exit(_object);
            }
        }

        /// <summary>
        /// Returns true if there is no booking for the given room on the date, otherwise false
        /// </summary>
        /// <param name="room">The room number requested</param>
        /// <param name="date">The date & time to book</param>
        /// <returns>bool - true if a room is available, false if not</returns>
        public bool IsRoomAvailable(int room, DateTime date)
        {
            bool roomAvailable = false;

            // Get all available rooms for the date
            IEnumerable<int> rooms = GetAvailableRooms(date.Date);

            foreach(var tempRoom in rooms)
            {
                if (tempRoom == room)
                    roomAvailable = true;
            }
            return roomAvailable;
        }

        /// <summary>
        /// Get a list of all available rooms
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>Enumerable Int array</returns>
        public IEnumerable<int> GetAvailableRooms(DateTime date)
        {
            List<int> rooms = new List<int>(bookings.Rooms);
            List<Booking> confirmedBookingsForDate = bookings.BookingList.Where(s => s.Date.Date == date.Date).ToList();
            
            foreach(Booking booking in confirmedBookingsForDate)
            {
                rooms.Remove(booking.Room);
            }

            return rooms;
        }
    }
}
