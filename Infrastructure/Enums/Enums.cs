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

    public enum Class : UInt16
    {
        Warrior,
        Priest,
        Rogue
    }

    public enum Objects : UInt16
    {
        Cube,
        Sphere
    }

    public enum ErrorType : UInt16
    {
       FilterError,
       ProtectionLevelError,
       ParsingError
    }
}
