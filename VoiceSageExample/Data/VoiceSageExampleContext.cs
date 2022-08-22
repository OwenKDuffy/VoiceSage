using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VoiceSageExample.Models;

namespace VoiceSageExample.Data
{
    public class VoiceSageExampleContext : DbContext
    {
        public VoiceSageExampleContext (DbContextOptions<VoiceSageExampleContext> options)
            : base(options)
        {

        }

        public DbSet<VoiceSageExample.Models.Group> Group { get; set; } = default!;
    }
}
