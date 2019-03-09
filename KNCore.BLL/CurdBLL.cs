using KNCore.DAL;
using KNCore.IDAL;
using KNCore.Model.CommModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KNCore.BLL
{
    public abstract class CurdBLL<T> where T:class,new()
    {
        public ICurdDAL<T> CurrentDAL { get; set; }

        public CurdBLL()
        {
            SetCurrentDal();
        }

        public void Dispose()
        {
            Console.WriteLine("clean");
        }

        public abstract void SetCurrentDal();

        public int Add(T entity)
        {            
            return CurrentDAL.Add(entity);
        }

        public bool Del(T entity)
        {
            return CurrentDAL.Del(entity);
        }

        public List<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDAL.GetEntities(whereLambda);
        }

        public T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDAL.GetEntity(whereLambda);
        }

        public List<T> GetPageEntityes(Expression<Func<T, bool>> whereLambda, Expression<Func<T, object>> orderbyLambda, int pageSize, int pageIndex, bool isAsc)
        {
            return CurrentDAL.GetPageEntities(whereLambda, orderbyLambda, isAsc, pageSize, pageIndex);
        }

        public bool Update(T entity)
        {
            return CurrentDAL.Update(entity);
        }

        public int UpdateRange(List<T> entities)
        {
            return CurrentDAL.UpdateRange(entities);
        }

        public int DelRange(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDAL.DelRange(whereLambda);
        }

        public int UpdateRange(List<T> entities, Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDAL.UpdateRange(entities, whereLambda);
        }

    }
}
