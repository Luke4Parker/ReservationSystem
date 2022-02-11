using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Daos
{
    public class ReservationSystemDao : DbContext
    {
        public ReservationSystemDao(DbContextOptions<ReservationSystemDao> options) : base(options)
        { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Brand> Brands { get; set; }

    }

    
}
