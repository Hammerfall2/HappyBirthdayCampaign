using System;

namespace HappyBirthdayCampain.BOL
{
	public class VotingKey
    {

        ///<summary>
        ///composite key :
        ///first/second primary key: Campaign key(Campaign) :BirthdayUserID + YearVote
        /// third primary key: VotingUser
        /// </summary>
        public CampaignKey CampaignId { get; set; }
        public int VotingUserId { get; set; }
    }

    public class Voting
    {

		public VotingKey Key { get; set; }

		/// <summary>
		/// в коя кампания се гласува
		/// </summary>
		public CampaignKey Campaign { get; set; }

		/// <summary>
		/// кой гласува
		/// </summary>
        public EmployeeKey VotingUser { get; set; }
		 
		/// <summary>
		/// за кой подарък гласува
		/// </summary>
        public GiftPresentKey GiftPresent { get; set; }

		/// <summary>
		/// кога е гласувано
		/// </summary>
        public DateTime VotingDate { get; set; }
    }

}
