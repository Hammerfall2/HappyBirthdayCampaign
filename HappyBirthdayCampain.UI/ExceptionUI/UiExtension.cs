
using HappyBirthdayCampain.UI.Models;
using System.Net.Http.Headers;


namespace HappyBirthdayCampain.UI.ExceptionUI
{
    public static class UiExtension
    {
        public static string DumpEmployeeModel(this Employee employee)
        {
            string messageDebugEmployeeModel = "EmployeeName: " + employee.FullName + " " + "EmployeeId: " + employee.Id;
            return messageDebugEmployeeModel;
        }

        public static string DumpStartCampaignModel(this StartCampaign startCampaign)
        {
            string msgDebugStartCampaignModel = 
                "BirthdayUserName: " + startCampaign.StartCampaignData.BirthdayUserFullName + " " +
                "BirthdayUserId: " + startCampaign.StartCampaignData.BirthdayUserId + " " + 
                "CampaignYear: " + startCampaign.StartCampaignData.CampaignYear;


            return msgDebugStartCampaignModel;
        }

        public static string DumpCampaignDataModel(this CampaignData campaignData)
        {
            string msgDebugCampaignDataModel = 
                "Title: " + campaignData.Title + " " +
                "BirthdayUserName: " + campaignData.BirthdayUserFullName + " " +
                "BirthdayUserId: " + campaignData.BirthdayUserId + " " + 
                "CampaignYear: " + campaignData.CampaignYear + " " +
                "UserStartCampaignName: " + campaignData.UserStartedCampaignFullName + " " + 
                "CampaignStatus " + campaignData.IsCampaignActive;


            return msgDebugCampaignDataModel;
        }

        public static string DumpVoteModel(this Vote vote)
        {
            string msgDebugVoteModel = 
                "Title: " + vote.Title + " " +
                "VoteUserId: " + vote.VoteUserId + " " +
                "VoteUserName: " + vote.VoteUserFullName + " " +
                "UserCampaignId: " + vote.UserCampaignUserId + " " +
                "UserCampaignName: " + vote.UserCampaignFullName + " " +
                "BirthdayUserId: " + vote.BirthdayUserId + " " +
                "BirthdayUserName: " + vote.BirthdayUserFullName + " " +
                "GiftPresentId: " + vote.GiftPresentId + " " +
                "GiftPresentName: " + vote.GiftPresentName + " " +
                "OpenCampaigns: " + vote.OpenCampaign + " " +
                "VoteCampaignYear: " + vote.CampaignYear + " " +
                "Campaign status: " + vote.IsVoteCampaignActive;

            return msgDebugVoteModel;
        }

        public static string DumpReportModel(this SelectReportData selectReportData)
        {
            string msgDebugReportModel =
                "Start Year: " + selectReportData.StartYear + " " +
                "End Year: " + selectReportData.EndYear + " " +
                "Birthday User Id: " + selectReportData.BirthdayUserId.Id + " " +
                "Birthday User User: " + selectReportData.BirthdayUserId.FullName + " " +
                "Vote User Id: " + selectReportData.VoteEmployee.Id + " " +
                "Vote User Name: " + selectReportData.VoteEmployee.FullName;

            return msgDebugReportModel;
        }
    }
}