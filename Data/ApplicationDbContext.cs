using EntityFramework_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EntityFramework_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<StudentModel> Students { get; set; }
    }
}
