using System;
using System.Text;
using System.Data;
using OWCChart;
using UtilityCollection.Config;

namespace UtilityCollection.Chart
{
    /// <summary>
    /// Assistant 的摘要说明。
    /// </summary>
    public sealed class Assistant
    {
        #region 创建显示图像的标签

        /// <summary>
        /// 创建显示图像的标签(flash加点击)
        /// </summary>
        public static string CreateTag(string adid, string filename, string desc, string fileType, string linkUrl, int width, int high)
        {
            var tagStr = new StringBuilder();
            switch (fileType)
            {
                case "image/gif":
                case "image/bmp":
                case "image/pjpeg":
                    {
                        if ((linkUrl.Trim() != "") && (linkUrl.Trim() != "http://"))//非空
                        {
                            tagStr.Append("<a href=\"");
                            tagStr.Append(ConfigHelper.GetConfigString("URL") + "/FormAdHit.aspx?ADID=" + adid);
                            tagStr.Append("&LinkURL=" + linkUrl.Replace("&", "$$$"));
                            tagStr.Append("\"");
                            tagStr.Append(" target=\"_blank\">");
                        }
                        tagStr.Append(" <IMG alt=\"" + desc + "\"");
                        tagStr.Append(" src=\"" + filename + "\"");
                        tagStr.Append(" width=\"" + width + "\" height=\"" + high + "\" ");
                        tagStr.Append(" border=\"0\">");
                        if ((linkUrl.Trim() != "") && (linkUrl.Trim() != "http://"))
                        {
                            tagStr.Append("</a>");
                        }
                        break;
                    }

                case "application/x-shockwave-flash":
                    {
                        //					TagStr.Append("<object ");
                        ////					TagStr.Append(" width="+Width+" height="+High+" ");
                        //					TagStr.Append("  classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"  ");
                        //					TagStr.Append(" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\"> ");
                        ////					TagStr.Append(" <param name=\"movie\" value=\""+filename+"?clickthru=");
                        ////					TagStr.Append("FormAdHit.aspx?ADID="+ADID);
                        ////					TagStr.Append("_LinkURL="+LinkURL);
                        ////					TagStr.Append("\"> ");					
                        //					TagStr.Append(" <param name=\"wmode\" value=\"opaque\"> ");
                        //					TagStr.Append(" <param name=\"quality\" value=\"autohigh\"> ");
                        //					
                        //					TagStr.Append(" <embed  ");
                        //					TagStr.Append(" width="+Width+" height="+High+"  ");
                        //					TagStr.Append(" src=\""+filename+"?clickthru=");
                        //					TagStr.Append("FormAdHit.aspx?ADID="+ADID);
                        //					if((LinkURL.Trim()!="")&&(LinkURL.Trim()!="http://"))
                        //					{
                        //						TagStr.Append("_LinkURL="+LinkURL);
                        //					}
                        //					TagStr.Append("\"  ");	
                        //					TagStr.Append(" quality=\"high\" wmode=\"opaque\" type=\"application/x-shockwave-flash\"  ");
                        //					TagStr.Append(" plugspace=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"> ");
                        //					TagStr.Append(" </embed></object> ");

                        tagStr.Append(" <embed ");
                        tagStr.Append(" src=\"" + filename + "\" ");
                        //					TagStr.Append(" src=\""+filename+"?clickthru=");
                        //					TagStr.Append("FormAdHit.aspx?ADID="+ADID);
                        //					if((LinkURL.Trim()!="")&&(LinkURL.Trim()!="http://"))
                        //					{
                        //						TagStr.Append("_LinkURL="+LinkURL);
                        //					}
                        //					TagStr.Append("\"  ");	
                        tagStr.Append(" width=" + width + " height=" + high + "  ");
                        tagStr.Append(" quality=\"high\" ");
                        tagStr.Append(" ></embed>");

                    }

                    break;

                case "video/x-ms-wmv":
                case "video/mpeg":
                case "video/x-ms-asf":
                case "video/avi":
                case "audio/mpeg":
                case "audio/mid":
                case "audio/wav":
                case "audio/x-ms-wma":
                    tagStr.Append("<embed");
                    tagStr.Append(" src=\"" + filename + "\" border=\"0\" ");
                    tagStr.Append(" width=\"" + width + "\" height=\"" + high + "\"");
                    tagStr.Append(" autoStart=\"1\" playCount=\"0\" enableContextMenu=\"0\"");
                    tagStr.Append(" type=\"application/x-mplayer2\"></embed>");
                    break;

                default:
                    tagStr.Append("不允许该格式文件显示！");
                    break;
            }

            return tagStr.ToString();

        }


