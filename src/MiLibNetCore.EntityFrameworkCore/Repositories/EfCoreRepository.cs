﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

    public class EfCoreRepository<TEntity> : RepositoryBase<TEntity>, IEfCoreRepository<TEntity>
        where TEntity : class, IEntity
    {
        //protected DbContext Context { get; }
        //private readonly SmartInventContext _context;
        //protected virtual DbContext DbContext => _dbContextProvider.GetDbContext();

        public EfCoreRepository(DbContext context)
        {
            DbContext = context;
        }

        /// <inheritdoc />
        public override TEntity Insert(TEntity entity, bool autoSave = false)
        {
            var savedEntity = DbSet.Add(entity).Entity;

            if (autoSave)
            {
                DbContext.SaveChanges();
            }

            return savedEntity;
        }

        public override async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var savedEntity = DbSet.Add(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }

            return savedEntity;
        }

        /// <inheritdoc />
        public override TEntity Update(TEntity entity, bool autoSave = false)
        {
            DbContext.Attach(entity);

            var updatedEntity = DbContext.Update(entity).Entity;

            if (autoSave)
            {
                DbContext.SaveChanges();
            }

            return updatedEntity;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbContext.Attach(entity);

            var updatedEntity = DbContext.Update(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }

            return updatedEntity;
        }

        /// <inheritdoc />
        public override void Delete(TEntity entity, bool autoSave = false)
        {
            DbSet.Remove(entity);

            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public override async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity);

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        /// <inheritdoc />
        public override List<TEntity> GetList(bool includeDetails = false)
        {
            return includeDetails
                ? WithDetails().ToList()
                : DbSet.ToList();
        }

        public override async Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails().ToListAsync(GetCancellationToken(cancellationToken))
                : await DbSet.ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <inheritdoc />
        public override long GetCount()
        {
            return DbSet.LongCount();
        }

        public override async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <inheritdoc />
        protected override IQueryable<TEntity> GetQueryable()
        {
            return DbSet.AsQueryable();
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = false)
        {
            base.Delete(predicate, autoSave);

            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public override async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entities = await GetQueryable()
                .Where(predicate)
                .ToListAsync(GetCancellationToken(cancellationToken));

            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        DbContext IEfCoreRepository<TEntity>.DbContext => DbContext;

        /// <inheritdoc />
        public DbContext DbContext { get; }

        /// <inheritdoc />
        public virtual DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        public override IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = GetQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }
    }

    public class EfCoreRepository<TEntity, TKey> : EfCoreRepository<TEntity>,
            IEfCoreRepository<TEntity, TKey> //,
        //ISupportsExplicitLoading<TEntity, TKey>

        //where TDbContext : IEfCoreDbContext
        where TEntity : class, IEntity<TKey>
    {
        /// <inheritdoc />
        public EfCoreRepository(DbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public virtual TEntity Get(TKey id, bool includeDetails = true)
        {
            var entity = Find(id, includeDetails);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken));

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        /// <inheritdoc />
        public virtual TEntity Find(TKey id, bool includeDetails = true)
        {
            return includeDetails
                ? WithDetails().FirstOrDefault(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id))
                : DbSet.Find(id);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails().FirstOrDefaultAsync(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id), GetCancellationToken(cancellationToken))
                : await DbSet.FindAsync(new object[] { id }, GetCancellationToken(cancellationToken));
        }

        /// <inheritdoc />
        public virtual void Delete(TKey id, bool autoSave = false)
        {
            var entity = Find(id, includeDetails: false);
            if (entity == null)
            {
                return;
            }

            Delete(entity, autoSave);
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, includeDetails: false, cancellationToken: cancellationToken);
            if (entity == null)
            {
                return;
            }

            await DeleteAsync(entity, autoSave, cancellationToken);
        }
    }

   
}
