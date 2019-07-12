using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MiLibNetCore.Domain.Entities;
using MiLibNetCore.Domain.Repositories;

namespace MiLibNetCore.EntityFrameworkCore.Repositories
{
    //public class EfCoreRepository<TDbContext, TEntity> : RepositoryBase<TEntity>, IEfCoreRepository<TEntity>
    //    //where TDbContext : IEfCoreDbContext
    //    where TEntity : class, IEntity
    //{
    //    private readonly TDbContext _context;

    //    public EfCoreRepository(TDbContext context)
    //    {
    //        _context = context;
    //    }

    //    /// <inheritdoc />
    //    public override TEntity Insert(TEntity entity, bool autoSave = false)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    /// <inheritdoc />
    //    public override TEntity Update(TEntity entity, bool autoSave = false)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    /// <inheritdoc />
    //    public override void Delete(TEntity entity, bool autoSave = false)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    /// <inheritdoc />
    //    public override List<TEntity> GetList(bool includeDetails = false)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    /// <inheritdoc />
    //    public override long GetCount()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    /// <inheritdoc />
    //    protected override IQueryable<TEntity> GetQueryable()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected virtual TDbContext DbContext => _context;

    //    /// <inheritdoc />
    //    DbContext IEfCoreRepository<TEntity>.DbContext => DbContext.As<DbContext>();

    //    /// <inheritdoc />
    //    public virtual DbSet<TEntity> DbSet => DbContext.Set<TEntity>();
    //}
}
