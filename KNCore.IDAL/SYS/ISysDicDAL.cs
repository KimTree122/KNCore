﻿using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.IDAL.SYS
{
    public interface ISysDicDAL:ICurdDAL<SysDic>{ }

    public interface ISysAuthDAL : ICurdDAL<Authority> { }

    public interface IPositionDAL : ICurdDAL<Position> { };
}
