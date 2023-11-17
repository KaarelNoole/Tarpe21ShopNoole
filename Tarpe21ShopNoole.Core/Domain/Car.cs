using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarpe21ShopNoole.Core.Domain
{
    public class Car
    {
        public Guid ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string GearShiftType { get; set; }

        public IEnumerable<FileToApi> FilesToApi { get; set; } = new List<FileToApi>(); //files to be added to the api


        //database properties
        public DateTime CreatedAt { get; set; } // when entry was added to the database
        public DateTime ModifiedAt { get; set; } //when was entry modified in the database
    }
}
