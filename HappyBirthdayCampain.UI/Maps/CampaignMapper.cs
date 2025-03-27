using HappyBirthdayCampain.BLL;
using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace HappyBirthdayCampain.UI.Maps
{
    internal static class CampaignMapper
    {
  
       
        public static Models.CampaignData Map(BOL.CampaignDTO campaign)
        {

            return new Models.CampaignData()
            {
                BirthdayUserFullName = campaign.BirthdayPresentUser.FirstName.Replace(" ", "") + " " + campaign.BirthdayPresentUser.LastName.Replace(" ", ""),
                CampaignYear = campaign.CampaignYearDate,
                UserStatedCampId = campaign.UserStartedCampain.Key.Id,
                BirthdayUserId = campaign.BirthdayPresentUser.Key.Id,
                UserStartedCampaignFullName = campaign.UserStartedCampain.FirstName.Replace(" ", "") + " " + campaign.UserStartedCampain.LastName.Replace(" ", ""),
                IsCampaignActive = campaign.IsActive


            };
           
        }

        public static List<SelectListItem> MapBirthdayListUsers(List<BOL.EmployeeDTO> dTO)
        {

            List <SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (BOL.EmployeeDTO dTO1 in dTO)
            {
                selectListItems.Add(new SelectListItem { Text = dTO1.FirstName + " " + dTO1.LastName, Value = dTO1.Key.Id.ToString() });
            }

            return selectListItems;
        }

        public static List<SelectListItem> MapCampaignListYears(List<BOL.EmployeeDTO> dTO)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            for (int i = DateTime.Now.Year; i < DateTime.MaxValue.Year; i++)
            {

                selectListItems.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            }
            return selectListItems;
        }

        public static List<Models.CampaignData> MapCampaigDtoToCampaign(List<BOL.CampaignDTO> campaignDTOs)
        {

            List<Models.CampaignData> stopCampaign = new List<Models.CampaignData>();
            if (campaignDTOs == null) { stopCampaign = null; }
            else
            {
                foreach (var campaignDTO in campaignDTOs)

                    stopCampaign.Add(Maps.CampaignMapper.Map(campaignDTO));
            }


            return stopCampaign;
        }

        public static BOL.CampaignDTO MapStartCampaignUiToBol(StartCampaign campaign)
        {
            var birthdayUserFullName = campaign.GetListBirthdayUsers.First(r => int.Parse(r.Value) == campaign.StartCampaignData.BirthdayUserId).Text;
            var year = campaign.GetListBirhdayYears.First(r => int.Parse(r.Value) == campaign.StartCampaignData.CampaignYear).Text;

            return new BOL.CampaignDTO()
            {
                Key = new BOL.CampaignKey() { BirthdayPresentUser = new BOL.EmployeeKey() { Id = campaign.StartCampaignData.BirthdayUserId }, CampaignYearDate = campaign.StartCampaignData.CampaignYear },
                BirthdayPresentUser = new BOL.EmployeeDTO() { Key = new BOL.EmployeeKey() { Id = campaign.StartCampaignData.BirthdayUserId }, FirstName = birthdayUserFullName.Split(' ')[0], LastName = birthdayUserFullName.Split(' ')[1] },
                UserStartedCampain = new BOL.EmployeeDTO() { Key = new EmployeeKey() { Id = campaign.StartCampaignData.UserStatedCampId }, FirstName = campaign.StartCampaignData.UserStartedCampaignFullName.Split(' ')[0], LastName = campaign.StartCampaignData.UserStartedCampaignFullName.Split(' ')[1] },
                CampaignYearDate = int.Parse(year)
            };

        }
        public static BOL.CampaignKey MapCampaignDataToCampaignKey(CampaignData campaign)
        {

            return new CampaignKey()
            {
                BirthdayPresentUser = new EmployeeKey() { Id = campaign.BirthdayUserId},
                CampaignYearDate = campaign.CampaignYear
            };

        }

        public static BOL.CampaignDTO MapCampaignDataToCampaignDTO(CampaignData campaign)
        {

            return new CampaignDTO()
            {
                Key = new CampaignKey() { BirthdayPresentUser = new EmployeeKey { Id = campaign.BirthdayUserId }, CampaignYearDate = campaign.CampaignYear },
                BirthdayPresentUser = new EmployeeDTO()
                {
                    FirstName = campaign.BirthdayUserFullName.Split(' ')[0],
                    LastName = campaign.BirthdayUserFullName.Split(' ')[1]
                },
                UserStartedCampain = new EmployeeDTO() { FirstName = campaign.UserStartedCampaignFullName.Split(' ')[0],
                    LastName = campaign.UserStartedCampaignFullName.Split(' ')[1]
                },
                CampaignYearDate = campaign.CampaignYear
            };

        }

    }
}