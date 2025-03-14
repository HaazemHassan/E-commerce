﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoriesContracts
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T?> Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        Task<T> Create(T entity);
        T? Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);

    }
}
