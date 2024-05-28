
using System.Collections.Generic;

using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.DAL;

namespace HappyBirthdayCampain.BLL
{
	public class VoteCampaign : IVote
    {
        //dependancy injection
        private readonly ICampaignData _action;

        public VoteCampaign(ICampaignData dbAction)
        {
            _action = dbAction;
        }

        public void StartCampaign(BOL.Campaign campaign)
        {
			//todo: check if UserStartedCampain, BirthdayPresentUser, CampaignYearDate are filled
			//todo: BirthdayPresentUser must not be the same as UserStartedCampain
			//toto: get all Campaigns for this BirthdayPresentUser for this CampaignYearDate - they must be none
            //toto: call _action.StartCampaign
        }

        public Campaign CampaignData(CampaignKey key, BOL.EmployeeDTO currentUser)
        {
			//todo: call _action.GetCampaignData
			//todo: BirthdayPresentUser must not be the same as currentUser
            return null;
        }

        public void StopCampaign(CampaignKey key, BOL.EmployeeDTO currentUser)
        {
			//todo: call _action.GetCampaignData
			//todo: BirthdayPresentUser must not be the same as currentUser
			//todo: UserStartedCampain must be currentUser
			//todo: status of the CampaignId must be active
			//todo: call _action.StopCampagnDal
        }

        public void Vote(BOL.Voting Voting)
        {
			//todo: call _action.GetCampaignData
			//todo: BirthdayPresentUser must not be the same as VotingUserId
			//todo: status of the CampaignId must be active
			//toto: get all Votes for this CampaignId for this VotingUserId - they must be none
			//todo: call _action.CreateVote
        }

    }
}
