using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.BOL
{
    public static class Extension
    {
        public static bool IsEqual(this EmployeeKey key1, EmployeeKey key2)
        {
            return key1.Id == key2.Id;
        }

        public static bool IsEqual(this VoteKey key1, VoteKey key2)
        {

            return key1.Campaign.CampaignYearDate == key2.Campaign.CampaignYearDate && key1.Campaign.BirthdayPresentUser.IsEqual(key2.Campaign.BirthdayPresentUser)&&
                                                    key1.VotingUser.IsEqual(key2.VotingUser);
        }

        public static string DumpCampaignDto(this CampaignDTO campaign)
        {
            string messageDebugCampaignDto = "CampaignUserId: " + campaign.UserStartedCampain.Key.Id + " " +
                         "BirthdayUser: " + campaign.BirthdayPresentUser.FirstName + " " + campaign.BirthdayPresentUser.LastName + " " +
                         "CampaignYear: " + campaign.CampaignYearDate;

            return messageDebugCampaignDto;
        }

        public static string DumpCampaignKey(this CampaignKey key)
        {
            string messageDebugCampaignKey = "BirthdayUserId: " + key.BirthdayPresentUser.Id + " " +
                         "CampaignYear: " + key.CampaignYearDate;

            return messageDebugCampaignKey;
        }

        public static string DumpVoteDto(this VoteDTO vote)
        {
            string messageDebugVoteDtoToString = "VoteUser: " + vote.VotingUser.FirstName + " " + vote.VotingUser.LastName + " " +
                         "BirthdayUser: " + vote.Campaign.BirthdayPresentUser.FirstName + " " + vote.Campaign.BirthdayPresentUser.LastName + " " +
                         "CampaignUserId: " + vote.Campaign.UserStartedCampain.Key + " " +
                         "Present: " + vote.GiftPresent.GiftName;

            return messageDebugVoteDtoToString;
        }

        public static string DumpVoteKey(this VoteKey key)
        {
            string messageDebugVoteKeyDtoToString = "VoteUserKey: " + key.Campaign.DumpCampaignKey() + key.VotingUser.DumpEmployeeKey();

            return messageDebugVoteKeyDtoToString;
        }

        public static string DumpEmployeeKey(this EmployeeKey key)
        {
             string messageEmployee = "EmployeeId: " + key.Id;
            return messageEmployee;
        }
    }
}
