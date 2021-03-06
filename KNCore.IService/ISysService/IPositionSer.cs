﻿using KNCore.Comm.ServiceRegistry;
using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.IService.ISysService
{
    public interface IPositionSer: IAppService
    {
        int AddPosition(Position position);
        List<Position> GetAllPosition();
        int CountPosition();
        bool UpdatePosition(Position position);
        bool DelPosition(Position position);
    }
}
