using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E18I4DABH4Gr4.Models;
using Microsoft.EntityFrameworkCore;
// Git ignore
namespace E18I4DABH4Gr4.Context
{
    public class TraderContext : DbContext
    {
        public TraderContext(DbContextOptions<TraderContext> options) : base(options)
        {
            
        }

        public DbSet<Trader> Traders { get; set; }

    }
}
