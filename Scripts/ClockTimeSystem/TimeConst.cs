using UnityEngine;

public class TimeConst
{
    public const int MinTimeSpeed = 1;
    public const int MaxTimeSpeed = 3600;

    public const int SecondsPerMinutes = 60;
    public const int MinutesPerHours = 60;
    public const int HoursPerDay = 24;
    public const int HoursPerHalfDay = HoursPerDay / 2;

    public const int SecondsPerHours = SecondsPerMinutes * MinutesPerHours;
    public const int SecondsPerDay = SecondsPerHours * HoursPerDay;
}
