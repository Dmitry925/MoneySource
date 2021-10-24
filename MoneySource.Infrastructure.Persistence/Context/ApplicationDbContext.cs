using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction = MoneySource.Core.Domain.Models.Transaction;

namespace MoneySource.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Source> Sources { get; set; }

        public async Task<int> SaveAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
