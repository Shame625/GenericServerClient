using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Enums
{
    public static class Enums
    {
        public enum LoginStatus : UInt16
        {
            Fail,
            Ok
        }
        public enum RegistrationStatus : UInt16
        {
            Fail,
            AlreadyExists,
            Ok
        }
    }
}
