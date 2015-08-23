using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpaqueLayer
{
    public class OpaqueCommand
    {
        private OpaqueLayer _opaqueLayer = null;//半透明蒙板层

        public OpaqueCommand(int alpha, bool isShowLoadingImage, Color color)
        {
            this._opaqueLayer = new OpaqueLayer(alpha, isShowLoadingImage,color);
        }

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="control">控件</param>
        public void ShowOpaqueLayer(Control control)
        {
            try
            {
                if (!control.Controls.Contains(this._opaqueLayer))
                {
                     control.Controls.Add(this._opaqueLayer);
                     this._opaqueLayer.Dock = DockStyle.Fill;
                     this._opaqueLayer.BringToFront();
                }
                this._opaqueLayer.Enabled = true;
                this._opaqueLayer.Visible = true;
            }
            catch { }
        }

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="alpha">透明度</param>
        /// <param name="isShowLoadingImage">是否显示图标</param>
        public void ShowOpaqueLayer(Control control, int alpha, bool isShowLoadingImage)
        {
            try
            {
                if (this._opaqueLayer == null)
                {
                    this._opaqueLayer = new OpaqueLayer(alpha, isShowLoadingImage);
                    control.Controls.Add(this._opaqueLayer);
                    this._opaqueLayer.Dock = DockStyle.Fill;
                    this._opaqueLayer.BringToFront();
                }
                else
                {
                    control.Controls.Add(this._opaqueLayer);
                }
                this._opaqueLayer.Enabled = true;
                this._opaqueLayer.Visible = true;
            }
            catch { }
        }

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="alpha">透明度</param>
        /// <param name="isShowLoadingImage">是否显示图标</param>
        /// <param name="color">背景色</param>
        public void ShowOpaqueLayer(Control control, int alpha, bool isShowLoadingImage, Color color)
        {
            try
            {
                if (this._opaqueLayer == null)
                {
                    this._opaqueLayer = new OpaqueLayer(alpha, isShowLoadingImage, color);
                    control.Controls.Add(this._opaqueLayer);
                    this._opaqueLayer.Dock = DockStyle.Fill;
                    this._opaqueLayer.BringToFront();
                }
                else
                {
                    control.Controls.Add(this._opaqueLayer);
                }
                this._opaqueLayer.Enabled = true;
                this._opaqueLayer.Visible = true;
            }
            catch { }
        }

        /// <summary>
        /// 隐藏遮罩层
        /// </summary>
        public void HideOpaqueLayer()
        {
            try
            {
                if (this._opaqueLayer != null)
                {
                    this._opaqueLayer.Visible = false;
                    this._opaqueLayer.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
