
using System.Collections.Generic;

using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.DAL;
using log4net;

namespace HappyBirthdayCampain.BLL
{
    public interface ICampaignManager
    {
        /// <summary>
        /// Get all Employees except current user
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns>List<BOL.EmployeeDTO></returns>
        List<BOL.EmployeeDTO> EmployeeUsersListExcept(BOL.EmployeeKey currentUser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaign"></param>
        void StartCampaign(BOL.CampaignDTO campaign);

        /// <summary>
        /// Start campaign
        /// </summary>
        /// <param name="currentUser"></param>
        void StopCampaign(CampaignKey key, BOL.EmployeeKey currentUser);

        /// <summary>
        /// Get all active campaign start by user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>st<CampaignDTO></returns>
        List<CampaignDTO> GetAllActiveCampaignsCreatedByUser(EmployeeKey user);

        /// <summary>
        /// Get all Active vote campaign by user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>List<VoteDTO></returns>
        List<VoteDTO> GetAllActiveVotesCreatedByUser(VoteKey user);

        /// <summary>
        /// Create vote 
        /// </summary>
        /// <param name="voteDTO"></param>
        void Vote(BOL.VoteDTO voteDTO);

        /// <summary>
        /// Get All available vote campaigns by user
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns>List<BOL.CampaignDTO></returns>
        List<BOL.CampaignDTO> AvailableVoteCampaignsList(BOL.EmployeeKey currentUser);

        /// <summary>
        /// Get all available Report campaigns by user
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns><BOL.CampaignDTO></returns>
        List<BOL.CampaignDTO> AvailableReportCampaignsList(int startYear, int endYear, BOL.EmployeeKey currentUser);

        /// <summary>
        /// Get all gift presents 
        /// </summary>
        /// <returns>List<BOL.GiftPresentDTO></returns>
        List<BOL.GiftPresentDTO> GetAllPresents();

        /// <summary>
        /// Get all active votes by vote key
        /// </summary>
        /// <param name="voteKey"></param>
        /// <returns>VoteDTO</returns>
        VoteDTO TryGetVoteByKey(VoteKey voteKey);

    }

	public class CampaignManager : ICampaignManager
    {
        //dependancy injection
        private readonly ICampaignDataDal _dbAction;
        private readonly IBllLogger _logger;

        public CampaignManager(ICampaignDataDal dbAction, IBllLogger logCampaignMngr)
        {
            
            _dbAction = dbAction;
            _logger = logCampaignMngr;
           
        }

        //done: check if UserCampainId, BirthdayUserId, CampaignYearDate are filled
        //done: BirthdayUserId must not be the same as UserCampainId
        //done: get all Campaigns for this BirthdayUserId for this CampaignYearDate - they must be none
        //done: call _dbAction.StartCampaign
        public void StartCampaign(BOL.CampaignDTO campaign)
        {


            _logger.Debug($"Call CampaignManager::StartCampaign({campaign.DumpCampaignDto()})");
            

            var obj = _dbAction.TryGetCampaignsByKey(campaign.Key);
            if(obj == null || obj.Key.BirthdayPresentUser.IsEqual(campaign.UserStartedCampain.Key) || obj.Key.CampaignYearDate == 0)
            {
                 campaign.IsActive = true;
                try
                {
                    _logger.Info("Start Campaign by voteKey ");
                    _dbAction.AddCampaign(campaign);
                }
                catch (DalException e)
                {
                    var ex = new BllException("Кампанията не може да бъде стартирана", e);
                    _logger.Error($"ERROR CampaignManager::StartCampaign( {campaign.DumpCampaignDto()})", ex);
                    throw ex;
                }
                
            }
            else
            {
                var ex = new BllException("За тази година има, за този потребител вече стартирана кампания");
                _logger.Error($"ERROR CampaignManager::StartCampaign( {campaign.DumpCampaignDto()})", ex);
                throw ex;
            }
            _logger.Debug("Return CampaignManager::StartCampaign()");
        }


