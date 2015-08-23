using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZT.Web.Models
{
    /// <summary>
    /// 弹出框模型
    /// </summary>
    public class AlertModels
    {
        /// <summary>
        /// 弹出的信息
        /// </summary>
        public string Message { get; set; }

        public AlertModels(string message)
        {
            Message = message;
        }
    }
}