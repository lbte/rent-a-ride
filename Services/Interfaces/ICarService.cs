using MongoDB.Bson;
using RentARide.Models;

namespace RentARide.Services.Interfaces;

public interface ICarService
{
    IEnumerable<Car> GetAll();
    
    Car? GetById(ObjectId id);

    void Add(Car car);

    void Edit(Car car);

    void Delete(ObjectId id);
}