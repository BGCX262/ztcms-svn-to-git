using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace ZT.DataAccess.MSSQL_Access
{
    /// <summary>
    /// DBManager 的摘要说明。
    /// </summary>
    public class DBManager : IDisposable
    {
        /// <summary>
        /// 释放数据库连接
        /// </summary>
        public void Dispose()
        {
            if (_currentlyConnection.State == ConnectionState.Open)
            {
                _currentlyConnection.Close();
            }
            this._currentlyConnection.Dispose();
            GC.SuppressFinalize(this);
        }

        protected void Finalize()//override
        {
            Dispose();
        }

        //DB连接字符串
        private string _connectionString;
        /// <summary>
        /// DB连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public DBManager()
        {
            ConnectionString = Model.MSSQL_ACCESS.Config.ConnectionString;
            if (_currentlyConnection == null)
            {
                _currentlyConnection = new OleDbConnection(ConnectionString);
            }
        }

        #region 数据库连接/事物操作

        //数据库链接
        private OleDbConnection _currentlyConnection = null;
        /// <summary>
        /// 数据库链接
        /// </summary>
        public OleDbConnection CurrentlyConnection
        {
            get
            {
                if (_currentlyConnection == null)
                {
                    _currentlyConnection = new OleDbConnection(ConnectionString);
                }
                if (_currentlyConnection.State == ConnectionState.Closed)
                {
                    _currentlyConnection.Open();
                }
                return _currentlyConnection;
            }
        }

        //当前事物
        private OleDbTransaction _currentlyTransaction = null;
        /// <summary>
        /// 当前事物
        /// </summary>
        public OleDbTransaction CurrentlyTransaction
        {
            get
            {
                return _currentlyTransaction;
            }
        }

        private int intLayer = 0;

        public bool BeginTransaction()
        {
            try
            {
                if (_currentlyTransaction == null)
                {
                    _currentlyTransaction = CurrentlyConnection.BeginTransaction();
                }
                intLayer++;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Commit()
        {
            if (intLayer > 1)
            {
                intLayer--;
                return true;
            }
            if (_currentlyTransaction != null && intLayer == 1)
            {
                try
                {
                    _currentlyTransaction.Commit();
                    _currentlyTransaction = null;
                    intLayer = 0;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public bool Rollback()
        {
            if (_currentlyTransaction != null)
            {
                try
                {
                    intLayer = 0;
                    _currentlyTransaction.Rollback();
                    _currentlyTransaction = null;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        #endregion 数据库连接/事物操作
    }
}
