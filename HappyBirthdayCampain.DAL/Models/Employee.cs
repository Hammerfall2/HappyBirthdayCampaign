using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace HappyBirthdayCampain.DAL

{

    [Table("Employee")]
    internal partial class Employee
    {

        public int Id { get; set; }

        [Key]
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(300)]
        public string Password { get; set; }

        public int PasswordType { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public int? GiftPresentId { get; set; }

        public int? GiftYear { get; set; }

        public bool? CampaignStarted { get; set; }

        public bool? BirthdayUserSelected { get; set; }

        public virtual GiftPresent GiftPresent { get; set; }

    }
}
