using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Xml;

namespace UtilityCollection.Xml
{
    public class XmlProcess
    {
        #region 构造函数
        public XmlProcess()
        { }

        public XmlProcess(string strPath)
        {
            XmlPath = strPath;
            XmlLoad();
        }
        #endregion

        #region 公有属性

        public string XmlPath { get; private set; }

        public XmlDocument XmlDoc { get; set; }

        public bool XmlFileExist { get; set; }

        #endregion

        #region 私有方法

        /// <summary>
        /// 导入XML文件
        /// </summary>
        private void XmlLoad()
        {
            string xmlFile = XmlPath;
            var xmldoc = new XmlDocument();
            string filename = AppDomain.CurrentDomain.BaseDirectory + xmlFile;
            if (File.Exists(filename))
            {
                XmlFileExist = true;
                xmldoc.Load(filename);
                XmlDoc = xmldoc;
            }
            else
            {
                XmlFileExist = false;
            }
        }

        /// <summary>
        /// 导入XML文件
        /// </summary>
        private static XmlDocument XmlLoad(string strPath)
        {
            var xmldoc = new XmlDocument();
            var filename = AppDomain.CurrentDomain.BaseDirectory.ToString(CultureInfo.InvariantCulture) + strPath;
            if (File.Exists(filename))
            {
                xmldoc.Load(filename);
            }
            else
            {
                throw new Exception(string.Format("文件{0}不存在", filename));
            }
            return xmldoc;
        }

        /// <summary>
        /// 导入XML文件
        /// </summary>
        private static XmlDocument XmlLoadFromStream(string strXmlStream)
        {
            if (strXmlStream == null) throw new ArgumentNullException("strXmlStream");
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(strXmlStream);
            return xmldoc;
        }
        /// <summary>
        /// 返回完整路径
        /// </summary>
        /// <param name="strPath">Xml的路径</param>
        private static string GetXmlFullPath(string strPath)
        {
            return strPath.IndexOf(":", StringComparison.Ordinal) > 0 ? strPath : System.Web.HttpContext.Current.Server.MapPath(strPath);
        }

        #endregion

        #region 读取数据
        /// <summary>
        /// 读取指定节点的数据
        /// </summary>
        /// <param name="node">节点</param>
        /// 使用示列:
        /// XMLProsess.Read("/Node", "")
        /// XMLProsess.Read("/Node/Element[@Attribute='Name']")
        public string Read(string node)
        {
            var value = "";
            var xn = XmlDoc.SelectSingleNode(node);
            if (xn != null) value = xn.InnerText;
            return value;
        }

        /// <summary>
        /// 读取指定节点的数据
        /// </summary>
        /// <param name="strXMlStream">xml流</param>
        /// <param name="node">节点</param>
        /// 使用示列:
        /// XMLProsess.Read("/Node", "")
        /// XMLProsess.Read("/Node/Element[@Attribute='Name']")
        public string ReadNodeFromXmlStream(string strXMlStream, string node)
        {
            string value = "";
            var doc = XmlLoadFromStream(strXMlStream);
            var xn = doc.SelectSingleNode(node);
            if (xn != null) value = xn.InnerText;
            return value;
        }

        /// <summary>
        /// 读取指定节点的指定属性的值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="strAttributeName">属性名</param>
        /// 使用示列:
        /// XMLProsess.Read("/Node", "")
        /// XMLProsess.Read("/Node/Element[@Attribute='Name']")
        public string ReadAttribute(string node, string strAttributeName)
        {
            var value = "";
            var xn = XmlDoc.SelectSingleNode(node);
            if (xn != null)
            {
                value = (strAttributeName.Equals("") ? "" : (xn.Attributes[strAttributeName] != null ? xn.Attributes[strAttributeName].Value : ""));
            }
            return value;
        }

