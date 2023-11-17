namespace Tarpe21ShopNoole.Models.RealEstates
{
    public enum EstateType
    {
        House, Apartment, Room, Land, ParkingSpace, TimeShare, Garage, StorageUnit, Mansion, Castle, Station
    }

    public class RealEstateCreateUpdateViewModel
    {
        public Guid? ID { get; set; } // unique id
        public string Address { get; set; } // street name, house numberm flat number. "Tulika 14-6"
        public string? City { get; set; } //city where realestate is , city is optional incase the
        public string Country { get; set; } //what country estate is in
        public string County { get; set; } // county where the realestate is
        public int PostalCode { get; set; } // postal code for the address
        public int PhoneNumber { get; set; } //phonenumber to call about the property
        public int FaxNumber { get; set; } //phonenumber to call about the property
        public string ListingDescription { get; set; } // Genereic description containg anything not reflected by the model
        public int SqueareMeters { get; set; } // How big the property is by square meters
        public DateTime BuildDate { get; set; } // when was it built
        public int Price { get; set; } // price of the estate property
        public int RoomCount { get; set; } //total room count in the estate
        public int FloorCount { get; set; } // how many floor does the building have
        public int? EstateFloor { get; set; } // what floor the flat/apartment is on
        public int Bathrooms { get; set; } // how many bathrooms are in the estate
        public int Bedrooms { get; set; } // how many bedrooms are in the estate
        public bool DoesHaveParkingSpace { get; set; } // does the property come with a parking space
        public int DoesHavePowerConnection { get; set; } // does the property have connection to the power grid
        public int DoesHaveWaterGridConnection { get; set; } // does the property have connection to water grud
        public decimal? SqMPrice // whats the price for square meter in this property
        {
            get { return Price / SqueareMeters; }
        }
        public string Type { get; set; } // what type of an estate is this
        public bool IsPropertyNewDevelopent { get; set; } // shows of the estate being sold is a newly developed housing unit, or an older existing one
        public bool IsPropertySold { get; set; } //shows if the property has been sold already
        public List<IFormFile> Files { get; set; } //files 
        public List<FileToApiViewModel> FileToApiViewModels { get; set; } = new List<FileToApiViewModel>(); //file viewmodels
        //database only properties

        public DateTime CreatedAt { get; set; } // when entry was added to the database
        public DateTime ModifiedAt { get; set; } //when was entry modified in the database

    }
}
