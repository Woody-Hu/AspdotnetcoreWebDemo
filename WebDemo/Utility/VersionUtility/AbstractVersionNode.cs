using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanDaWeb.Utility
{
    /// <summary>
    /// 默认的版本节点实现
    /// </summary>
    public abstract class AbstractVersionNode<T> : IVersionNode<T>
        where T : class, IVersionData
    {
        public abstract HashSet<T> GetAddedVersionData();

        public abstract IVersionNode<T> GetParentNode();

        public abstract List<IVersionNode<T>> GetSubNode();

        public abstract HashSet<T> GetUpDatedVersionData();

        public abstract HashSet<T> GetDeleteVersionData();

        public abstract IVersionNode<T> DeriveEmptyNode();

        /// <summary>
        /// 获得当前版本控制数值的字典
        /// </summary>
        /// <param name="ifRemoveLast"></param>
        /// <returns></returns>
        public Dictionary<string, T> GetNowVersionData(Func<IVersionNode<T>, IVersionNode<T>> useLoad,bool ifRemoveLast = false)
        {
            return VersionControlUtility<T>.GetNowVersionData(this, useLoad, ifRemoveLast);
        }

        /// <summary>
        /// 批量更新接口
        /// </summary>
        /// <param name="addedData"></param>
        /// <param name="updateData"></param>
        /// <param name="removeData"></param>
        /// <param name="wishaddedNode"></param>
        /// <returns></returns>
        public IVersionNode<T> UpdateNode
            (HashSet<T> addedData, HashSet<T> updateData, HashSet<T> removeData, Func<IVersionNode<T>, IVersionNode<T>> useLoad ,IVersionNode<T> wishaddedNode = null)
        {
            return VersionControlUtility<T>.UpdateNode(this, addedData, updateData, removeData, useLoad, wishaddedNode);
        }


    }
}
