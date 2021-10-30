using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<Transaction> Transactions { get; set; }
    }
}
