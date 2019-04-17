using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNCore.Comm.TreeNodeHelper
{
    public class TreeNodeHelper
    {
        public object InitTreeNode(List<Authority> treeList,bool showoper, int fid = 0)
        {
            List<WebTreeNode> webTreeNodes = new List<WebTreeNode>();
            List<Authority> fAu_ls = treeList.Where(au => au.FatherID == fid).ToList();
            foreach (var tn in fAu_ls)
            {
                WebTreeNode wtn = new WebTreeNode
                {
                    id = tn.Id,
                    iconCls = tn.ImagePath,
                    text = tn.NodeName,
                    children = GetChild(treeList, tn.Id, showoper),
                    attributes = CreateUrl(tn)
                };
                webTreeNodes.Add(wtn);
            }

            return webTreeNodes;
        }

        private object GetChild(List<Authority> treelist, int id, bool showoper)
        {
            List<WebTreeNode> wtnls = new List<WebTreeNode>();
            List<Authority> sonAu_ls = treelist.Where(au => au.FatherID == id).ToList();
            if (!showoper)
            {
                sonAu_ls = sonAu_ls.Where(au => au.AuthTypeID != 6).ToList();
            }
            foreach (var au in sonAu_ls)
            {

                WebTreeNode wtn = new WebTreeNode
                {
                    id = au.Id,
                    iconCls = au.ImagePath,
                    text = au.NodeName,
                    children = GetChild(treelist, au.Id, showoper),
                    attributes = CreateUrl(au)
                };
                wtnls.Add(wtn);
            }
            return wtnls;
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


        public object InitTreeNode2(List<Authority> treeList, bool showoper, int fid = 0)
        {
            List<WebTreeNode> webTreeNodes = new List<WebTreeNode>();
            List<Authority> fAu_ls = treeList.Where(au => au.FatherID == fid).ToList();
            foreach (var tn in fAu_ls)
            {
                WebTreeNode wtn = new WebTreeNode
                {
                    id = tn.Id,
                    iconCls = tn.ImagePath,
                    text = tn.NodeName,
                    children = GetChild2(treeList, tn.Id, showoper),
                    attributes = CreateUrl(tn)
                };
                webTreeNodes.Add(wtn);
            }

            return webTreeNodes;
        }

        private object GetChild2(List<Authority> treelist, int id, bool showoper)
        {
            List<WebTreeNode> wtnls = new List<WebTreeNode>();
            List<Authority> sonAu_ls = treelist.Where(au => au.FatherID == id).ToList();
            if (!showoper)
            {
                sonAu_ls = sonAu_ls.Where(au => au.AuthTypeID != 6).ToList();
            }
            foreach (var au in sonAu_ls)
            {

                WebTreeNode wtn = new WebTreeNode
                {
                    id = au.Id,
                    iconCls = au.ImagePath,
                    text = au.NodeName,
                    children = GetChild2(treelist, au.Id, showoper),
                    attributes = CreateUrl(au)
                };
                wtnls.Add(wtn);
            }
            return wtnls;
        }

    }

    public class WebNode
    {
        public int id { get; set; }
        public string name { get; set; }
        public object tag { get; set; }
        public List<WebNode> sonWN { get; set; }
    }

}
