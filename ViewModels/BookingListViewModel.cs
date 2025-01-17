using RentARide.Models;

namespace RentARide.ViewModels;
public class BookingListViewModel
{
    public IEnumerable<Booking> Bookings { get; set; }
}
