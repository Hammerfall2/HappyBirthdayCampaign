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
        
        //public int Id { get; set; }

        [Key, Column(Order = 0)]
        public int BirthdayPresentUser { get; set; }

        [Key, Column(Order = 1)]
        public int UserStartedCampain { get; set; }

        public int CampaignYearDate { get; set; }
    }
}
