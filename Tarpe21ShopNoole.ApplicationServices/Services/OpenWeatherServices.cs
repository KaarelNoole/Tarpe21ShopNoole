using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarpe21ShopNoole.Core.Dto.OpenWeatherDto;
using Tarpe21ShopNoole.Core.ServiceInterface;

namespace Tarpe21ShopNoole.ApplicationServices.Services
{
    public class OpenWeatherServices : IWeatherForecastsServices
    {
        public OpenWeatherServices(IOpenWeatherServices)
        {
                
        }
        public async Task<OpenWeatherResultDto>
    }
}
