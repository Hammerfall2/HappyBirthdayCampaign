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
        public EmployeeKey BirthdayPresentUser { get; set; }
        public int CampaignYearDate { get; set; }

    }

    public class CampaignDTO
    {

		public CampaignKey Key { get; set; }


		/// <summary>
		/// за кой е рожденник е кампанията
		/// </summary>
        public EmployeeDTO BirthdayPresentUser { get; set; }
		 
		/// <summary>
		/// отговорнк за кампанията
		/// </summary>
        public EmployeeDTO UserStartedCampain { get; set; }

		/// <summary>
		/// за коя година е кампанията
		/// </summary>
        public int CampaignYearDate { get; set; }

        /// <summary>
        /// Статус на кампанията активна/неактивна 
        /// </summary>
        public bool IsActive { get; set; }
    }

}
