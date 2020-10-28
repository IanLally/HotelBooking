using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingExample
{
    /// <summary>
    /// Class to hold booking information
    /// </summary>
    public class Booking : IEnumerable
    {
        public int Room;
        public string Guest;
        public DateTime Date;

        public Booking(int room, string guest, DateTime date)
        {
            Room = room;
            Guest = guest;
            Date = date;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Date.ToString()).GetEnumerator();
        }
    }
}
