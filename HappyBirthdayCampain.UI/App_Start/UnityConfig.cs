using HappyBirthdayCampain.BLL;
using HappyBirthdayCampain.DAL;
using HappyBirthdayCampain.BLL.PasswordManager;
using System;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
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
			container.RegisterType<ICampaignManager, CampaignManager>();
            container.RegisterType<IReportManager, ReportManager>();
            container.RegisterType<IUserManager, UserManager>();
			container.RegisterType<ICampaignDataDal, CampaignData>();
            container.RegisterType<IPasswordVerifier, Argon2PasswordVerifier>();

            container.RegisterType<IBllLogger, BllLogger>();
            container.RegisterType<IDalLogger, DalLogger>();
            container.RegisterType<IUiLogger, UiLogger>();
            

        }
	}
}