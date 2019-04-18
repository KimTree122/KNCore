using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNCore.Comm.TreeNodeHelper
{
    public class TreeNodeComm
    {
        //委托传入树属性的字典数据
        public delegate Dictionary<string, string> DelTreeAttr(object obj);

        //初始化以easyui树的数据结构
        public List<WebTreeNode> InitTreeNode<T>(List<T> treelist,DelTreeAttr delTreeAttr, int fid=0)
        {
            List<WebTreeNode> webTreeNodes = new List<WebTreeNode>();
            foreach (var tn in treelist)
            {
                BaseTree bt = (BaseTree)(object)tn;
                if (bt.FatherID !=fid)
                {
                    continue;
                }
                WebTreeNode wtn = new WebTreeNode
                {
                    id = bt.Id,
                    iconCls = bt.ImagePath,
                    text = bt.NodeName,
                    state = bt.State,
                    children = GetChild(treelist, bt.Id,delTreeAttr),
                    attributes = delTreeAttr(tn)
                };
                webTreeNodes.Add(wtn);
            }
            return webTreeNodes;
        }

        //递归子类
        private object GetChild<T>(List<T> treelist, int id , DelTreeAttr delTreeAttr)
        {
            List<WebTreeNode> wtnls = new List<WebTreeNode>();
            foreach (var au in treelist)
            {
                BaseTree bt = (BaseTree)(object)au;
                if (bt.FatherID != id)
                {
                    continue;
                }
                WebTreeNode wtn = new WebTreeNode
                {
                    id = bt.Id,
                    iconCls = bt.ImagePath,
                    text = bt.NodeName,
                    state = bt.State,
                    children = GetChild(treelist, bt.Id,delTreeAttr),
                    attributes = delTreeAttr(au)
                };
                wtnls.Add(wtn);
            }
            return wtnls;
        }

        

    }
}
