using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaSoft.Models
{
    public class DeltaSoftContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<TaskTable> TaskTables { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public virtual DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //builder.Entity<UserTask>().HasKey(v => new { v.TaskId, v.UserId });
            builder.Entity<TaskTable>()
             .HasOne(e => e.ApplicationUsers)
               .WithMany(c => c.TaskTables);


            builder.Entity<TaskTable>()
           .Property(b => b.CreatedAt)
           .HasDefaultValueSql("getdate()");


        }

        public DeltaSoftContext(DbContextOptions options) : base(options)
        {
        }

    }
}
