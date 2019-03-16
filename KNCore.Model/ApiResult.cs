using System;

namespace KNCore.Model
{
    /// <summary>
    /// 后台统一返回数据
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// 数据数量
        /// </summary>
        public int rows { get; set; } = 0;

        /// <summary>
        /// 附注
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// 主数据
        /// </summary>
        public object Main { get; set; }

        /// <summary>
        /// 列表数据
        /// </summary>
        public object Item { get; set; }

    }

    /// <summary>
    /// 列表数据格式类
    /// </summary>
    public class DataGridJson
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 列表明细
        /// </summary>
        public object rows { get; set; }

        /// <summary>
        /// 底边栏
        /// </summary>
        public object footer { get; set; }

        /// <summary>
        /// 附注
        /// </summary>
        public string msg { get; set; }
        
    }

}
