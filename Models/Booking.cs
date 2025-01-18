using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace RentARide.Models;

[Collection("bookings")]
public class Booking
{
    public ObjectId Id { get; set; }

    public ObjectId CarId { get; set; }

    [DisplayName("Car Model")]    
    public string CarModel { get; set; } = null!;

    [DisplayName("Plate Number")]    
    public string CarPlateNumber { get; set; } = null!;

    [Required(ErrorMessage = "The start date is required to make this booking")]
    [DisplayName("Start Date")]    
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "The end date is required to make this booking")]
    [DisplayName("End Date")]    
    public DateTime EndDate { get; set; }
}