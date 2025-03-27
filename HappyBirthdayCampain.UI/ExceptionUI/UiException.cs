using System;

namespace HappyBirthdayCampain.UI
{
    public class UiException : Exception
    {
        public UiException() : base() { }
        public UiException(string Meassage) : base(Meassage) { }
        public UiException(string Meassage, Exception innerException) : base(Meassage, innerException) { }

    }
}
