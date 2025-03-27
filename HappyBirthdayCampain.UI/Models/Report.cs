using HappyBirthdayCampain.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyBirthdayCampain.UI.Models
{
    public partial class Report
    {
        public string Title { get; set; }

        public int CampaignYear { get; set; }

        public string BirthDayUserFullName { get; set; }

        public List<string> VoteUserFullName { get; set; } = new List<string>();

        public List<String> GiftPresentName { get; set; } = new List<string>();

        public Dictionary<GiftPresentDTO, List<BOL.EmployeeDTO>> GiftPresentEmployees { get; set; } = new Dictionary<GiftPresentDTO, List<BOL.EmployeeDTO>>();

        public string GiftPresentWinner { get; set; } 
    }

    public partial class SelectReportData
    {
        public int StartYear { get; set; }
        public List<SelectListItem> SelectItemsStartYear { get; set; }

        public int EndYear { get; set; }
        public List<SelectListItem> SelectItemsEndYear { get; set; }

        public Employee BirthdayUserId { get; set; }
        public List<SelectListItem> SelectItemsBirthdayUser { get; set; }

        public Employee VoteEmployee {  get; set; }
    }
}