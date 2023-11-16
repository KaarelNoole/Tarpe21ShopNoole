using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarpe21ShopNoole.Core.Domain;

namespace Tarpe21ShopNoole.Core.Dto
{
    public class CarDto
    {
        public Guid? ID { get; set; } // creates unique id
        public string Brand { get; set; } // creates car brand
        public string Model { get; set; } // creates car model
        public decimal Price { get; set; } // creates car model
        public string Description { get; set; }
        public string GearShiftType { get; set; }

        public List<IFormFile> Files { get; set; } //files 
        public IEnumerable<FileToApiDto> FileToApiDtos { get; set; } = new List<FileToApiDto>(); //file viewmodels

        //database only properties

        public DateTime CreatedAt { get; set; } // when entry was added to the database
        public DateTime ModifiedAt { get; set; } //when was entry modified in the database
    }
}
