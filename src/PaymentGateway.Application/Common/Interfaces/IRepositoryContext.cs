using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Common.Interfaces
{
    public interface IRepositoryContext : IDisposable
    {
        DbSet<Transaction> Transactions { get; set; }
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
