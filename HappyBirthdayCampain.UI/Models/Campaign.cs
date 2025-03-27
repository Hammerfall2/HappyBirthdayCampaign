using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HappyBirthdayCampain.UI.Models
{
    public partial class CampaignData
    {
        public string Title { get; set; }
        public int CampaignYear { get; set; }

        public int BirthdayUserId { get; set; }
        public string BirthdayUserFullName { get; set; }

        public int UserStatedCampId { get; set; }
        public string UserStartedCampaignFullName { get; set; }

        public bool IsCampaignActive { get; set; }
    }
    public partial class StartCampaign
    {

        public CampaignData StartCampaignData { get; set; }
        public List<SelectListItem> GetListBirthdayUsers { get; set; }
        public List<SelectListItem> GetListBirhdayYears { get; set; }

       
    }
    public partial class StopCampaign
    {
        public CampaignData StopCampaignData { get; set; }
        public string Title { get; set; }
        public List<CampaignData> AvailableCampaigns { get; set; }

    }
}