namespace HappyBirthdayCampain.BOL
{
	public class CampaignKey
    {
        //public int Id { get; set; }


        ///<summary>
        /// composite key
        /// firts primary key : BirthdayPresentUser
        /// seconf primary key: CampaignYearDate
        /// </summary>
        public int BirthdayPresentUserID { get; set; }
        public int CampaignYearDate { get; set; }

    }

    public class Campaign
    {

		public CampaignKey Key { get; set; }


		/// <summary>
		/// за кой е рожденник е кампанията
		/// </summary>
        public EmployeeKey BirthdayPresentUser { get; set; }
		 
		/// <summary>
		/// отговорнк за кампанията
		/// </summary>
        public EmployeeKey UserStartedCampain { get; set; }

		/// <summary>
		/// за коя година е кампанията
		/// </summary>
        public int CampaignYearDate { get; set; }
    }

}
