using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web;
using System.Drawing.Imaging;

namespace UtilityCollection.IO.CustomImage
{
   public class ImageHandler
    {
        #region

        public static bool ImgWidthBiggerThenHeight(string url)
        {
            var img = Image.FromFile(url);
            try
            {
                if (img.Width >= img.Height)
                {
                    img.Dispose();
                    return true;
                }
                else
                {
                    img.Dispose();
                    return false;
                }

            }
            catch (Exception imgerror)
            {
                return false;
            }
        }

        public static bool IsImage(HttpPostedFile postfile)
        {
            try
            {
                using (var img = Image.FromStream(postfile.InputStream))
                {
                    var memoimg = new MemoryStream();
                    img.Save(memoimg, ImageFormat.Jpeg);
                    img.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsImageByUploadFile(HttpPostedFile postfile)
        {
            try
            {
                //var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                var reader = new BinaryReader(postfile.InputStream);
                var b = new byte[2];
                var buffer = reader.ReadByte();
                b[0] = buffer;
                var fileClass = buffer.ToString();
                buffer = reader.ReadByte();
                b[1] = buffer;
                fileClass += buffer.ToString();


                reader.Close();
                //fs.Close();
                if (fileClass == "255216" || fileClass == "7173" || fileClass == "6677" || fileClass == "13780")//255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsImage(string path)
        {
            try
            {
                var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                var reader = new BinaryReader(fs);
                var b = new byte[2];
                var buffer = reader.ReadByte();
                b[0] = buffer;
                var fileClass = buffer.ToString();
                buffer = reader.ReadByte();
                b[1] = buffer;
                fileClass += buffer.ToString();


                reader.Close();
                fs.Close();
                if (fileClass == "255216" || fileClass == "7173" || fileClass == "6677" || fileClass == "13780")//255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 绘制缩略图

        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <param name="mode">保留着，暂时未用</param>
        /// <returns>处理以后的图片</returns>
        private static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH, int mode)
        {
            var b = new Bitmap(newW, newH);
            try
            {
                var g = Graphics.FromImage(b);
                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // 高质量
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
            //finally
            //{
            //    b.Dispose();
            //}
        }

        /// <summary>
        ///  缩放
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="savePath"></param>
        /// <param name="newW"></param>
        /// <param name="newH"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static void KiResizeImage(string imageUrl, string savePath, int newW, int newH, int mode)
        {
            var image = System.Drawing.Image.FromFile(imageUrl);

            var tHeight = 0;
            var tWidth = 0;

            //按比例计算出缩略图的宽度和高度
            if (image.Width >= image.Height)
            {
                if (newW < image.Width)
                {
                    tHeight = (int)Math.Floor(Convert.ToDouble(image.Height) * (Convert.ToDouble(newW)
                    / Convert.ToDouble(image.Width)));
                    tWidth = newW;
                }
                else
                {
                    tHeight = image.Height;
                    tWidth = image.Width;
                }
            }
            else
            {
                if (newH > image.Height)
                {
                    tWidth = (int)Math.Floor(Convert.ToDouble(image.Width) * (Convert.ToDouble(newH)
                    / Convert.ToDouble(image.Height)));
                    tHeight = newH;
                }
                else
                {
                    tHeight = image.Height;
                    tWidth = image.Width;
                }
            }

            var nbm = new Bitmap(image, image.Width, image.Height);
            try
            {
                image = KiResizeImage(nbm, tWidth, tHeight, mode);
                if (File.Exists(savePath))
                    File.Delete(savePath);
                image.Save(savePath, ImageFormat.Jpeg);
            }
            catch
            {
                throw;
            }
        }

        public static void KiResizeImage(Image image, string savePath, int newW, int newH, int mode)
        {
            var tHeight = 0;
            var tWidth = 0;

            //按比例计算出缩略图的宽度和高度
            if (image.Width >= image.Height)
            {
                if (newW < image.Width)
                {
                    tHeight = (int)Math.Floor(Convert.ToDouble(image.Height) * (Convert.ToDouble(newW)
                    / Convert.ToDouble(image.Width)));
                    tWidth = newW;
                }
                else
                {
                    tHeight = image.Height;
                    tWidth = image.Width;
                }
            }
            else
            {
                if (newH > image.Height)
                {
                    tWidth = (int)Math.Floor(Convert.ToDouble(image.Width) * (Convert.ToDouble(newH)
                    / Convert.ToDouble(image.Height)));
                    tHeight = newH;
                }
                else
                {
                    tHeight = image.Height;
                    tWidth = image.Width;
                }
            }

            var nbm = new Bitmap(image, image.Width, image.Height);
            try
            {
                image = KiResizeImage(nbm, tWidth, tHeight, mode);
                if (File.Exists(savePath))
                    File.Delete(savePath);
                image.Save(savePath, ImageFormat.Jpeg);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        /// <summary>
        /// 在图片上生成图片水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_syp">生成的带图片水印的图片路径</param>
        /// <param name="Path_sypf">水印图片路径</param>

        protected void AddShuiYinPic(string Path, string Path_syp, string Path_sypf)
        {
            Stream objStream = new MemoryStream();
            using (Stream fs = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                CopyStream(fs, objStream, (int)fs.Length);
            }
            System.Drawing.Image image = System.Drawing.Image.FromStream(objStream);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height - 30, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();

            if (File.Exists(Path_syp))
                File.Delete(Path_syp);
            image.Save(Path_syp);
            image.Dispose();
        }

        protected void AddShuiYinPic(Stream sourceStream, string Path_syp, string Path_sypf)
        {
            Stream objStream = new MemoryStream();
            CopyStream(sourceStream, objStream, (int)sourceStream.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(objStream);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height - 30, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();

            if (File.Exists(Path_syp))
                File.Delete(Path_syp);
            image.Save(Path_syp);
            image.Dispose();
        }


        public static void CopyStream(Stream srcStream, Stream decStream, int bufferSize)
        {
            srcStream.Position = 0;
            byte[] buffer = new byte[bufferSize];
            int n;
            while ((n = srcStream.Read(buffer, 0, bufferSize)) > 0)
            {
                decStream.Write(buffer, 0, n);
                decStream.Position = srcStream.Position;
            }
            decStream.Position = 0;
            srcStream.Position = 0;
        }
    }
}
