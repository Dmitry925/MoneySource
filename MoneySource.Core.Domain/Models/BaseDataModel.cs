using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Domain.Models
{
    public abstract class BaseDataModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public DateTimeOffset CreationDate { get; set; }
    }
}
