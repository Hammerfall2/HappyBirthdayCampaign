using HappyBirthdayCampain.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.DAL
{
    [Table("Campaign")]
    internal partial class Campaign
    {

        [Key, Column(Order = 0)]
        public int BirthdayUserId { get; set; }

        [Key, Column(Order = 1)]
        public int CampaignYearDate { get; set; }

        public string BirthdayUserFirstName { get; set; }
        public string BirthdayUserLastName { get; set; }


        public int UserCampainId { get; set; }

        public bool IsCampaignActive { get; set; }
        public string UserCampainFirstName { get; set; }
        public string UserCampainLastName { get; set; }
    }
}
