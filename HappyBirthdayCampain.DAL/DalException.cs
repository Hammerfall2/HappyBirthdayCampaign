using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.DAL
{
    public class DalException : Exception
    {
        public DalException() : base() { }
        public DalException(string message) : base(message) { }
        public DalException(string Meassage, Exception innerException) : base(Meassage, innerException) { }

    }
}
