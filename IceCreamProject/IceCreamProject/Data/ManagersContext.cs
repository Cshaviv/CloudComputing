using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IceCreamProject.Models;

    public class ManagersContext : DbContext
    {
        public ManagersContext (DbContextOptions<ManagersContext> options)
            : base(options)
        {
        }

        public DbSet<IceCreamProject.Models.Manager> Manager { get; set; }
    }
