using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.DAL;
using System.Collections.Generic;
using System.Linq;



namespace HappyBirthdayCampain.BLL
{
    public interface IReportManager
    {
        /// <summary>
        /// Create report 
        /// </summary>
        /// <param name="campaignKey"></param>
        /// <returns>VoteDTO</returns>
        ReportDTO CreateReport(CampaignKey campaignKey);
    }
    public class ReportManager : IReportManager
    {
        private readonly ICampaignManager _campaignManager;
        private readonly ICampaignDataDal _campaignDataDal;
        private readonly IUserManager _userManager;
        private readonly IBllLogger _logger;

        public ReportManager(ICampaignManager campaignManager, ICampaignDataDal campaignDataDal, IUserManager userManager, IBllLogger logger)
        {
            _campaignManager = campaignManager; _campaignDataDal = campaignDataDal; _userManager = userManager; _logger = logger;
        }

        public ReportDTO CreateReport(CampaignKey campaignKey)
        {

            _logger.Debug("Call ReportManager::CreateReport()");
            

            ReportDTO reportDTO = new ReportDTO();
            List<EmployeeDTO> employees = new List<EmployeeDTO>();

            VoteKey voteKey = new VoteKey();
            List<GiftPresentDTO> giftsDTO = new List<GiftPresentDTO>();

            try
            {
                //header BirhdayUser DTO
                _logger.Info("Get Birthday user data ");
                reportDTO.header.BirthDayUser = _userManager.GetUser(campaignKey.BirthdayPresentUser);

                //header Campaign Year
                _logger.Info("Get Campaign Year ");
                reportDTO.header.CampaignYear = campaignKey.CampaignYearDate;

                //all employees
                _logger.Info("Get All employees without Birthday User ");
                var allVoteEmployee = _campaignDataDal.GetAllEmployeesExcept(campaignKey.BirthdayPresentUser);

                //all presents
                _logger.Info("Get All gift presents");
                var allPresents = _campaignDataDal.GetAllPresents();

                //gets vote 
                _logger.Info("Get All votes data");
                var allVotes = _campaignDataDal.GetVotesInCampaign(campaignKey);

                //fill giftEmployeeDto with vote users 
                foreach (var vote in allVotes)
                {

                    if (reportDTO.summary.giftEmployeeDto.Keys.Any(giftEmp => giftEmp.Key.Id.Equals(vote.GiftPresent.Key.Id)))
                    {
                        employees.Add(vote.VotingUser);
                    }
                    else
                        reportDTO.summary.giftEmployeeDto.Add(vote.GiftPresent, employees = new List<EmployeeDTO>() { vote.VotingUser });

                    allVoteEmployee.Remove(allVoteEmployee.Find(emp => emp.Key.Id == vote.VotingUser.Key.Id));
                }

                //fill giftEmployeeDto with none vote users 
                foreach (var present in allPresents)
                {
                    if (!reportDTO.summary.giftEmployeeDto.Keys.Any(giftEmp => giftEmp.Key.Id.Equals(present.Key.Id)))
                        reportDTO.summary.giftEmployeeDto.Add(present, employees = new List<EmployeeDTO>() { });
                }

                //fill noVoteEmployees 
                reportDTO.noVoteEmployees = allVoteEmployee;

                //the winner
                reportDTO.WiningGiftPresent = reportDTO.summary.giftEmployeeDto.Where(gift => gift.Value != null).OrderByDescending(winner => winner.Value.Count).First().Key;

                _logger.Debug("Return ReportManager::CreateReport()");
                return reportDTO;
            }
            catch (DalException e)
            {

                var ex = new BllException("Unable to create report", e);
                _logger.Error($"ERROR ReportManager::CreateReport( {campaignKey.DumpCampaignKey()})", ex);
                throw ex;
            }




            
        }
    }
}
