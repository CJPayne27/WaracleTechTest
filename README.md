# Hotel Waracle Booking API

Welcome to the Readme of the Hotel Waracle Booking API. Here you will find some notes and information regarding the API, created by me, CJ Grant.

I decided on a layered architecture approach, with business logic and DAL separate from the presentation layer. Given this task was solely backend focused, I have decided to keep working on it and develop a basic frontend for it also, to build on my React skills. The backend API will remain static for the revision of the tech test.

#### Supporting Documents and Information

* I generated a `test-data.xslx` file which can be found in the `/docs` folder. I used ChatGPT to scaffold up the data so that the Database Seeding can be spun up and tore down when required (within the `/Database` endpoint)
* There is a Postman collection for the API which contains all endpoints, some valid and invalid requests, and this is located in `\Docs\Hotel Waracle Booking Api.postman_collection.json`
* I decided on using a SQL Server 2022 Db locally. This Db had multiple tables to hold dummy data when initially seeded, and then populated as and when was required through `POST` endpoints (mainly the `CreateBooking` endpoint).
    - The db was then transferred to Azure hosting via a bacpac file (file is located in the  `HotelWaracleBookingApi\Docs` folder alongside a .bak file)

# API

API is available here - https://hotelwaraclebookingapi.azurewebsites.net/swagger

This **should** be unlocked and publicly available, but if there are any issues viewing or hitting Swagger, please  <a href="mailto:cjpayne27@gmail.com?subject=HotelWaracle%20Azure%20API%20access%20request">Request API Access</a>.

Please import the Swagger json collection and the `Azure` environment, both located in the `/Docs` folder.

## Hotels
 
I have created 3 main Hotel Groups, each with 3 locality hotels.

- Hotel Alpha
    - Glasgow, Liverpool and Brighton
- Hotel Beta
    - Edinburgh, Aberdeen and Manchester
- Hotel Delta
    - London, Inverness and Newcastle

## Hotel Rooms

Each of these hotels has 6 rooms, and these rooms are a mixture of Single/Double/Deluxe, and a mix of minimum capacity and maximum capacity.

## Bookings

I have created some dummy data for bookings, but the simple customer journey would be;

1. Make a booking
2. Validate booking information (max number of guests, date validation), which would satify Business Rule *"A room cannot be occupied by more people than its capacity."*
3. Check booking date doesn't overlap with existing booking
4. Return availability checks
5. Return booking information (BookingId)

When Creating a Booking, I have implemented a simple `Guid.NewGuid()` to satify the Business Rule of "*Booking numbers should be unique. There should not be overlapping at any given time*." If I knew for sure that the API was going to be a high volume/high traffic API, we should potentially look to implement an auto-incremental `BookingId` but for the simplest, quickest way to get the API up, I chose the Guid route.

_We could extend this functionality to its full extent, but due to the time constraints, I chose to keep it quite basic._

## Availability

* Checks for basic room availability.
* Added a single `IsOccupied` flag onto the Availability model, which would satify the Business Rule of "*A room cannot be double booked for any given night.*" If a room is marked as `IsOccupied`, it doesn't show up on the Availability check.

* This would then also partly cover the Business Rule of "*Any booking at the hotel must not require guests to change rooms at any point during their stay*"

# Tests

* I created an initial class/mockery to show the setup I would ideally take when it comes to writing unit/acceptance tests for the API project.
* Due to time constraints and project dependencies, I haven't spent long at all considering tests. In a real-world example, I would likely lean more toward TDD as this is how I've spent the past 3 years or so when developing automated testing.
* I would also consider E2E testing as standard using Playwright/Typescript and generate a Postman collection.