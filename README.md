# Hotel Reservation API
###### .NET 8.0
------

Hotel Reservation System is a reservation system for booking hotel rooms. It charges various rates for particular sections of the hotel.
For example, penthouse suites which cost more. The system keeps track of when rooms will be available and can be scheduled.

## Run Project With Dotnet CLI
* Download and install .NET 8.0 SDK: [https://dotnet.microsoft.com/en-us/download/dotnet/8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Build and Run:
```sh
cd HotelReservationApi
dotnet run
```
Open a browser and navigate to [http://localhost:5173](http://localhost:5173)

## Run Project With Docker
#### Requirements:
  * Make sure your machine has Docker and Docker Compose installed.
  ([https://docs.docker.com/compose/install/](https://docs.docker.com/compose/install/))
 
#### Build and Run image using docker-compose:

```sh
docker-compose up --build
```
Open a browser and navigate to [http://localhost:44330](http://localhost:44330)

## Swagger Documentation

When debugging the API, the browser will automatically open to the Swagger UI.


## Unit Test
Build and Run Tests:
```sh
cd ServicesTests
dotnet test
```
## License

MIT