        /// <summary>
        /// 创建显示图像的标签(flash无点击)
        /// </summary>		
        public static string CreateTag2(string adid, string filename, string desc, string fileType, string linkUrl, int width, int high)
        {
            var tagStr = new StringBuilder();
            switch (fileType)
            {
                case "image/gif":
                case "image/bmp":
                case "image/pjpeg":
                    {
                        tagStr.Append("<a href=\"");
                        tagStr.Append(ConfigHelper.GetConfigString("URL") + "\\FormAdHit.aspx?ADID=" + adid);
                        tagStr.Append("&LinkURL=" + linkUrl);
                        tagStr.Append("\"");
                        tagStr.Append(" target=\"_blank\">");
                        tagStr.Append(" <IMG alt=\"" + desc + "\"");
                        tagStr.Append(" src=\"" + filename + "\"");
                        tagStr.Append(" width=\"" + width + "\" height=\"" + high + "\" ");
                        tagStr.Append(" border=\"0\">");
                        tagStr.Append("</a>");
                        break;
                    }

                case "application/x-shockwave-flash":
                    {
                        //					TagStr.Append("<a href=\"");
                        //					TagStr.Append(LinkURL);
                        //					TagStr.Append("FormAdHit.aspx?ADID="+ADID);
                        //					TagStr.Append("&LinkURL="+LinkURL);
                        //					TagStr.Append("\"");
                        //					TagStr.Append(" target=\"_blank\">");

                        tagStr.Append(" <embed src=\"" + filename + "\" ");
                        tagStr.Append(" quality=\"high\" bgcolor=\"#f5f5f5\" ");
                        tagStr.Append(" ></embed>");

                        //					TagStr.Append("</a>");
                    }

                    break;

                case "video/x-ms-wmv":
                case "video/mpeg":
                case "video/x-ms-asf":
                case "video/avi":
                case "audio/mpeg":
                case "audio/mid":
                case "audio/wav":
                case "audio/x-ms-wma":

                    //					TagStr.Append("<a href=\"");
                    //					TagStr.Append(LinkURL);
                    //					TagStr.Append("FormAdHit.aspx?ADID="+ADID);
                    //					TagStr.Append("&LinkURL="+LinkURL);
                    //					TagStr.Append("\"");
                    //					TagStr.Append(" target=\"_blank\">");
                    tagStr.Append("<embed");
                    tagStr.Append(" src=\"" + filename + "\" border=\"0\" ");
                    tagStr.Append(" width=\"" + width + "\" height=\"" + high + "\"");
                    tagStr.Append(" autoStart=\"1\" playCount=\"0\" enableContextMenu=\"0\"");
                    tagStr.Append(" type=\"application/x-mplayer2\"></embed>");
                    //					TagStr.Append("</a>");


                    break;

                default:
                    tagStr.Append("不允许该格式文件显示！");
                    break;
            }

            return tagStr.ToString();

        }


