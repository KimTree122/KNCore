using KNCore.IDAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KNCore.DAL
{
    public class CurdDAL<T> : MysqlBaseDbContext<T>, ICurdDAL<T> where T : class, new()
    {
        public DbSet<T> CurrentDb { get { return new DbSet<T>(Db); } }
        
        public object DbContext { get => CurrentDb; }

        public int Add(T entity)
        {
            int id = CurrentDb.AsInsertable(entity).ExecuteReturnIdentity();
            return id;
        }

        public bool Del(T entity)
        {
            return CurrentDb.Delete(entity);
        }

        public int DelRange(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDb.AsDeleteable().Where(whereLambda).ExecuteCommand();
        }

        public List<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDb.GetList(whereLambda);
        }

        public T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDb.GetSingle(whereLambda);
        }

        public List<T> GetPageEntities(Expression<Func<T, bool>> whereLambda, Expression<Func<T, object>> order, bool isAsc, int pageSize, int pageIndex)
        {
            SqlSugar.PageModel page = new SqlSugar.PageModel() { PageIndex= pageIndex, PageSize = pageSize };
            SqlSugar.OrderByType oby = isAsc ? SqlSugar.OrderByType.Asc : SqlSugar.OrderByType.Desc;
            return CurrentDb.GetPageList(whereLambda,page,order,oby);
        }

        public bool Update(T entity)
        {
            return CurrentDb.Update(entity);
        }

        public int UpdateRange(List<T> entities)
        {
            return CurrentDb.AsUpdateable(entities).ExecuteCommand();
        }

        public int UpdateRange(List<T> entities, Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDb.AsUpdateable(entities).Where(whereLambda).ExecuteCommand();
        }

    }
}
