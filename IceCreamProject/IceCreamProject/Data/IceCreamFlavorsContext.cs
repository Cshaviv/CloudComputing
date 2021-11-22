using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IceCreamProject.Models;

    public class IceCreamFlavorsContext : DbContext
    {
        public IceCreamFlavorsContext (DbContextOptions<IceCreamFlavorsContext> options)
            : base(options)
        {
        }

        public DbSet<IceCreamProject.Models.IceCreamFlavor> IceCreamFlavor { get; set; }
    }
