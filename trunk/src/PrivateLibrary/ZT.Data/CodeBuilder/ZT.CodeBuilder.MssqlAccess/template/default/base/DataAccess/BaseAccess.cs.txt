using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using UtilityCollection.StringEx;

namespace ZT.DataAccess.MSSQL_Access
{
    /// <summary>
    /// 数据操作层的基类,主要封装了数据的增,删,改,查功能
    /// </summary>
    public abstract class BaseAccess
    {
        protected DBManager m_DBManager;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="l_DBManager">访问管理器</param>
        public BaseAccess(DBManager l_DBManager)
        {
            this.m_DBManager = l_DBManager;
        }

        /// <summary>
        /// 基本SELECT SQL语句
        /// </summary>
        protected string m_SelectSql
        {
            get
            {
                return "SELECT * FROM [" + m_TableName + "] ";
            }
        }

        /// <summary>
        /// 基本统计SELECT SQL语句
        /// </summary>
        protected string Total_SelectSql
        {
            get
            {
                return "SELECT count(*) FROM [" + m_TableName + "] ";
            }
        }

        /// <summary>
        /// 表名
        /// </summary>
        protected string m_TableName = "";

        /// <summary>
        /// 主键字段名
        /// </summary>
        protected string m_PKName = "";

        /// <summary>
        /// 当前活动数据库连接对象
        /// </summary>
        public OleDbConnection CurrentlyConnection
        {
            get
            {
                return this.m_DBManager.CurrentlyConnection;
            }
        }

        /// <summary>
        /// 当前活动事务对象
        /// </summary>
        public OleDbTransaction CurrentlyTransaction
        {
            get
            {
                return this.m_DBManager.CurrentlyTransaction;
            }
        }

        /// <summary>
        /// 数据库更新方法
        /// </summary>
        /// <param name="l_Ds">数据集</param>
        /// <returns>返回更新行数</returns>
        public int Update(DataSet obj_Ds)
        {


            OleDbCommand l_Command = new OleDbCommand(m_SelectSql, CurrentlyConnection);
            try
            {
                if (this.CurrentlyTransaction != null)
                {
                    l_Command.Transaction = CurrentlyTransaction;
                }
                OleDbDataAdapter obj_OleDbDataAdapter = new OleDbDataAdapter();
                obj_OleDbDataAdapter.SelectCommand = l_Command;
                OleDbCommandBuilder l_CommandBuilder = new OleDbCommandBuilder(obj_OleDbDataAdapter);
                try
                {
                    return obj_OleDbDataAdapter.Update(obj_Ds, m_TableName);
                }
                finally
                {
                    l_CommandBuilder.Dispose();
                    obj_OleDbDataAdapter.Dispose();
                }
            }
            finally
            {
                l_Command.Dispose();
            }
        }

        /// <summary>
        /// 查询全部数据方法
        /// </summary>
        /// <param name="l_Ds">返回数据集</param>
        /// <returns>返回查询行数</returns>
        public int QueryAll(ref DataSet obj_Ds)
        {

            OleDbDataAdapter obj_OleDbDataAdapter = new OleDbDataAdapter(m_SelectSql, CurrentlyConnection);
            if (this.CurrentlyTransaction != null)
            {
                obj_OleDbDataAdapter.SelectCommand.Transaction = CurrentlyTransaction;
            }
            OleDbCommandBuilder obj_CommandBuilder = new OleDbCommandBuilder(obj_OleDbDataAdapter);
            try
            {
                int inRet = obj_OleDbDataAdapter.Fill(obj_Ds, m_TableName);
                return inRet;
            }
            finally
            {
                obj_CommandBuilder.Dispose();
                obj_OleDbDataAdapter.Dispose();
            }
        }

        /// <summary>
        /// 获取条目数
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            OleDbCommand obj_Command = new OleDbCommand(Total_SelectSql);
            try
            {
                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyTransaction != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }

