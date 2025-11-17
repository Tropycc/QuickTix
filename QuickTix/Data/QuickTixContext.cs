using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickTix.Models;

namespace QuickTix.Data
{
    public class QuickTixContext : DbContext
    {
        public QuickTixContext (DbContextOptions<QuickTixContext> options)
            : base(options)
        {
        }

        public DbSet<QuickTix.Models.Listing> Listing { get; set; } = default!;
        public DbSet<QuickTix.Models.Category> Category { get; set; } = default!;
        public DbSet<QuickTix.Models.Owner> Owner { get; set; } = default!;
        public DbSet<QuickTix.Models.Purchase> Purchase { get; set; } = default!;
    }
}
