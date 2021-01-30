using System;
using SqlSugar;

namespace Swt.Public.Model.Public
{
	[SugarTable("Sys_User")] //别名处理
	public partial class Sys_UserEntity
	{
		/// <summary>
		/// 描述：
		/// 可空：不为空
		/// 默认值：(newid())
		/// </summary>
		[SugarColumn(IsPrimaryKey = true)]
		public Guid User_ID { get; set; }

		/// <summary>
		/// 描述：
		/// 可空：不为空
		/// 默认值：
		/// </summary>
		[SugarColumn(IsPrimaryKey = true)]
		public string User_Name { get; set; }

		/// <summary>
		/// 描述：
		/// 可空：空
		/// 默认值：
		/// </summary>
		public string User_LoginName { get; set; }

		/// <summary>
		/// 描述：
		/// 可空：空
		/// 默认值：
		/// </summary>
		public string User_Pwd { get; set; }

		/// <summary>
		/// 描述：
		/// 可空：空
		/// 默认值：
		/// </summary>
		public string User_Email { get; set; }

		/// <summary>
		/// 描述：
		/// 可空：空
		/// 默认值：
		/// </summary>
		public int? User_IsDelete { get; set; }

		/// <summary>
		/// 描述：
		/// 可空：空
		/// 默认值：(getdate())
		/// </summary>
		public DateTime? User_CreateTime { get; set; }

	}
}