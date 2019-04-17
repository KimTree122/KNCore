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

    public class Authority:BaseTree
    {
        public string Path { get; set; }
        public int? AuthTypeID { get; set; }
        public string AuthTypeName { get; set; }
        public string ImagePath { get; set; }
        public int? Order { get; set; }
        public string SysPort { get; set; }
    }

    public class WebTreeNode
    {
        public int id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public bool Checked { get; set; }
        public string state { get; set; }
        public Dictionary<string, string> attributes { get; set; }
        public object children { get; set; }
    }

}
