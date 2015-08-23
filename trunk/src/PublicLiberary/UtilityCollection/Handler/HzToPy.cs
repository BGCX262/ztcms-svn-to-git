using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityCollection.Handler
{
    /// <summary>
    /// 作用：汉字转拼音类
    /// 日期：2011-11-11
    /// </summary>
    public class HzToPy
    {
        /// <summary>
        /// 获取简体中文首个汉字的拼音首字母
        /// </summary>
        /// <param name="cn">简体中文字</param>
        /// <returns>拼音首字母</returns>
        public static string getSpellByFirst(string cnStr)
        {
            string en = "";
            List<string> list = new List<string>();
            if (cnStr.Length > 1)
            {
                list.Add(cnStr.Substring(0, 1));
            }
            else if (cnStr.Length == 1)
            {
                list.Add(cnStr);
            }
            foreach (string cn in list)
            {
                byte[] arrCN = Encoding.Default.GetBytes(cn);
                if (arrCN.Length > 1)
                {
                    int area = (short)arrCN[0];
                    int pos = (short)arrCN[1];
                    int code = (area << 8) + pos;
                    int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                    for (int i = 0; i < 26; i++)
                    {
                        int max = 55290;
                        if (i != 25) max = areacode[i + 1];
                        if (areacode[i] <= code && code < max)
                        {
                            en += Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                        }
                    }
                }
                else en += cn;
            }
            return en;
        }

        /// <summary>
        /// 获取简体中文字的拼音首字母
        /// </summary>
        /// <param name="cn">简体中文字</param>
        /// <returns>拼音首字母</returns>
        public static string getSpell(string cnStr)
        {
            string en = "";
            List<string> list = new List<string>();
            if (cnStr.Length > 1)
            {
                for (int i = 0; i < cnStr.Length; i++)
                {
                    list.Add(cnStr.Substring(i, 1));
                }
            }
            foreach (string cn in list)
            {
                byte[] arrCN = Encoding.Default.GetBytes(cn);
                if (arrCN.Length > 1)
                {
                    int area = (short)arrCN[0];
                    int pos = (short)arrCN[1];
                    int code = (area << 8) + pos;
                    int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                    for (int i = 0; i < 26; i++)
                    {
                        int max = 55290;
                        if (i != 25) max = areacode[i + 1];
                        if (areacode[i] <= code && code < max)
                        {
                            en += Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                        }
                    }
                }
                else en += cn;
            }
            return en;
        }





        /// <summary> 
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串 
        /// </summary> 
        /// <param name="CnStr">汉字字符串</param> 
        /// <returns>相对应的汉语拼音首字母串</returns> 
        public static string GetSpellCode(string CnStr)
        {
            string strTemp = "";
            int iLen = CnStr.Length;
            int i = 0;

            for (i = 0; i <= iLen - 1; i++)
            {
                strTemp += GetCharSpellCode(CnStr.Substring(i, 1));
            }

            return strTemp;
        }


        /// <summary> 
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母 
        /// </summary> 
        /// <param name="CnChar">单个汉字</param> 
        /// <returns>单个大写字母</returns> 
        private static string GetCharSpellCode(string CnChar)
        {
            long iCnChar;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

            //如果是字母，则直接返回 
            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }

            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= .51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else return ("?");
        }
    }
}
