namespace ZT.CodeBuilder
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.GPBox_Connection = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_windows = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Windows_Name = new System.Windows.Forms.TextBox();
            this.button_Windows_closebtn = new System.Windows.Forms.Button();
            this.button_Windows_connbtn = new System.Windows.Forms.Button();
            this.textBox_Windows_Password = new System.Windows.Forms.TextBox();
            this.textBox_Windows_LoginID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage_SqlServer = new System.Windows.Forms.TabPage();
            this.textBox_SQLServer_Name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_SQLServer_closebtn = new System.Windows.Forms.Button();
            this.button_SQLServer_connbtn = new System.Windows.Forms.Button();
            this.textBox_SQLServer_Password = new System.Windows.Forms.TextBox();
            this.textBox_SQLServer_LoginID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage_Access = new System.Windows.Forms.TabPage();
            this.groupBoxDatatables = new System.Windows.Forms.GroupBox();
            this.checkedListBoxDatatables = new System.Windows.Forms.CheckedListBox();
            this.groupBoxDatabases = new System.Windows.Forms.GroupBox();
            this.listBoxDatabases = new System.Windows.Forms.ListBox();
            this.groupBoxChangeCheck = new System.Windows.Forms.GroupBox();
            this.buttonUnCheckAll = new System.Windows.Forms.Button();
            this.buttonCheckInvert = new System.Windows.Forms.Button();
            this.buttonCheckAll = new System.Windows.Forms.Button();
            this.groupBoxGeneratechoose = new System.Windows.Forms.GroupBox();
            this.buttonCreateFile = new System.Windows.Forms.Button();
            this.checkBoxDataAccess = new System.Windows.Forms.CheckBox();
            this.checkBoxClassFactory = new System.Windows.Forms.CheckBox();
            this.checkBoxBusinessRules = new System.Windows.Forms.CheckBox();
            this.groupBoxNHibernate = new System.Windows.Forms.GroupBox();
            this.buttonNHibernateCreate = new System.Windows.Forms.Button();
            this.checkBoxNHDataXML = new System.Windows.Forms.CheckBox();
            this.checkBoxNHEntity = new System.Windows.Forms.CheckBox();
            this.checkBoxNHDataAccessLayer = new System.Windows.Forms.CheckBox();
            this.buttonOpenPosition = new System.Windows.Forms.Button();
            this.tabControlCreateModeChoose = new System.Windows.Forms.TabControl();
            this.tabPageInnerFactory = new System.Windows.Forms.TabPage();
            this.tabPageNHibernate = new System.Windows.Forms.TabPage();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.GPBox_Connection.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_windows.SuspendLayout();
            this.tabPage_SqlServer.SuspendLayout();
            this.groupBoxDatatables.SuspendLayout();
            this.groupBoxDatabases.SuspendLayout();
            this.groupBoxChangeCheck.SuspendLayout();
            this.groupBoxGeneratechoose.SuspendLayout();
            this.groupBoxNHibernate.SuspendLayout();
            this.tabControlCreateModeChoose.SuspendLayout();
            this.tabPageInnerFactory.SuspendLayout();
            this.tabPageNHibernate.SuspendLayout();
            this.SuspendLayout();
            // 
            // GPBox_Connection
            // 
            this.GPBox_Connection.Controls.Add(this.tabControl1);
            this.GPBox_Connection.Location = new System.Drawing.Point(12, 12);
            this.GPBox_Connection.Name = "GPBox_Connection";
            this.GPBox_Connection.Size = new System.Drawing.Size(200, 168);
            this.GPBox_Connection.TabIndex = 0;
            this.GPBox_Connection.TabStop = false;
            this.GPBox_Connection.Text = "数据库连接模式";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_windows);
            this.tabControl1.Controls.Add(this.tabPage_SqlServer);
            this.tabControl1.Controls.Add(this.tabPage_Access);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(194, 148);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_windows
            // 
            this.tabPage_windows.Controls.Add(this.label6);
            this.tabPage_windows.Controls.Add(this.textBox_Windows_Name);
            this.tabPage_windows.Controls.Add(this.button_Windows_closebtn);
            this.tabPage_windows.Controls.Add(this.button_Windows_connbtn);
            this.tabPage_windows.Controls.Add(this.textBox_Windows_Password);
            this.tabPage_windows.Controls.Add(this.textBox_Windows_LoginID);
            this.tabPage_windows.Controls.Add(this.label1);
            this.tabPage_windows.Controls.Add(this.label2);
            this.tabPage_windows.Location = new System.Drawing.Point(4, 22);
            this.tabPage_windows.Name = "tabPage_windows";
            this.tabPage_windows.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_windows.Size = new System.Drawing.Size(186, 122);
            this.tabPage_windows.TabIndex = 0;
            this.tabPage_windows.Text = "windows";
            this.tabPage_windows.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "服务器：";
            // 
            // textBox_Windows_Name
            // 
            this.textBox_Windows_Name.Location = new System.Drawing.Point(71, 9);
            this.textBox_Windows_Name.Name = "textBox_Windows_Name";
            this.textBox_Windows_Name.Size = new System.Drawing.Size(100, 21);
            this.textBox_Windows_Name.TabIndex = 10;
            // 
            // button_Windows_closebtn
            // 
            this.button_Windows_closebtn.Enabled = false;
            this.button_Windows_closebtn.Location = new System.Drawing.Point(122, 93);
            this.button_Windows_closebtn.Name = "button_Windows_closebtn";
            this.button_Windows_closebtn.Size = new System.Drawing.Size(40, 23);
            this.button_Windows_closebtn.TabIndex = 9;
            this.button_Windows_closebtn.Text = "关闭";
            this.button_Windows_closebtn.UseVisualStyleBackColor = true;
            this.button_Windows_closebtn.Click += new System.EventHandler(this.button_Windows_closebtn_Click);
            // 
            // button_Windows_connbtn
            // 
            this.button_Windows_connbtn.Location = new System.Drawing.Point(71, 93);
            this.button_Windows_connbtn.Name = "button_Windows_connbtn";
            this.button_Windows_connbtn.Size = new System.Drawing.Size(45, 23);
            this.button_Windows_connbtn.TabIndex = 9;
            this.button_Windows_connbtn.Text = "连接";
            this.button_Windows_connbtn.UseVisualStyleBackColor = true;
            // 
            // textBox_Windows_Password
            // 
            this.textBox_Windows_Password.Location = new System.Drawing.Point(71, 65);
            this.textBox_Windows_Password.Name = "textBox_Windows_Password";
            this.textBox_Windows_Password.ReadOnly = true;
            this.textBox_Windows_Password.Size = new System.Drawing.Size(100, 21);
            this.textBox_Windows_Password.TabIndex = 8;
            // 
            // textBox_Windows_LoginID
            // 
            this.textBox_Windows_LoginID.Location = new System.Drawing.Point(71, 37);
            this.textBox_Windows_LoginID.Name = "textBox_Windows_LoginID";
            this.textBox_Windows_LoginID.ReadOnly = true;
            this.textBox_Windows_LoginID.Size = new System.Drawing.Size(100, 21);
            this.textBox_Windows_LoginID.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "登录名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "密码：";
            // 
            // tabPage_SqlServer
            // 
            this.tabPage_SqlServer.Controls.Add(this.textBox_SQLServer_Name);
            this.tabPage_SqlServer.Controls.Add(this.label5);
            this.tabPage_SqlServer.Controls.Add(this.button_SQLServer_closebtn);
            this.tabPage_SqlServer.Controls.Add(this.button_SQLServer_connbtn);
            this.tabPage_SqlServer.Controls.Add(this.textBox_SQLServer_Password);
            this.tabPage_SqlServer.Controls.Add(this.textBox_SQLServer_LoginID);
            this.tabPage_SqlServer.Controls.Add(this.label3);
            this.tabPage_SqlServer.Controls.Add(this.label4);
            this.tabPage_SqlServer.Location = new System.Drawing.Point(4, 22);
            this.tabPage_SqlServer.Name = "tabPage_SqlServer";
            this.tabPage_SqlServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_SqlServer.Size = new System.Drawing.Size(186, 122);
            this.tabPage_SqlServer.TabIndex = 1;
            this.tabPage_SqlServer.Text = "SqlServer";
            this.tabPage_SqlServer.UseVisualStyleBackColor = true;
            // 
            // textBox_SQLServer_Name
            // 
            this.textBox_SQLServer_Name.Location = new System.Drawing.Point(70, 10);
            this.textBox_SQLServer_Name.Name = "textBox_SQLServer_Name";
            this.textBox_SQLServer_Name.Size = new System.Drawing.Size(100, 21);
            this.textBox_SQLServer_Name.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "服务器：";
            // 
            // button_SQLServer_closebtn
            // 
            this.button_SQLServer_closebtn.Enabled = false;
            this.button_SQLServer_closebtn.Location = new System.Drawing.Point(115, 93);
            this.button_SQLServer_closebtn.Name = "button_SQLServer_closebtn";
            this.button_SQLServer_closebtn.Size = new System.Drawing.Size(43, 23);
            this.button_SQLServer_closebtn.TabIndex = 14;
            this.button_SQLServer_closebtn.Text = "关闭";
            this.button_SQLServer_closebtn.UseVisualStyleBackColor = true;
            this.button_SQLServer_closebtn.Click += new System.EventHandler(this.button_SQLServer_closebtn_Click);
            // 
            // button_SQLServer_connbtn
            // 
            this.button_SQLServer_connbtn.Location = new System.Drawing.Point(70, 93);
            this.button_SQLServer_connbtn.Name = "button_SQLServer_connbtn";
            this.button_SQLServer_connbtn.Size = new System.Drawing.Size(39, 23);
            this.button_SQLServer_connbtn.TabIndex = 14;
            this.button_SQLServer_connbtn.Text = "连接";
            this.button_SQLServer_connbtn.UseVisualStyleBackColor = true;
            // 
            // textBox_SQLServer_Password
            // 
            this.textBox_SQLServer_Password.Location = new System.Drawing.Point(70, 65);
            this.textBox_SQLServer_Password.Name = "textBox_SQLServer_Password";
            this.textBox_SQLServer_Password.PasswordChar = '*';
            this.textBox_SQLServer_Password.Size = new System.Drawing.Size(100, 21);
            this.textBox_SQLServer_Password.TabIndex = 13;
            this.textBox_SQLServer_Password.UseSystemPasswordChar = true;
            // 
            // textBox_SQLServer_LoginID
            // 
            this.textBox_SQLServer_LoginID.Location = new System.Drawing.Point(70, 37);
            this.textBox_SQLServer_LoginID.Name = "textBox_SQLServer_LoginID";
            this.textBox_SQLServer_LoginID.Size = new System.Drawing.Size(100, 21);
            this.textBox_SQLServer_LoginID.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "登录名：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "密码：";
            // 
            // tabPage_Access
            // 
            this.tabPage_Access.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Access.Name = "tabPage_Access";
            this.tabPage_Access.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Access.Size = new System.Drawing.Size(186, 122);
            this.tabPage_Access.TabIndex = 2;
            this.tabPage_Access.Text = "Access";
            this.tabPage_Access.UseVisualStyleBackColor = true;
            // 
            // groupBoxDatatables
            // 
            this.groupBoxDatatables.Controls.Add(this.checkedListBoxDatatables);
            this.groupBoxDatatables.Location = new System.Drawing.Point(228, 12);
            this.groupBoxDatatables.Name = "groupBoxDatatables";
            this.groupBoxDatatables.Size = new System.Drawing.Size(200, 397);
            this.groupBoxDatatables.TabIndex = 1;
            this.groupBoxDatatables.TabStop = false;
            this.groupBoxDatatables.Text = "数据表";
            // 
            // checkedListBoxDatatables
            // 
            this.checkedListBoxDatatables.CheckOnClick = true;
            this.checkedListBoxDatatables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxDatatables.FormattingEnabled = true;
            this.checkedListBoxDatatables.HorizontalScrollbar = true;
            this.checkedListBoxDatatables.Location = new System.Drawing.Point(3, 17);
            this.checkedListBoxDatatables.Name = "checkedListBoxDatatables";
            this.checkedListBoxDatatables.Size = new System.Drawing.Size(194, 377);
            this.checkedListBoxDatatables.TabIndex = 0;
            // 
            // groupBoxDatabases
            // 
            this.groupBoxDatabases.Controls.Add(this.listBoxDatabases);
            this.groupBoxDatabases.Location = new System.Drawing.Point(12, 186);
            this.groupBoxDatabases.Name = "groupBoxDatabases";
            this.groupBoxDatabases.Size = new System.Drawing.Size(200, 220);
            this.groupBoxDatabases.TabIndex = 2;
            this.groupBoxDatabases.TabStop = false;
            this.groupBoxDatabases.Text = "数据库列表";
            // 
            // listBoxDatabases
            // 
            this.listBoxDatabases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDatabases.FormattingEnabled = true;
            this.listBoxDatabases.HorizontalScrollbar = true;
            this.listBoxDatabases.ItemHeight = 12;
            this.listBoxDatabases.Location = new System.Drawing.Point(3, 17);
            this.listBoxDatabases.Name = "listBoxDatabases";
            this.listBoxDatabases.Size = new System.Drawing.Size(194, 200);
            this.listBoxDatabases.TabIndex = 0;
            this.listBoxDatabases.SelectedIndexChanged += new System.EventHandler(this.listBoxDatabases_SelectedIndexChanged);
            // 
            // groupBoxChangeCheck
            // 
            this.groupBoxChangeCheck.Controls.Add(this.buttonUnCheckAll);
            this.groupBoxChangeCheck.Controls.Add(this.buttonCheckInvert);
            this.groupBoxChangeCheck.Controls.Add(this.buttonCheckAll);
            this.groupBoxChangeCheck.Location = new System.Drawing.Point(450, 12);
            this.groupBoxChangeCheck.Name = "groupBoxChangeCheck";
            this.groupBoxChangeCheck.Size = new System.Drawing.Size(206, 49);
            this.groupBoxChangeCheck.TabIndex = 3;
            this.groupBoxChangeCheck.TabStop = false;
            this.groupBoxChangeCheck.Text = "变更选中";
            // 
            // buttonUnCheckAll
            // 
            this.buttonUnCheckAll.Location = new System.Drawing.Point(76, 20);
            this.buttonUnCheckAll.Name = "buttonUnCheckAll";
            this.buttonUnCheckAll.Size = new System.Drawing.Size(63, 23);
            this.buttonUnCheckAll.TabIndex = 5;
            this.buttonUnCheckAll.Text = "全部取消";
            this.buttonUnCheckAll.UseVisualStyleBackColor = true;
            this.buttonUnCheckAll.Click += new System.EventHandler(this.buttonUnCheckAll_Click);
            // 
            // buttonCheckInvert
            // 
            this.buttonCheckInvert.Location = new System.Drawing.Point(145, 20);
            this.buttonCheckInvert.Name = "buttonCheckInvert";
            this.buttonCheckInvert.Size = new System.Drawing.Size(53, 23);
            this.buttonCheckInvert.TabIndex = 4;
            this.buttonCheckInvert.Text = "反选";
            this.buttonCheckInvert.UseVisualStyleBackColor = true;
            this.buttonCheckInvert.Click += new System.EventHandler(this.buttonCheckInvert_Click);
            // 
            // buttonCheckAll
            // 
            this.buttonCheckAll.Location = new System.Drawing.Point(6, 20);
            this.buttonCheckAll.Name = "buttonCheckAll";
            this.buttonCheckAll.Size = new System.Drawing.Size(62, 23);
            this.buttonCheckAll.TabIndex = 3;
            this.buttonCheckAll.Text = "全部选中";
            this.buttonCheckAll.UseVisualStyleBackColor = true;
            this.buttonCheckAll.Click += new System.EventHandler(this.buttonCheckAll_Click);
            // 
            // groupBoxGeneratechoose
            // 
            this.groupBoxGeneratechoose.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxGeneratechoose.Controls.Add(this.buttonCreateFile);
            this.groupBoxGeneratechoose.Controls.Add(this.checkBoxDataAccess);
            this.groupBoxGeneratechoose.Controls.Add(this.checkBoxClassFactory);
            this.groupBoxGeneratechoose.Controls.Add(this.checkBoxBusinessRules);
            this.groupBoxGeneratechoose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGeneratechoose.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGeneratechoose.Name = "groupBoxGeneratechoose";
            this.groupBoxGeneratechoose.Size = new System.Drawing.Size(186, 109);
            this.groupBoxGeneratechoose.TabIndex = 4;
            this.groupBoxGeneratechoose.TabStop = false;
            this.groupBoxGeneratechoose.Text = "内置工厂模式生成选项";
            // 
            // buttonCreateFile
            // 
            this.buttonCreateFile.Location = new System.Drawing.Point(18, 83);
            this.buttonCreateFile.Name = "buttonCreateFile";
            this.buttonCreateFile.Size = new System.Drawing.Size(56, 23);
            this.buttonCreateFile.TabIndex = 3;
            this.buttonCreateFile.Text = "生成";
            this.buttonCreateFile.UseVisualStyleBackColor = true;
            this.buttonCreateFile.Click += new System.EventHandler(this.buttonCreateFile_Click);
            // 
            // checkBoxDataAccess
            // 
            this.checkBoxDataAccess.AutoSize = true;
            this.checkBoxDataAccess.Location = new System.Drawing.Point(18, 66);
            this.checkBoxDataAccess.Name = "checkBoxDataAccess";
            this.checkBoxDataAccess.Size = new System.Drawing.Size(84, 16);
            this.checkBoxDataAccess.TabIndex = 2;
            this.checkBoxDataAccess.Text = "DataAccess";
            this.checkBoxDataAccess.UseVisualStyleBackColor = true;
            // 
            // checkBoxClassFactory
            // 
            this.checkBoxClassFactory.AutoSize = true;
            this.checkBoxClassFactory.Location = new System.Drawing.Point(18, 44);
            this.checkBoxClassFactory.Name = "checkBoxClassFactory";
            this.checkBoxClassFactory.Size = new System.Drawing.Size(96, 16);
            this.checkBoxClassFactory.TabIndex = 1;
            this.checkBoxClassFactory.Text = "ClassFactory";
            this.checkBoxClassFactory.UseVisualStyleBackColor = true;
            // 
            // checkBoxBusinessRules
            // 
            this.checkBoxBusinessRules.AutoSize = true;
            this.checkBoxBusinessRules.Location = new System.Drawing.Point(18, 21);
            this.checkBoxBusinessRules.Name = "checkBoxBusinessRules";
            this.checkBoxBusinessRules.Size = new System.Drawing.Size(102, 16);
            this.checkBoxBusinessRules.TabIndex = 0;
            this.checkBoxBusinessRules.Text = "BusinessRules";
            this.checkBoxBusinessRules.UseVisualStyleBackColor = true;
            // 
            // groupBoxNHibernate
            // 
            this.groupBoxNHibernate.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxNHibernate.Controls.Add(this.buttonNHibernateCreate);
            this.groupBoxNHibernate.Controls.Add(this.checkBoxNHDataXML);
            this.groupBoxNHibernate.Controls.Add(this.checkBoxNHEntity);
            this.groupBoxNHibernate.Controls.Add(this.checkBoxNHDataAccessLayer);
            this.groupBoxNHibernate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxNHibernate.Location = new System.Drawing.Point(3, 3);
            this.groupBoxNHibernate.Name = "groupBoxNHibernate";
            this.groupBoxNHibernate.Size = new System.Drawing.Size(186, 109);
            this.groupBoxNHibernate.TabIndex = 5;
            this.groupBoxNHibernate.TabStop = false;
            this.groupBoxNHibernate.Text = "NHibernate生成选项";
            // 
            // buttonNHibernateCreate
            // 
            this.buttonNHibernateCreate.Location = new System.Drawing.Point(18, 83);
            this.buttonNHibernateCreate.Name = "buttonNHibernateCreate";
            this.buttonNHibernateCreate.Size = new System.Drawing.Size(56, 23);
            this.buttonNHibernateCreate.TabIndex = 3;
            this.buttonNHibernateCreate.Text = "生成";
            this.buttonNHibernateCreate.UseVisualStyleBackColor = true;
            // 
            // checkBoxNHDataXML
            // 
            this.checkBoxNHDataXML.AutoSize = true;
            this.checkBoxNHDataXML.Location = new System.Drawing.Point(18, 66);
            this.checkBoxNHDataXML.Name = "checkBoxNHDataXML";
            this.checkBoxNHDataXML.Size = new System.Drawing.Size(66, 16);
            this.checkBoxNHDataXML.TabIndex = 2;
            this.checkBoxNHDataXML.Text = "DataXML";
            this.checkBoxNHDataXML.UseVisualStyleBackColor = true;
            // 
            // checkBoxNHEntity
            // 
            this.checkBoxNHEntity.AutoSize = true;
            this.checkBoxNHEntity.Location = new System.Drawing.Point(18, 44);
            this.checkBoxNHEntity.Name = "checkBoxNHEntity";
            this.checkBoxNHEntity.Size = new System.Drawing.Size(60, 16);
            this.checkBoxNHEntity.TabIndex = 1;
            this.checkBoxNHEntity.Text = "Entity";
            this.checkBoxNHEntity.UseVisualStyleBackColor = true;
            // 
            // checkBoxNHDataAccessLayer
            // 
            this.checkBoxNHDataAccessLayer.AutoSize = true;
            this.checkBoxNHDataAccessLayer.Location = new System.Drawing.Point(18, 21);
            this.checkBoxNHDataAccessLayer.Name = "checkBoxNHDataAccessLayer";
            this.checkBoxNHDataAccessLayer.Size = new System.Drawing.Size(114, 16);
            this.checkBoxNHDataAccessLayer.TabIndex = 0;
            this.checkBoxNHDataAccessLayer.Text = "DataAccessLayer";
            this.checkBoxNHDataAccessLayer.UseVisualStyleBackColor = true;
            // 
            // buttonOpenPosition
            // 
            this.buttonOpenPosition.Location = new System.Drawing.Point(448, 214);
            this.buttonOpenPosition.Name = "buttonOpenPosition";
            this.buttonOpenPosition.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenPosition.TabIndex = 5;
            this.buttonOpenPosition.Text = "打开位置";
            this.buttonOpenPosition.UseVisualStyleBackColor = true;
            this.buttonOpenPosition.Click += new System.EventHandler(this.buttonOpenPosition_Click);
            // 
            // tabControlCreateModeChoose
            // 
            this.tabControlCreateModeChoose.Controls.Add(this.tabPageInnerFactory);
            this.tabControlCreateModeChoose.Controls.Add(this.tabPageNHibernate);
            this.tabControlCreateModeChoose.Location = new System.Drawing.Point(448, 67);
            this.tabControlCreateModeChoose.Name = "tabControlCreateModeChoose";
            this.tabControlCreateModeChoose.SelectedIndex = 0;
            this.tabControlCreateModeChoose.Size = new System.Drawing.Size(200, 141);
            this.tabControlCreateModeChoose.TabIndex = 6;
            // 
            // tabPageInnerFactory
            // 
            this.tabPageInnerFactory.Controls.Add(this.groupBoxGeneratechoose);
            this.tabPageInnerFactory.Location = new System.Drawing.Point(4, 22);
            this.tabPageInnerFactory.Name = "tabPageInnerFactory";
            this.tabPageInnerFactory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInnerFactory.Size = new System.Drawing.Size(192, 115);
            this.tabPageInnerFactory.TabIndex = 0;
            this.tabPageInnerFactory.Text = "内置工厂";
            this.tabPageInnerFactory.UseVisualStyleBackColor = true;
            // 
            // tabPageNHibernate
            // 
            this.tabPageNHibernate.Controls.Add(this.groupBoxNHibernate);
            this.tabPageNHibernate.Location = new System.Drawing.Point(4, 22);
            this.tabPageNHibernate.Name = "tabPageNHibernate";
            this.tabPageNHibernate.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNHibernate.Size = new System.Drawing.Size(192, 115);
            this.tabPageNHibernate.TabIndex = 1;
            this.tabPageNHibernate.Text = "NHibernate";
            this.tabPageNHibernate.UseVisualStyleBackColor = true;
            // 
            // textBoxLog
            // 
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBoxLog.Location = new System.Drawing.Point(448, 243);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.Size = new System.Drawing.Size(200, 160);
            this.textBoxLog.TabIndex = 7;
            this.textBoxLog.WordWrap = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 421);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.buttonOpenPosition);
            this.Controls.Add(this.tabControlCreateModeChoose);
            this.Controls.Add(this.groupBoxChangeCheck);
            this.Controls.Add(this.groupBoxDatabases);
            this.Controls.Add(this.groupBoxDatatables);
            this.Controls.Add(this.GPBox_Connection);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.GPBox_Connection.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_windows.ResumeLayout(false);
            this.tabPage_windows.PerformLayout();
            this.tabPage_SqlServer.ResumeLayout(false);
            this.tabPage_SqlServer.PerformLayout();
            this.groupBoxDatatables.ResumeLayout(false);
            this.groupBoxDatabases.ResumeLayout(false);
            this.groupBoxChangeCheck.ResumeLayout(false);
            this.groupBoxGeneratechoose.ResumeLayout(false);
            this.groupBoxGeneratechoose.PerformLayout();
            this.groupBoxNHibernate.ResumeLayout(false);
            this.groupBoxNHibernate.PerformLayout();
            this.tabControlCreateModeChoose.ResumeLayout(false);
            this.tabPageInnerFactory.ResumeLayout(false);
            this.tabPageNHibernate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GPBox_Connection;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_windows;
        private System.Windows.Forms.TabPage tabPage_SqlServer;
        private System.Windows.Forms.Button button_Windows_connbtn;
        private System.Windows.Forms.TextBox textBox_Windows_Password;
        private System.Windows.Forms.TextBox textBox_Windows_LoginID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage_Access;
        private System.Windows.Forms.Button button_SQLServer_connbtn;
        private System.Windows.Forms.TextBox textBox_SQLServer_Password;
        private System.Windows.Forms.TextBox textBox_SQLServer_LoginID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxDatatables;
        private System.Windows.Forms.CheckedListBox checkedListBoxDatatables;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_Windows_Name;
        private System.Windows.Forms.TextBox textBox_SQLServer_Name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBoxDatabases;
        private System.Windows.Forms.ListBox listBoxDatabases;
        private System.Windows.Forms.Button button_Windows_closebtn;
        private System.Windows.Forms.Button button_SQLServer_closebtn;
        private System.Windows.Forms.GroupBox groupBoxChangeCheck;
        private System.Windows.Forms.Button buttonUnCheckAll;
        private System.Windows.Forms.Button buttonCheckInvert;
        private System.Windows.Forms.Button buttonCheckAll;
        private System.Windows.Forms.GroupBox groupBoxGeneratechoose;
        private System.Windows.Forms.CheckBox checkBoxDataAccess;
        private System.Windows.Forms.CheckBox checkBoxClassFactory;
        private System.Windows.Forms.CheckBox checkBoxBusinessRules;
        private System.Windows.Forms.Button buttonCreateFile;
        private System.Windows.Forms.GroupBox groupBoxNHibernate;
        private System.Windows.Forms.Button buttonNHibernateCreate;
        private System.Windows.Forms.CheckBox checkBoxNHDataXML;
        private System.Windows.Forms.CheckBox checkBoxNHEntity;
        private System.Windows.Forms.CheckBox checkBoxNHDataAccessLayer;
        private System.Windows.Forms.TabControl tabControlCreateModeChoose;
        private System.Windows.Forms.TabPage tabPageInnerFactory;
        private System.Windows.Forms.TabPage tabPageNHibernate;
        private System.Windows.Forms.Button buttonOpenPosition;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}

