using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using RentARide.Models;
using RentARide.Services.Interfaces;

namespace RentARide.Services;

public class BookingService(RentARideDbContext context, ICarService carService) : IBookingService
{
    private readonly RentARideDbContext _context = context;

    private readonly ICarService _carService = carService;

    public void Add(Booking booking)
    {
        var bookedCar = _carService.GetById(booking.CarId);

        if (bookedCar is null)
        {
            throw new ArgumentException("Car to book could not be found");
        }

        booking.CarModel = bookedCar!.Model ?? "";
        bookedCar.IsAvailable = false;
        _context.Cars.Update(bookedCar);

        _context.Bookings.Add(booking);

        _context.ChangeTracker.DetectChanges();
        Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

        _context.SaveChanges();
    }

    public void Delete(ObjectId id)
    {
        var bookingToDelete = GetById(id);

        if (bookingToDelete is not null)
        {
            var bookedCar = _carService.GetById(bookingToDelete.CarId);
            bookedCar!.IsAvailable = true;

            _context.Cars.Update(bookedCar);
            _context.Bookings.Remove(bookingToDelete);

            _context.ChangeTracker.DetectChanges();
            Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

            _context.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Booking to delete could not be found");
        }
    }

    public void Edit(Booking booking)
    {
        var bookingToUpdate = GetById(booking.Id);

        if (bookingToUpdate is not null)
        {
            bookingToUpdate.StartDate = booking.StartDate;
            bookingToUpdate.EndDate = booking.EndDate;

            _context.Bookings.Update(bookingToUpdate);

            _context.ChangeTracker.DetectChanges();
            Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

            _context.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Booking to update could not be found");
        }
    }

    public IEnumerable<Booking> GetAll()
    {
        return _context.Bookings.OrderBy(b => b.StartDate)
                                .AsNoTracking()
                                .AsEnumerable();
    }

    public Booking? GetById(ObjectId id)
    {
        return _context.Bookings.FirstOrDefault(b => b.Id == id);
    }
}