using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingExample
{
    /// <summary>
    /// Class to hold information on a collection of bookings
    /// </summary>
    internal class Bookings : IEnumerable
    {
        internal List<Booking> BookingList = new List<Booking>();
        internal List<int> Rooms = new List<int>();

        /// <summary>
        /// Constructor - sets initial values for rooms and pre existing bookings
        /// </summary>
        internal Bookings()
        {
            // Here you could load from a Database/API
            Rooms.Add(101);
            Rooms.Add(102);
            Rooms.Add(201);
            Rooms.Add(203);

            BookingList.Add(new Booking(101, "Lally", new DateTime(2020, 10, 27)));
            BookingList.Add(new Booking(101, "Lally", new DateTime(2020, 10, 28)));
            BookingList.Add(new Booking(101, "Lally", new DateTime(2020, 10, 29)));

            BookingList.Add(new Booking(102, "Gates", new DateTime(2020, 10, 27)));
            BookingList.Add(new Booking(102, "Gates", new DateTime(2020, 10, 29)));

            BookingList.Add(new Booking(201, "Zuckerberg", new DateTime(2020, 10, 28)));
            BookingList.Add(new Booking(201, "Zuckerberg", new DateTime(2020, 10, 29)));

            BookingList.Add(new Booking(203, "Musk", new DateTime(2020, 10, 27)));
            BookingList.Add(new Booking(203, "Musk", new DateTime(2020, 10, 29)));
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)BookingList).GetEnumerator();
        }
    }
}
