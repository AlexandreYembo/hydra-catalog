using System;
using System.Linq;
using System.Threading.Tasks;
using Hydra.Catalog.Core.Data;
using Hydra.Catalog.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Hydra.Catalog.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {

        //DbContextOptions used for entity framework core in dotnet core.
        //It is a kind of factory that will be configure the context on Startup.cs
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {

        }

        public DbSet<Product> Products {get; set; }
        public DbSet<Category> Categories {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)"); // avoid do create any column NVarchar(MAX)

                //Does not need to add map for each element, new EF supports
                //It will find all entities and mapping defined on DbSet<TEntity> via reflection
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }


        public async Task<bool> Commit()
        {
            //ChangeTracker -> EF: change mapper
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedDate") != null))
            {
                if(entry.State == EntityState.Added)
                    entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                
                if(entry.State == EntityState.Modified)
                    entry.Property("CreatedDate").IsModified = false;   //Ignore to update any value set for the property = "CreatedDate"
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
