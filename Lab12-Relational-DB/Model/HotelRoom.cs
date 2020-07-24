using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model
{
    public class HotelRoom
    {
        // our composite key is both keys together combines
        public int HotelId { get; set; }

        public int RoomNumber { get; set; }

        public int RoomId { get; set; }

        public Decimal Rate { get; set; }

        public bool PetFriendly { get; set; }


        // Navigation property

        public Hotel Hotel { get; set; }
        public Room Room { get; set; }

    }
}
