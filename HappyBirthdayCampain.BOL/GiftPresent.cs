//using System.Data.Entity.Spatial;

namespace HappyBirthdayCampain.BOL
{
		
	public partial class GiftPresentKey
    {
        public int Id { get; set; }
    }

    public partial class GiftPresentDTO
    {
        public GiftPresentKey Key { get; set; }
        public string GiftName { get; set; }

    }

}
