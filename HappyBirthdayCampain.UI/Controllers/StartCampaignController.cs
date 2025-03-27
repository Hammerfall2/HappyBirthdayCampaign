using HappyBirthdayCampain.BLL;
using HappyBirthdayCampain.UI.Models;
using System.Web.Mvc;
using HappyBirthdayCampain.UI.ExceptionUI;

namespace HappyBirthdayCampain.UI.Views
{
    public class StartCampaignController : Controller
    {

        private readonly ICampaignManager _campaign;
        private readonly IUiLogger _logger;

        public StartCampaignController(ICampaignManager campaign, IUserManager employee, IUiLogger logger) { _campaign = campaign; _logger = logger; }
        // GET: Campaign
        public ActionResult Index(Employee currentUserViewModel)
        {

     
            try
            {
                _logger.Debug("[HttpGet] StartCampaignController:Index()");
                BOL.EmployeeKey employeeKey = Maps.EmployeeMapper.MapVmIdToKeyBol(currentUserViewModel);
                var birhdayListEmployee = _campaign.EmployeeUsersListExcept(employeeKey);
                _logger.Info("Create startCampaignModel data");
                StartCampaign startCampaignModel = new StartCampaign()
                {
                    GetListBirthdayUsers = Maps.CampaignMapper.MapBirthdayListUsers(birhdayListEmployee),
                    GetListBirhdayYears = Maps.CampaignMapper.MapCampaignListYears(birhdayListEmployee),
                    StartCampaignData = new CampaignData()
                    {
                        UserStatedCampId = currentUserViewModel.Id,
                        UserStartedCampaignFullName = currentUserViewModel.FullName
                    }
                };

                _logger.Debug("Return [HttpGet] StartCampaignController:Index()");
                return View(startCampaignModel);
            }
            catch (BllException e)
            {

                var ex = new UiException("Employee list can not be recieved", e);
                _logger.Error($"ERROR StartCampaignController::Index({currentUserViewModel.DumpEmployeeModel()})", ex);
                throw ex;
            }
          

        }

        [HttpPost]
        public ActionResult Index(Models.StartCampaign campaign)
        {

            try
            {
                _logger.Debug("[HttpPost] StartCampaignController:Index()");

                var campaignDTO = Maps.CampaignMapper.MapStartCampaignUiToBol(campaign);

                _logger.Info("Start Campaign");
                _campaign.StartCampaign(campaignDTO);

                _logger.Debug("Return [HttpPost] StartCampaignController:Index()");
                return RedirectToAction("Index", "Home");
            }
            catch (BllException e)
            {

                var ex = new UiException("Campaign can not be started", e);
                _logger.Error($"ERROR StartCampaignController::Index({campaign.DumpStartCampaignModel()})", ex);
                throw ex;

  
            }
            
        }
    }
        
}