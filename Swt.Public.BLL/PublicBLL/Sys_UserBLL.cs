using Swt.Public.Model.PublicModel; 
using Swt.Public.DAL.PublicDAL; 
using Swt.Public.IBLL.PublicIBLL; 

namespace Swt.Public.BLL.PublicBLL
{
	public class Sys_UserBLL : CurdBLL<Sys_UserEntity>,ISys_UserIBLL 
	{ 
		public override void SetCurrentDal()
		{ 
			 CurrentDAL = new Sys_UserDAL(); 
		} 
	} 
}