        /// <summary>
        /// 读取指定节点的指定属性的值
        /// </summary>
        /// <param name="strXMlStream">XML流</param>
        /// <param name="node">节点</param>
        /// <param name="strAttributeName">属性名</param>
        /// 使用示列:
        /// XMLProsess.Read("/Node", "")
        /// XMLProsess.Read("/Node/Element[@Attribute='Name']")
        public string ReadAttributeFromXmlStream(string strXMlStream, string node, string strAttributeName)
        {
            string value = "";
            var doc = XmlLoadFromStream(strXMlStream);
            var xn = doc.SelectSingleNode(node);
            if (xn != null)
            {
                value = (strAttributeName.Equals("") ? "" : (xn.Attributes[strAttributeName] != null ? xn.Attributes[strAttributeName].Value : ""));
            }
            return value;
        }
        /// <summary>
        /// 读取指定路径和节点的串联值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// 使用示列:
        /// XMLProsess.Read(path, "/Node", "")
        /// XMLProsess.Read(path, "/Node/Element[@Attribute='Name']")
        public static string Read(string path, string node)
        {
            var value = "";
            var doc = XmlLoad(path);
            var xn = doc.SelectSingleNode(node);
            if (xn != null) value = xn.InnerText;
            return value;
        }

        /// <summary>
        /// 读取指定路径和节点的属性值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// 使用示列:
        /// XMLProsess.Read(path, "/Node", "")
        /// XMLProsess.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
        public static string Read(string path, string node, string attribute)
        {
            var value = "";
            var doc = XmlLoad(path);
            var xn = doc.SelectSingleNode(node);
            if (xn != null) value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            return value;
        }

