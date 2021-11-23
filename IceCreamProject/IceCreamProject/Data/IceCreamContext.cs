using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IceCreamProject.Models;
//using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace IceCreamProject.Data
{
 
    public class IceCreamContext : DbContext
    {
        public IceCreamContext() : base("IceCreamDB")
        {
            
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //}
        public DbSet<Orders> Orders { get; set; }
        //public DbSet<AddressChecking> Addresses { get; set; }
        public DbSet<IceCreamFlavor> IceCreamFlavors { get; set; }
        public DbSet<Manager> Managers { get; set; }

    }
}

