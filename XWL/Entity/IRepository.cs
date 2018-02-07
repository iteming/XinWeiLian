using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Entity
{
    /// <summary>
    /// Repository
    /// </summary>
    public partial interface IRepository<T> where T : class
    {
        IQueryable<T> Get();

        IQueryable<T> Get(Expression<Func<T, bool>> filter);

        IQueryable<T> Get<TOrderKey>(Expression<Func<T, bool>> filter, int page, int limit, Expression<Func<T, TOrderKey>> sort, bool isAsc = true);
        
        int Insert(T entity);
        int Insert(List<T> list);
        
        int Update(T entity);
        int Update(List<T> list);
        
        int Delete(T entity);
        int Delete(List<T> list);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }

        int ExecuteSql(string sqlString);

        void Dispose();
    }
}
