/******************************************* 
* 说明：查询纯真IP数据库，数据库来自CZ88.NET(纯真网络)，感谢 LumaQQ 作者：http://lumaqq.linuxsir.org/article/qqwry_format_detail.html 
* 作者：孙宇 
* 日期：2011/11/08 

* 修改：卢志涛
* 日期：2013/06/017
/*******************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace IPLibrary.QQIP
{
    /// <summary>  
    /// 存储地区的结构  
    /// </summary>  
    public struct stLocation
    {
        /// <summary>  
        /// 未使用  
        /// </summary>  
        public string Ip;

        /// <summary>  
        /// 国家名  
        /// </summary>  
        public string Contry;

        /// <summary>  
        /// 城市名  
        /// </summary>  
        public string City;
    }


    /// <summary>  
    /// 纯真IP数据库查询辅助类  
    /// </summary>  
    public static class QQIPHelper
    {
        #region 成员变量

        private const byte REDIRECT_MODE_1 = 0x01;//名称存储模式一  
        private const byte REDIRECT_MODE_2 = 0x02;//名称存储模式二  
        private const int IP_RECORD_LENGTH = 7; //每条索引的长度  

        private static long beginIndex = 0;//索引开始  
        private static long endIndex = 0;//索引结束  

        private static stLocation loc = new stLocation() { City = "未知城市", Contry = "未知国家" };

        private static Stream fs;

        #endregion

        #region 私有成员函数

        /// <summary>  
        /// 在索引区查找指定IP对应的记录区地址  
        /// </summary>  
        /// <param name="_ip">字节型IP</param>  
        /// <returns></returns>  
        private static long SearchIpIndex(byte[] _ip)
        {
            long index = 0;

            byte[] nextIp = new byte[4];

            ReadIp(beginIndex, ref nextIp);

            int flag = CompareIp(_ip, nextIp);
            if (flag == 0) return beginIndex;
            else if (flag < 0) return -1;

            for (long i = beginIndex, j = endIndex; i < j; )
            {
                index = GetMiddleOffset(i, j);

                ReadIp(index, ref nextIp);
                flag = CompareIp(_ip, nextIp);

                if (flag == 0) return ReadLong(index + 4, 3);
                else if (flag > 0) i = index;
                else if (flag < 0)
                {
                    if (index == j)
                    {
                        j -= IP_RECORD_LENGTH;
                        index = j;
                    }
                    else
                    {
                        j = index;
                    }
                }
            }

            index = ReadLong(index + 4, 3);
            ReadIp(index, ref nextIp);

            flag = CompareIp(_ip, nextIp);
            if (flag <= 0) return index;
            else return -1;
        }

        /// <summary>  
        /// 获取两个索引的中间位置  
        /// </summary>  
        /// <param name="begin">索引1</param>  
        /// <param name="end">索引2</param>  
        /// <returns></returns>  
        private static long GetMiddleOffset(long begin, long end)
        {
            long records = (end - begin) / IP_RECORD_LENGTH;
            records >>= 1;
            if (records == 0) records = 1;
            return begin + records * IP_RECORD_LENGTH;
        }

        /// <summary>  
        /// 读取记录区的地区名称  
        /// </summary>  
        /// <param name="offset">位置</param>  
        /// <returns></returns>  
        private static string ReadString(long offset)
        {
            fs.Position = offset;

            byte b = (byte)fs.ReadByte();
            if (b == REDIRECT_MODE_1 || b == REDIRECT_MODE_2)
            {
                long areaOffset = ReadLong(offset + 1, 3);
                if (areaOffset == 0)
                    return "未知地区";

                else fs.Position = areaOffset;
            }
            else
            {
                fs.Position = offset;
            }

            List<byte> buf = new List<byte>();

            int i = 0;
            for (i = 0, buf.Add((byte)fs.ReadByte()); buf[i] != (byte)(0); ++i, buf.Add((byte)fs.ReadByte())) ;

            if (i > 0) return Encoding.Default.GetString(buf.ToArray(), 0, i);
            else return "";
        }

        /// <summary>  
        /// 从自定位置读取指定长度的字节，并转换为big-endian字节序(数据源文件为little-endian字节序)  
        /// </summary>  
        /// <param name="offset">开始读取位置</param>  
        /// <param name="length">读取长度</param>  
        /// <returns></returns>  
        private static long ReadLong(long offset, int length)
        {
            long ret = 0;
            fs.Position = offset;
            for (int i = 0; i < length; i++)
            {
                ret |= ((fs.ReadByte() << (i * 8)) & (0xFF * ((int)Math.Pow(16, i * 2))));
            }

            return ret;
        }

        /// <summary>  
        /// 从指定位置处读取一个IP  
        /// </summary>  
        /// <param name="offset">指定的位置</param>  
        /// <param name="_buffIp">保存IP的缓存区</param>  
        private static void ReadIp(long offset, ref byte[] _buffIp)
        {
            fs.Position = offset;
            fs.Read(_buffIp, 0, _buffIp.Length);

            for (int i = 0; i < _buffIp.Length / 2; i++)
            {
                byte temp = _buffIp[i];
                _buffIp[i] = _buffIp[_buffIp.Length - i - 1];
                _buffIp[_buffIp.Length - i - 1] = temp;
            }
        }

        /// <summary>  
        /// 比较两个IP是否相等，1:IP1大于IP2，-1：IP1小于IP2，0：IP1=IP2  
        /// </summary>  
        /// <param name="_buffIp1">IP1</param>  
        /// <param name="_buffIp2">IP2</param>  
        /// <returns></returns>  
        private static int CompareIp(byte[] _buffIp1, byte[] _buffIp2)
        {
            if (_buffIp1.Length > 4 || _buffIp2.Length > 4) throw new Exception("指定的IP无效。");

            for (int i = 0; i < 4; i++)
            {
                if ((_buffIp1[i] & 0xFF) > (_buffIp2[i] & 0xFF)) return 1;
                else if ((_buffIp1[i] & 0xFF) < (_buffIp2[i] & 0xFF)) return -1;
            }

            return 0;
        }

        /// <summary>  
        /// 从指定的地址获取区域名称  
        /// </summary>  
        /// <param name="offset"></param>  
        private static void GetAreaName(long offset)
        {
            fs.Position = offset + 4;
            long flag = fs.ReadByte();
            long contryIndex = 0;
            if (flag == REDIRECT_MODE_1)
            {
                contryIndex = ReadLong(fs.Position, 3);
                fs.Position = contryIndex;

                flag = fs.ReadByte();

                if (flag == REDIRECT_MODE_2)    //是否仍然为重定向  
                {
                    loc.Contry = ReadString(ReadLong(fs.Position, 3));
                    fs.Position = contryIndex + 4;
                }
                else
                {
                    loc.Contry = ReadString(contryIndex);
                }
                loc.City = ReadString(fs.Position);
            }
            else if (flag == REDIRECT_MODE_2)
            {
                contryIndex = ReadLong(fs.Position, 3);
                loc.Contry = ReadString(contryIndex);
                loc.City = ReadString(contryIndex + 3);
            }
            else
            {
                loc.Contry = ReadString(offset + 4);
                loc.City = ReadString(fs.Position);
            }
        }

        #endregion

        #region 公有成员函数

        /// <summary>  
        /// 加载数据库文件到缓存  
        /// </summary>  
        /// <param name="path">数据库文件地址</param>  
        /// <returns></returns>  
        public static void Init(string path)
        {
            fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /// <summary>  
        /// 根据IP获取区域名  
        /// </summary>  
        /// <param name="ip">指定的IP</param>  
        /// <returns></returns>  
        public static stLocation GetLocation(string ip)
        {
            IPAddress ipAddress = null;
            if (!IPAddress.TryParse(ip, out ipAddress)) throw new Exception("无效的IP地址。");

            byte[] buff_local_ip = ipAddress.GetAddressBytes();

            beginIndex = ReadLong(0, 4);
            endIndex = ReadLong(4, 4);

            long offset = SearchIpIndex(buff_local_ip);
            if (offset != -1)
            {
                GetAreaName(offset);
            }

            loc.Contry = loc.Contry.Trim();
            loc.City = loc.City.Trim().Replace("CZ88.NET", "");

            return loc;
        }

        /// <summary>  
        /// 释放资源  
        /// </summary>  
        public static void Dispose()
        {
            fs.Dispose();
        }

        #endregion
    }
}
