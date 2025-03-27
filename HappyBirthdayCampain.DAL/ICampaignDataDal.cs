using HappyBirthdayCampain.BOL;
using System.Collections.Generic;


namespace HappyBirthdayCampain.DAL
{
    public interface ICampaignDataDal
    {
        /// <summary>
        /// Try Get Employee By Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>EmployeeDTO</returns>
        /// <exception cref="DalException"></exception>
        BOL.EmployeeDTO TryGetEmployeeByUsername(string username);

        /// <summary>
        /// Get Emploeey by Key Id
        /// </summary>
        /// <param name="EmployeeKey"></param>
        /// <returns>BOL.EmployeeDTO</returns>
        /// <exception cref="DalException"></exception>
		BOL.EmployeeDTO GetEmployeeById(EmployeeKey key);

        /// <summary>
        /// Get List All available birthday users
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns>List<BOL.EmployeeDTO></returns>
        /// <exception cref="DalException"></exception>
        List<BOL.EmployeeDTO> GetAllEmployeesExcept(EmployeeKey key);

        /// <summary>
        /// Get all campaign data by user
        /// </summary>
        /// <param name="employeeKey"></param>
        /// <returns>List<BOL.CampaignDTO></returns>
        List<BOL.CampaignDTO> GetCampaignsDataByUserId(EmployeeKey employeeKey);

        /// <summary>
        /// Get all vote data by user
        /// </summary>
        /// <param name="employeeKey"></param>
        /// <returns><BOL.VoteDTO></returns>
        List<BOL.VoteDTO> GetVotesDataByUser(EmployeeKey employeeKey);

        /// <summary>
        /// Try get campaign by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>BOL.CampaignDTO</returns>
        BOL.CampaignDTO TryGetCampaignsByKey(CampaignKey key);

        /// <summary>
        /// Get all presents from db
        /// </summary>
        /// <returns>List<BOL.GiftPresentDTO></returns>
        List<BOL.GiftPresentDTO>GetAllPresents();

        /// <summary>
        /// Add campaign 
        /// </summary>
        /// <param name="campaignDTO"></param>
        void AddCampaign(CampaignDTO campaignDTO);

        /// <summary>
        /// Update campaign
        /// </summary>
        /// <param name="key"></param>
        /// <param name="campaignStatus"></param>
        void UpdateCampaign(CampaignKey key, bool campaignStatus);

        /// <summary>
        /// Add vote
        /// </summary>
        /// <param name="voteDTO"></param>
        void AddVote(BOL.VoteDTO voteDTO);

        /// <summary>
        /// Get all active campaign exceprt current user
        /// </summary>
        /// <param name="key"></param>
        /// <returns>List<BOL.CampaignDTO></returns>
        List<BOL.CampaignDTO> GetAllActiveCampaignsExcept(BOL.EmployeeKey key);

        /// <summary>
        /// Get all Inactive campaign exceprt current user
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <param name="key"></param>
        /// <returns>List<BOL.CampaignDTO></returns>
        List<BOL.CampaignDTO> GetAllInActiveCampaignsExcept( int startYear, int endYear, BOL.EmployeeKey key);

        /// <summary>
        /// Get vote data by key
        /// </summary>
        /// <param name="voteKey"></param>
        /// <returns>BOL.VoteDTO</returns>
        BOL.VoteDTO GetVoteDataByKey(VoteKey voteKey);

        /// <summary>
        /// Get votes in campaign
        /// </summary>
        /// <param name="campaignKey"></param>
        /// <returns>List<BOL.VoteDTO></returns>
        List<BOL.VoteDTO> GetVotesInCampaign(CampaignKey campaignKey);

        /// <summary>
        /// Update password with a hash value 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="password"></param>
        void UpdateUserPassword(BOL.EmployeeKey key, string password);


    }
}
