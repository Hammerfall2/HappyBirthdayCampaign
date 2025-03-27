
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace HappyBirthdayCampain.DAL
{

    [Table("GiftPresent")]
    internal partial class GiftPresent
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string GiftName { get; set; }


    }
}
