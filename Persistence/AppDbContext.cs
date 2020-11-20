using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            int result = await base.SaveChangesAsync(cancellationToken);            

            return result;
        }
    }
}
