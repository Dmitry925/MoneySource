using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Source> Sources { get; set; }

        Task<int> SaveAsync();
    }
}
