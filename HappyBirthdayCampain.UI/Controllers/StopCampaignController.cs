using HappyBirthdayCampain.BLL;
using HappyBirthdayCampain.UI.ExceptionUI;
using HappyBirthdayCampain.UI.Models;
using System.Web.Mvc;

namespace HappyBirthdayCampain.UI.Controllers
{

    public class StopCampaignController : Controller
    {
        private readonly ICampaignManager _campaign;
        private readonly IUserManager _employee;
        private readonly IUiLogger _logger;

        public StopCampaignController(ICampaignManager vote, IUserManager employee, UiLogger logger) { _campaign = vote; _employee = employee; _logger = logger; }
        // GET: StopCampaign
        public ActionResult Index(Employee currentUserViewModel)
        {

            _logger.Debug("[HttpGet] StopCampaignController:Index()");
            BOL.EmployeeKey employeeKey = Maps.EmployeeMapper.MapVmIdToKeyBol(currentUserViewModel);
            var campaigns = _campaign.GetAllActiveCampaignsCreatedByUser(employeeKey);
            var getCampainList = Maps.CampaignMapper.MapCampaigDtoToCampaign(campaigns);

            _logger.Info("Create stopCampaignModel data");
            StopCampaign stopCampaign = new StopCampaign()
            {
                AvailableCampaigns = getCampainList,
                StopCampaignData = new CampaignData()
                {
                    UserStatedCampId = currentUserViewModel.Id,
                    UserStartedCampaignFullName = currentUserViewModel.FullName
                }
            };

            _logger.Debug("Return [HttpGet] StopCampaignController:Index()");
            return View(stopCampaign);
        }

        [HttpPost]
        public ActionResult Index(CampaignData campaignData)
        {
            _logger.Debug("[HttpPost] StopCampaignController:Index()");
            StopCampaign stopCampaign = new StopCampaign();
            stopCampaign.Title = $" User capaign for {campaignData.BirthdayUserFullName} for year {campaignData.CampaignYear} is stopped successful";

            var campaignKey = Maps.CampaignMapper.MapCampaignDataToCampaignKey(campaignData);
            BOL.EmployeeDTO employee = new BOL.EmployeeDTO();

            try
            {

                employee = _employee.GetUser(new BOL.EmployeeKey() { Id = campaignData.UserStatedCampId });


            }
            catch (BllException e)
            {

                var ex = new UiException("Invalid User", e);
                _logger.Error($"ERROR StopCampaignController::Index( {campaignData.DumpCampaignDataModel()})", ex);
                throw ex;
            }

            try
            {
                _logger.Info("Stop Campaign");
                _campaign.StopCampaign(campaignKey, employee.Key);


            }
            catch (BllException e)
            {

                var ex = new UiException("Campaign can not be stopped", e);
                _logger.Error($"ERROR StopCampaignController::Index( {campaignData.DumpCampaignDataModel()})", ex);
                throw ex;
            }

            _logger.Debug("Return [HttpPost] StopCampaignController:Index()");
            return Json(stopCampaign);

        }
    }
}