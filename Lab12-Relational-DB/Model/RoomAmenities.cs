using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model
{
    public class RoomAmenities
    {
        // our composite key is both keys together combined
        public int RoomId { get; set; }

        public int AmenityId { get; set; }


        // Navigation property

        public Room Room { get; set; }
        public Amenity Amenity { get; set; }


    }
}
