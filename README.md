# Relational Databases

## Async Inn

Lab12-Relational Databases
Lab13 - Dependency Injection

*Author: Na'ama Bar-Ilan*

----

## Description

This is a C# console web application. It builds out a relational database and an API server for a Hotel Asset Management system, based on client requirements.

---

### Getting Started
Clone this repository to your local machine.

```
$ git clone [https://github.com/NaamaBarIlan/Lab12-Relational-DB.git]
```

### To run the program from Visual Studio:
Select ```File``` -> ```Open``` -> ```Project/Solution```

Next navigate to the location you cloned the Repository.

Double click on the ```Lab12-Relational-DB``` directory.

Then select and open ```Lab12-Relational-DB.sln```

---

### Visuals

#### Database ERD Diagram

* This web application is implemented the following database Schema, created for Code 401: Advanced Software Development in ASP.NET Core.
![ERD](Assets/AsyncInn2.png)

---

### Entity Relationship Overview

* **Hotel** - has a 1:* relationship with HotelRoom, since a hotel has many rooms, but each room only belongs to one hotel. 
* **HotelRoom** - in addition to the *:1 relationship with Hotel described above, has a *:1 relationship with Room, since there can be many hotel Rooms who have the same layout, but each room can only have one layout.
* **Room** - in addition to the 1:* relationship with HotelRoom described above, has a 1:* relationship with RoomAmenities, since each room can have multiple amenities, but the unique combination of the Pure Join table RoomAmenities only relates to one room. 
* **RoomAmenities** -  in addition to the *:1 relationship with Room described above, has a *:1 relationship with Amenities, since each specific Amenity will only be included once in RoomAmenities and RoomAmenities can include many different amenities.  
* **Amenity** - has a 1:* relationship with RoomAmenities described above. 

---

### Architecture

The project architecture was refactored using a repository design pattern to allow and implement dependency injection.

In the previous version of the application the controller was directly connected to the DBContext through _context. This dependency in the design pattern would prove to be problematic if another databased, with a different structure, needed to be added to the project later on. 

In the redesign, two additional middle parts were added to the project: an interface and a service(repository). Now the controller does not directly access or depend on DBContext. It has access to all the relevant behaviors via the interface, while the repository implements the interface. 

This added level of abstraction created loosely coupled components, and ensures that the controller does not depend on specific data, only the behaviors of a database. 



### Change Log

1.1 *Refactored the project architecture to allow and implement dependency injection.* - 22 Jul 2020

------------------------------