        /// <summary>
        /// 获取某一节点的所有孩子节点的值
        /// </summary>
        /// <param name="node">要查询的节点</param>
        public string[] ReadAllChildallValue(string node)
        {
            var i = 0;
            string[] str = { };
            var xn = XmlDoc.SelectSingleNode(node);
            if (xn != null)
            {
                var nodelist = xn.ChildNodes;  //得到该节点的子节点
                if (nodelist.Count > 0)
                {
                    str = new string[nodelist.Count];
                    foreach (XmlElement el in nodelist)//读元素值
                    {
                        str[i] = el.Value;
                        i++;
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// 获取某一节点的所有孩子节点的值
        /// </summary>
        public XmlDocument GetDocument()
        {
            return XmlDoc;
        }

        /// <summary>
        /// 获取某一节点的所有孩子节点的值
        /// </summary>
        /// <param name="node">要查询的节点</param>
        public XmlNodeList ReadAllChild(string node)
        {
            var xn = XmlDoc.SelectSingleNode(node);
            if (xn != null)
            {
                var nodelist = xn.ChildNodes;  //得到该节点的子节点
                return nodelist;
            }
            return null;
        }

        /// <summary>
        /// 获取特定子节点组
        /// </summary>
        /// <param name="strXpath">Xpath</param>
        public XmlNodeList ReadNodesByXpath(string strXpath)
        {
            var xn = XmlDoc.SelectNodes(strXpath);
            return xn;
        }

        /// <summary> 
        /// 读取XML返回经排序或筛选后的DataView
        /// </summary>
        /// <param name="strWhere">筛选条件，如:"name='kgdiwss'"</param>
        /// <param name="strSort"> 排序条件，如:"Id desc"</param>
        public DataView GetDataViewByXml(string strWhere, string strSort)
        {
            try
            {
                var xmlFile = this.XmlPath;
                var filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + xmlFile;
                var ds = new DataSet();
                ds.ReadXml(filename);
                var dv = new DataView(ds.Tables[0]); //创建DataView来完成排序或筛选操作	
                if (strSort != null)
                {
                    dv.Sort = strSort; //对DataView中的记录进行排序
                }
                if (strWhere != null)
                {
                    dv.RowFilter = strWhere; //对DataView中的记录进行筛选，找到我们想要的记录
                }
                return dv;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 读取XML返回DataSet
        /// </summary>
        /// <param name="strXmlPath">XML文件相对路径</param>
        public DataSet GetDataSetByXml(string strXmlPath)
        {
            try
            {
                var ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables.Count > 0)
                {
                    return ds;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 插入数据
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Insert(path, "/Node", "Element", "", "Value")
        /// XMLProsess.Insert(path, "/Node", "Element", "Attribute", "Value")
        /// XMLProsess.Insert(path, "/Node", "", "Attribute", "Value")
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            var doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + path);
            var xn = doc.SelectSingleNode(node);
            if (element.Equals(""))
            {
                if (!attribute.Equals(""))
                {
                    var xe = (XmlElement)xn;
                    if (xe != null) xe.SetAttribute(attribute, value);
                }
            }
            else
            {
                var xe = doc.CreateElement(element);
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                if (xn != null) xn.AppendChild(xe);
            }
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + path);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="strList">由XML属性名和值组成的二维数组</param>
        public static void Insert(string path, string node, string element, string[][] strList)
        {
            var doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + path);
            var xn = doc.SelectSingleNode(node);
            var xe = doc.CreateElement(element);
            var strAttribute = "";
            var strValue = "";
            foreach (string[] t in strList)
            {
                for (int j = 0; j < t.Length; j++)
                {
                    if (j == 0)
                        strAttribute = t[j];
                    else
                        strValue = t[j];
                }
                if (strAttribute.Equals(""))
                    xe.InnerText = strValue;
                else
                    xe.SetAttribute(strAttribute, strValue);
            }
            if (xn != null) xn.AppendChild(xe);
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + path);
        }

        /// <summary>
        /// 插入一行数据
        /// </summary>
        /// <param name="strXmlPath">XML文件相对路径</param>
        /// <param name="columns">要插入行的列名数组，如：string[] Columns = {"name","IsMarried"};</param>
        /// <param name="columnValue">要插入行每列的值数组，如：string[] ColumnValue={"XML大全","false"};</param>
        /// <returns>成功返回true,否则返回false</returns>
        public static bool WriteXmlByDataSet(string strXmlPath, string[] columns, string[] columnValue)
        {
            try
            {
                //根据传入的XML路径得到.XSD的路径，两个文件放在同一个目录下
                string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".", StringComparison.Ordinal)) + ".xsd";
                var ds = new DataSet();
                ds.ReadXmlSchema(GetXmlFullPath(strXsdPath)); //读XML架构，关系到列的数据类型
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                var dt = ds.Tables[0];
                var newRow = dt.NewRow();                 //在原来的表格基础上创建新行
                for (int i = 0; i < columns.Length; i++)      //循环给一行中的各个列赋值
                {
                    newRow[columns[i]] = columnValue[i];
                }
                dt.Rows.Add(newRow);
                dt.AcceptChanges();
                ds.AcceptChanges();
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改指定节点的数据
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="value">值</param>
        public void Update(string node, string value)
        {
            var xn = XmlDoc.SelectSingleNode(node);
            if (xn != null) xn.InnerText = value;
            XmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + XmlPath);
        }

        /// <summary>
        /// 修改指定节点的数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Insert(path, "/Node","Value")
        /// XMLProsess.Insert(path, "/Node","Value")
        public static void Update(string path, string node, string value)
        {
            var doc = XmlLoad(path);
            var xn = doc.SelectSingleNode(node);
            if (xn != null) xn.InnerText = value;
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + path);
        }

        /// <summary>
        /// 修改指定节点的属性值(静态)
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Insert(path, "/Node", "", "Value")
        /// XMLProsess.Insert(path, "/Node", "Attribute", "Value")
        public static void Update(string path, string node, string attribute, string value)
        {
            var doc = XmlLoad(path);
            var xn = doc.SelectSingleNode(node);
            var xe = (XmlElement)xn;
            if (attribute.Equals(""))
            {
                if (xe != null) xe.InnerText = value;
            }
            else if (xe != null) xe.SetAttribute(attribute, value);
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + path);
        }

        /// <summary>
        /// 修改指定节点的指定属性的值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="strAttributeName"></param>
        /// <param name="strValue"></param>
        /// 使用示列:
        /// XMLProsess.Read("/Node", "")
        /// XMLProsess.Read("/Node/Element[@Attribute='Name']")
        public void WriteAttribute(string node, string strAttributeName, string strValue)
        {
            var xn = XmlDoc.SelectSingleNode(node);
            if (xn != null)
            {
                if (xn.Attributes[strAttributeName] != null)
                {
                    xn.Attributes[strAttributeName].Value = strValue;
                }
                else
                {
                    var objXmlAttribute = XmlDoc.CreateAttribute(strAttributeName);
                    objXmlAttribute.Value = strValue;
                    xn.Attributes.Append(objXmlAttribute);
                }
                XmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + XmlPath);
            }
        }

