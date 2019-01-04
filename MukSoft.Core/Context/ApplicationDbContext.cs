using Microsoft.EntityFrameworkCore;
using MukSoft.Core.Domain;

namespace MukSoft.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

    }
}
