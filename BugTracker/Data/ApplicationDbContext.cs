using BugTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<IssueType> IssueTypes { get; set; }
        public virtual DbSet<Priority> PriorityTypes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public DbSet<BugTracker.Models.Tempo> Tempo { get; set; }

    }
}

