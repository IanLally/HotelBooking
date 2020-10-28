using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingExample
{
    public interface IBookingManager
    {
        /// <summary>
        /// Returns true if there is no booking for the given room on the date, otherwise false
        /// </summary>
        /// <param name="room">The room number requested</param>
        /// <param name="date">The date & time to book</param>
        /// <returns></returns>
        bool IsRoomAvailable(int room, DateTime date);
        
        /// <summary>Adds a booking for the given guest in the given room on the given date.
        /// If the room is not available, throw a suitable Exception.
        /// </summary>
        /// <param name="guest">The guest surname</param>
        /// <param name="room">The room requested</param>
        /// <param name="date">The date</param>
        void AddBooking(string guest, int room, DateTime date);

        /// <summary>
        /// Get a list of all available rooms
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>Enumerable Int array</returns>
        IEnumerable<int> GetAvailableRooms(DateTime date);
    }
}
