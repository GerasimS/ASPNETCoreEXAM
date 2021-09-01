using ASPNetCoreExam.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreExam.Data
{
    public class StreamingPlatformContext : DbContext
    {
        public StreamingPlatformContext(DbContextOptions<StreamingPlatformContext> options) : base(options)
        {
        }

        public DbSet<StreamingPlatform> StreamingPlatforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StreamingPlatform>().ToTable("StreamingPlatform");
        }
    }
}
