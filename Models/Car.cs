using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace RentARide.Models;

[Collection("cars")]
public class Car
{
    public ObjectId Id { get; set; }

    [Required(ErrorMessage = "You must provide the make and model")]
    [DisplayName("Make and Model")]
    public string? Model { get; set; }

    [Required(ErrorMessage = "The number plate is required to identify the vehicle")]
    [DisplayName("Plate Number")]
    public string PlateNumber { get; set; } = null!;

    [Required(ErrorMessage = "You must add the location of the car")]
    public string Location { get; set; } = null!;

    public bool IsAvailable { get; set; } = true;
}