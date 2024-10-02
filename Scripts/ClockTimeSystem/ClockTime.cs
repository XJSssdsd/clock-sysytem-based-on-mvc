using UnityEngine;

[System.Serializable]
public struct ClockTime
{
    [Range(0,23)]
    public int hour;
    [Range(0,59)]
    public int minute;
    [Range(0,59)]
    public int second;

    public ClockTime(int hour,int minute)
    {
        this.hour = hour;
        this.minute = minute;
        second = 0;
    }
    public ClockTime(int hour,int minute,int secode) : this(hour,minute)
    {
        this.hour = hour;
        this.minute = minute;
        this.second = secode;
    }
    public static int ToSecond(ClockTime clockTime)
    {
        return clockTime.hour * TimeConst.SecondsPerHours + clockTime.minute * TimeConst.SecondsPerMinutes + clockTime.second;
    }
    public static ClockTime FromSeconds(float currentTime)
    {
        int hours = Mathf.FloorToInt(currentTime / TimeConst.SecondsPerHours);
        int minutes = Mathf.FloorToInt(currentTime % TimeConst.SecondsPerHours / TimeConst.SecondsPerMinutes);
        int seconds = Mathf.FloorToInt(currentTime % TimeConst.SecondsPerMinutes);
        return new(hours,minutes,seconds);
    }
}
