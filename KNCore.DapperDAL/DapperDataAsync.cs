using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace KNCore.DapperDAL
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    /// abstract partial
    public abstract partial class DapperDataAsync
    {
         public IDbConnection con = DapperConfig.GetSqlConnection();

        #region 查询系
        /// <summary>
        /// 获取Model-Key为int类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(int id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = DapperConfig.GetSqlConnection())
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }
        /// <summary>
        /// 获取Model-Key为long类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(long id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = DapperConfig.GetSqlConnection())
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }
        /// <summary>
        /// 获取Model-Key为Guid类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(System.Guid id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = DapperConfig.GetSqlConnection())
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }
        /// <summary>
        /// 获取Model-Key为string类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(string id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = DapperConfig.GetSqlConnection())
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }
        /// <summary>
        /// 获取Model集合（没有Where条件）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, new()
        {
            using (var conn = DapperConfig.GetSqlConnection())
            {
                return await conn.GetAllAsync<T>();
            }
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 插入一个Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns></returns>
        public static async Task<int> InsertAsync<T>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = DapperConfig.GetSqlConnection())
            {
                return await conn.InsertAsync<T>(model, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 更新一个Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> UpdateAsync<T>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = DapperConfig.GetSqlConnection())
            {
                bool b = await conn.UpdateAsync<T>(model, transaction, commandTimeout);
                if (b) { return model; }
                else { return null; }
            }
        }

        /// <summary>
        /// 删除一个Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteAsync<T>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = DapperConfig.GetSqlConnection())
            {
                bool b = await conn.DeleteAsync<T>(model, transaction, commandTimeout);
                return b;
            }
        }

        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询(为什么不用out，请参考：http://www.cnblogs.com/dunitian/p/5556909.html)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="p">动态参数</param>
        /// <param name="sqlTotal">total语句</param>
        /// <param name="p2">Total动态参数</param>
        /// <returns></returns>
        public static async Task<string> PageLoadAsync<T>(string sql, object p = null, string sqlTotal = "", object p2 = null)
        {
            //var rows = await QueryAsync<T>(sql.ToString(), p);
            //var total = rows.Count();
            //if (!sqlTotal.IsNullOrWhiteSpace()) { total = await ExecuteScalarAsync<int>(sqlTotal, p2); }
            //return new { rows = rows, total = total }.ObjectToJson();

            using (var conn = DapperConfig.GetSqlConnection())
            {
                var rows = await conn.QueryAsync<T>(sql, p);
                var total = rows.AsList().Count;
                if (!string.IsNullOrWhiteSpace(sqlTotal))
                {
                    total = await conn.ExecuteScalarAsync<int>(sqlTotal, p2);
                }
                return "";
            }
        }
        #endregion
    }

    public class BaseUserRep : DapperDataAsync
    {
        public void test()
        {
            
        }
    }
}
