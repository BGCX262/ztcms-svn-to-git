using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using UtilityCollection.StringEx;
using OpaqueLayer;

namespace ZT.CodeBuilder
{
    public enum DatabaseLinkMode
    {
        windows = 1,
        SQLServer = 2,
        Access = 3,
    }

    public partial class FormMain : Form
    {
        public OpaqueCommand OpaqueCommand = new OpaqueCommand(10, true, Color.White);

        //某一个选定的数据库
        private string _oneDBName;

        /// <summary>
        /// 某一个选定的数据库
        /// </summary>
        public string OneDBName
        {
            get { return _oneDBName; }
            set { _oneDBName = value; }
        }

        //连接字符串
        private string _connString;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnString
        {
            get { return _connString; }
            set { _connString = value; }
        }

        //数据库连接
        private SqlConnection sqlconnection;

        /// <summary>
        /// 数据库连接
        /// </summary>
        public SqlConnection Sqlconnection
        {
            get { return sqlconnection; }
            set { sqlconnection = value; }
        }

        public FormMain()
        {
            InitializeComponent();
            //windows连接
            button_Windows_connbtn.MouseDown += new MouseEventHandler(MouseDown);
            button_Windows_connbtn.MouseUp += new MouseEventHandler(button_Windows_connbtn_MouseUp);

            //NHibernate生成
            buttonNHibernateCreate.MouseDown += new MouseEventHandler(MouseDown);
            buttonNHibernateCreate.MouseUp += new MouseEventHandler(NHibernateCreate);
        }

        public bool OneDBConnectionBuilder(string dbName)
        {
            bool result = false;
            checkedListBoxDatatables.Items.Clear();
            ConnString = ConnString.Replace(OneDBName, dbName);
            OneDBName = dbName;

            if (!string.IsNullOrEmpty(ConnString))
            {
                Sqlconnection = new SqlConnection(ConnString);
                try
                {
                    Sqlconnection.Open();
                    result = true;

                    SqlCommand sqlcmd =
                        new SqlCommand(
                            "SELECT OBJECT_NAME (id) FROM sysobjects WHERE xtype = 'U' AND OBJECTPROPERTY (id, 'IsMSShipped') = 0",
                            Sqlconnection);
                    SqlDataReader dr = sqlcmd.ExecuteReader();
                    while (dr.Read())
                    {
                        checkedListBoxDatatables.Items.Add(dr.GetString(0), true);
                    }

                    buttonCheckAll.Enabled = true;
                    buttonCheckAll.Enabled = true;
                    buttonCheckInvert.Enabled = true;

                    Sqlconnection.Close();
                }
                catch (Exception ex)
                {
                    Sqlconnection.Close();
                }
            }
            return result;
        }

        public bool MasterConnectionBuilder(int linkMode)
        {
            bool result = false;
            OneDBName = "master";
            listBoxDatabases.Items.Clear();
            if (linkMode == (int)DatabaseLinkMode.windows)
            {
                if (string.IsNullOrEmpty(textBox_Windows_Name.Text.Trim()))
                {
                    textBox_Windows_Name.Text = "(local)";
                }

                var serverName = textBox_Windows_Name.Text.Trim();
                ConnString = string.Format("Server={0}; Integrated Security=SSPI;database=master;", serverName);
            }
            else if (linkMode == (int)DatabaseLinkMode.SQLServer)
            {

            }
            else if (linkMode == (int)DatabaseLinkMode.Access)
            {

            }
            else
            {
            }

            if (!string.IsNullOrEmpty(ConnString) && (linkMode != (int)DatabaseLinkMode.Access))
            {
                Sqlconnection = new SqlConnection(ConnString);
                try
                {
                    Sqlconnection.Open();
                    result = true;

                    DataTable DBNameTable = new DataTable();
                    SqlDataAdapter Adapter = new SqlDataAdapter("select name from master..sysdatabases", Sqlconnection);

                    lock (Adapter)
                    {
                        Adapter.Fill(DBNameTable);
                    }

                    foreach (DataRow row in DBNameTable.Rows)
                    {
                        listBoxDatabases.Items.Add(row["name"].ToString());
                    }

                    Sqlconnection.Close();
                }
                catch (Exception ex)
                {
                    Sqlconnection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// window模式打开链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Windows_connbtn_MouseUp(object sender, EventArgs e)
        {
            bool connectionResult = MasterConnectionBuilder((int)DatabaseLinkMode.windows);

            if (connectionResult)
            {
                button_SQLServer_closebtn.Enabled = false;
                button_SQLServer_connbtn.Enabled = false;

                button_Windows_closebtn.Enabled = true;
                button_Windows_connbtn.Enabled = false;
            }
            else
            {
                MessageBox.Show(@"连接不正确");
            }
            OpaqueCommand.HideOpaqueLayer();
        }

        /// <summary>
        /// SqlServer模式关闭连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SQLServer_closebtn_Click(object sender, EventArgs e)
        {
            ConnString = "";
            Sqlconnection = new SqlConnection();
            listBoxDatabases.Items.Clear();
            resetButton();
        }

        /// <summary>
        /// windoes模式关闭连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Windows_closebtn_Click(object sender, EventArgs e)
        {
            ConnString = "";
            Sqlconnection = new SqlConnection();
            listBoxDatabases.Items.Clear();
            resetButton();
        }

        public void resetButton()
        {
            button_SQLServer_closebtn.Enabled = false;
            button_SQLServer_connbtn.Enabled = true;

            button_Windows_closebtn.Enabled = false;
            button_Windows_connbtn.Enabled = true;

            buttonCheckAll.Enabled = false;
            buttonCheckAll.Enabled = false;
            buttonCheckInvert.Enabled = false;
        }

        /// <summary>
        /// listbox点击某一项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dbName = listBoxDatabases.SelectedItem.ToString();
            bool connectionResult = OneDBConnectionBuilder(dbName);

            if (connectionResult)
            {
                button_SQLServer_closebtn.Enabled = false;
                button_SQLServer_connbtn.Enabled = false;

                button_Windows_closebtn.Enabled = true;
                button_Windows_connbtn.Enabled = false;
            }
            else
            {
                MessageBox.Show("连接不正确");
            }
        }

        private void buttonCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxDatatables.Items.Count; i++)
            {
                checkedListBoxDatatables.SetItemChecked(i, true);
            }
        }

