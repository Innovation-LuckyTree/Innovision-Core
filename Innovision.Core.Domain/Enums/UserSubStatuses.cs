namespace Innovision.Core.Domain.Enums
{
    public enum UserSubStatuses
    {
        Compliant = 0,
        Non_Compliant = 1,
        Warning = 2,
        //Suspended = 6,
        ThreeDaysSuspension = 3,
        SevenDaysSuspension = 4,
        ThirtyDaysSuspension = 5,
        Terminated = 6,
        Banned = 7,
        Locked = 8,
        Self_Exclusion = 9,
        Administrative_Exclusion = 10,
        Dormant = 11
    }
}
