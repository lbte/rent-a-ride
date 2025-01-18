# Rent A Ride

Rent A Ride is a web application that allows users to manage cars and their bookings seamlessly. Built using the .NET MVC framework with Razor Pages, this app provides CRUD operations for cars and bookings, along with a user-friendly interface to view available cars and manage reservations.

## Features

### 1. **Home Page**
- Displays a welcome message to the user.
- Includes a button to view the available cars and book any of them.

![Home Page](/Docs/home-page.png)

### 2. **Cars**
Displays a list of all available cars and has the following options:
- **Add New Car**: Add new car to the database by specifying the car model, license plate, and location.
- **Edit Car**: Update the details of an existing car.
- **Delete Car**: Remove car from the database.
- **Book Car**: Book any available car by selecting the desired car and booking start and end date.

![Cars Page](/Docs/cars-view.png)

### 3. **Bookings**
Displays all bookings with their details (car model, license plate, and booking start and end date).
- **Edit Booking**: Modify existing booking.
- **Delete Booking**: Remove booking when no longer needed.

![Bookings Page](/Docs/bookings-view.png)

## Database Structure
Rent A Ride uses MongoDB as the database backend. The database consists of two collections:

### Cars Collection
**Fields**:
- `Model`: The car's model.
- `PlateNumber`: The car's license plate number.
- `Location`: The car's location.
- `IsAvailable`: Boolean to check availability of the car.

```json
{
  "_id": "ObjectId",
  "Model": "Toyota Corolla",
  "PlateNumber": "ABC123",
  "Location": "New York",
  "IsAvailable": false
}
```

### Bookings Collection
**Fields**:
- `CarId`: The booked car's id.
- `CarModel`: The booked car's model.
- `CarPlateNumber`: The booked car's license plate number.
- `StartDate`: The start date of the booking.
- `EndDate`: The end date of the booking.

```json
{
  "_id": "ObjectId",
  "CarId": "ObjectId",
  "CarModel": "Toyota Corolla",
  "CarPlateNumber": "ABC123",
  "StartDate": "2025-01-18T05:00:00.000+00:00",
  "EndDate": "2025-01-19T05:00:00.000+00:00",
}
```

## Screenshots

### Viewing Cars
![View Cars](/Docs/cars-view-1-booked.png)

### Adding a Car
![Add Car](/Docs/add-car.png)

### Editing a Car
![Edit Car](/Docs/edit-car.png)

### Deleting a Car
![Delete Car](/Docs/delete-car.png)

### Cars collection
![Cars collection](/Docs/cars-collection.png)

### Booking a Car
![Book Car](/Docs/add-booking.png)

### Editing a Booking
![Edit Booking](/Docs/edit-booking.png)

### Deleting a Booking
![Delete Booking](/Docs/delete-booking.png)

### Bookings collection
![Bookings collection](/Docs/bookings-collection.png)

## Setup Instructions

### Prerequisites
- .NET 6 SDK or later
- Free MongoDB Atlas account and free tier cluster
- Visual Studio Code or any preferred IDE

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/lbte/rent-a-ride.git
   ```
2. Navigate to the project directory:
   ```bash
   cd rent-a-ride
   ```
3. Set up your MongoDB connection string and database name in the `appsettings.json` file, just by adding your [cluster's connection string](https://www.mongodb.com/docs/guides/atlas/connection-string/?utm_campaign=devrel&utm_medium=cta&utm_content=NHezylU9Cw8&utm_term=luce.carter) in the `AtlasURI` field:
    ```json
   "MongoDBSettings": {
        "AtlasURI": "",
        "DatabaseName": "rent-a-ride"
    }
   ```
   
4. Run the application:
   ```bash
   dotnet run
   ```
