using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RentARide.Models;
using RentARide.Services.Interfaces;
using RentARide.ViewModels;

namespace RentARide.Controllers;

public class CarController(ICarService carService) : Controller
{
    private readonly ICarService _carService = carService;

    public IActionResult Index()
    {
        CarListViewModel viewModel = new()
        {
            Cars = _carService.GetAll()
        };

        return View(viewModel);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(CarAddViewModel carAddViewModel)
    {
        if (ModelState.IsValid)
        {
            Car newCar = new()
            {   
                Model = carAddViewModel.Car.Model,
                Location = carAddViewModel.Car.Location,
                PlateNumber = carAddViewModel.Car.PlateNumber,
            };
            _carService.Add(newCar);
            return RedirectToAction("Index");
        }

        return View(carAddViewModel);
    }

    public IActionResult Edit(string id)
    {
        if (string.IsNullOrEmpty(id) || _carService.GetById(new ObjectId(id)) is null)
        {
            return NotFound();
        }

        var selectedCar = _carService.GetById(new ObjectId(id));
        return View(selectedCar);
    }

    [HttpPost]
    public IActionResult Edit(Car car)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var carToEdit = _carService.GetById(car.Id);
            if (carToEdit is not null)
            {
                _carService.Edit(car);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", $"Car with Id {car.Id} could not be found");
            }
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", $"Updating the car failed, try again. Error: {e.Message}");
        }

        return View(car);
    }

    public IActionResult Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var selectedCar = _carService.GetById(new ObjectId(id));
        return View(selectedCar);
    }

    [HttpPost]
    public IActionResult Delete(ObjectId id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (string.IsNullOrEmpty(id.ToString()) || _carService.GetById(id) is null)
        {
            ViewData["ErrorMessage"] = "Deleting the car failed, invalid ID";
            return View();
        }
        try
        {
            _carService.Delete(id);
            TempData["CarDeleted"] = "Car deleted successfully";
            
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ViewData["ErrorMessage"] = $"Deleting the car failed, try again. Error: {e.Message}";
        }

        var selectedCar = _carService.GetById(id);
        return View(selectedCar);
    }
}