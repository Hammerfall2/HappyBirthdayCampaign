
using System;
using System.Collections.Generic;
using System.Linq;
using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.DAL.Maps;



namespace HappyBirthdayCampain.DAL
{

    public class CampaignData : ICampaignDataDal
    {

        private CampaignDb DbModel1 { get; set; } = new CampaignDb();
     
        private readonly IDalLogger _logger;

        public CampaignData(IDalLogger dalLogger)
        {
            _logger = dalLogger;
        }

        public BOL.EmployeeDTO TryGetEmployeeByUsername(string username)
        {
            _logger.Debug("Call CampaignData:TryGetEmployeeByUsername()");

            var dbObj = DbModel1.Employees.SingleOrDefault(emp => emp.UserName == username); 
			if (dbObj == null) 
				return null;

            _logger.Info("Вземане на данни на юзера по име");
            var res = EmployeeMapper.Map(dbObj);

            _logger.Debug("Return CampaignData:TryGetEmployeeByUsername()");
            return res;

		}
		public BOL.EmployeeDTO GetEmployeeById(EmployeeKey employeeKey)
		{
            _logger.Debug("Call CampaignData:GetEmployeeById()");

            try
            {
                _logger.Info("Get User by Id");
                var dbObj = DbModel1.Employees.Single(user => user.Id == employeeKey.Id); 
				var res = EmployeeMapper.Map(dbObj);

                _logger.Debug("Return CampaignData:GetEmployeeById()");
                return res;
			} 
			catch(Exception e) 
			{
                var ex = new DalException("Юзерът не съществува", e);
                _logger.Error($"ERROR CampaignData::GetEmployeeById( {employeeKey.DumpEmployeeKey()})", ex);
                throw ex;
            }
            
		}

        public List<CampaignDTO> GetCampaignsDataByUserId(EmployeeKey employeeKey)
        {

            _logger.Debug("Call CampaignData::GetCampaignsDataByUserId()");
            
            List<CampaignDTO> dTOs = new List<CampaignDTO>();
            var dbObj = DbModel1.Campaigns.Where(emp => emp.UserCampainId == employeeKey.Id);

            if (dbObj.Count() == 0)
                dTOs= null;
            else
            {
                _logger.Info("Get Campaigns data by employee Id");
                foreach (Campaign c in dbObj)
                    dTOs.Add(CampaignMapper.MapCampaignDalToBol(c));
            }

            _logger.Debug("Return CampaignData:GetCampaignsDataByUserId()");
            return dTOs;
   
        }

        public CampaignDTO TryGetCampaignsByKey(CampaignKey key)
        {
            _logger.Debug("Call CampaignData::TryGetCampaignsByKey()");

            _logger.Info("Get Campaigns data by campaign Id");
            var result = DbModel1.Campaigns.SingleOrDefault(emp => emp.BirthdayUserId == key.BirthdayPresentUser.Id &&
                                                                emp.CampaignYearDate == key.CampaignYearDate);
            if (result == null)
                return null;
            else
            {
                _logger.Debug("Return CampaignData::TryGetCampaignsByKey()");
                return CampaignMapper.MapCampaignDalToBol(result);
            }

        }

        public List<EmployeeDTO> GetAllEmployeesExcept(EmployeeKey key)
        {
            _logger.Debug("Call CampaignData::GetAllEmployeesExcept()");
 
            List< EmployeeDTO > birhdayList = new List < EmployeeDTO >();
            try
            {
                var dbObj = DbModel1.Employees.Where(emp => emp.Id != key.Id);
                if (dbObj == null)
                    birhdayList = null;
                else
                {
                    _logger.Info("Get all possible employee except cuurent user Id");
                    foreach (Employee dTO in dbObj)
                        birhdayList.Add(EmployeeMapper.Map(dTO));
                } 

                return birhdayList;
            }
            catch (Exception e)
            {
                var ex = new DalException("Невалиден списък от юзъри", e);
                _logger.Error($"ERROR CampaignData::GetAllEmployeesExcept( {key.DumpEmployeeKey()})", ex);
                throw ex;
            }
        }

        public List<GiftPresentDTO> GetAllPresents()
        {
            List<GiftPresentDTO> giftPresentDTOs = new List<GiftPresentDTO>();
            var dbObj = DbModel1.GiftPresents.ToList();
            foreach (GiftPresent dTO in dbObj)
                giftPresentDTOs.Add(Maps.PresentMapper.MapPresentDalToBol(dTO));

            return giftPresentDTOs;
        }

        public void AddCampaign(BOL.CampaignDTO dTO)
        {
            _logger.Debug("Call  CampaignData::AddCampaign()");
            
            try
            {
                _logger.Info("Add a new campaign in db");
                Campaign campaign = Maps.CampaignMapper.MapCampaignBolToDal(dTO);

                DbModel1.Campaigns.Add(campaign);
                DbModel1.SaveChanges();
                
            }
            catch (Exception e)
            {
                var ex = new DalException("Не може да бъде създадена нова кампания", e);
                _logger.Error($"ERROR CampaignData::AddCampaign()", ex);
                throw ex;

            }

        }

        public void UpdateCampaign(CampaignKey key, bool campaignStatus)
        {
            _logger.Debug("Call CampaignData::UpdateCampaign()");

            try
            {
                _logger.Info("Update a campaign in db");
                DbModel1.Campaigns.SingleOrDefault(u => u.CampaignYearDate == key.CampaignYearDate &&
                                                    u.BirthdayUserId == key.BirthdayPresentUser.Id).IsCampaignActive = campaignStatus;
                DbModel1.SaveChanges();
               

            }
            catch (Exception e)
            {
                var ex = new DalException("Не може да бъде ъпдейтната съществуваща кампания", e);
                _logger.Error($"ERROR CampaignData::UpdateCampaign()", ex);
                throw ex;

            }
        }

