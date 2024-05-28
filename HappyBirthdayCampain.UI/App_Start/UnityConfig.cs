using HappyBirthdayCampain.BLL;
using HappyBirthdayCampain.DAL;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace HappyBirthdayCampain.UI
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();
			
			// register all your components with the container here
			// it is NOT necessary to register your controllers
			
			// e.g. container.RegisterType<ITestService, TestService>();
			
			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
			//container.RegisterType<IVote, VoteCampaign>();
			container.RegisterType<IUserManager, UserManager>();
			container.RegisterType<ICampaignData, CampaignData>();
		}
	}
}