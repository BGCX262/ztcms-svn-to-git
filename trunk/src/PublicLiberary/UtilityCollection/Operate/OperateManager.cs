using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityCollection.Operate
{
    class OperateManager
    {
        //userPurview是用户具有的总权限
        //optPurview是一个操作要求的权限为一个整数（没有经过权的！）
        public static bool HasOperateManager(int userPurview, int optPurview)
        {
            int purviewValue = (int)Math.Pow(2, optPurview);
            return (userPurview & purviewValue) == purviewValue;
        }
    }
}
