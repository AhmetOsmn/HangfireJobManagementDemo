using HangfireJobManagementDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace HangfireJobManagementDemo.Context
{
    public class JobDemoContext : DbContext
    {
        public JobDemoContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Message> Messages { get; set; }
    }
}