        //done: call _dbAction.GetCampaignData
        //done: BirthdayUserId must not be the same as currentUser
        //done: UserCampainId must be currentUser
        //todo: status of the CampaignId must be active -- set flag IsActive
        //done: call _dbAction.StopCampagnDal
        public void StopCampaign(CampaignKey key, BOL.EmployeeKey currentUser)
        {

            _logger.Debug("Call CampaignManager::StopCampaign()");

            CampaignDTO dTO =  GetCampaignData(key);
            if (dTO.UserStartedCampain.Key.IsEqual(currentUser) && dTO.IsActive && !dTO.Key.BirthdayPresentUser.IsEqual(currentUser))
            {
                try
                {
                    _logger.Info("Stop Campaign by voteKey ");
                    _dbAction.UpdateCampaign(key, false);
                }
                catch (DalException e)
                {
                    var ex = new BllException("Кампанията не може да бъде спряна", e);
                    _logger.Error($"ERROR CampaignManager::StopCampaign( {key.DumpCampaignKey()})", ex);
                    throw ex;
                }
                
            }
            else
            {
                var ex = new BllException("Не сте орторизиран да спрете кампанията");
                _logger.Error($"ERROR CampaignManager::StopCampaign( {key.DumpCampaignKey()})", ex);
                throw ex;
            }
            _logger.Debug("Return CampaignManager::StopCampaign()");
        }

        //done: call _dbAction.GetCampaignData
        //done: BirthdayUserId must not be the same as currentUser
        public CampaignDTO GetCampaignData(CampaignKey key)
        {

            _logger.Debug("Call CampaignManager::GetCampaignData()");
            _logger.Info("Get Campaign Data ");

            var result = _dbAction.TryGetCampaignsByKey(key);

            if (result.UserStartedCampain.Key.IsEqual(key.BirthdayPresentUser))
            {
                var ex = new BllException("Рожденикът няма право до данните");
                _logger.Error($"ERROR CampaignManager::StopCampaign( {key.DumpCampaignKey()})", ex);
                throw ex;
            }

            _logger.Debug("Return CampaignManager::GetCampaignData()");
            return result;

        }

        public List<CampaignDTO> GetAllActiveCampaignsCreatedByUser(EmployeeKey user)
        {
            _logger.Debug("Call CampaignManager::GetAllActiveCampaignsCreatedByUser()");

            List<CampaignDTO> activeSessions = new List<CampaignDTO>();
            var obj = _dbAction.GetCampaignsDataByUserId(user);

            if(obj == null)
            {
                activeSessions = null;
            }
            else
            {
                _logger.Info("Get Active Campaign by User");
                foreach (var item in obj)
                {
                    if (item.IsActive)
                        activeSessions.Add(item);
                }
            }
            _logger.Debug("Return CampaignManager::GetAllActiveCampaignsCreatedByUser()");
            return activeSessions;
        }

        public List<VoteDTO> GetAllActiveVotesCreatedByUser(VoteKey user)
        {

            _logger.Debug("Call CampaignManager::GetAllActiveVotesCreatedByUser()");

            List<VoteDTO> activeVotes = new List<VoteDTO>();
            var obj = _dbAction.GetVotesDataByUser(user.VotingUser);

            if (obj == null)
            {
                activeVotes = null;
            }
            else
            {
                _logger.Info("Get Active Votes by User");
                
                foreach (var item in obj)
                {
                    if (item.Key.IsEqual(user))
                    {

                        var ex = new BllException("The voteKey is already vote fot this campaign");
                        _logger.Error($"ERROR CampaignManager::GetAllActiveVotesCreatedByUser( {user.DumpVoteKey()})", ex);
                        throw ex;
                    }
                    else
                        activeVotes.Add(item);


                }
            }

            _logger.Debug("Return CampaignManager::GetAllActiveVotesCreatedByUser()");
            return activeVotes;
        }


        public VoteDTO TryGetVoteByKey(VoteKey voteKey)
        {

            _logger.Debug("Call CampaignManager::TryGetVoteByKey()");

            VoteDTO vote = new VoteDTO();
            var obj = _dbAction.GetVoteDataByKey(voteKey);

            if (obj == null)
            {
                vote = null;
            }
            else
            {
                _logger.Info("Get Active Vote by vote key");

                vote = obj;
                
            }

            _logger.Debug("Return CampaignManager::TryGetVoteByKey()");
            return vote;
        }


