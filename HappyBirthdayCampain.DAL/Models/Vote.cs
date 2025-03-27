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
    [Table("Vote")]
    internal partial class Vote
    {
        [Key, Column(Order = 0)]
        public int BirthdayUserId { get; set; }

        [Key, Column(Order = 1)]
        public int CampaignYear { get; set; }

        [Key, Column(Order = 2)]
        public int VoteUserId { get; set; }


        public DateTime VoteDate { get; set; }

        public int GiftPresentId { get; set; }

        public string GiftPresentName { get; set; }

        public int UserStartedCampainId { get; set; }

        public string UserStartedCampainFirstName { get; set; }

        public string UserStartedCampainLastName { get; set; }

        public string BirthdayUserFirstName { get; set; }

        public string BirthdayUserLastName { get; set; }

        public string VoteUserFirstName { get; set; }

        public string VoteUserLastName { get; set; }

    }
}
