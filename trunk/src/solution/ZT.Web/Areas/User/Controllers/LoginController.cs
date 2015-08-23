using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ZT.Api.User.Authorize;
using ZT.Api.User.Identity;
using ZT.Web.Models;

namespace ZT.Web.Areas.User.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 登录输出视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 接受登陆参数
        /// </summary>
        /// <param name="userId">登录ID</param>
        /// <param name="password">密码</param>
        /// <param name="checkcode">验证码</param>
        /// <returns></returns>
        [OriginCheck]//验证页面post来源
        [ValidateAntiForgeryToken]//接收@Html.AntiForgeryToken() 并进行验证
        [HttpPost]
        public ActionResult Index(string userId, string password, string checkcode)
        {
            ModelState.Clear();
            if (Request.Cookies["UserValidateCode"] == null)
            {
                var altermodel = new AlertModels(@"验证码不存在");
                return View(altermodel);
            }
            if (checkcode != Request.Cookies["UserValidateCode"].Value)
            {
                var altermodel = new AlertModels(@"验证码不正确");
                return View(altermodel);
            }
            IdentityHelper.LoginUser(userId, password);
            if (!IdentityHelper.User.LoginResult)
            {
                var altermodel = new AlertModels(@"登录失败，用户名或密码不正确");
                return View(altermodel);
            }
            //登陆成功跳转
            return RedirectToAction("Index", "User");
        }

        #region 验证码

        private const int LetterWidth = 15; //单个字体的宽度范围  
        private const int LetterHeight = 27; //单个字体的高度范围  
        private const int LetterCount = 5; //验证码位数  
        private readonly char[] _chars = "0123456789QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm".ToCharArray();
        private readonly string[] _fonts = { "Arial", "Georgia" };

        /// <summary>  
        /// 产生波形滤镜效果  
        /// </summary>  
        //private const double Pi = 3.1415926535897932384626433832795;
        private const double Pi2 = 6.283185307179586476925286766559;

        public ActionResult Checkcode()
        {
            //防止网页后退--禁止缓存      
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            var strValidateCode = GetRandomNumberString(LetterCount);
            var objCookie = new HttpCookie("UserValidateCode")
                {
                    Value = strValidateCode.ToLower(),
                    Path = "/",
                    Expires = DateTime.Now.AddSeconds(1200)
                };
            Response.Cookies.Add(objCookie);
            CreateImage(strValidateCode);
            return View();
        }

        public void CreateImage(string checkCode)
        {
            var intImageWidth = checkCode.Length * LetterWidth + 5;
            var newRandom = new Random();
            var image = new Bitmap(intImageWidth, LetterHeight);
            var g = Graphics.FromImage(image);
            //生成随机生成器  
            var random = new Random();
            //白色背景  
            g.Clear(Color.White);
            //画图片的背景噪音线  
            for (var i = 0; i < 10; i++)
            {
                var x1 = random.Next(image.Width);
                var x2 = random.Next(image.Width);
                var y1 = random.Next(image.Height);
                var y2 = random.Next(image.Height);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }

            //画图片的前景噪音点  
            for (var i = 0; i < 10; i++)
            {
                var x = random.Next(image.Width);
                var y = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            //随机字体和颜色的验证码字符  

            for (var intIndex = 0; intIndex < checkCode.Length; intIndex++)
            {
                var findex = newRandom.Next(_fonts.Length - 1);
                var strChar = checkCode.Substring(intIndex, 1);
                var newBrush = new SolidBrush(GetRandomColor());
                var thePos = new Point(intIndex * LetterWidth + 1 + newRandom.Next(3), 1 + newRandom.Next(3));//5+1+a+s+p+x  
                g.DrawString(strChar, new Font(_fonts[findex], 14, FontStyle.Bold), newBrush, thePos);
            }
            //灰色边框  
            g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, intImageWidth - 1, (LetterHeight - 1));
            //图片扭曲  
            //image = TwistImage(image, true, 3, 4);  
            //将生成的图片发回客户端  
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            Response.ClearContent(); //需要输出图象信息 要修改HTTP头   
            Response.ContentType = "image/Png";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }

        /// <summary>  
        /// 正弦曲线Wave扭曲图片  
        /// </summary>  
        /// <param name="srcBmp">图片路径</param>  
        /// <param name="bXDir">如果扭曲则选择为True</param>  
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>  
        /// <returns></returns>  
        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色  
            var graph = Graphics.FromImage(destBmp);

            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);

            graph.Dispose();

            var dBaseAxisLen = bXDir ? destBmp.Height : (double)destBmp.Width;

            for (var i = 0; i < destBmp.Width; i++)
            {
                for (var j = 0; j < destBmp.Height; j++)
                {
                    var dx = bXDir ? (Pi2 * j) / dBaseAxisLen : (Pi2 * i) / dBaseAxisLen;

                    dx += dPhase;

                    var dy = Math.Sin(dx);

                    // 取得当前点的颜色  
                    var nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    var nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    var color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }
        public Color GetRandomColor()
        {
            var randomNumFirst = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(randomNumFirst.Next(50));
            var randomNumSencond = new Random((int)DateTime.Now.Ticks);
            var intRed = randomNumFirst.Next(210);
            var intGreen = randomNumSencond.Next(180);
            var intBlue = (intRed + intGreen > 300) ? 0 : 400 - intRed - intGreen;
            intBlue = (intBlue > 255) ? 255 : intBlue;
            return Color.FromArgb(intRed, intGreen, intBlue);
        }
        //  生成随机数字字符串  
        public string GetRandomNumberString(int intNumberLength)
        {
            var random = new Random();
            string validateCode = string.Empty;
            for (int i = 0; i < intNumberLength; i++)
                validateCode += _chars[random.Next(0, _chars.Length)].ToString(CultureInfo.InvariantCulture);
            return validateCode;
        }
        #endregion
    }
}