        /// <summary>
        /// 创建显示图像的标签(重载)，无宽高限制，(flash加点击)
        /// </summary>
        public static string CreateTag(string adid, string filename, string desc, string fileType, string linkUrl)
        {
            var tagStr = new StringBuilder();
            switch (fileType)
            {
                case "image/gif":
                case "image/bmp":
                case "image/pjpeg":
                    {
                        tagStr.Append("<a href=\"");
                        tagStr.Append(ConfigHelper.GetConfigString("URL") + "\\FormAdHit.aspx?ADID=" + adid);
                        tagStr.Append("&LinkURL=" + linkUrl);
                        tagStr.Append("\"");
                        tagStr.Append(" target=\"_blank\">");
                        tagStr.Append(" <IMG alt=\"" + desc + "\"");
                        tagStr.Append(" src=\"" + filename + "\"");
                        //					TagStr.Append(" width=\""+Width+"\" height=\""+High+"\" ");
                        tagStr.Append(" border=\"0\">");
                        tagStr.Append("</a>");
                        break;
                    }

                case "application/x-shockwave-flash":
                    {
                        tagStr.Append("<object ");
                        //					TagStr.Append(" width="+Width+" height="+High+" ");
                        tagStr.Append("  classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"  ");
                        tagStr.Append(" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\"> ");
                        //					TagStr.Append(" <param name=\"movie\" value=\""+filename+"?clickthru=");
                        //					TagStr.Append("FormAdHit.aspx?ADID="+ADID);
                        //					TagStr.Append("_LinkURL="+LinkURL);
                        //					TagStr.Append("\"> ");					
                        tagStr.Append(" <param name=\"wmode\" value=\"opaque\"> ");
                        tagStr.Append(" <param name=\"quality\" value=\"autohigh\"> ");
                        tagStr.Append(" <embed  ");
                        //					TagStr.Append(" width="+Width+" height="+High+"  ");
                        tagStr.Append(" src=\"" + filename + "?clickthru=");
                        tagStr.Append(ConfigHelper.GetConfigString("URL") + "\\FormAdHit.aspx?ADID=" + adid);
                        tagStr.Append("_LinkURL=" + linkUrl);
                        tagStr.Append("\"  ");
                        tagStr.Append(" quality=\"autohigh\" wmode=\"opaque\" type=\"application/x-shockwave-flash\"  ");
                        tagStr.Append(" plugspace=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"> ");
                        tagStr.Append(" </embed></object> ");
                    }

                    break;

                case "video/x-ms-wmv":
                case "video/mpeg":
                case "video/x-ms-asf":
                case "video/avi":
                case "audio/mpeg":
                case "audio/mid":
                case "audio/wav":
                case "audio/x-ms-wma":
                    tagStr.Append("<embed");
                    tagStr.Append(" src=\"" + filename + "\" border=\"0\" ");
                    //					TagStr.Append(" width=\""+Width+"\" height=\""+High+"\"");	
                    tagStr.Append(" autoStart=\"1\" playCount=\"0\" enableContextMenu=\"0\"");
                    tagStr.Append(" type=\"application/x-mplayer2\"></embed>");


                    break;
            }

            return tagStr.ToString();

        }


        /// <summary>
        /// 创建显示图像的标签(重载)，无宽高限制，(flash无点击)
        /// </summary>
        public static string CreateTag2(string adid, string filename, string desc, string fileType, string linkUrl)
        {
            var tagStr = new StringBuilder();
            switch (fileType)
            {
                case "image/gif":
                case "image/bmp":
                case "image/pjpeg":
                    {
                        tagStr.Append("<a href=\"");
                        tagStr.Append("FormAdHit.aspx?ADID=" + adid);
                        tagStr.Append("&LinkURL=" + linkUrl);
                        tagStr.Append("\"");
                        tagStr.Append(" target=\"_blank\">");
                        tagStr.Append(" <IMG alt=\"" + desc + "\"");
                        tagStr.Append(" src=\"" + filename + "\"");
                        //					TagStr.Append(" width=\""+Width+"\" height=\""+High+"\" ");
                        tagStr.Append(" border=\"0\">");
                        tagStr.Append("</a>");
                        break;
                    }

                case "application/x-shockwave-flash":
                    {
                        tagStr.Append(" <embed src=\"" + filename + "\" ");
                        tagStr.Append(" quality=\"high\" bgcolor=\"#f5f5f5\" ");
                        tagStr.Append(" ></embed>");
                    }

                    break;

                case "video/x-ms-wmv":
                case "video/mpeg":
                case "video/x-ms-asf":
                case "video/avi":
                case "audio/mpeg":
                case "audio/mid":
                case "audio/wav":
                case "audio/x-ms-wma":
                    tagStr.Append("<embed");
                    tagStr.Append(" src=\"" + filename + "\" border=\"0\" ");
                    //					TagStr.Append(" width=\""+Width+"\" height=\""+High+"\"");	
                    tagStr.Append(" autoStart=\"1\" playCount=\"0\" enableContextMenu=\"0\"");
                    tagStr.Append(" type=\"application/x-mplayer2\"></embed>");


                    break;
            }

            return tagStr.ToString();

        }


        #region
        /// <summary>
        /// 创建显示图像的标签
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="desc"></param>
        /// <param name="fileType"></param>
        /// <param name="linkUrl"></param>
        /// <param name="width"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static string CreateTagOld(string filename, string desc, string fileType, string linkUrl, int width, int high)
        {
            var tagStr = new StringBuilder();
            switch (fileType)
            {
                case "image/gif":
                case "image/bmp":
                case "image/pjpeg":
                    {
                        tagStr.Append("<a href=\"");
                        tagStr.Append(linkUrl);
                        tagStr.Append("\"");
                        tagStr.Append(" target=\"_blank\">");
                        tagStr.Append(" <IMG alt=\"" + desc + "\"");
                        tagStr.Append(" src=\"" + filename + "\"");
                        tagStr.Append(" width=\"" + width + "\" height=\"" + high + "\" border=\"0\">");
                        tagStr.Append("</a>");
                        break;
                    }

                case "application/x-shockwave-flash":
                    {
                        tagStr.Append("<a href=\"");
                        tagStr.Append(linkUrl);
                        tagStr.Append("\"");
                        tagStr.Append(" target=\"_blank\">");
                        tagStr.Append(" <embed src=\"" + filename + "\" ");
                        tagStr.Append(" quality=\"high\" bgcolor=\"#f5f5f5\"");
                        tagStr.Append(" ></embed>");

                        //					TagStr.Append(" <embed src=\""+filename+"\" ");		
                        //					TagStr.Append("pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"");					
                        //					TagStr.Append(" type=\"application/x-shockwave-flash\"");
                        //					TagStr.Append(" width=\""+Width+"\" height=\""+High+"\"");
                        //					TagStr.Append(" play=\"true\" loop=\"true\" quality=\"high\" scale=\"showall\" ");					
                        //					TagStr.Append(" ></embed>");

                        tagStr.Append("</a>");
                    }

                    break;

                case "video/x-ms-wmv":
                case "video/mpeg":
                case "video/x-ms-asf":
                case "video/avi":
                case "audio/mpeg":
                case "audio/mid":
                case "audio/wav":
                case "audio/x-ms-wma":
                    //					TagStr.Append("<a href=\"");
                    //					TagStr.Append(LinkURL);
                    //					TagStr.Append("\"");
                    //					TagStr.Append(" target=\"_blank\">");
                    //					TagStr.Append("<OBJECT  classid=\"clsid:6BF52A52-394A-11D3-B153-00C04F79FAA6\" VIEWASTEXT>");
                    //					TagStr.Append("<PARAM NAME=\"URL\" VALUE=\""+filename+"\">");
                    //					TagStr.Append("<PARAM NAME=\"autoStart\" VALUE=\"1\">");
                    //					TagStr.Append("<PARAM NAME=\"enableContextMenu\" VALUE=\"0\" ></OBJECT>");	
                    //					TagStr.Append("</a>");

                    tagStr.Append("<a href=\"");
                    tagStr.Append(linkUrl);
                    tagStr.Append("\"");
                    tagStr.Append(" target=\"_blank\">");
                    tagStr.Append("<embed");
                    tagStr.Append(" src=\"" + filename + "\" border=\"0\" width=\"" + width + "\" height=\"" + high + "\"");
                    tagStr.Append(" autoStart=\"1\" playCount=\"0\" enableContextMenu=\"0\"");
                    tagStr.Append(" type=\"application/x-mplayer2\"></embed>");
                    tagStr.Append("</a>");


                    break;

                default://其他类型作为附件链接下载
                    tagStr.Append("不允许该格式文件显示！");
                    break;
            }

            return tagStr.ToString();

        }

        #endregion

        #endregion

        #region	 创建数据图形文件

        /// <summary>
        /// 创建数据图形文件
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="charType">图类型 Column,Pie</param>
        /// <param name="imagePath">图像存放目录</param>
        /// <param name="title">图形标题</param>
        /// <returns></returns>
        public static string CreateChart(DataTable dt, string charType, string imagePath, string title)
        {
            var phaysicalImagePath = imagePath;
            var mychart = new OWCChartFactory(title, phaysicalImagePath, 530, 300, new OWCChartFontStyle());
            var myItem = new OWCSeriesClass {SeriesName = "次数"};

            myItem.SetDataSource(dt, "Item", "Value");
            switch (charType)
            {
                case "Column":
                    mychart.CreateOneColumn("时间", "次", myItem);
                    break;
                case "Pie":
                    mychart.CreateSinglePie(myItem);
                    break;

            }
            var imageName = mychart.ExportPictuire();
            return imageName;
            //			Image1.ImageUrl = ".\\"+m_imagePath+imageName;

        }
        public static string CreateMultiColumns(DataTable[] dts, string imagePath, string title)
        {
            var phaysicalImagePath = imagePath;
            var mychart = new OWCChartFactory(title, phaysicalImagePath, 530, 300, new OWCChartFontStyle());
            var myItems = new OWCSeriesClass[dts.Length];

            myItems[0] = new OWCSeriesClass {SeriesName = "显示次数"};
            myItems[0].SetDataSource(dts[0], "Item", "Value");

            myItems[1] = new OWCSeriesClass {SeriesName = "点击次数"};
            myItems[1].SetDataSource(dts[1], "Item", "Value");


            mychart.CreateMultiColumns("时间", "次", myItems);


            String imageName = mychart.ExportPictuire();
            return imageName;

        }

        public static string CreateSingleBar(DataTable dt, string charType, string imagePath, string title)
        {
            var phaysicalImagePath = imagePath;
            var mychart = new OWCChartFactory(title, phaysicalImagePath, 500, 600, new OWCChartFontStyle());
            var myItem = new OWCSeriesClass {SeriesName = "次数"};

            myItem.SetDataSource(dt, "Item", "Value");
            mychart.CreateSingleBar(" ", "", myItem);
            var imageName = mychart.ExportPictuire();
            return imageName;
        }
        public static string CreateMultiBar(DataTable[] dts, string imagePath, string title)
        {
            var phaysicalImagePath = imagePath;
            var mychart = new OWCChartFactory(title, phaysicalImagePath, 500, 600, new OWCChartFontStyle());
            var myItems = new OWCSeriesClass[dts.Length];

            myItems[0] = new OWCSeriesClass {SeriesName = "显示次数"};
            myItems[0].SetDataSource(dts[0], "Item", "Value");

            myItems[1] = new OWCSeriesClass {SeriesName = "点击次数"};
            myItems[1].SetDataSource(dts[1], "Item", "Value");


            mychart.CreateMultiBar(" ", "", myItems);


            var imageName = mychart.ExportPictuire();
            return imageName;

        }

        #endregion

        #region

        /// <summary>
        /// 从字符串里随机得到，规定个数的字符串.
        /// </summary>
        /// <param name="allChar"></param>
        /// <param name="codeCount"></param>
        /// <returns></returns>
        private string GetRandomCode(string allChar, int codeCount)
        {
            //string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"; 
            var allCharArray = allChar.Split(',');
            var randomCode = "";
            var temp = -1;
            var rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                var t = rand.Next(allCharArray.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }

                temp = t;
                randomCode += allCharArray[t];
            }

            return randomCode;
        }

        #endregion


    }
}
