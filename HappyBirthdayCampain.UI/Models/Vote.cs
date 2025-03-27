using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyBirthdayCampain.UI.Models
{
    public partial class Vote
    {
        public string Title { get; set; }

        public bool IsVoteCampaignActive { get; set; }
        public int CampaignYear { get; set; }


        public int BirthdayUserId { get; set; }
        public string BirthdayUserFullName { get; set; }

        public int VoteUserId { get; set; }
        public string VoteUserFullName { get; set; }

        public int UserCampaignUserId { get; set; }
        public string UserCampaignFullName { get; set; }

        public int GiftPresentId { get; set; }
        public string GiftPresentName { get; set; }

        public List<SelectListItem> GetListGiftPresent { get; set; }
        public List<CampaignData> OpenCampaign { get; set; }
    }

}