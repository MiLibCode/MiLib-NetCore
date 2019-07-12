using System;
using System.Collections.Generic;
using System.Text;
using MiLibNetCore.Domain.Entities;
using MiLibNetCore.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MiLibNetCore.EntityFrameworkCore.Repositories
{
    public interface IEfCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        DbContext DbContext { get; }

        DbSet<TEntity> DbSet { get; }
    }

    public interface IEfCoreRepository<TEntity, TKey> : IEfCoreRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}
