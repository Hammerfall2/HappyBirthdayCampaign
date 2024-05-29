
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

        public void StartCampaign(BOL.CampaignDTO campaign)
        {
            //campaign from mapped from UI

            //todo: check if UserStartedCampain, BirthdayPresentUser, CampaignYearDate are filled
            // var res = _action.GetCampaignUserById(campaign.UserStartedCampain)
            // if(res.UserStartedCampain == null && res.BirthdayPresentUser ==null && res.CampaignYearDate ==0)

            //todo: BirthdayPresentUser must not be the same as UserStartedCampain
            // if(res.UserStartedCampain != res.BirthdayPresentUser)

            //toto: get all Campaigns for this BirthdayPresentUser for this CampaignYearDate - they must be none
            //var res = _action.GetCampaignsByBirthdayId(campaign.BirthdayPresentUser)
            //if(res.Year == null)
            //toto: call _action.StartCampaign

            ///summary 
            /// _action.GetCampaignUserById(campaign.UserStartedCampain)
            /// _action.GetCampaignsByBirthdayId(campaign.BirthdayPresentUser)
            /// call _action.StartCampaign
            /// 

        }

        public CampaignDTO CampaignData(CampaignKey key, BOL.EmployeeDTO currentUser)
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
			//todo: status of the CampaignId must be active -- let try UserCanpaignUser set to null or 0
			//todo: call _action.StopCampagnDal
        }

        public void Vote(BOL.VotingDTO Voting)
        {
            //todo: call _action.GetCampaignData
            //todo: BirthdayPresentUser must not be the same as VotingUserId
            //todo: status of the CampaignId must be active -- UserCanpaignUser !=0
            //toto: get all Votes for this CampaignId for this VotingUserId - they must be none
            //todo: call _action.CreateVote
        }

    }
}
