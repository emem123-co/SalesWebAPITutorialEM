using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebAPITutorialEM.Models;

namespace SalesWebAPITutorialEM.Data
{
    public class AppdBContext : DbContext
    {
        public AppdBContext (DbContextOptions<AppdBContext> options)
            : base(options)
        {
        } //no default constructor or "OnConfiguring" present because this is a Web Api app and those are only needed with console apps.

        public DbSet<Customer> Customers { get; set; } = default!; 
    }
}
