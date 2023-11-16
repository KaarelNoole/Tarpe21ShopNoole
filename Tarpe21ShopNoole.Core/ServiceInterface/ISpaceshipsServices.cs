using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarpe21ShopNoole.Core.Domain;
using Tarpe21ShopNoole.Core.Dto;

namespace Tarpe21ShopNoole.Core.ServiceInterface
{
    public interface ISpaceshipsServices
    {
        Task<SpaceShip> Create(SpaceshipDto dto);

        Task<SpaceShip> Update(SpaceshipDto dto);

        Task<SpaceShip> GetUpdate(Guid id);

        Task<SpaceShip> Delete(Guid id);
        Task<SpaceShip> GetAsync(Guid id);
    }
}
