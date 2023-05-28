using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prototype.Models;

namespace Prototype.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Prototype.Models.Attendances> Attendances { get; set; }
        public DbSet<Prototype.Models.Classrooms> Classrooms { get; set; }
        public DbSet<Prototype.Models.Courses> Courses { get; set; }
        public DbSet<Prototype.Models.Sessions> Sessions { get; set; }
        public DbSet<Prototype.Models.Students> Students { get; set; }
        public DbSet<Prototype.Models.Teachers> Teachers { get; set; }
    }
}
