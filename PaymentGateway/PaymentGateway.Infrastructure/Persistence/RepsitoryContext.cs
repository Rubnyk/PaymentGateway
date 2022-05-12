using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Infrastructure.Persistence.Extensions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Persistence
{
    public class RepositoryContext : DbContext, IRepositoryContext
    {
        public RepositoryContext(DbContextOptions options)
          : base(options)
        { }
        public virtual DbSet<User> Users { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync(new CancellationToken()).ConfigureAwait(false);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SetCamelCase();
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SetGlobalQueryFiltersToEntities(builder);
            base.OnModelCreating(builder);
        }

        public void SetGlobalQueryFiltersToEntities(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                //if (typeof(IEntity).IsAssignableFrom(entity.ClrType))
                //    modelBuilder.Entity(entity.ClrType).AddQueryFilter<IEntity>(e => e.ID_DB == true);                
            }
        }

        public void EnsureCreated()
        {
            this.Database.EnsureCreated();
        }

        public void EndsureDeleted()
        {
            this.Database.EnsureDeleted();
        }


    }
}
