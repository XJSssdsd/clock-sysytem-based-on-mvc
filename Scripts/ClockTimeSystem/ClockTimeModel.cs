using System.Text;
using Unity.Properties;
using UnityEngine;

public class ClockTimeModel : MonoBehaviour
{
    [SerializeField,Range(TimeConst.MinTimeSpeed,TimeConst.MaxTimeSpeed)]
    float timeSpeed = 1.0f;
    [SerializeField]
    ClockTime systemStart = new(5,0,0);

    [SerializeField]
    ClockTime sunriseTime = new(6,0,0);
    [SerializeField]
    ClockTime sunsetTime = new(18,0,0);

    [SerializeField]
    TimeDisplayFormat displayFormat;
    float currentTime;
    float previousTime;

    readonly StringBuilder stringBuilder = new();
    float CurrentTimeHour => currentTime / TimeConst.SecondsPerHours;
    float NightLengthInSeconds => ClockTime.ToSecond(sunriseTime) + (TimeConst.SecondsPerDay - ClockTime.ToSecond(sunsetTime));

    public bool IsDayTime => currentTime >= ClockTime.ToSecond(sunriseTime) && 
                             currentTime < ClockTime.ToSecond(sunsetTime);
    [CreateProperty]
    public string CurrentTimeString {  get; private set; }

    public float NormalizedDayTime
    {
        get
        {
                return Mathf.InverseLerp(ClockTime.ToSecond(sunriseTime),
                    ClockTime.ToSecond(sunsetTime), currentTime);
        }
    }
    public float NormalizedNightTime
    {
        get
        {
            float sunsetTimeInSeconds = ClockTime.ToSecond(sunsetTime);
            float currentTimeInSeconds = currentTime > sunsetTimeInSeconds
          ? currentTime - sunsetTimeInSeconds : TimeConst.SecondsPerDay - sunsetTimeInSeconds + currentTime;
            return currentTimeInSeconds / NightLengthInSeconds;
        }
    }
    public event System.Action NightFail = delegate { };
    public event System.Action DayBreak = delegate { };

    string GetTimeString()
    {
        stringBuilder.Clear();
        if (currentTime >= TimeConst.SecondsPerDay)
        {
            currentTime %= TimeConst.SecondsPerDay;
            previousTime = 0;
        }
        int hours = Mathf.FloorToInt(currentTime  / TimeConst.SecondsPerHours);
        int minutes = Mathf.FloorToInt(currentTime % TimeConst.SecondsPerHours / TimeConst.SecondsPerMinutes);
        int seconds = Mathf.FloorToInt(currentTime % TimeConst.SecondsPerMinutes);
        if (displayFormat == TimeDisplayFormat.Standard)
        {
            stringBuilder.AppendFormat("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
            return stringBuilder.ToString();
        }
        string amPm;
        if (hours >= TimeConst.HoursPerHalfDay)
        {
            amPm = "PM";
            hours -= TimeConst.HoursPerHalfDay;
           
        }
        else
        {
            amPm = "AM";
            if (hours == 0)
            {
                hours = TimeConst.HoursPerHalfDay;
            }
        }


        stringBuilder.AppendFormat("{0:D2}:{1:D2}:{2:D2} {3}", hours, minutes, seconds, amPm);
        return stringBuilder.ToString();
    }
    void Start()
    {
        currentTime = ClockTime.ToSecond(systemStart);
        previousTime = currentTime;
        CurrentTimeString = GetTimeString();
    }
    void Update()
    {
        currentTime += Time.deltaTime * timeSpeed;
        if (currentTime >= ClockTime.ToSecond(sunriseTime))
        {
            DayBreak.Invoke();
        }
        if (currentTime >= ClockTime.ToSecond(sunsetTime))
        {
            NightFail.Invoke();
        }
       

        if(currentTime - previousTime >= 1)
        {
            CurrentTimeString = GetTimeString();
            previousTime = currentTime;
        }
         
    }
}
