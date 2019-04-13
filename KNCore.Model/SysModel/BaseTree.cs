using Dapper.Contrib.Extensions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.Model.SysModel
{

    public class BaseTree
    {
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public int? FatherID { get; set; }

        public string NodeName { get; set; }
    }

    [SugarTable("authority")]
    public class Authority:BaseTree
    {
        public string Path { get; set; }
        public int? AuthTypeID { get; set; }
        public string AuthTypeName { get; set; }
        public string ImagePath { get; set; }
        public int? Order { get; set; }
        public string SysPort { get; set; }
    }
}
