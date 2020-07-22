# Relational Databases

## Async Inn

Lab12-Relational Databases

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


### Change Log

------------------------------