        private void buttonUnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxDatatables.Items.Count; i++)
            {
                checkedListBoxDatatables.SetItemChecked(i, false);
            }
        }

        private void buttonCheckInvert_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxDatatables.Items.Count; i++)
            {
                bool checkresult = checkedListBoxDatatables.GetItemChecked(i);
                checkedListBoxDatatables.SetItemChecked(i, !checkresult);
            }
        }

        private void buttonCreateFile_Click(object sender, EventArgs e)
        {
            string tmpDataAccess = LoadFile("default\\DataAccess.ctf");
            StringBuilder objBuilder = new StringBuilder();
            string currenttablename = "";
            if (checkBoxDataAccess.Checked)
            {
                for (int i = 0; i < checkedListBoxDatatables.Items.Count; i++)
                {
                    bool checkresult = checkedListBoxDatatables.GetItemChecked(i);
                    if (checkresult)
                    {
                        currenttablename = checkedListBoxDatatables.GetItemText(checkedListBoxDatatables.Items[i]);
                        DataTable DBOneTable = new DataTable();

                        SqlDataAdapter Adapter =
                            new SqlDataAdapter(string.Format("select * from [{0}]", currenttablename), Sqlconnection);
                        Adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                        lock (Adapter)
                        {
                            Adapter.Fill(DBOneTable);
                        }

                        //if (DBOneTable.Rows.Count == 0)
                        //{
                        //    DataRow objrow = DBOneTable.NewRow();
                        //    DBOneTable.Rows.Add(objrow);
                        //}

                        objBuilder = new StringBuilder(tmpDataAccess);
                        if (DBOneTable.PrimaryKey.Length > 0)
                        {
                            objBuilder.Replace("$tablename$", currenttablename);
                            objBuilder.Replace("$table_pk$", DBOneTable.PrimaryKey[0].ColumnName);
                            var dirpath = AppDomain.CurrentDomain.BaseDirectory + "buildresult\\default\\DataAccess\\";
                            if (!Directory.Exists(dirpath))
                            {
                                Directory.CreateDirectory(dirpath);
                            }
                            using (
                                FileStream fs = new FileStream(dirpath + currenttablename + ".cs", FileMode.Create,
                                                               FileAccess.Write, FileShare.Read))
                            {
                                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                                sw.Write(objBuilder.ToString());
                                sw.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("表{0}没有设定主键", currenttablename));
                        }
                    }
                }
            }
            MessageBox.Show(@"生成成功");
            OpaqueCommand.HideOpaqueLayer();
        }

        /// <summary>
        /// 按下鼠标显示遮罩层，阻止危险操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public new void MouseDown(object sender, MouseEventArgs e)
        {
            OpaqueCommand.ShowOpaqueLayer(this);
        }

        #region NHibernate

        protected void NHibernateCreate(object sender, EventArgs e)
        {
            //dal
            if (checkBoxNHDataAccessLayer.Checked)
            {
                string tmp = LoadFile("NHibernate\\DataAccessLayer.cs.ctf");
                string tmpEx = LoadFile("NHibernate\\DataAccessLayerEx.cs.ctf");
                for (int i = 0; i < checkedListBoxDatatables.Items.Count; i++)
                {
                    var checkresult = checkedListBoxDatatables.GetItemChecked(i);
                    if (checkresult)
                    {
                        //读取表结构
                        var currenttablename = checkedListBoxDatatables.GetItemText(checkedListBoxDatatables.Items[i]).ToUpperFirstChar();
                        var dbOneTable = new DataTable();

                        var adapter =
                            new SqlDataAdapter(string.Format("select * from [{0}]", currenttablename), Sqlconnection)
                            {
                                MissingSchemaAction = MissingSchemaAction.AddWithKey
                            };

                        lock (adapter)
                        {
                            adapter.Fill(dbOneTable);
                        }

                        var objBuilder = new StringBuilder(tmp);
                        if (dbOneTable.PrimaryKey.Length > 0)
                        {
                            objBuilder.Replace("$tablename$", currenttablename);
                            objBuilder.Replace("$table_pk$", dbOneTable.PrimaryKey[0].ColumnName.ToUpperFirstChar());
                            var dirpath = AppDomain.CurrentDomain.BaseDirectory + "buildresult\\NHibernate\\DataAccessLayer\\";
                            if (!Directory.Exists(dirpath))
                            {
                                Directory.CreateDirectory(dirpath);
                            }
                            using (var fs = new FileStream(dirpath + currenttablename + "Dal.cs", FileMode.Create, FileAccess.Write, FileShare.Read))
                            {
                                var sw = new StreamWriter(fs, Encoding.UTF8);
                                sw.Write(objBuilder.ToString());
                                sw.Close();
                            }

                            //Ex
                            objBuilder = new StringBuilder(tmpEx);
                            objBuilder.Replace("$tablename$", currenttablename);
                            dirpath = AppDomain.CurrentDomain.BaseDirectory + "buildresult\\NHibernate\\DataAccessLayerEx\\";
                            if (!Directory.Exists(dirpath))
                            {
                                Directory.CreateDirectory(dirpath);
                            }
                            using (var fs = new FileStream(dirpath + currenttablename + "DalEx.cs", FileMode.Create, FileAccess.Write, FileShare.Read))
                            {
                                var sw = new StreamWriter(fs, Encoding.UTF8);
                                sw.Write(objBuilder.ToString());
                                sw.Close();
                            }
                        }
                        else
                        {
                            //MessageBox.Show(string.Format("表{0}没有设定主键", currenttablename));
                            textBoxLog.Text += string.Format("表{0}没有设定主键, 跳过生成Dal\r\n", currenttablename);
                        }
                    }
                }
            }

            //entity
            if (checkBoxNHEntity.Checked)
            {
                string tmp = LoadFile("NHibernate\\Entity.cs.ctf");
                for (int i = 0; i < checkedListBoxDatatables.Items.Count; i++)
                {
                    var checkresult = checkedListBoxDatatables.GetItemChecked(i);
                    if (checkresult)
                    {
                        //读取表结构
                        var currenttablename = checkedListBoxDatatables.GetItemText(checkedListBoxDatatables.Items[i]).ToUpperFirstChar();
                        var dbOneTable = new DataTable();

                        var adapter =
                            new SqlDataAdapter(string.Format("select * from [{0}]", currenttablename), Sqlconnection)
                            {
                                MissingSchemaAction = MissingSchemaAction.AddWithKey
                            };

                        lock (adapter)
                        {
                            adapter.Fill(dbOneTable);
                        }

                        var objBuilder = new StringBuilder(tmp);
                        if (dbOneTable.PrimaryKey.Length > 0)
                        {
                            objBuilder.Replace("$tablename$", currenttablename);
                            var tablePk = dbOneTable.PrimaryKey[0].ColumnName;
                            var tablePkType = dbOneTable.PrimaryKey[0].DataType.Name;
                            objBuilder.Replace("$table_pk_property$", string.Format("public virtual {0} {1} {2} \r\n", tablePkType, tablePk.ToUpperFirstChar(), "{get; set;}"));

                            var onPkProperty = new StringBuilder();
                            for (int j = 0; j < dbOneTable.Columns.Count; j++)
                            {
                                if (dbOneTable.Columns[j].ColumnName != tablePk)
                                {
                                    var colname = dbOneTable.Columns[j].ColumnName.ToUpperFirstChar();
                                    var colType = dbOneTable.Columns[j].DataType.Name;
                                    if (string.IsNullOrEmpty(onPkProperty.ToString()))
                                    {
                                        onPkProperty.Append(string.Format("public virtual {0} {1} {2} \r\n", colType,
                                  colname.ToUpperFirstChar(), "{get; set;}"));
                                    }
                                    else
                                    {
                                        onPkProperty.Append(string.Format("        public virtual {0} {1} {2} \r\n", colType,
                                  colname.ToUpperFirstChar(), "{get; set;}"));
                                    }
                                }
                            }
                            objBuilder.Replace("$table_nopl_property$", onPkProperty.ToString());

                            var dirpath = AppDomain.CurrentDomain.BaseDirectory +
                                          "buildresult\\NHibernate\\Entity\\";
                            if (!Directory.Exists(dirpath))
                            {
                                Directory.CreateDirectory(dirpath);
                            }
                            using (
                                var fs = new FileStream(dirpath + currenttablename + ".cs", FileMode.Create,
                                                        FileAccess.Write, FileShare.Read))
                            {
                                var sw = new StreamWriter(fs, Encoding.UTF8);
                                sw.Write(objBuilder.ToString());
                                sw.Close();
                            }
                        }
                        else
                        {
                            //MessageBox.Show(string.Format("表{0}没有设定主键", currenttablename));
                            textBoxLog.Text += string.Format("表{0}没有设定主键, 跳过生成Dal\r\n", currenttablename);
                        }
                    }
                }
            }

            //entity
            if (checkBoxNHDataXML.Checked)
            {
                string tmp = LoadFile("NHibernate\\DataXml.hbm.xml.ctf");
                for (int i = 0; i < checkedListBoxDatatables.Items.Count; i++)
                {
                    var checkresult = checkedListBoxDatatables.GetItemChecked(i);
                    if (checkresult)
                    {
                        //读取表结构
                        var currenttablename = checkedListBoxDatatables.GetItemText(checkedListBoxDatatables.Items[i]).ToUpperFirstChar();
                        var dbOneTable = new DataTable();

                        var adapter =
                            new SqlDataAdapter(string.Format("select * from [{0}]", currenttablename), Sqlconnection)
                            {
                                MissingSchemaAction = MissingSchemaAction.AddWithKey
                            };

                        lock (adapter)
                        {
                            adapter.Fill(dbOneTable);
                        }

                        var objBuilder = new StringBuilder(tmp);
                        if (dbOneTable.PrimaryKey.Length > 0)
                        {
                            objBuilder.Replace("$tablename$", currenttablename);
                            var tablePk = dbOneTable.PrimaryKey[0].ColumnName;
                            var tablePkType = dbOneTable.PrimaryKey[0].DataType.Name;
                            objBuilder.Replace("$table_pk$", tablePk.ToUpperFirstChar());
                            objBuilder.Replace("$table_pk_type$", tablePkType);

                            var onPkProperty = new StringBuilder();
                            for (int j = 0; j < dbOneTable.Columns.Count; j++)
                            {
                                if (dbOneTable.Columns[j].ColumnName != tablePk)
                                {
                                    var colname = dbOneTable.Columns[j].ColumnName.ToUpperFirstChar();
                                    var colType = dbOneTable.Columns[j].DataType.Name;
                                    if (string.IsNullOrEmpty(onPkProperty.ToString()))
                                    {
                                        onPkProperty.Append(string.Format("<property name=\"{0}\" column=\"{0}\" type=\"{1}\"  /> \r\n", colname.ToUpperFirstChar(), colType));
                                    }
                                    else
                                    {
                                        onPkProperty.Append(string.Format("        <property name=\"{0}\" column=\"{0}\" type=\"{1}\"  /> \r\n", colname.ToUpperFirstChar(), colType));
                                    }
                                }
                            }
                            objBuilder.Replace("$table_nopl_property$", onPkProperty.ToString());

                            var dirpath = AppDomain.CurrentDomain.BaseDirectory +
                                          "buildresult\\NHibernate\\Mappings\\";
                            if (!Directory.Exists(dirpath))
                            {
                                Directory.CreateDirectory(dirpath);
                            }
                            using (
                                var fs = new FileStream(dirpath + currenttablename + ".hbm.xml", FileMode.Create,
                                                        FileAccess.Write, FileShare.Read))
                            {
                                var sw = new StreamWriter(fs, Encoding.UTF8);
                                sw.Write(objBuilder.ToString());
                                sw.Close();
                            }
                        }
                        else
                        {
                            //MessageBox.Show(string.Format("表{0}没有设定主键", currenttablename));
                            textBoxLog.Text += string.Format("表{0}没有设定主键, 跳过生成Dal\r\n", currenttablename);
                        }
                    }
                }
            }
            MessageBox.Show(@"生成成功");
            OpaqueCommand.HideOpaqueLayer();
        }

        private void buttonOpenPosition_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe",  AppDomain.CurrentDomain.BaseDirectory + "buildresult\\");
        }

        #endregion

        #region 加载模版

        protected string LoadFile(string ctfpath)
        {
            var result = "";
            var path = AppDomain.CurrentDomain.BaseDirectory + "template\\" + ctfpath;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var sr = new StreamReader(fs);
                result = sr.ReadToEnd();
                sr.Close();
            }

            return result;
        }

        #endregion
    }
}
