using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RentARide.Models;
using RentARide.Services.Interfaces;
using RentARide.ViewModels;

namespace RentARide.Controllers;

public class BookingController(IBookingService BookingService, ICarService carService) : Controller
{
    private readonly IBookingService _bookingService = BookingService;
    private readonly ICarService _carService = carService;

    public IActionResult Index()
    {
        BookingListViewModel viewModel = new()
        {
            Bookings = _bookingService.GetAll()
        };

        return View(viewModel);
    }

    public IActionResult Add(string carId)
    {
        if (string.IsNullOrEmpty(carId) || _carService.GetById(new ObjectId(carId)) is null)
        {
            return NotFound();
        }

        var selectedCar = _carService.GetById(new ObjectId(carId));

        BookingAddViewModel bookingAddViewModel = new()
        {
            Booking = new Booking
            {
                CarId = selectedCar!.Id,
                CarModel = selectedCar!.Model,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1)
            }
        };

        return View(bookingAddViewModel);
    }

    [HttpPost]
    public IActionResult Add(BookingAddViewModel bookingAddViewModel)
    {
        if (ModelState.IsValid)
        {
            Booking newBooking = new()
            {
                CarId = bookingAddViewModel.Booking!.CarId,
                StartDate = bookingAddViewModel.Booking.StartDate,
                EndDate = bookingAddViewModel.Booking.EndDate
            };
            _bookingService.Add(newBooking);
            return RedirectToAction("Index");
        }

        return View(bookingAddViewModel);
    }

    public IActionResult Edit(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var selectedBooking = _bookingService.GetById(new ObjectId(id));
        return View(selectedBooking);
    }

    [HttpPost]
    public IActionResult Edit(Booking booking)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var bookingToEdit = _bookingService.GetById(booking.Id);
            if (bookingToEdit is not null)
            {
                _bookingService.Edit(booking);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", $"Booking with Id {booking.Id} could not be found");
            }
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", $"Updating the Booking failed, try again. Error: {e.Message}");
        }

        return View(booking);
    }

    public IActionResult Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var selectedBooking = _bookingService.GetById(new ObjectId(id));
        return View(selectedBooking);
    }

    [HttpPost]
    public IActionResult Delete(ObjectId id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (string.IsNullOrEmpty(id.ToString()) || _bookingService.GetById(id) is null)
        {
            ViewData["ErrorMessage"] = "Deleting the Booking failed, invalid ID";
            return View();
        }
        try
        {
            _bookingService.Delete(id);
            TempData["BookingDeleted"] = "Booking deleted successfully";

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ViewData["ErrorMessage"] = $"Deleting the Booking failed, try again. Error: {e.Message}";
        }

        var selectedBooking = _bookingService.GetById(id);
        return View(selectedBooking);
    }
}