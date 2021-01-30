using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Swt.Public.Helper.T4Manager
{
    #region 数据类型参照
    public class SqlServerDbTypeMap
    {

        public static string TitleToUpper(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            char[] s = str.ToCharArray();
            char c = s[0];

            if ('a' <= c && c <= 'z')
                c = (char)(c & ~0x20);

            s[0] = c;

            return new string(s);
        }

        public static string MapCsharpType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype)) return dbtype;
            dbtype = dbtype.ToLower();
            string csharpType = "object";
            switch (dbtype)
            {
                case "bigint": csharpType = "long"; break;
                case "binary": csharpType = "byte[]"; break;
                case "bit": csharpType = "bool"; break;
                case "char": csharpType = "string"; break;
                case "date": csharpType = "DateTime"; break;
                case "datetime": csharpType = "DateTime"; break;
                case "datetime2": csharpType = "DateTime"; break;
                case "datetimeoffset": csharpType = "DateTimeOffset"; break;
                case "decimal": csharpType = "decimal"; break;
                case "float": csharpType = "double"; break;
                case "image": csharpType = "byte[]"; break;
                case "int": csharpType = "int"; break;
                case "money": csharpType = "decimal"; break;
                case "nchar": csharpType = "string"; break;
                case "ntext": csharpType = "string"; break;
                case "numeric": csharpType = "decimal"; break;
                case "nvarchar": csharpType = "string"; break;
                case "real": csharpType = "Single"; break;
                case "smalldatetime": csharpType = "DateTime"; break;
                case "smallint": csharpType = "short"; break;
                case "smallmoney": csharpType = "decimal"; break;
                case "sql_variant": csharpType = "object"; break;
                case "sysname": csharpType = "object"; break;
                case "text": csharpType = "string"; break;
                case "time": csharpType = "TimeSpan"; break;
                case "timestamp": csharpType = "byte[]"; break;
                case "tinyint": csharpType = "byte"; break;
                case "uniqueidentifier": csharpType = "Guid"; break;
                case "varbinary": csharpType = "byte[]"; break;
                case "varchar": csharpType = "string"; break;
                case "xml": csharpType = "string"; break;
                default: csharpType = "object"; break;
            }
            return csharpType;
        }

        public static Type MapCommonType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype)) return Type.Missing.GetType();
            dbtype = dbtype.ToLower();
            Type commonType = typeof(object);
            switch (dbtype)
            {
                case "bigint": commonType = typeof(long); break;
                case "binary": commonType = typeof(byte[]); break;
                case "bit": commonType = typeof(bool); break;
                case "char": commonType = typeof(string); break;
                case "date": commonType = typeof(DateTime); break;
                case "datetime": commonType = typeof(DateTime); break;
                case "datetime2": commonType = typeof(DateTime); break;
                case "datetimeoffset": commonType = typeof(DateTimeOffset); break;
                case "decimal": commonType = typeof(decimal); break;
                case "float": commonType = typeof(double); break;
                case "image": commonType = typeof(byte[]); break;
                case "int": commonType = typeof(int); break;
                case "money": commonType = typeof(decimal); break;
                case "nchar": commonType = typeof(string); break;
                case "ntext": commonType = typeof(string); break;
                case "numeric": commonType = typeof(decimal); break;
                case "nvarchar": commonType = typeof(string); break;
                case "real": commonType = typeof(Single); break;
                case "smalldatetime": commonType = typeof(DateTime); break;
                case "smallint": commonType = typeof(short); break;
                case "smallmoney": commonType = typeof(decimal); break;
                case "sql_variant": commonType = typeof(object); break;
                case "sysname": commonType = typeof(object); break;
                case "text": commonType = typeof(string); break;
                case "time": commonType = typeof(TimeSpan); break;
                case "timestamp": commonType = typeof(byte[]); break;
                case "tinyint": commonType = typeof(byte); break;
                case "uniqueidentifier": commonType = typeof(Guid); break;
                case "varbinary": commonType = typeof(byte[]); break;
                case "varchar": commonType = typeof(string); break;
                case "xml": commonType = typeof(string); break;
                default: commonType = typeof(object); break;
            }
            return commonType;
        }
    }
    #endregion

    #region 表字段映射实体

    public class TableColumns
    {
        public object TABLE_NAME { get; set; }

        public object TABLE_ID { get; set; }

        public object COLUMN_NAME { get; set; }

        public object DATA_TYPE { get; set; }

        public object CHARACTER_MAXIMUM_LENGTH { get; set; }

        public object COLUMN_DESCRIPTION { get; set; }

        public object COLUMN_DEFAULT { get; set; }

        public object IS_NULLABLE { get; set; }
    }

    public class ColumnsKey
    {
        public object TABLE_NAME { get; set; }
        public object COLUMN_NAME { get; set; }
        public object ORDINAL_POSITION { get; set; }

    }

    #endregion

    // 模板帮助库
    public class MssqlHelper
    {
        public static string connStr { get; set; }

        public static string Project = "Swt.Public.";
        public static string bll = "BLL";
        public static string ibll = "IBLL";
        public static string dal = "DAL";
        public static string idal = "IDAL";
        public static string Model = "Model";
        public static string API = "TestAPI";
        //public static string TargetPathdir = "Public";

        private static string blldir = Project + bll;
        private static string iblldir = Project + ibll;
        private static string daldir = Project + dal;
        private static string idaldir = Project + idal;
        private static string Modeldir = Project + Model;
        private static string APIdir = Project + API;


        // 执行Sql语句
        public static List<T> RunSql<T>(string sql, object whereObj = null)
        {
            using (SqlSugarClient dbClient = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connStr,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
            }))
            {
                return dbClient.Ado.SqlQuery<T>(sql);
                //return dbClient.SqlQueryable<DataTableMap>(sql).ToList();
            }
        }

        ////// 获取所有的表，包括视图
        //public static List<string> GetTables()
        //{
        //    return RunSql<string>("select name from sysobjects where xtype in('U','V')");
        //}

        // 获取表结构信息
        public static List<TableColumns> GetTableColumns(string tableName)
        {
            string sql = @"SELECT  Sysobjects.name AS TABLE_NAME ,
                syscolumns.Id  AS TABLE_ID,
                syscolumns.name AS COLUMN_NAME ,
                systypes.name AS DATA_TYPE ,
                syscolumns.length AS CHARACTER_MAXIMUM_LENGTH ,
                sys.extended_properties.[value] AS COLUMN_DESCRIPTION ,
                syscomments.text AS COLUMN_DEFAULT ,
                syscolumns.isnullable AS IS_NULLABLE
                FROM    syscolumns
                INNER JOIN systypes ON syscolumns.xtype = systypes.xusertype
                LEFT JOIN sysobjects ON syscolumns.id = sysobjects.id
                LEFT OUTER JOIN sys.extended_properties ON ( sys.extended_properties.minor_id = syscolumns.colid
                AND sys.extended_properties.major_id = syscolumns.id
                )
                LEFT OUTER JOIN syscomments ON syscolumns.cdefault = syscomments.id
                WHERE   syscolumns.id IN ( SELECT   id
                FROM     SYSOBJECTS
                WHERE    xtype in( 'U','V') )
                AND ( systypes.name <> 'sysname' ) AND Sysobjects.name='" + tableName + "'  ORDER BY syscolumns.colid";

            return RunSql<TableColumns>(sql);
        }

        public static List<ColumnsKey> GetColumnKeys(string tableName)
        {
            string sql = string.Format("SELECT TABLE_NAME, COLUMN_NAME, ORDINAL_POSITION FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME='{0}'", tableName);
            return RunSql<ColumnsKey>(sql);
        }

        // 判断目录是否存在
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public static void CreatDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        // 判断文件是否存在
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        // 写入文件
        public static void CreateFile(string filePath, string text, Encoding encoding)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (IsExistFile(filePath))
                {
                    File.Delete(filePath);
                }
                //创建文件
                FileInfo file = new FileInfo(filePath);
                using (FileStream stream = file.Create())
                {
                    using (StreamWriter writer = new StreamWriter(stream, encoding))
                    {
                        writer.Write(text);
                        writer.Flush();
                    }
                }
            }
            catch
            {
            }
        }


        public static void CreatCSFile(string solutionDir, string moudleDir, string tableName)
        {
            CreateEntity(solutionDir, moudleDir, tableName);
            CreateIDAL(solutionDir, moudleDir, tableName);
            CreateDAL(solutionDir, moudleDir, tableName);
            CreateIBLL(solutionDir, moudleDir, tableName);
            CreateBLL(solutionDir, moudleDir, tableName);
        }

        // 生成实体类文件，会覆盖原文件
        public static void CreateEntity(string solutionDir, string moudleDir, string tableName)
        {
            if (string.IsNullOrEmpty(tableName) && string.IsNullOrWhiteSpace(tableName))
            {
                return;
            }
            // 生成字段
            List<TableColumns> tableColumns = GetTableColumns(tableName);
            List<ColumnsKey> columnsKeys = GetColumnKeys(tableName);

            string path = solutionDir + "\\" + Modeldir + "\\" + moudleDir+Model + "\\";
            string name = tableName + "Entity.cs";

            // 不覆盖
            if (IsExistFile(path + name))
            {
                return;
            }

            if (!IsExistDirectory(path))
            {
                CreatDirectory(path);
            }

            string codeStr = string.Empty;

            // 描述信息
            //codeStr = "/*!\r\n";

            //codeStr += "*/\r\n\r\n";

            // 引用
            codeStr += "using System;\r\n";
            codeStr += "using SqlSugar;\r\n\r\n";


            // 命名空间声明
            codeStr += "namespace " +Modeldir+ "."+ moudleDir+ "\r\n";
            codeStr += "{\r\n";

            // 序列化
            codeStr += "\t[SugarTable(\"" + tableName + "\")] //别名处理\r\n";

            // 生成类声明
            codeStr += "\tpublic partial class " + tableName + "Entity\r\n";
            codeStr += "\t{\r\n";


            foreach (TableColumns tableColumn in tableColumns)
            {
                // 生成字段描述
                codeStr += "\t\t/// <summary>\r\n";
                codeStr += "\t\t/// 描述：" + tableColumn.COLUMN_DESCRIPTION + "\r\n";
                codeStr += "\t\t/// 可空：" + (Convert.ToInt32(tableColumn.IS_NULLABLE) == 0 ? "不为空" : "空") + "\r\n";
                codeStr += "\t\t/// 默认值：" + tableColumn.COLUMN_DEFAULT + "\r\n";
                codeStr += "\t\t/// </summary>\r\n";

                // 序列化
                //codeStr += "\t\t[DataMember]\r\n";
                // 生成属性
                //[SugarColumn(IsPrimaryKey = true)]

                if (columnsKeys.Exists(a => a.COLUMN_NAME.Equals(tableColumn.COLUMN_NAME)))
                {
                    codeStr += "\t\t[SugarColumn(IsPrimaryKey = true)]\r\n";
                }
                codeStr += "\t\tpublic " + (((Convert.ToInt32(tableColumn.IS_NULLABLE) == 1) && (SqlServerDbTypeMap.MapCsharpType(tableColumn.DATA_TYPE.ToString()) != "string")) ? "" : "") + SqlServerDbTypeMap.MapCsharpType(tableColumn.DATA_TYPE.ToString()) + (((Convert.ToInt32(tableColumn.IS_NULLABLE) == 1) && (SqlServerDbTypeMap.MapCsharpType(tableColumn.DATA_TYPE.ToString()) != "string")) ? "?" : "") + " " + SqlServerDbTypeMap.TitleToUpper(tableColumn.COLUMN_NAME.ToString()) + " { get; set; }\r\n\r\n";
            }

            // 生成类声明结束符
            codeStr += "\t}\r\n";
            codeStr += "}";

            // 保存文件
            CreateFile(path + name, codeStr, Encoding.UTF8);


        }

        public static void CreateIDAL(string solutionDir, string moudleDir, string tableName)
        {
            string path = solutionDir + "\\" + idaldir + "\\" + moudleDir + idal + "\\";
            string usingname = Modeldir + "." + moudleDir+Model;
            string name = "I"+tableName + "IDAL.cs";
            // 不覆盖
            if (IsExistFile(path + name))
            {
                return;
            }

            if (!IsExistDirectory(path))
            {
                CreatDirectory(path);
            }

            string codeStr = string.Empty;
            codeStr += "using " + usingname + "; \r\n";
            codeStr += "\r\n";
            // 命名空间声明
            codeStr += "namespace " + idaldir+ "." + moudleDir+idal + "\r\n";
            
            codeStr += "{\r\n";

            codeStr += "\tpublic interface I" + tableName + "IDAL : ICurdDAL<"+tableName+ "Entity> { }\r\n";

            // 生成类声明结束符
            codeStr += "}";
            CreateFile(path + name, codeStr, Encoding.UTF8);

        }

        public static void CreateDAL(string solutionDir, string moudleDir, string tableName)
        {
            string path = solutionDir + "\\" + daldir + "\\" + moudleDir+dal + "\\";
            string usingmodel = Modeldir + "." + moudleDir+Model;
            string usingidal = idaldir + "." + moudleDir + idal;
            string name =  tableName + "DAL.cs";
            // 不覆盖
            if (IsExistFile(path + name))
            {
                return;
            }

            if (!IsExistDirectory(path))
            {
                CreatDirectory(path);
            }

            string codeStr = string.Empty;
            codeStr += "using " + usingmodel + "; \r\n";
            codeStr += "using " + usingidal + "; \r\n";
            codeStr += "\r\n";
            // 命名空间声明
            codeStr += "namespace " + daldir + "." + moudleDir+dal + "\r\n";


            codeStr += "{\r\n";

            codeStr += "\tpublic class " + tableName + "DAL : CurdDAL<" + tableName + "Entity>,I"+tableName+idal+" { }\r\n";

            // 生成类声明结束符
            codeStr += "}";
            CreateFile(path + name, codeStr, Encoding.UTF8);

        }

        public static void CreateIBLL(string solutionDir, string moudleDir, string tableName)
        {
            string path = solutionDir + "\\" + iblldir + "\\" + moudleDir + ibll + "\\";
            string usingmodel = Modeldir + "." + moudleDir + Model;
            string name = "I" + tableName + "IBLL.cs";
            // 不覆盖
            if (IsExistFile(path + name))
            {
                return;
            }

            if (!IsExistDirectory(path))
            {
                CreatDirectory(path);
            }

            string codeStr = string.Empty;
            codeStr += "using " + usingmodel + "; \r\n";
            codeStr += "\r\n";
            // 命名空间声明
            codeStr += "namespace " + iblldir + "." + moudleDir + ibll + "\r\n";

            codeStr += "{\r\n";

            codeStr += "\tpublic interface I" + tableName + "IBLL : ICurdBLL<" + tableName + "Entity> \r\n";
            codeStr += "\t{ \r\n";
            codeStr += "\t\tvoid SetCurrentDal();\r\n";

            codeStr += "\t} \r\n";
            // 生成类声明结束符
            codeStr += "}";
            CreateFile(path + name, codeStr, Encoding.UTF8);

        }

        public static void CreateBLL(string solutionDir, string moudleDir, string tableName)
        {
            string path = solutionDir + "\\" + blldir + "\\" + moudleDir + bll + "\\";
            string usingmodel = Modeldir + "." + moudleDir + Model;
            string usingdal = daldir + "." + moudleDir + dal;
            string usingibll = iblldir + "." + moudleDir + ibll;
            string name =  tableName + "BLL.cs";
            // 不覆盖
            if (IsExistFile(path + name))
            {
                return;
            }

            if (!IsExistDirectory(path))
            {
                CreatDirectory(path);
            }

            string codeStr = string.Empty;
            codeStr += "using " + usingmodel + "; \r\n";
            codeStr += "using " + usingdal + "; \r\n";
            codeStr += "using " + usingibll + "; \r\n";

            codeStr += "\r\n";
            // 命名空间声明
            codeStr += "namespace " + blldir + "." + moudleDir + bll + "\r\n";

            codeStr += "{\r\n";

            codeStr += "\tpublic class " + tableName + "BLL : CurdBLL<" + tableName + "Entity>,I"+tableName+ibll+" \r\n";
            codeStr += "\t{ \r\n";
            codeStr += "\t\tpublic override void SetCurrentDal()\r\n";
            codeStr += "\t\t{ \r\n";
            codeStr += "\t\t\t CurrentDAL = new "+tableName+"DAL(); \r\n";
            codeStr += "\t\t} \r\n";
            codeStr += "\t} \r\n";
            // 生成类声明结束符
            codeStr += "}";
            CreateFile(path + name, codeStr, Encoding.UTF8);

        }

    }
}
