using HappyBirthdayCampain.BLL;
using HappyBirthdayCampain.UI.Models;
using System.Web.Mvc;
using HappyBirthdayCampain.UI.ExceptionUI;

namespace HappyBirthdayCampain.UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly ICampaignManager _campaign;
        private readonly IUiLogger _logger;
        private readonly IReportManager _reportManager;
        private readonly IUserManager _userManager;
        public ReportController(ICampaignManager campaign, IReportManager reportManager, IUiLogger logger, IUserManager userManager)
        { 
            _campaign = campaign;
            _logger = logger; 
            _reportManager = reportManager; 
            _userManager = userManager;
        }


        [HttpGet]
        public ActionResult SelectReport(Employee loginUser)
        {
            try
            {
                _logger.Debug("[HttpGet] ReportController:SelectReport()");
                BOL.EmployeeKey employeeKey = Maps.EmployeeMapper.MapVmIdToKeyBol(loginUser);
                var birhdayListEmployee = _campaign.EmployeeUsersListExcept(employeeKey);

                _logger.Info("Create SelectReportDataModel data");
                SelectReportData selectReportData = new SelectReportData()
                {
                    SelectItemsBirthdayUser = Maps.CampaignMapper.MapBirthdayListUsers(birhdayListEmployee),
                    SelectItemsStartYear = Maps.CampaignMapper.MapCampaignListYears(birhdayListEmployee),
                    SelectItemsEndYear = Maps.CampaignMapper.MapCampaignListYears(birhdayListEmployee),
                    VoteEmployee = new Employee
                    {
                        FullName = loginUser.FullName,
                        Id = loginUser.Id
                    }
                };

                _logger.Debug("Return [HttpGet] ReportController:SelectReport()");
                return View(selectReportData);
            }
            catch (BllException e)
            {

                var ex = new UiException("Employee list can not be recieved", e);
                _logger.Error($"ERROR ReportController::Index({loginUser.DumpEmployeeModel()})", ex);
                throw ex;
            }

        }

       
        [HttpGet]
        public ActionResult Index(SelectReportData reportData)
        {
            try
            {
                _logger.Debug("[HttpGet] ReportController:Index()");
                BOL.EmployeeKey employeeKey = Maps.EmployeeMapper.MapVmIdToKeyBol(reportData.BirthdayUserId);
                var availableCampaignsDto = _campaign.AvailableReportCampaignsList(reportData.StartYear, reportData.EndYear, employeeKey);
                var availbleCampaigns = Maps.CampaignMapper.MapCampaigDtoToCampaign(availableCampaignsDto);

                _logger.Info("Create VoteModel data");
                Vote voteData = new Vote()
                {
                    OpenCampaign = availbleCampaigns,
                    VoteUserFullName = reportData.VoteEmployee.FullName,
                    VoteUserId = reportData.VoteEmployee.Id,

                };
                _logger.Debug("Return [HttpGet] ReportController:Index()");
                return View(voteData);
            }
            catch (BllException e)
            {

                var ex = new UiException("Available Report Campaigns can not be recieved", e);
                _logger.Error($"ERROR ReportController::Index({reportData.DumpReportModel()})", ex);
                throw ex;
            }

        }

        public ActionResult Result(Models.CampaignData campaignData)
        {
            try
            {
                 _logger.Debug("[HttpGet] ReportController:Result()");
                var employeebirhdayUser = Maps.EmployeeMapper.Map(_userManager.GetUser(new BOL.EmployeeKey() { Id = campaignData.BirthdayUserId }));
                var employeeuStartedCamp = Maps.EmployeeMapper.Map(_userManager.GetUser(new BOL.EmployeeKey() { Id = campaignData.UserStatedCampId }));

                campaignData.BirthdayUserFullName = employeebirhdayUser.FullName;
                campaignData.UserStartedCampaignFullName = employeeuStartedCamp.FullName;
                var campaignDto = Maps.CampaignMapper.MapCampaignDataToCampaignDTO(campaignData);

                 _logger.Info("Create report");
                var reportDto = _reportManager.CreateReport(campaignDto.Key);

                var report = Maps.ReportMapper.MapReportDtoToReport(reportDto);
                 _logger.Debug("[HttpGet] ReportController:Result()");
                 return View(report);
            }
            catch (BllException e)
            {

                var ex = new UiException("Employee list can not be recieved", e);
                _logger.Error($"ERROR ReportController::Index({campaignData.DumpCampaignDataModel()})", ex);
                throw ex;
            }

        }
  

    }
}