        /// <summary>
        /// 更改符合条件的一条记录
        /// </summary>
        /// <param name="strXmlPath">XML文件路径</param>
        /// <param name="columns">列名数组</param>
        /// <param name="columnValue">列值数组</param>
        /// <param name="strWhereColumnName">条件列名</param>
        /// <param name="strWhereColumnValue">条件列值</param>
        public static bool UpdateXmlRow(string strXmlPath, string[] columns, string[] columnValue, string strWhereColumnName, string strWhereColumnValue)
        {
            try
            {
                var strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".", StringComparison.Ordinal)) + ".xsd";
                var ds = new DataSet();
                ds.ReadXmlSchema(GetXmlFullPath(strXsdPath));//读XML架构，关系到列的数据类型
                ds.ReadXml(GetXmlFullPath(strXmlPath));

                //先判断行数
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //如果当前记录为符合Where条件的记录
                        if (ds.Tables[0].Rows[i][strWhereColumnName].ToString().Trim().Equals(strWhereColumnValue))
                        {
                            //循环给找到行的各列赋新值
                            for (int j = 0; j < columns.Length; j++)
                            {
                                ds.Tables[0].Rows[i][columns[j]] = columnValue[j];
                            }
                            ds.AcceptChanges();                     //更新DataSet
                            ds.WriteXml(GetXmlFullPath(strXmlPath));//重新写入XML文件
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除节点值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// 使用示列:
        /// XMLProsess.Delete(path, "/Node", "")
        /// XMLProsess.Delete(path, "/Node", "Attribute")
        public static void Delete(string path, string node)
        {
            var doc = XmlLoad(path);
            var xn = doc.SelectSingleNode(node);
            if (xn != null) if (xn.ParentNode != null) xn.ParentNode.RemoveChild(xn);
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + path);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// 使用示列:
        /// XMLProsess.Delete(path, "/Node", "")
        /// XMLProsess.Delete(path, "/Node", "Attribute")
        public static void Delete(string path, string node, string attribute)
        {
            var doc = XmlLoad(path);
            var xn = doc.SelectSingleNode(node);
            var xe = (XmlElement)xn;
            if (attribute.Equals(""))
            {
                if (xn != null) if (xn.ParentNode != null) xn.ParentNode.RemoveChild(xn);
            }
            else if (xe != null) xe.RemoveAttribute(attribute);
            doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
        }

        /// <summary>
        /// 删除所有行
        /// </summary>
        /// <param name="strXmlPath">XML路径</param>
        public static bool DeleteXmlAllRows(string strXmlPath)
        {
            try
            {
                var ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.Clear();
                }
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 通过删除DataSet中指定索引行，重写XML以实现删除指定行
        /// </summary>
        /// <param name="strXmlPath"></param>
        /// <param name="iDeleteRow">要删除的行在DataSet中的Index值</param>
        public static bool DeleteXmlRowByIndex(string strXmlPath, int iDeleteRow)
        {
            try
            {
                var ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows[iDeleteRow].Delete();
                }
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定列中指定值的行
        /// </summary>
        /// <param name="strXmlPath">XML相对路径</param>
        /// <param name="strColumn">列名</param>
        /// <param name="columnValue">指定值</param>
        public static bool DeleteXmlRows(string strXmlPath, string strColumn, string[] columnValue)
        {
            try
            {
                var ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //判断行多还是删除的值多，多的for循环放在里面
                    if (columnValue.Length > ds.Tables[0].Rows.Count)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            foreach (var t in columnValue)
                            {
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(t))
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var t in columnValue)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(t))
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                    }
                    ds.WriteXml(GetXmlFullPath(strXmlPath));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}