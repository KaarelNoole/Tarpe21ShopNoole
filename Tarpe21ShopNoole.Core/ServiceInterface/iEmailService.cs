using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarpe21ShopNoole.Core.Dto;

namespace Tarpe21ShopNoole.Core.ServiceInterface
{
    public interface IEmailService
    {
        void SendEmail(EmailDto dto);

    }
}
