using HappyBirthdayCampain.BLL;
using HappyBirthdayCampain.BOL;
using System.Web.Mvc;
using System;
using HappyBirthdayCampain.UI.ExceptionUI;
using HappyBirthdayCampain.UI.Models;

namespace HappyBirthdayCampain.UI.Controllers
{
    [MyExceptionHandler]
    public class HomeController : Controller
    {
        private readonly IUserManager _userManager;
        
        //private readonly CustomCrypto crypto;

        public HomeController(IUserManager userManager) { _userManager = userManager;}

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmployeeLogin emp)
        {

			EmployeeDTO user = null;
            if (ModelState.IsValid && _userManager.LogInVerification(emp.UserName, emp.Password, out user))
            {
     
                #if TODO 
                if(!_vote.IsCampainStarted() && _vote.CheckCampaignConditions(emp.UserName) == (int)SkipCampainType.Vote)
                    return RedirectToAction("Index", "StartCampaign", new { id = crypto.Encrypt(emp) });

                if (_vote.CheckCampaignConditions(emp.UserName) > (int)SkipCampainType.Vote && _vote.CheckCampaignConditions(emp.UserName) != (int)SkipCampainType.UsrStartCampainLogin)
                    return RedirectToAction("Exit", "SkipVoteCampaign", new { id = _vote.CheckCampaignConditions(emp.UserName) });

                else if (_vote.CheckCampaignConditions(emp.UserName) == (int)SkipCampainType.UsrStartCampainLogin)
                    return  RedirectToAction("Index", "StopCampaign", new { id = crypto.Encrypt(new Tuple<Employee, Campaign>(emp, _vote.CampaignData())) });

                else
                    return RedirectToAction("Index", "Vote", new { id = crypto.Encrypt(new Tuple<Employee, Campaign>(emp, _vote.CampaignData())) });   
				#endif
            }

            ModelState.AddModelError("Password", "Your Username or Password are incorrect");
            return View();
        }

    }
}