        //done: call _dbAction.GetCampaignData
        //done: BirthdayUserId must not be the same as VotingUserId
        //done: status of the CampaignId must be active
        //done: get all Votes for this CampaignId for this VotingUserId - they must be none
        //done: call _dbAction.CreateVote
        public void Vote(BOL.VoteDTO voteDTO)
        {

            _logger.Debug("Call CampaignManager::Vote()");
            

            var campaigns = _dbAction.TryGetCampaignsByKey(voteDTO.Key.Campaign);
            if (campaigns == null)
            {
                var ex = new BllException("Няма активна кампания за гласуване", new System.NullReferenceException());
                _logger.Error($"ERROR CampaignManager::Vote( {voteDTO.DumpVoteDto()})", ex);
                throw ex;
            }

                
            else
            {
                var activeVote = TryGetVoteByKey (voteDTO.Key);
                if (!campaigns.BirthdayPresentUser.Key.IsEqual(voteDTO.Key.VotingUser) && voteDTO.Campaign.IsActive && activeVote == null)
                {
                    try
                    {
                        _logger.Info("Create a vote");
                        _dbAction.AddVote(voteDTO);
                    }
                    catch (DalException e)
                    { 
                        var ex = new BllException("Гласуването не може да бъде стартирано",e);
                        _logger.Error($"ERROR CampaignManager::Vote( {voteDTO.DumpVoteDto()})", ex);
                        throw ex;
                    }
                    
                }
                else
                {
                    var ex = new BllException("Гласуването не може да бъде осъществено");
                    _logger.Error($"ERROR CampaignManager::Vote( {voteDTO.DumpVoteDto()})", ex);
                    throw ex;
                }


            }
            _logger.Debug("Return CampaignManager::Vote()");
        }

        public List<BOL.EmployeeDTO> EmployeeUsersListExcept(BOL.EmployeeKey currentUser)
        {
            _logger.Debug("Call CampaignManager::EmployeeUsersListExcept()");
            
            try
            {
                _logger.Info("Get Birthday voteKey list");
                var res = _dbAction.GetAllEmployeesExcept(currentUser);

                _logger.Debug("Return CampaignManager::EmployeeUsersListExcept()");
                return res;
            }
            catch (DalException e )
            {
                var ex = new BllException("Списъкът с рожденици не може да бъде получен", e);
                _logger.Error($"ERROR CampaignManager::EmployeeUsersListExcept( {currentUser.DumpEmployeeKey()})", ex);
                throw ex;
            }

        }

        public List<BOL.CampaignDTO> AvailableVoteCampaignsList(BOL.EmployeeKey currentUser)
        {
            _logger.Debug("Call CampaignManager::AvailableVoteCampaignsList()");

            try
            {
                _logger.Info("Get Vote Campaign list");
                var res = _dbAction.GetAllActiveCampaignsExcept(currentUser);

                _logger.Debug("Return CampaignManager::AvailableVoteCampaignsList()");
                return res;
            }
            catch (DalException e)
            {
                var ex = new BllException("Списъкът за гласуване не може да бъде получен", e);
                _logger.Error($"ERROR CampaignManager::AvailableVoteCampaignsList( {currentUser.DumpEmployeeKey()})", ex);
                throw ex;
            }

        }

        public List<BOL.CampaignDTO> AvailableReportCampaignsList(int startYear, int endYear, BOL.EmployeeKey currentUser)
        {
            _logger.Debug("Call CampaignManager::AvailableReportCampaignsList()");

            try
            {
                _logger.Info("Get Vote Campaign list");
                var res = _dbAction.GetAllInActiveCampaignsExcept(startYear, endYear,currentUser);

                _logger.Debug("Return CampaignManager::AvailableReportCampaignsList()");
                return res;
            }
            catch (DalException e)
            {
                var ex = new BllException("Списъкът за гласуване не може да бъде получен", e);
                _logger.Error($"ERROR CampaignManager::AvailableReportCampaignsList( {currentUser.DumpEmployeeKey()})", ex);
                throw ex;
            }

        }

        public List<GiftPresentDTO> GetAllPresents()
        {
            _logger.Debug("Return CampaignManager::GetAllPresents()");
            return _dbAction.GetAllPresents();
        }

    }
}
