using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNCore.Comm.TreeNodeHelper
{
    public class TreeNodeComm<T> where T: class,new()
    {

        public List<WebNode> InitTreeNode(List<T> treeList ,int fid = 0)
        {
            List<WebNode> webTreeNodes = new List<WebNode>();
            
            foreach (var tn in treeList)
            {
                BaseTree bt = (BaseTree)(object)tn;
                T t = (T)(object)tn;

                if (bt.FatherID == fid)
                {
                    WebNode wn = new WebNode() {
                        id = bt.Id,
                        name = bt.NodeName,
                        tag = t
                    };
                    wn.sonWN = new List<WebNode>();
                    wn.sonWN = GetSon(wn,treeList,bt.Id);
                    webTreeNodes.Add(wn);
                }
            }
            return webTreeNodes;
        }

        private List<WebNode> GetSon(WebNode wn, List<T> treeList, int id)
        {
            List<WebNode> wnlist = new List<WebNode>();
            foreach (var tl in treeList)
            {
                BaseTree bt = (BaseTree)(object)tl;
                if (bt.FatherID == id)
                {
                    WebNode swn = new WebNode() {
                        id = bt.Id,
                        name = bt.NodeName,
                        tag = tl
                    };
                    swn.sonWN = new List<WebNode>();
                    swn.sonWN = GetSon(swn, treeList, bt.Id);
                    wnlist.Add(swn);
                }
            }
            return wnlist;
        }

        public List<string> TranslateWebNode(List<T> treeList, int fid = 0)
        {
            List<string> strlist = new List<string>();
            List<WebNode> flist = InitTreeNode(treeList, fid);
            foreach (var wn in flist)
            {
                strlist.Add(wn.name);
                if (wn.sonWN.Count > 0)
                {
                    strlist.AddRange(AddSon(strlist, wn));
                }
            }
            return strlist;
        }

        private List<string> AddSon(List<string> strlist, WebNode wn)
        {
            List<string> strs = new List<string>();
            foreach (var swn in wn.sonWN)
            {
                strlist.Add(swn.name);
                if (swn.sonWN.Count > 0)
                {
                   strs.AddRange(AddSon(strlist, swn));
                }
            }
            return strs;
        }

        private Dictionary<string, string> CreateUrl(Authority auth)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "url", auth.Path }
                ,{ "AOrder",auth.Order.ToString()}
                , {"AuthTypeID",auth.AuthTypeID.ToString() }
                ,{ "ParentID",auth.FatherID.ToString()}
            };
            return dic;
        }

    }
}
