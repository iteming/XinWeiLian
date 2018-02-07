using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Entity
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class 
    {
        private readonly DbHelper _context;
        private IDbSet<T> _entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public Repository()
        {
            if (_context == null)
            {
                _context = new DbHelper();
            }
        }
        
        public virtual IQueryable<T> Get()
        {
            return Entities.AsQueryable();
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter)
        {
            return Entities.Where(filter).AsQueryable();
        }

        public virtual IQueryable<T> Get<TOrderKey>(Expression<Func<T, bool>> filter, 
            int page, int limit, Expression<Func<T, TOrderKey>> sort, bool isAsc = true)
        {
            if (isAsc)
                return Entities.Where(filter).OrderBy(sort).Skip((page - 1) * limit).Take(limit).AsQueryable();
            else
                return Entities.Where(filter).OrderByDescending(sort).Skip((page - 1) * limit).Take(limit).AsQueryable();
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual int Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Add(entity);

                return _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                return 0;
            }
        }
        
        public virtual int Insert(List<T> list)
        {
            try
            {
                foreach (var entity in list)
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    Entities.Add(entity);
                }
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                return 0;
            }
        }
        
        public virtual int Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                //Entities.AddOrUpdate(entity);

                return _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                return 0;
            }
        }
        
        public virtual int Update(List<T> list)
        {
            try
            {
                foreach (var entity in list)
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");
                    
                    Entities.AddOrUpdate(entity);
                }
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                return 0;
            }
        }
        
        public virtual int Delete(T entity)
        {
            try
            {

                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Remove(entity);

                return _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                return 0;
            }
        }
        
        public virtual int Delete(List<T> list)
        {
            try
            {
                foreach (var entity in list)
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    _context.Entry(entity).State = EntityState.Deleted;
                    Entities.Remove(entity);
                }
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                return 0;
            }
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled 
        /// (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        public int ExecuteSql(string sqlString)
        {
            return this._context.Database.ExecuteSqlCommand(sqlString);
        }

        public void Dispose()
        {
            if (this._context != null)
                this._context.Dispose();
        }
    }
}
