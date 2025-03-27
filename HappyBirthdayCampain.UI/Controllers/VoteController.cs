using HappyBirthdayCampain.BLL;
using HappyBirthdayCampain.UI.ExceptionUI;
using HappyBirthdayCampain.UI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HappyBirthdayCampain.UI.Controllers
{
    public class VoteController : Controller
    {
        private readonly ICampaignManager _campaign;
        private readonly IUserManager _employee;
        private readonly IUiLogger _logger;

        public VoteController(ICampaignManager campaign, IUserManager employee, UiLogger logger) { _campaign = campaign; _employee = employee; _logger = logger; }

        // GET: Vote
        public ActionResult Index(Employee employeeObj)
        {

            _logger.Debug("[HttpGet] VoteController:Index()");
            BOL.EmployeeKey employeeKey = Maps.EmployeeMapper.MapVmIdToKeyBol(employeeObj);
            BOL.EmployeeDTO employee = new BOL.EmployeeDTO();
            List<BOL.CampaignDTO> availableCampaignsDto = new List<BOL.CampaignDTO>();
            try
            {
                employee = _employee.GetUser(employeeKey);
            }
            catch (BllException e)
            {

                var ex = new UiException("User and Password are invalid");
                _logger.Error($"ERROR VoteController::Index({employeeObj.DumpEmployeeModel()})", ex);
                throw ex;
            }

            try
            {
                availableCampaignsDto = _campaign.AvailableVoteCampaignsList(employeeKey);
            }
            catch (BllException e)
            {

                var ex = new UiException("Failed to get all available vote campaign");
                _logger.Error($"ERROR VoteController::Index({employeeObj.DumpEmployeeModel()})", ex);
                throw ex;
            }
            
            var availbleCampaigns = Maps.CampaignMapper.MapCampaigDtoToCampaign(availableCampaignsDto);
            var getAllPresent = _campaign.GetAllPresents();

            _logger.Info("Create VotenModel data");
            Vote vote = new Vote()
            {
                OpenCampaign = availbleCampaigns,
                VoteUserFullName = employeeObj.FullName,
                VoteUserId = employeeObj.Id,
                GetListGiftPresent = Maps.VoteMapper.MapVoteList(getAllPresent)

            };

            _logger.Debug("Return [HttpGet] VoteController:Index()");
            return View(vote);
        }

        [HttpPost]
        public ActionResult Index(Vote voteObj)
        {
            try
            {
                _logger.Debug("[HttpPost] VoteController:Index()");

                Vote vote = new Vote();
                vote.Title = $" Vote for user : \n{voteObj.BirthdayUserFullName} for year {voteObj.CampaignYear} with present {voteObj.GiftPresentName} is add successiful";
                BOL.VoteDTO voteDTO = new BOL.VoteDTO();
                voteDTO = Maps.VoteMapper.MapVoteToVoteDto(voteObj);

                _logger.Info("Vote");
                _campaign.Vote(voteDTO);

                _logger.Debug("Return [HttpPost] VoteController:Index()");
                return Json(vote);
            }
            catch (BllException e)
            {

                var ex = new UiException("Failed to vote");
                _logger.Error($"ERROR VoteController::Index({voteObj.DumpVoteModel()})", ex);
                throw ex;
            }


        }


    }
}