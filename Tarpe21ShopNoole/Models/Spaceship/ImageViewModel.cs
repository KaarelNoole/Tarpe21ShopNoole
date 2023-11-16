namespace Tarpe21ShopNoole.Models.Spaceship
{
    public class ImageViewModel
    {
        public Guid ImageID { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? SpaceshipID { get; set; }
    }
}
