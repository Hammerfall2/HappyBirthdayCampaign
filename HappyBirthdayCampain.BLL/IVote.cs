namespace HappyBirthdayCampain.BLL
{
	public interface IVote
    {
		#if TODO
        bool LogInVerification(string user, string password);

        string UserFirstName(string user);

        string UserLastName(string user);
     
        string UserNameById(int Id);

        bool IsCampainStarted();

        int UserId(string user);

        Campaign StartCampaign(string user, string birthdayUser);

        Campaign CampaignData();

        void StopCampaign(string user, Campaign campaign);

        void Vote(string user, int gift);

        Dictionary<List<int>, List<SelectListItem>> GetListUsers(string user);
        Dictionary<List<int>, List<SelectListItem>> GetListPresents();
        int CheckCampaignConditions(string user);

        Tuple<List<string>, List<string>, List<string>,List<string>> VoteReport();
        void ClearReportDataBll();
  #endif
    }
}
