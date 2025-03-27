
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.DAL.Maps
{
    internal static class CampaignMapper
    {
        public static BOL.CampaignDTO MapCampaignDalToBol(DAL.Campaign src)
        {

            return new BOL.CampaignDTO()
            {
                Key = new BOL.CampaignKey() { BirthdayPresentUser = new BOL.EmployeeKey() { Id = src.BirthdayUserId }, CampaignYearDate = src.CampaignYearDate },
                BirthdayPresentUser = new BOL.EmployeeDTO()
                {
                    Key = new  BOL.EmployeeKey { Id = src.BirthdayUserId },
                    FirstName = src.BirthdayUserFirstName,
                    LastName = src.BirthdayUserLastName,
                    //UserName = src.BirthdayUserName,
                    //Password = src.BirthdayUserPassword
                },
                UserStartedCampain = new BOL.EmployeeDTO()
                {
                    Key = new BOL.EmployeeKey { Id = src.UserCampainId },
                    FirstName = src.UserCampainFirstName,
                    LastName = src.UserCampainLastName,
                    //UserName= src.UserCampainUserName,
                    //Password = src.UserCampainPassword
                },
                CampaignYearDate = src.CampaignYearDate,
                IsActive = src.IsCampaignActive
            };

        }
        public static Campaign MapCampaignBolToDal(BOL.CampaignDTO src)
        {

            return new Campaign()
            {
                BirthdayUserId = src.Key.BirthdayPresentUser.Id,
                UserCampainId = src.UserStartedCampain.Key.Id,
                BirthdayUserFirstName = src.BirthdayPresentUser.FirstName,
                BirthdayUserLastName = src.BirthdayPresentUser.LastName,   
                UserCampainFirstName = src.UserStartedCampain.FirstName,
                UserCampainLastName = src.UserStartedCampain.LastName,
                CampaignYearDate = src.CampaignYearDate,
                IsCampaignActive = src.IsActive
            };

        }

        public static Campaign MapCampaignKeyBolToDal(BOL.CampaignKey src)
        {

            return new Campaign()
            {
                BirthdayUserId = src.BirthdayPresentUser.Id,
                CampaignYearDate = src.CampaignYearDate
            };

        }


    }
}
