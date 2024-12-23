﻿using Microsoft.EntityFrameworkCore;
using SupleNet.Application.Interfaces.Persistence.Repositories.Common;
using SupleNet.Domain.Entities.Common;
using SupleNet.Persistence.Data;

namespace SupleNet.Persistence.Repositories.Common
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : CommonEntity
    {
        protected readonly SupaNetContext _context;

        public GenericRepository(SupaNetContext context)
        {
            _context = context;
        }

        private DbSet<T> _set => _context.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _set.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity) => _set.Remove(entity));

        public IQueryable<T> GetAll() => _set;

        public IQueryable<T> GetAllReadOnly() => _set.AsNoTracking();

        public async Task<T?> GetAsync(Guid id) => await _set.SingleOrDefaultAsync(e => e.ID == id);

        public async Task<T?> GetReadOnlyAsync(Guid id) => await _set.AsNoTracking().SingleOrDefaultAsync(e => e.ID == id);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<T> UpdateAsync(T entity)
        {
            _set.Update(entity);
            await SaveChangesAsync();
            return entity;
        }
    }
}
