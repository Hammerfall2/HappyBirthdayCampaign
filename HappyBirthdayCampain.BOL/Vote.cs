using System;

namespace HappyBirthdayCampain.BOL
{
	public class VoteKey
    {

        ///<summary>
        ///composite key :
        ///first/second primary key: Campaign key(Campaign) :BirthdayUserID + YearVote
        /// third primary key: VotingUser
        /// </summary>
        public CampaignKey Campaign { get; set; }
        public EmployeeKey VotingUser { get; set; }
    }

    public class VoteDTO
    {

		public VoteKey Key { get; set; }

		/// <summary>
		/// в коя кампания се гласува
		/// </summary>
		public CampaignDTO Campaign { get; set; }

		/// <summary>
		/// кой гласува
		/// </summary>
        public EmployeeDTO VotingUser { get; set; }
		 
		/// <summary>
		/// за кой подарък гласува
		/// </summary>
        public GiftPresentDTO GiftPresent { get; set; }

		/// <summary>
		/// кога е гласувано
		/// </summary>
        public DateTime VotingDate { get; set; }
    }

}
