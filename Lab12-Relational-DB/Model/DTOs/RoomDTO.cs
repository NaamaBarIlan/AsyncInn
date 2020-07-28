using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.DTOs
{
    public class RoomDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Layout { get; set; }

        public List<AmenityDTO> Amenities { get; set; }
    }
}
