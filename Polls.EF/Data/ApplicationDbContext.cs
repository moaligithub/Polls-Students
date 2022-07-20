using Microsoft.EntityFrameworkCore;
using Polls.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.EF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Poll> Poll { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {  
        }
    }
}
