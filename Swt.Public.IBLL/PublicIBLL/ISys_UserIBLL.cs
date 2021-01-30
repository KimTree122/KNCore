using Swt.Public.Model.PublicModel; 

namespace Swt.Public.IBLL.PublicIBLL
{
	public interface ISys_UserIBLL : ICurdBLL<Sys_UserEntity> 
	{ 
		void SetCurrentDal();
	} 
}