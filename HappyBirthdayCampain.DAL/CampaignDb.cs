//using HappyBirthdayCampain.BOL;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HappyBirthdayCampain.DAL
{
    internal class CampaignDb : DbContext
    {
        public CampaignDb()
            : base("name=CampainDb")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<GiftPresent> GiftPresents { get; set; }
        public virtual DbSet<Vote> Vote { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
