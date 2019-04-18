using KNCore.BLL.SYS;
using KNCore.IBLL.SYS;
using KNCore.IService.ISysService;
using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.Service.SysService
{
    public class PositionSer : BaseService, IPositionSer
    {
        private readonly IPositionBLL _positionBLL;

        public PositionSer()
        {
            _positionBLL = new PositionBLL();
        }

        public int AddPosition(Position position)
        {
            int count = _positionBLL.Add(position);
            return count;
        }

        public int CountPosition()
        {
            int count = _positionBLL.GetEntities(z => z.Id != 0).Count;
            return count;
        }

        public bool DelPosition(Position position)
        {
            bool del = _positionBLL.Del(position);
            return del;
        }

        public List<Position> GetAllPosition()
        {
            List<Position> positions = _positionBLL.GetEntities(z => z.Id != 0);
            return positions;
        }

        public bool UpdatePosition(Position position)
        {
            bool update = _positionBLL.Update(position);
            return update;
        }
    }
}
