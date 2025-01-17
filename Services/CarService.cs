using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using RentARide.Models;
using RentARide.Services.Interfaces;

namespace RentARide.Services;

public class CarService(RentARideDbContext context) : ICarService
{
    private readonly RentARideDbContext _context = context;

    public void Add(Car car)
    {
        _context.Cars.Add(car);

        _context.ChangeTracker.DetectChanges();
        Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

        _context.SaveChanges();
    }

    public void Delete(ObjectId id)
    {
        var carToDelete = GetById(id);

        if (carToDelete is not null)
        {
            _context.Cars.Remove(carToDelete);
            
            _context.ChangeTracker.DetectChanges();
            Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
        }
        else
        {
            throw new ArgumentException("Car to delete could not be found");
        }
    }

    public void Edit(Car car)
    {
        var carToUpdate = GetById(car.Id);

        if (carToUpdate is not null)
        {
            carToUpdate.Model = car.Model;
            carToUpdate.PlateNumber = car.PlateNumber;
            carToUpdate.Location = car.Location;
            carToUpdate.IsAvailable = car.IsAvailable;

            _context.Cars.Update(carToUpdate);

            _context.ChangeTracker.DetectChanges();
            Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

            _context.SaveChanges();
        }
        else 
        {
            throw new ArgumentException("Car to update could not be found");
        }
    }

    public IEnumerable<Car> GetAll()
    {
        return _context.Cars.OrderBy(c => c.Id)
                            .AsNoTracking()
                            .AsEnumerable();
    }

    public Car? GetById(ObjectId id)
    {
        return _context.Cars.FirstOrDefault(c => c.Id == id);
    }
}