                object intResultCount = obj_Command.ExecuteScalar();
                if (intResultCount != null)
                {
                    return int.Parse(Convert.ToString(intResultCount));
                }
                else
                {
                    return 0;
                }
            }
            finally
            {
                obj_Command.Dispose();
            }
        }

        /// <summary>
        /// 获取条目数
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount(string strWhereSql)
        {
            OleDbCommand obj_Command = new OleDbCommand(Total_SelectSql + " where " + strWhereSql.FilterSql());
            try
            {
                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyTransaction != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }

                object intResultCount = obj_Command.ExecuteScalar();
                if (intResultCount != null)
                {
                    return int.Parse(Convert.ToString(intResultCount));
                }
                else
                {
                    return 0;
                }
            }
            finally
            {
                obj_Command.Dispose();
            }
        }

        /// <summary>
        /// 自定义获取条目数
        /// </summary>
        /// <returns></returns>
        public int GetRecordCountByCustomSql(string strCustomSql)
        {
            OleDbCommand obj_Command = new OleDbCommand(strCustomSql.FilterSql());
            try
            {
                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyTransaction != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }

                object intResultCount = obj_Command.ExecuteScalar();
                if (intResultCount != null)
                {
                    return int.Parse(Convert.ToString(intResultCount));
                }
                else
                {
                    return 0;
                }
            }
            finally
            {
                obj_Command.Dispose();
            }
        }

        /// <summary>
        /// 得到ID对应的数据
        /// </summary>
        /// <param name="l_Ds">返回数据集</param>
        /// <param name="ID">主键值</param>
        /// <returns>返回查询行数</returns>
        public int QueryByID(ref DataSet obj_Ds, int ID)
        {
            string strSql = m_SelectSql + " WHERE " + this.m_PKName + "=" + ID.ToString();

            OleDbCommand obj_Command = new OleDbCommand(strSql);
            try
            {
                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyTransaction != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }

                OleDbDataAdapter obj_OleDbDataAdapter = new OleDbDataAdapter();
                try
                {
                    obj_OleDbDataAdapter.SelectCommand = obj_Command;


                    return obj_OleDbDataAdapter.Fill(obj_Ds, m_TableName);
                }
                finally
                {
                    obj_OleDbDataAdapter.Dispose();
                }

            }
            finally
            {
                obj_Command.Dispose();
            }
        }
        /// <summary>
        /// 自定义sql语句。通用修改、新增
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>返回影响行数</returns>
        public int UpdateSection(string strSql)
        {
            OleDbCommand obj_Command = new OleDbCommand(strSql.FilterSql());
            try
            {
                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyConnection != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }
                return obj_Command.ExecuteNonQuery();
            }
            finally
            {
                obj_Command.Dispose();
            }
        }

        /// <summary>
        /// 用SQL查询数据
        /// </summary>
        /// <param name="l_Ds">返回数据集</param>
        /// <param name="s_Sql">SQL语句(WHERE 以后部分)</param>
        /// <returns>返回查询行数</returns>
        public int QueryBySql(ref DataSet obj_Ds, string s_Sql)
        {
            OleDbCommand obj_Command = new OleDbCommand(m_SelectSql + " WHERE " + s_Sql.FilterSql());
            try
            {
                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyTransaction != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }

                OleDbDataAdapter obj_OleDbDataAdapter = new OleDbDataAdapter();
                try
                {
                    obj_OleDbDataAdapter.SelectCommand = obj_Command;

                    int inRet = obj_OleDbDataAdapter.Fill(obj_Ds, m_TableName);

                    obj_OleDbDataAdapter.Dispose();
                    obj_Command.Dispose();

                    return inRet;
                }
                finally
                {
                    obj_OleDbDataAdapter.Dispose();
                }
            }
            finally
            {
                obj_Command.Dispose();
            }
        }

        /// <summary>
        /// 用SQL查询数据
        /// </summary>
        /// <param name="dataSet">返回数据集</param>
        /// <param name="s_Sql">整个SQL语句</param>
        /// <returns>返回查询行数</returns>
        public int QueryByQuerySql(ref DataSet dataSet, string s_Sql)
        {
            OleDbCommand obj_Command = new OleDbCommand(s_Sql.FilterSql());
            try
            {
                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyTransaction != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }

                OleDbDataAdapter obj_OleDbDataAdapter = new OleDbDataAdapter();
                try
                {
                    obj_OleDbDataAdapter.SelectCommand = obj_Command;

                    return obj_OleDbDataAdapter.Fill(dataSet, this.m_TableName);
                }
                finally
                {
                    obj_OleDbDataAdapter.Dispose();
                }
            }
            finally
            {
                obj_Command.Dispose();
            }
        }

        /// <summary>
        /// 删除数据库中ID对应数据
        /// </summary>
        /// <param name="ID">对象ID</param>
        /// <returns>删除行数</returns>
        public int Delete(int ID)
        {
            string str_DeleteSql = "";
            str_DeleteSql += "DELETE FROM " + m_TableName + " WHERE " + m_PKName + "=" + ID;

            OleDbCommand l_Command = new OleDbCommand(str_DeleteSql);
            try
            {
                if (CurrentlyConnection.State == ConnectionState.Closed)
                {
                    CurrentlyConnection.Open();
                }
                l_Command.Connection = CurrentlyConnection;
                if (CurrentlyTransaction != null)
                {
                    l_Command.Transaction = CurrentlyTransaction;
                }
                return l_Command.ExecuteNonQuery();
            }
            finally
            {
                l_Command.Dispose();
            }
        }
        //<summary>
        //删除数据库中ID对应数据
        //</summary>
        //<param name="ID">对象ID</param>
        //<returns>删除行数</returns>
        public int DeleteWhere(string whereSql)
        {
            string str_DeleteSql = "";
            str_DeleteSql += "DELETE FROM " + m_TableName + " WHERE " + whereSql.FilterSql();

            OleDbCommand l_Command = new OleDbCommand(str_DeleteSql);
            try
            {
                if (CurrentlyConnection.State == ConnectionState.Closed)
                {
                    CurrentlyConnection.Open();
                }
                l_Command.Connection = CurrentlyConnection;
                if (CurrentlyTransaction != null)
                {
                    l_Command.Transaction = CurrentlyTransaction;
                }
                return l_Command.ExecuteNonQuery();
            }
            finally
            {
                l_Command.Dispose();
            }
        }
     
        /// <summary>
        /// 获取唯一标识
        /// </summary>
        /// <returns></returns>
        public int GetIdentity()
        {

            OleDbCommand l_Command = new OleDbCommand("SELECT @@IDENTITY");
            try
            {
                if (CurrentlyConnection.State == ConnectionState.Closed)
                {
                    CurrentlyConnection.Open();
                }
                if (CurrentlyTransaction != null)
                {
                    l_Command.Transaction = CurrentlyTransaction;
                }
                l_Command.Connection = CurrentlyConnection;

                decimal decTmp = (decimal)l_Command.ExecuteScalar();

                return Decimal.ToInt32(decTmp);
            }
            finally
            {
                l_Command.Dispose();
            }
        }

        public int ExecuteSql(string strSql)
        {

            OleDbCommand objCmd = new OleDbCommand(strSql.FilterSql());
            try
            {
                objCmd.Connection = this.CurrentlyConnection;
                if (CurrentlyTransaction != null)
                {
                    objCmd.Transaction = CurrentlyTransaction;
                }
                return objCmd.ExecuteNonQuery();
            }
            finally
            {
                objCmd.Dispose();
            }

        }

        public DataTable ExecuteDataTable(string strSql)
        {

            OleDbCommand objCmd = new OleDbCommand(strSql.FilterSql());
            try
            {
                objCmd.Connection = this.CurrentlyConnection;
                if (CurrentlyTransaction != null)
                {
                    objCmd.Transaction = CurrentlyTransaction;
                }
                OleDbDataAdapter objAdapter = new OleDbDataAdapter(objCmd);
                DataTable objTable = new DataTable();
                try
                {
                    objAdapter.Fill(objTable);
                }
                finally
                {
                    objAdapter.Dispose();
                }

                return objTable;
            }
            finally
            {
                objCmd.Dispose();
            }

        }

        public int ExecuteDataRetDataSet(string strSql, ref DataSet objDataSet)
        {
            OleDbCommand objCmd = new OleDbCommand(strSql.FilterSql());
            try
            {
                objCmd.Connection = this.CurrentlyConnection;
                if (CurrentlyTransaction != null)
                {
                    objCmd.Transaction = CurrentlyTransaction;
                }
                OleDbDataAdapter objAdapter = new OleDbDataAdapter(objCmd);

                int intCount = 0;

                try
                {
                    intCount = objAdapter.Fill(objDataSet, this.m_TableName);
                }
                finally
                {
                    objAdapter.Dispose();
                }

                return intCount;
            }
            finally
            {
                objCmd.Dispose();
            }
        }
        public int ExecuteDataRetDataSet(string strSql, ref DataSet objDataSet, OleDbParameter objPara)
        {
            OleDbCommand objCmd = new OleDbCommand(strSql.FilterSql());
            objCmd.Parameters.Add(objPara);
            try
            {
                objCmd.Connection = this.CurrentlyConnection;
                if (CurrentlyTransaction != null)
                {
                    objCmd.Transaction = CurrentlyTransaction;
                }
                OleDbDataAdapter objAdapter = new OleDbDataAdapter(objCmd);

                int intCount = 0;

                try
                {
                    intCount = objAdapter.Fill(objDataSet, this.m_TableName);
                }
                finally
                {
                    objAdapter.Dispose();
                }

                return intCount;
            }
            finally
            {
                objCmd.Dispose();
            }
        }

        /// <summary>
        /// 获取下一个可用的种子值,并更新可用的种子值
        /// </summary>
        /// <returns></returns>
        public int GetNextSeed()
        {
            string strSql = "";
            strSql = "SELECT [tableID] FROM [Seed] WHERE [tableName]='" + this.m_TableName + "'";
            OleDbCommand obj_Command = new OleDbCommand(strSql);
            try
            {

                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyTransaction != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }

                object objValue = obj_Command.ExecuteScalar();

                //插入种子值
                if (objValue == null)
                {
                    strSql = "INSERT [Seed]([tableName],[tableID]) VALUES('" + this.m_TableName + "', 2) ";
                    if (ExecuteSql(strSql) > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return -3;
                    }
                }
                int n_value = 0;
                try
                {
                    n_value = int.Parse(objValue.ToString());
                }
                catch
                {
                    return -1;
                }

                //更新本月数据
                strSql = "UPDATE Seed SET [tableID]=[tableID]+1 WHERE [tableName]='" + this.m_TableName + "'";
                if (this.ExecuteSql(strSql) > 0)
                {
                }
                else
                {
                    return -2;
                }

                return n_value;
            }
            finally
            {
                obj_Command.Dispose();
            }
        }


        /// <summary>
        /// 得到最大ID
        /// </summary>
        /// <param name="strTable"></param>
        /// <returns></returns>
        public int GetMaxAutoID()
        {

            string strSql = "";
            strSql = "SELECT MAX(" + this.m_PKName + ") FROM " + this.m_TableName;
            OleDbCommand obj_Command = new OleDbCommand(strSql);
            try
            {
                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyTransaction != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }

                //this.m_DBManager.BeginTransaction();
                object objValue = obj_Command.ExecuteScalar();
                return int.Parse(objValue.ToString());
            }
            finally
            {
                obj_Command.Dispose();
            }
        }

        /// <summary>
        /// 获取当前一个唯一标识,插入记录后用此函数获取系统自动生成的ID值
        /// </summary>
        /// <returns></returns>
        public int GetCurIdentity()
        {

            string strSql = "";
            strSql = string.Format("SELECT IDENT_CURRENT('{0}')", m_TableName);
            OleDbCommand obj_Command = new OleDbCommand(strSql);
            try
            {
                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyTransaction != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }

                //this.m_DBManager.BeginTransaction();
                object objValue = obj_Command.ExecuteScalar();
                return int.Parse(objValue.ToString());
            }
            finally
            {
                obj_Command.Dispose();
            }
        }

        public DataTable GetTableColumnByName(string strName)
        {
            string strSql = "";
            strSql += "			SELECT syscolumns.name as columnName,systypes.name as columnType,dbo.sysproperties.value as columnproperties,syscolumns.length,syscolumns.isnullable  ";//,cast(sysproperties.value as varchar(200))+ '('+syscolumns.name+')' as TitleText
            strSql += " FROM syscolumns INNER JOIN ";
            strSql += "      systypes ON syscolumns.xtype = systypes.xtype ";
            strSql += "      INNER JOIN sysobjects  ";
            strSql += "      ON syscolumns.id=sysobjects.id join dbo.sysproperties on dbo.sysproperties.id=sysobjects.id and dbo.sysproperties.smallid=syscolumns.colid  ";
            strSql += " WHERE (syscolumns.id = ";
            strSql += "          (SELECT id ";
            strSql += "         FROM sysobjects ";
            strSql += "         WHERE (xtype = 'U') AND (name = '" + strName + "'))) AND (systypes.name IN ('bigint','binary','bit','char',  ";
            strSql += "      'datetime', 'decimal','float','image','int','money', 'nchar','ntext','numeric',  'nvarchar',  'real', 'smalldatetime',  ";
            strSql += "      'smallint', 'smallmoney', 'text','tinyint','varchar')) ";

            OleDbCommand objCmd = new OleDbCommand(strSql);
            try
            {
                objCmd.Connection = this.CurrentlyConnection;
                if (CurrentlyTransaction != null)
                {
                    objCmd.Transaction = CurrentlyTransaction;
                }
                OleDbDataAdapter objAdapter = new OleDbDataAdapter(objCmd);
                DataTable objTable = new DataTable();
                objAdapter.Fill(objTable);
                return objTable;
            }
            finally
            {
                objCmd.Dispose();
            }

        }

        public DataTable GetTableColumnByID(int intID)
        {
            string strSql = "";
            strSql += "			SELECT syscolumns.name as columnName,systypes.name as columnType,dbo.sysproperties.value as columnproperties";
            strSql += " FROM syscolumns INNER JOIN ";
            strSql += "      systypes ON syscolumns.xtype = systypes.xtype ";
            strSql += "      INNER JOIN sysobjects  ";
            strSql += "      ON syscolumns.id=sysobjects.id join dbo.sysproperties on dbo.sysproperties.id=sysobjects.id and dbo.sysproperties.smallid=syscolumns.colid  ";
            strSql += " WHERE (syscolumns.id = ";
            strSql += "          (SELECT id ";
            strSql += "         FROM sysobjects ";
            strSql += " INNER JOIN ";
            strSql += "      Section ON syscolumns.name = Section.chvTableName ";

            strSql += "         WHERE (xtype = 'U') AND (Section.intSTID = " + intID.ToString() + ") AND (Section.chvTableName IS NOT NULL))) AND (systypes.name IN ('char',   ";
            strSql += "      'datetime',    'nchar',  'nvarchar',  'smalldatetime',  ";
            strSql += "        'varchar','text','ntext')) ";

            OleDbCommand objCmd = new OleDbCommand(strSql);
            try
            {
                objCmd.Connection = this.CurrentlyConnection;
                if (CurrentlyTransaction != null)
                {
                    objCmd.Transaction = CurrentlyTransaction;
                }
                OleDbDataAdapter objAdapter = new OleDbDataAdapter(objCmd);
                DataTable objTable = new DataTable();
                if (objAdapter.Fill(objTable) < 1)
                {
                    return null;
                }
                return objTable;
            }
            finally
            {
                objCmd.Dispose();
            }

        }

        /// <summary>
        /// 根据参数条件，查询数据
        /// </summary>
        /// <param name="objDataSet">返回数据集</param>
        /// <param name="strWhere">SQL语句(WHERE 以后部分)</param>
        /// <returns>返回查询行数</returns>
        public int QueryByWhereParam(ref DataSet objDataSet, string strWhere, System.Data.OleDb.OleDbParameter objParam)
        {

            OleDbCommand objCommand = new OleDbCommand(this.m_SelectSql + " WHERE " + strWhere.FilterSql());
            objCommand.Parameters.Add(objParam);
            objCommand.Connection = this.CurrentlyConnection;
            if (this.CurrentlyTransaction != null)
            {
                objCommand.Transaction = CurrentlyTransaction;
            }

            OleDbDataAdapter objOleDbDataAdapter = new OleDbDataAdapter();
            objOleDbDataAdapter.SelectCommand = objCommand;

            return objOleDbDataAdapter.Fill(objDataSet, m_TableName);

        }

        /// <summary>
        /// 执行存储过程并返回DataSet
        /// </summary>
        /// <param name="strProcedureName">存储过程名称</param>
        /// <param name="strPar">参数</param>
        /// <returns>影响的行数</returns>
        public int ExecuteByProcedure(ref DataSet objDataSet, string strProcedureName, System.Collections.Hashtable objHashtable)
        {
            System.Data.OleDb.OleDbCommand objOleDbCommand = new OleDbCommand();
            objOleDbCommand.Connection = this.CurrentlyConnection;
            objOleDbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objOleDbCommand.CommandText = strProcedureName;
            if (objHashtable != null)
            {
                foreach (object key in objHashtable.Keys)
                {
                    objOleDbCommand.Parameters.Add(new OleDbParameter(key.ToString(), objHashtable[key].ToString()));
                }
            }
            OleDbDataAdapter objOleDbDataAdapter = new OleDbDataAdapter();
            objOleDbDataAdapter.SelectCommand = objOleDbCommand;
            return objOleDbDataAdapter.Fill(objDataSet);
        }
        /// <summary>
        /// 操作系统或浏览器情况
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>操作系统或浏览器情况数据集</returns>

        public static DataSet ExecuteSqlForDataSet(string strSql)
        {
            DBManager objDB = new DBManager();
            OleDbCommand objCmd = new OleDbCommand(strSql.FilterSql());
            try
            {
                objCmd.Connection = objDB.CurrentlyConnection;
                if (objDB.CurrentlyTransaction != null)
                {
                    objCmd.Transaction = objDB.CurrentlyTransaction;
                }
                OleDbDataAdapter objAdapter = new OleDbDataAdapter(objCmd);
                DataSet ds = new DataSet();
                objAdapter.Fill(ds);
                return ds;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                objCmd.Dispose();
            }
        }

        public int QueryByWS_ID(ref DataSet obj_Ds, int WS_ID)
        {
            OleDbCommand obj_Command = new OleDbCommand(m_SelectSql + " WHERE websiteID=" + WS_ID.ToString());
            try
            {
                obj_Command.Connection = CurrentlyConnection;
                if (this.CurrentlyTransaction != null)
                {
                    obj_Command.Transaction = CurrentlyTransaction;
                }

                OleDbDataAdapter obj_OleDbDataAdapter = new OleDbDataAdapter();
                try
                {
                    obj_OleDbDataAdapter.SelectCommand = obj_Command;
                    return obj_OleDbDataAdapter.Fill(obj_Ds, m_TableName);
                }
                finally
                {
                    obj_OleDbDataAdapter.Dispose();
                }

            }
            finally
            {
                obj_Command.Dispose();
            }
        }
    }
}