        public void AddVote(BOL.VoteDTO dTO)
        {
            _logger.Debug("Call CampaignData::AddVote()");
            try
            {
                _logger.Info("Create a vote in db");

                Vote vote = Maps.VoteMapper.MapVoteBolToDal(dTO);
                DbModel1.Vote.Add(vote);
                DbModel1.SaveChanges();

            }
            catch (Exception e)
            {
                var ex = new DalException("Не може да бъде създадено ново гласуване", e);
                _logger.Error($"ERROR CampaignData::AddVote()", ex);
                throw ex;
            }

        }

        public List<VoteDTO> GetVotesDataByUser(EmployeeKey employeeKey)
        {
            _logger.Debug("Call CampaignData::GetVotesDataByUser()");

            List<VoteDTO> dTOs = new List<VoteDTO>();
            var dbObj = DbModel1.Vote.Where(emp => emp.VoteUserId == employeeKey.Id);
            if (dbObj.Count() == 0)
                dTOs = null;
            else
            {
                _logger.Info("get all votes by user Id");
                foreach (Vote c in dbObj)
                    dTOs.Add(VoteMapper.Map(c));
            }

            _logger.Debug("Return CampaignData::GetVotesDataByUser()");
            return dTOs;

        }

        public VoteDTO GetVoteDataByKey(VoteKey voteKey)
        {
            _logger.Debug("Call CampaignData::GetVoteDataByKey()");

            VoteDTO dTO = new VoteDTO();
            var dbObj = DbModel1.Vote.SingleOrDefault(emp => emp.VoteUserId == voteKey.VotingUser.Id &&
                                            emp.BirthdayUserId == voteKey.Campaign.BirthdayPresentUser.Id &&
                                            emp.CampaignYear == voteKey.Campaign.CampaignYearDate);

            if (dbObj == null)
                dTO = null;
            else
            {
                _logger.Info("get vote by vote key");
               
                    dTO = VoteMapper.Map(dbObj);
            }

            _logger.Debug("Return CampaignData::GetVoteDataByKey()");
            return dTO;

        }

        public List<BOL.CampaignDTO> GetAllActiveCampaignsExcept(BOL.EmployeeKey key)
        {
            _logger.Debug("Call CampaignData::GetAllActiveCampaignsExcept()");

            List<BOL.CampaignDTO> campaignDTOs = new List<BOL.CampaignDTO>();

            var dbObj = DbModel1.Campaigns.Where(emp => emp.IsCampaignActive == true && emp.BirthdayUserId != key.Id);
            if(dbObj.Count() == 0)
                campaignDTOs = null;
            else
            {
                _logger.Info("Get all active campaigns");
                foreach (Campaign c in dbObj)
                    campaignDTOs.Add(Maps.CampaignMapper.MapCampaignDalToBol(c));
            }

            _logger.Debug("Return CampaignData::GetAllActiveCampaignsExcept()");
            return campaignDTOs;
        }

        public List<BOL.CampaignDTO> GetAllInActiveCampaignsExcept(int startYear, int endYear, BOL.EmployeeKey key)
        {
            _logger.Debug("Call CampaignData::GetAllActiveCampaignsExcept()");

            List<BOL.CampaignDTO> campaignDTOs = new List<BOL.CampaignDTO>();

            var dbObj = DbModel1.Campaigns.Where(emp => emp.IsCampaignActive == false && emp.BirthdayUserId == key.Id &&
                                                 (emp.CampaignYearDate >= startYear && emp.CampaignYearDate <= endYear));
            if (dbObj.Count() == 0)
                campaignDTOs = null;
            else
            {
                _logger.Info("Get all active campaigns");
                foreach (Campaign c in dbObj)
                    campaignDTOs.Add(Maps.CampaignMapper.MapCampaignDalToBol(c));
            }

            _logger.Debug("Return CampaignData::GetAllActiveCampaignsExcept()");
            return campaignDTOs;
        }

        public List<VoteDTO> GetVotesInCampaign(CampaignKey campaignKey)
        {
            _logger.Debug("Call CampaignData::GetVotesInCampaign()");

            List<VoteDTO> dTO = new List<VoteDTO>();
            try
            {
                var dbObj = DbModel1.Vote.Where(camp => camp.BirthdayUserId == campaignKey.BirthdayPresentUser.Id &&
                                            camp.CampaignYear == campaignKey.CampaignYearDate).ToList();


                foreach (var vote in dbObj)
                {
                    dTO.Add(VoteMapper.Map(vote));
                }
            }
            catch (Exception e)
            {

                var ex = new DalException("Empty Votes in Campaign", e);
                _logger.Error($"ERROR CampaignData::GetVotesInCampaign()", ex);
                throw ex;
            }

            _logger.Debug("Return CampaignData::GetVotesInCampaign()");
            return dTO;

        }

        public void UpdateUserPassword(BOL.EmployeeKey userKey, string password)
        {
            _logger.Debug("Call CampaignData::UpdateUserPassword()");

            try
            {

                DbModel1.Employees.Single(u => u.Id == userKey.Id).Password = password;
                DbModel1.SaveChanges();
            }
            catch (Exception e)
            {

                var ex = new DalException("Can not update DB with Hash value ", e);
                _logger.Error($"ERROR CampaignData::UpdateUserPassword()", ex);
                throw ex;
            }
        }
    }
}