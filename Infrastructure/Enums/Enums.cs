using System;
namespace Infrastructure.Enums
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

    public enum PacketType : UInt16
    {
        Nothing,
        ReturnToSender,
        Others,
        Local
    }
}
