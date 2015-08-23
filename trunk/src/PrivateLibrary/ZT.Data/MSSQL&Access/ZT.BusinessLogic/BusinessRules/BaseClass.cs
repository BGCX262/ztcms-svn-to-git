using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZT.BusinessLogic.MSSQL_ACCESS.BusinessRules
{
    /// <summary>
    /// 规则层的基类,主要处理 业务数据的访问,业务规则方法,强类型的访问,事物处理
    /// </summary>
    public class BaseClass//:IDisposable
    {
        //		public void Dispose()  
        //		{  
        //
        //			GC.SuppressFinalize(this);
        //		}
        //
        //		public override void Finalize()
        //		{
        //			Dispose();
        //			base.Finalize();
        //		} 

        /// <summary>
        /// 最后更新者列名
        /// </summary>
        protected string strLastUserColumn = "";
        /// <summary>
        /// 最后更新时间列名
        /// </summary>
        protected string strLastTimeColumn = "";

        /// <summary>
        /// 数据访问管理器
        /// </summary>
        public DataAccess.MSSQL_Access.DBManager m_DBManger = null;

        protected DataSet m_DataSet;
        protected DataRow m_CurrentRow;

        /// <summary>
        /// 将指定数据行,设置成当前操作对象(并且在重载代码中清空全部强类型,必须重载)
        /// </summary>
        /// <param name="n_index">指定数据行号</param>
        public virtual void SelectRow(int n_index)
        {
        }

        /// <summary>
        /// 构造方法(继承类的构造方法必须调用)
        /// </summary>
        /// <param name="l_DBManager">数据访问管理器</param>
        /// <param name="l_Access">数据操作对象</param>
        public BaseClass(DataAccess.MSSQL_Access.DBManager m_DBManager, DataAccess.MSSQL_Access.BaseAccess m_Access)
        {
            this.m_DBManger = m_DBManager;
            this.m_Access = m_Access;
        }


        /// <summary>
        /// 创建新数据集(一般在新建数据时使用,必须重载)
        /// </summary>
        public virtual void NewDataSet()
        {

        }

        /// <summary>
        /// 设置数据集(必须重载)
        /// </summary>
        /// <param name="l_DataSet">数据集</param>
        public virtual void SetDataSet(DataSet l_DataSet)
        {
        }

        /// <summary>
        /// 数据集行数(必须重载)
        /// </summary>
        public virtual int Count
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// 把数据集更改的内容保存到数据管理中(一般用于批量新增,修改)
        /// </summary>
        /// <returns></returns>
        public int PutDataToDB()
        {
            return this.Update();
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns>更新的行数</returns>
        public int Update()
        {
            return OnUpdate();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns>插入的行数</returns>
        public int Insert()
        {
            if (!OnInsert())
            {
                return 0;
            }
            return OnUpdate();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns>插入的行数</returns>
        public int InsertReturnID()//string strPKColumn,string strTable
        {
            if (!OnInsert())
            {
                return 0;
            }
            if (OnUpdate() < 1)
            {
                return -1;
            }
            return this.GetMaxAutoID();//strPKColumn,strTable

        }

        public virtual void NewRow()
        {
        }


        /// <summary>
        /// 保存数据集,以备全增
        /// </summary>
        public bool Save()
        {
            return this.OnSave();
        }

        public virtual bool OnSave()
        {
            if (!this.OnInsert())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 在插入数据前调用(用来融合空的数据集和数据行,必须重载)
        /// </summary>
        /// <returns>融合是否成功</returns>
        public virtual bool OnInsert()
        {
            return true;
        }

        /// <summary>
        /// 删除前的工作
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>工作是否成功</returns>
        protected virtual bool OnDelete(int ID)
        {
            return true;
        }

        /// <summary>
        /// 删除前的工作
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>工作是否成功</returns>
        protected virtual bool OnDelete(string ID)
        {
            return true;
        }


        /// <summary>
        /// 删除指定对象ID,对应的数据
        /// </summary>
        /// <param name="ID">对象ID</param>
        /// <returns>删除的行数</returns>
        public int Delete(int ID)
        {
            if (!OnDelete(ID))
            {
                return 0;
            }
            return this.m_Access.Delete(ID);
        }



        /// <summary>
        /// 删除指定对象ID,对应的数据
        /// </summary>
        /// <param name="ID">对象ID</param>
        /// <returns>删除的行数</returns>
        public int Delete(string ID)
        {
            if (!OnDelete(ID))
            {
                return 0;
            }
            return this.m_Access.Delete(Convert.ToInt32(ID));
        }

        /// <summary>
        /// 更新数据方法的实现(必须重载)
        /// </summary>
        /// <returns>更新的行数</returns>
        protected virtual int OnUpdate()
        {
            return 0;
        }

        /// <summary>
        /// 数据操作对象
        /// </summary>		
        public DataAccess.MSSQL_Access.BaseAccess m_Access = null;


        /// <summary>
        /// 用SQL查询数据
        /// </summary>
        /// <param name="objDs">返回数据集</param>
        /// <param name="strSql">整个SQL语句</param>
        /// <returns>返回查询行数</returns>
        public int QueryByQuerySql(DataSet objDs, string strSql)
        {
            return this.m_Access.QueryByQuerySql(ref objDs, strSql);
        }

        /// <summary>
        /// 获取最新标识
        /// </summary>
        /// <returns></returns>
        public int GetIdentity()
        {
            return this.m_Access.GetIdentity();
        }

        /// <summary>
        /// 获取数据记录条目数
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount()
        {
            return this.m_Access.GetRecordCount();
        }

        /// <summary>
        /// 获取数据记录条目数
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount(string strWhereSql)
        {
            return this.m_Access.GetRecordCount(strWhereSql);
        }

        /// <summary>
        /// 自定义获取数据记录条目数
        /// </summary>
        /// <returns></returns>
        public int GetTotalCountByCustomSql(string strCustomSql)
        {
            return this.m_Access.GetRecordCountByCustomSql(strCustomSql);
        }

        /// <summary>
        /// 获取将要获得的标识值
        /// </summary>
        /// <returns></returns>
        public int GetCurIdentity()
        {
            return m_Access.GetCurIdentity();
        }


        /// <summary>
        /// 获取下一个可用的种子值
        /// </summary>
        /// <returns></returns>
        public int GetNextSeed()
        {
            return this.m_Access.GetNextSeed();
        }


        /// <summary>
        /// 得到刚刚插入的最大ID
        /// </summary>
        /// <param name="strTable"></param>
        /// <returns></returns>
        public int GetMaxAutoID()//string strPKColumn,string strTable
        {
            return this.m_Access.GetMaxAutoID();//strPKColumn,strTable
        }

        public DataTable GetTableColumnByName(string strName)
        {
            return this.m_Access.GetTableColumnByName(strName);
        }

        public DataTable GetTableColumnByID(int intID)
        {
            return this.m_Access.GetTableColumnByID(intID);
        }

        /// <summary>
        /// 删除当前对象的所有记录（在内存中）
        /// </summary>
        public void DeleteAllRow()
        {
            foreach (DataRow objRow in this.m_DataSet.Tables[0].Rows)
            {
                objRow.Delete();
            }
        }

        /// <summary>
        /// 删除当前对象的所有记录（在数据库中）
        /// </summary>
        public void DeleteAllInDataBase()
        {
            string strSql = string.Empty;
            string strTableName = this.m_DataSet.Tables[0].TableName;
            string strKeyName = this.m_DataSet.Tables[0].PrimaryKey[0].ColumnName;

            strSql = string.Format(" Delete From {0} Where {1} in ({2})", strTableName, strKeyName, GetPrimaryKeyIDs());
            System.Data.OleDb.OleDbCommand objOleDbCommand = new System.Data.OleDb.OleDbCommand(strSql, this.m_DBManger.CurrentlyConnection);
            objOleDbCommand.Transaction = this.m_DBManger.CurrentlyTransaction;
            objOleDbCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 返回当前对象的主键ID字符串
        /// </summary>
        /// <returns></returns>
        public string GetPrimaryKeyIDs()
        {
            string strIDs = string.Empty;
            string strKeyName = this.m_DataSet.Tables[0].PrimaryKey[0].ColumnName;
            for (int intI = 0; intI < this.Count; intI++)
            {
                strIDs += this.m_DataSet.Tables[0].Rows[intI][strKeyName].ToString() + ",";
            }
            if (strIDs.EndsWith(","))
                strIDs = strIDs.Remove(strIDs.Length - 1, 1);
            return strIDs;
        }
    }
}
