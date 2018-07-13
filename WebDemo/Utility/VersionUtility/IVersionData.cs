using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanDaWeb.Utility
{
    /// <summary>
    /// 被版本管理的数据接口
    /// </summary>
    public interface IVersionData
    {
        /// <summary>
        /// 获得Key标签
        /// </summary>
        /// <returns></returns>
        string GetKeyTag();
    }
}
