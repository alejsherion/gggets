
using System.Data.Objects;
using System.IO;
using EFCachingProvider.Caching;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Core.Entities;


namespace ETS.GGGETSApp.Infrastructure.Data.Core
{
    /// <summary>
    /// This is the minimun contract for all unit of work, one unit of work per module, that extend
    /// base IUnitOfWork contract with specific features of ADO .NET EF and STE. 
    /// Creation of this and base contract add isolation feature from specific contract for
    /// testing purposed and delete innecesary dependencies
    /// </summary>
    public interface IQueryableUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Create a object set for a type TEntity
        /// </summary>
        /// <typeparam name="TEntity">Type of elements in object set</typeparam>
        /// <returns>Object set of type {TEntity}</returns>
        IObjectSet<TEntity> CreateSet<TEntity>() where TEntity : class,IObjectWithChangeTracker;

        ICache Cache { get; set; }

        CachingPolicy CachingPolicy { get; set; }

        TextWriter Log { get; set; }

    }
}
