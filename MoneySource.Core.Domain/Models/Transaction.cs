using MoneySource.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Domain.Models
{
    public class Transaction : BaseDataModel
    {
        public TransactionType Type { get; set; }

        public double Amount { get; set; }

        public bool IsCompleted { get; set; }

        public DateTimeOffset ComplitionDate { get; set; }

        public Guid SourceId { get; set; }
        public Source Source { get; set; }
    }
}
