﻿using KNCore.DAL;
using KNCore.DAL.Comm;
using KNCore.IBLL.Comm;
using KNCore.Model.CommModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KNCore.BLL.Comm
{
    public class BaseUserBLL : CurdBLL<BaseUser>,IBaseUserBLL
    {
        public override void SetCurrentDal()
        {
            CurrentDAL = new BaseUserDAL();
        }
    }
}