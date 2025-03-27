using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.BLL
{
    public class BllException : Exception
    {
        public BllException() : base() { }
        public BllException(string Meassage) : base(Meassage) { }
        public BllException(string Meassage, Exception innerException) : base(Meassage,innerException) { }

    }
}
