using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanDaWeb.Utility
{
    /// <summary>
    /// 版本节点接口
    /// </summary>
    public interface IVersionNode<T> where T : IVersionData
    {
        /// <summary>
        /// 当前版本的新增列表
        /// </summary>
        /// <returns></returns>
        HashSet<T> GetAddedVersionData();

        /// <summary>
        /// 当前版本的更新列表
        /// </summary>
        /// <returns></returns>
        HashSet<T> GetUpDatedVersionData();

        /// <summary>
        /// 当前版本的移除列表
        /// </summary>
        /// <returns></returns>
        HashSet<T> GetDeleteVersionData();

        /// <summary>
        /// 变更派生
        /// </summary>
        /// <param name="addedData"></param>
        /// <param name="updateData"></param>
        /// <param name="removeData"></param>
        /// <param name="addedNode"></param>
        /// <returns></returns>
        IVersionNode<T> UpdateNode(HashSet<T> addedData, HashSet<T> updateData
            , HashSet<T> removeData, Func<IVersionNode<T>, IVersionNode<T>> useLoad, IVersionNode<T> addedNode = null);


        /// <summary>
        /// 根据当前版本节点派生一个Null节点
        /// </summary>
        /// <returns></returns>
        IVersionNode<T> DeriveEmptyNode();

        /// <summary>
        /// 获取当前的版本控制数据
        /// </summary>
        /// <returns></returns>
        Dictionary<string, T> GetNowVersionData(Func<IVersionNode<T>, IVersionNode<T>> useLoad,bool ifRemoveLast = false);

        /// <summary>
        /// 获取父节点
        /// </summary>
        /// <returns></returns>
        IVersionNode<T> GetParentNode();

        /// <summary>
        /// 子节点列表
        /// </summary>
        /// <returns></returns>
        List<IVersionNode<T>> GetSubNode();

    }
}
