﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace KNCore.Model.CommModel
{
    ///<summary>
    ///用户基础登陆信息
    ///</summary>
    [Table("LoginUser")]
    [SugarTable("LoginUser")] 
    public partial class LoginUser
    {
        public LoginUser()
        {


        }

        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Desc:用户姓名
        /// Default:
        /// Nullable:False
        /// </summary>  
        [SugarColumn(ColumnName = "nameuser")]
        public string UserName { get; set; }

        /// <summary>
        /// Desc:用户账号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserCode { get; set; }

        /// <summary>
        /// Desc:登陆密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LogPWD { get; set; }

        /// <summary>
        /// Desc:电子邮件地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Email { get; set; }

        /// <summary>
        /// Desc:角色列表
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RoleList { get; set; }

        /// <summary>
        /// Desc:是否删除
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool? Del { get; set; }

    }

}
