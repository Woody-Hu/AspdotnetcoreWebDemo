using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanDaWeb.Utility
{
    /// <summary>
    /// 版本控制公共方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class VersionControlUtility<T>
        where T : IVersionData
    {
        /// <summary>
        /// 获得当前版本控制数值的字典
        /// </summary>
        /// <param name="IfRemoveLast"></param>
        /// <returns></returns>
        public static Dictionary<string, T> GetNowVersionData(IVersionNode<T> inputNode, Func<IVersionNode<T>, IVersionNode<T>> useLoad, bool IfRemoveLast = false)
        {
            Dictionary<string, T> returnValue = new Dictionary<string, T>();

            //获得至根节点的节点栈
            Stack<IVersionNode<T>> tempStack = GetParentNodeStack(inputNode, useLoad);

            LinkedList<T> useLastRemoveDataLinkedList = new LinkedList<T>();

            //递归制备
            while (tempStack.Count != 0)
            {
                IVersionNode<T> tempNode = tempStack.Pop();

                PrepareInOneLayer(IfRemoveLast, returnValue, useLastRemoveDataLinkedList, tempNode);
            }

            //清楚终端队列
            foreach (var oneData in useLastRemoveDataLinkedList)
            {
                RemoveInDic(returnValue, oneData);
            }

            return returnValue;
        }

        /// <summary>
        /// 批量更新接口
        /// </summary>
        /// <param name="addedData"></param>
        /// <param name="updateData"></param>
        /// <param name="removeData"></param>
        /// <param name="WishaddedNode"></param>
        /// <returns></returns>
        public static IVersionNode<T> UpdateNode
            (IVersionNode<T> inputNode, HashSet<T> addedData, HashSet<T> updateData, HashSet<T> removeData, Func<IVersionNode<T>, IVersionNode<T>> useLoad, IVersionNode<T> WishaddedNode = null)
        {
            IVersionNode<T> returnValue = null;

            //若只更新则不派生
            if (0 == updateData.Count && 0 == removeData.Count)
            {
                AddData(inputNode, addedData, useLoad, WishaddedNode);
                returnValue = inputNode;
            }
            else
            {
                //进行空派生
                returnValue = inputNode.DeriveEmptyNode();

                //新增
                AddData(returnValue, addedData, useLoad ,WishaddedNode);

                //更新
                UpDate(returnValue, updateData);


                RemoveData(returnValue, removeData);
            }

            return returnValue;
        }

        /// <summary>
        /// 按照一层准备
        /// </summary>
        /// <param name="IfRemoveLast"></param>
        /// <param name="returnValue"></param>
        /// <param name="useLastRemoveDataLinkedList"></param>
        /// <param name="tempNode"></param>
        public static void PrepareInOneLayer(bool IfRemoveLast, Dictionary<string, T> returnValue, LinkedList<T> useLastRemoveDataLinkedList, IVersionNode<T> tempNode)
        {
            foreach (var oneAddedVersionData in tempNode.GetAddedVersionData())
            {
                var tempTag = oneAddedVersionData.GetKeyTag();
                if (!returnValue.ContainsKey(tempTag))
                {
                    returnValue.Add(tempTag, oneAddedVersionData);
                }
            }

            foreach (var oneUpdatedVersionData in tempNode.GetUpDatedVersionData())
            {
                var tempTag = oneUpdatedVersionData.GetKeyTag();
                if (returnValue.ContainsKey(tempTag))
                {
                    returnValue[tempTag] = oneUpdatedVersionData;
                }
            }

            foreach (var oneData in tempNode.GetDeleteVersionData())
            {
                if (!IfRemoveLast)
                {
                    RemoveInDic(returnValue, oneData);
                }
                else
                {
                    useLastRemoveDataLinkedList.AddLast(oneData);
                }

            }
        }

        /// <summary>
        /// 从字典中清除
        /// </summary>
        /// <param name="returnValue"></param>
        /// <param name="oneData"></param>
        public static void RemoveInDic(Dictionary<string, T> returnValue, T oneData)
        {
            var tempTag = oneData.GetKeyTag();
            if (returnValue.ContainsKey(tempTag))
            {
                returnValue.Remove(tempTag);
            }
        }

        /// <summary>
        /// 获取递归栈
        /// </summary>
        /// <returns></returns>
        public static Stack<IVersionNode<T>> GetParentNodeStack(IVersionNode<T> inputNode, Func<IVersionNode<T>, IVersionNode<T>> useLoad)
        {
            Stack<IVersionNode<T>> reutrnValue = new Stack<IVersionNode<T>>();

            IVersionNode<T> nowNode = inputNode;

            while (null != nowNode)
            {
                nowNode = useLoad(nowNode);
                reutrnValue.Push(nowNode);
                nowNode = nowNode.GetParentNode();
            }

            return reutrnValue;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="addedData"></param>
        /// <param name="wishaddedNode"></param>
        public static void AddData(IVersionNode<T> inputNode, HashSet<T> addedData, Func<IVersionNode<T>, IVersionNode<T>> useLoad, IVersionNode<T> wishaddedNode = null)
        {
            IVersionNode<T> useNode = null;

            if (null == inputNode.GetParentNode())
            {
                useNode = inputNode;
            }
            else
            {
                var useStack = GetParentNodeStack(inputNode, useLoad);

                var useRoot = useStack.Peek();

                while (useStack.Count != 0)
                {
                    var tempNode = useStack.Pop();

                    if (tempNode.Equals(wishaddedNode))
                    {
                        useNode = tempNode;
                        break;
                    }
                }

                if (null == useNode)
                {
                    useNode = useRoot;
                }
            }

            var tempSet = useNode.GetAddedVersionData();

            foreach (var oneData in addedData)
            {
                tempSet.Add(oneData);
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="inputUpDatedData"></param>
        public static void UpDate(IVersionNode<T> inputNode, HashSet<T> inputUpDatedData)
        {
            var useUpdateData = inputNode.GetUpDatedVersionData();

            foreach (var oneData in inputUpDatedData)
            {
                useUpdateData.Add(oneData);
            }
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="inputRemoveData"></param>
        public static void RemoveData(IVersionNode<T> inputNode, HashSet<T> inputRemoveData)
        {
            var useRemoveData = inputNode.GetDeleteVersionData();

            foreach (var oneData in inputRemoveData)
            {
                useRemoveData.Add(oneData);
            }
        }
    }
}
