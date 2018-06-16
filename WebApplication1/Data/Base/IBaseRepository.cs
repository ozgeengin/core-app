using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data.Base
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        DbContext BaseContext { get; }
        int Save();
        TEntity GetById(int id);
        List<TEntity> GetAll();
        int Insert(TEntity entity);
        void InsertWithoutSave(TEntity entity);
        int Update(TEntity entity);
        void UpdateWithoutSave(TEntity entity);
        int Delete(TEntity entity);
        int DeleteById(int id);
        void DeleteByIdWithoutSave(int id);
        List<TEntity> GetByExpression(Expression<Func<TEntity, bool>> func);
    }
}
