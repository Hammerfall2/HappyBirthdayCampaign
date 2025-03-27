using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.DAL.Maps
{
    internal static class VoteMapper
    {
        public static BOL.VoteDTO Map(DAL.Vote vote)
        {
            return new BOL.VoteDTO()
            {
                Key = new BOL.VoteKey()
                {
                    Campaign = new BOL.CampaignKey() 
                    { 
                        CampaignYearDate = vote.CampaignYear,
                        BirthdayPresentUser = new BOL.EmployeeKey() { Id = vote.BirthdayUserId } 
                    },
                    VotingUser = new BOL.EmployeeKey() { Id = vote.VoteUserId }
                },
                Campaign = new BOL.CampaignDTO()
                {
                    Key = new BOL.CampaignKey() 
                    { 
                        BirthdayPresentUser = new BOL.EmployeeKey() { Id = vote.BirthdayUserId }, 
                        CampaignYearDate = vote.CampaignYear
               
                    },

                    BirthdayPresentUser = new BOL.EmployeeDTO() 
                    { 
                        Key = new BOL.EmployeeKey() { Id = vote.BirthdayUserId },
                        FirstName = vote.BirthdayUserFirstName.Replace(" ", ""),
                        LastName = vote.BirthdayUserLastName.Replace(" ", "")
                    },
                     
                   CampaignYearDate = vote.CampaignYear, 
                   UserStartedCampain = new BOL.EmployeeDTO()
                    {
                        Key = new BOL.EmployeeKey() { Id = vote.UserStartedCampainId },
                        FirstName = vote.BirthdayUserFirstName.Replace(" ", ""),
                        LastName = vote.BirthdayUserLastName.Replace(" ", "")

                   },
                },
                VotingUser = new BOL.EmployeeDTO 
                { 
                    Key = new BOL.EmployeeKey() { Id= vote.VoteUserId },
                    FirstName= vote.VoteUserFirstName.Replace(" ", ""),
                    LastName= vote.VoteUserLastName.Replace(" ", "")
                },
                GiftPresent = new BOL.GiftPresentDTO() 
                {
                    Key = new BOL.GiftPresentKey() { Id = vote.GiftPresentId },
                    GiftName = vote.GiftPresentName
                },
                VotingDate = vote.VoteDate    
            };
        }

        public static Vote MapVoteBolToDal(BOL.VoteDTO src)
        {

            return new Vote()
            {
                BirthdayUserFirstName = src.Campaign.BirthdayPresentUser.FirstName,
                BirthdayUserLastName = src.Campaign.BirthdayPresentUser.LastName,
                BirthdayUserId = src.Campaign.Key.BirthdayPresentUser.Id,
                UserStartedCampainFirstName = src.Campaign.UserStartedCampain.FirstName,
                UserStartedCampainLastName = src.Campaign.UserStartedCampain.LastName,
                UserStartedCampainId = src.Campaign.UserStartedCampain.Key.Id,
                VoteUserFirstName = src.VotingUser.FirstName,
                VoteUserLastName = src.VotingUser.LastName,
                VoteUserId = src.Key.VotingUser.Id,
                GiftPresentId = src.GiftPresent.Key.Id,
                GiftPresentName = src.GiftPresent.GiftName,
                CampaignYear = src.Campaign.CampaignYearDate,
                VoteDate = src.VotingDate

            };

        }

    }
}
