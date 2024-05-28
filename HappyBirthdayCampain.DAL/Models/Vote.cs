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
        public int VotingUserId { get; set; }

        [Key, Column(Order = 2)]
        public DateTime VotingDate { get; set; }

        public int GiftPresentId { get; set; }

        
    }
}
