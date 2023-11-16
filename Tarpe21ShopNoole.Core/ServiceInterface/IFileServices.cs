using Tarpe21ShopNoole.Core.Domain;
using Tarpe21ShopNoole.Core.Dto;

namespace Tarpe21ShopNoole.ApplicationServices.Services
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(SpaceshipDto dto, SpaceShip domain);
        Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto);
        Task<List<FileToDatabase>> RemoveImageFromDatabase(FileToDatabaseDto[] dtos);
        void FilesToApi(RealEstateDto dto, RealEstate realEstate);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);
        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);
        void FilesToApi(CarDto dto, Car car);
    }
}