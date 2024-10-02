using UnityEngine;

public class DayAndNightView : MonoBehaviour
{
    [SerializeField] Light sunLight;
    [SerializeField] Light moonLight;

    public Light SunLight => sunLight;
    public Light MoonLight => moonLight;
    public void UpdateSunAndMoonRotationAngle(bool isDayTime,float normalizedTimeOfDay,float normalizedNightTime)
    {
        float dayTimeAngle = 180 * normalizedTimeOfDay;
        float nightTimeAngle = 180 + 180 * normalizedNightTime;
        float angle = isDayTime ? dayTimeAngle : nightTimeAngle;    
        transform.eulerAngles = new Vector3(angle,transform.rotation.y,transform.rotation.z);
    }

}
