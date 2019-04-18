using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.IService.ISysService
{
    public interface IPositionSer
    {
        int AddPosition(Position position);
        List<Position> GetAllPosition();
        int CountPosition();
        bool UpdatePosition(Position position);
        bool DelPosition(Position position);
    }
}
