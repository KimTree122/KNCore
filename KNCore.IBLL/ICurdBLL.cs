using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KNCore.IBLL
{
    public interface ICurdBLL<T> where T :class,new()
    {
        T GetEntity(Expression<Func<T, bool>> whereLamdda);

        List<T> GetEntities(Expression<Func<T, bool>> whereLambda);

        List<T> GetPageEntityes(Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, object>> orderbyLambda, int pageSize, int pageIndex, bool isAsc);

        int Add(T entity);

        bool Update(T entity);

        bool Del(T entity);

        int DelRange(Expression<Func<T, bool>> whereLambda);

        int UpdateRange(List<T> entities, Expression<Func<T, bool>> whereLambda);

        int UpdateRange(List<T> entities);
    }
}
