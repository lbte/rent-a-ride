using MongoDB.Bson;
using RentARide.Models;

namespace RentARide.Services.Interfaces;

public interface IBookingService
{
    IEnumerable<Booking> GetAll();
    
    Booking? GetById(ObjectId id);

    void Add(Booking booking);

    void Edit(Booking booking);

    void Delete(ObjectId id);
}