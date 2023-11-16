using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarpe21ShopNoole.Core.Dto
{
    public class FileToApiDto
    {
        public Guid ID { get; set; }
        public string ExistingFilePath { get; set; }
        public Guid? RealEstateId { get; set; }
        public Guid? CarId { get; set; }
    }
}
