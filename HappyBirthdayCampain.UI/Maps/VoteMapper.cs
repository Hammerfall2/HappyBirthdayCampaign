using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HappyBirthdayCampain.UI.Maps
{
    internal static class VoteMapper
    {
        public static Models.Vote MapCampaignDtoToVote(BOL.CampaignDTO campaignDTO, BOL.EmployeeDTO employeeDTO, GiftPresentDTO giftPresentDTO)
        {

            return new Models.Vote()
            {

                BirthdayUserId = campaignDTO.Key.BirthdayPresentUser.Id,
                BirthdayUserFullName = campaignDTO.BirthdayPresentUser.FirstName + " " + campaignDTO.BirthdayPresentUser.LastName,
                VoteUserId = employeeDTO.Key.Id,
                VoteUserFullName = employeeDTO.FirstName + " " + employeeDTO.LastName,
                UserCampaignUserId = campaignDTO.UserStartedCampain.Key.Id,
                UserCampaignFullName = campaignDTO.UserStartedCampain.FirstName + " " + campaignDTO.UserStartedCampain.LastName,
                GiftPresentName = giftPresentDTO.GiftName.Replace(" ", ""),
                CampaignYear = campaignDTO.CampaignYearDate

            };

        }

        public static BOL.VoteDTO MapVoteToVoteDto(Vote vote)
        {
            var birthDayFirstName = vote.BirthdayUserFullName.Split(' ')[0];
            var birthDayLastName = vote.BirthdayUserFullName.Split(' ')[1];

            var userStartedCampaignFirstName = vote.UserCampaignFullName.Split(' ')[0];
            var userStartedCampaignLastName = vote.UserCampaignFullName.Split(' ')[1];

            var voteUserFirstName = vote.VoteUserFullName.Split(' ')[0];
            var voteUserLastName = vote.VoteUserFullName.Split(' ')[1];



            return new VoteDTO()
            {
                Key = new VoteKey()
                {
                    Campaign = new CampaignKey() { CampaignYearDate = vote.CampaignYear, BirthdayPresentUser = new EmployeeKey() { Id = vote.BirthdayUserId } },
                    VotingUser = new EmployeeKey() { Id = vote.VoteUserId }
                },
                Campaign = new CampaignDTO()
                {
                    Key = new CampaignKey() { CampaignYearDate = vote.CampaignYear, BirthdayPresentUser = new EmployeeKey() { Id = vote.BirthdayUserId } },
                    BirthdayPresentUser = new EmployeeDTO() { Key = new EmployeeKey() { Id = vote.BirthdayUserId }, FirstName = birthDayFirstName, LastName = birthDayLastName },
                    UserStartedCampain = new EmployeeDTO() { Key = new EmployeeKey() { Id = vote.UserCampaignUserId }, FirstName = userStartedCampaignFirstName, LastName = userStartedCampaignLastName },
                    CampaignYearDate = vote.CampaignYear,
                   IsActive = vote.IsVoteCampaignActive
                },
                VotingDate = DateTime.Now,
                GiftPresent = new GiftPresentDTO() { Key = new GiftPresentKey() { Id = vote.GiftPresentId }, GiftName = vote.GiftPresentName },
                VotingUser = new EmployeeDTO { Key = new EmployeeKey() { Id = vote.VoteUserId }, FirstName = voteUserFirstName, LastName = voteUserLastName }

            };
        }

        public static List<SelectListItem> MapVoteList(List<BOL.GiftPresentDTO> dTO)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem { Text = "--------", Value = "0" });
            foreach (BOL.GiftPresentDTO dTO1 in dTO)
            {
                selectListItems.Add(new SelectListItem { Text = dTO1.GiftName, Value = dTO1.Key.Id.ToString() });
            }

            return selectListItems;

        }

       public static BOL.VoteDTO MapCampaignEmployeeToVoteDto(CampaignDTO campaignDTO, EmployeeDTO dTO)
        {
            return null;
        }

    }
}