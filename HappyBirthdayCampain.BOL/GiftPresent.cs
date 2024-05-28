//using System.Data.Entity.Spatial;

namespace HappyBirthdayCampain.BOL
{
		
	public partial class GiftPresentKey
    {
        public int Id { get; set; }
    }

    public partial class GiftPresent
    {
        public GiftPresentKey Id { get; set; }
        public string GiftName { get; set; }

    }

}
