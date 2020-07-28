using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Layout { get; set; }

        // Nav property
        public List<RoomAmenities> RoomAmenities { get; set; }

        public HotelRoom HotelRoom { get; set; }

    }
}
