using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace ZT.BusinessLogic.MSSQL_ACCESS.ClassFactory
{
    /// <summary>
    /// 此类无效,可以不用继承,但是必须有同名方法
    /// </summary>
    public class BaseFactory : IDisposable
    {
        /// <summary>
        /// 构造方法(不需要事务支持时使用)
        /// </summary>
        public BaseFactory()
        {
        }
        public void Dispose()
        {
            this.m_Manager.Dispose();
            GC.SuppressFinalize(this);
        }
        protected void Finalize()//override
        {
            Dispose();
            //base.Finalize();
        }
        /// <summary>
        /// 构造方法(需要事务支持时使用)
        /// </summary>
        /// <param name="l_DataAccess.DBManager">数据访问管理器</param>
        public BaseFactory(DataAccess.MSSQL_Access.DBManager l_DBManager)
        {
            objManager = l_DBManager;
        }


        private DataAccess.MSSQL_Access.DBManager objManager = null;

        /// <summary>
        /// 数据访问管理器
        /// </summary>
        protected DataAccess.MSSQL_Access.DBManager m_Manager
        {
            get
            {
                if (objManager == null)
                {
                    objManager = new DataAccess.MSSQL_Access.DBManager();
                }
                return objManager;
            }
            set
            {
                objManager = value;
            }
        }

        #region 公用的方法
        /// <summary>
        /// 根据条件（页索引、页大小、过滤条件、视图名称、排序条件）返回数据集
        /// </summary>
        /// <param name="nPageIndex">起始页,0为起始索引</param>
        /// <param name="nPageSize">每页包含记录数</param>
        /// <param name="strFilter">过滤条件</param>
        /// <param name="strViewName">视图名称</param>
        /// <param name="strViewSort">排序条件</param>
        /// <returns>
        /// 返回两个表,表1:只有一列:TotalCount, 只有一行记录,上报邮件的总数
        /// 表2:返回上报邮件所有信息的列表
        /// </returns>
        public DataSet GetVListByVName(int nPageIndex, int nPageSize, string strFilter,
            string strViewName, string strViewSort)
        {
            string strSql = "exec prcGetList @PageIndex=?, @PageSize=?, @Filter=?,@ViewName=?,@ViewSort=?";
            OleDbCommand objCmd = new OleDbCommand(strSql, this.m_Manager.CurrentlyConnection);
            OleDbParameter objParameter = objCmd.Parameters.Add("@PageIndex", OleDbType.Integer);
            objParameter.Value = nPageIndex;
            objParameter = objCmd.Parameters.Add("@PageSize", OleDbType.Integer);
            objParameter.Value = nPageSize;
            objParameter = objCmd.Parameters.Add("@Filter", OleDbType.LongVarChar);
            objParameter.Value = strFilter;
            objParameter = objCmd.Parameters.Add("@ViewName", OleDbType.LongVarChar);
            objParameter.Value = strViewName;
            objParameter = objCmd.Parameters.Add("@ViewSort", OleDbType.LongVarChar);
            objParameter.Value = strViewSort;
            OleDbDataAdapter objAdapter = new OleDbDataAdapter(objCmd);
            DataSet dsReturn = new DataSet();
            if (objAdapter.Fill(dsReturn) < 1)
                return null;
            return dsReturn;
        }
        #endregion
    }
}
