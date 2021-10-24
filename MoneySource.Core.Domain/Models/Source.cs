using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Domain.Models
{
    public class Source : BaseDataModel
    {
        public ICollection<Transaction> Transactions { get; set; }
    }
}
