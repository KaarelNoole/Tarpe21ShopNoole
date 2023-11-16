using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarpe21ShopNoole.Core.Domain;
using Tarpe21ShopNoole.Core.Dto;

namespace Tarpe21ShopNoole.Core.ServiceInterface
{
    public interface IRealEstatesServices
    {
        Task<RealEstate> GetAsync(Guid id);
        Task<RealEstate> Create(RealEstateDto dto);
        Task<RealEstate> Update(RealEstateDto dto);
        Task<RealEstate> Delete(Guid id);
    }
}
