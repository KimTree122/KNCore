using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KNCore.IDAL
{
    public interface ICurdDAL<T> where T :class,new()
    {
        object DbContext { get; }

        //Creat
        int Add(T entity);
        //Update
        bool Update(T entity);
        int UpdateRange(List<T> entities);
        int UpdateRange(List<T> entities, Expression<Func<T, bool>> whereLambda);
        //Read
        T GetEntity(Expression<Func<T, bool>> whereLambda);
        List<T> GetEntities(Expression<Func<T, bool>> whereLambda);
        List<T> GetPageEntities(Expression<Func<T, bool>> whereLambda,Expression<Func<T,object>> order,bool isAsc, int pageSize, int pageIndex);
        //Delete
        bool Del(T entity);
        int DelRange(Expression<Func<T, bool>> whereLambda);

    }
}
