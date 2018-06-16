using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using log4net;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;
using WebApplication1.Helpers;

namespace WebApplication1.Data.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected BaseRepository(CustomContext context) => BaseContext = context;

        private DbSet<TEntity> EntitySet => BaseContext.Set<TEntity>();

        protected readonly ILog Log = CustomLogger.Log();

        public DbContext BaseContext { get; set; }

        public TEntity GetById(int id) =>
            EntitySet.AsNoTracking().FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        
        public List<TEntity> GetAll() => EntitySet.Where(x => !x.IsDeleted).AsNoTracking().ToList();

        public void InsertWithoutSave(TEntity entity) => EntitySet.Add(entity);

        public int Insert(TEntity entity)
        {
            entity.CreatedDate=DateTime.Now;
            EntitySet.Add(entity);
            return Save();
        }

        public int Update(TEntity entity)
        {
            BaseContext.Entry(entity).State = EntityState.Modified;
            return Save();
        }

        public void UpdateWithoutSave(TEntity entity) => EntitySet.Update(entity);

        public int DeleteById(int id)
        {
            var entity = EntitySet.Find(id);
            entity?.GetType().GetProperty("IsDeleted")?.SetValue(entity, true, null);
            return Save();
        }

        public void DeleteByIdWithoutSave(int id)
        {
            var entity = EntitySet.Find(id);
            entity?.GetType().GetProperty("IsDeleted")?.SetValue(entity, true, null);
        }

        public int Delete(TEntity entity) => DeleteById(entity.Id);

        public int Save()
        {
            try
            {
                return BaseContext.SaveChanges();
            }
            catch (DataException e)
            {
             Log.Error($"DBError Key: \"{e.Data.Keys}\", Error: \"{e.Data.Values}\"", e ); 
             throw;
            }
        }

        public List<TEntity> GetByExpression(Expression<Func<TEntity, bool>> func) => EntitySet.Where(x => !x.IsDeleted).Where(func).AsNoTracking().ToList();
        
    }
}
