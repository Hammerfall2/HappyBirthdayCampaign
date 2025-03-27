using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.DAL.Maps
{
    internal static class PresentMapper
    {
        public static BOL.GiftPresentDTO MapPresentDalToBol(DAL.GiftPresent src)
        {
            return new BOL.GiftPresentDTO()
            {
                Key = new BOL.GiftPresentKey() { Id = src.Id },
                GiftName = src.GiftName,

            };
        }
    }
}
