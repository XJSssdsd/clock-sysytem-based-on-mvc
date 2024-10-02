using UnityEngine;

public class DayAndNightPresenter : MonoBehaviour
{
    [SerializeField]
    ClockTimeModel clockTimeModel;
    [SerializeField]
    DayAndNightView dayAndNightView;

    void Start()
    {
        clockTimeModel.DayBreak += OnDayBreak;
        clockTimeModel.NightFail += OnNightFail;

        dayAndNightView.MoonLight.enabled = !clockTimeModel.IsDayTime;
        dayAndNightView.SunLight.enabled = clockTimeModel.IsDayTime;

    }
    void OnDayBreak()
    {
        dayAndNightView.MoonLight.enabled = false;
        dayAndNightView.SunLight.enabled = true;

    }
    void OnNightFail()
    {
        dayAndNightView.MoonLight.enabled = true;
        dayAndNightView.SunLight.enabled = false;
    }
    void Update()
    {
        dayAndNightView.UpdateSunAndMoonRotationAngle(clockTimeModel.IsDayTime,clockTimeModel.NormalizedDayTime,clockTimeModel.NormalizedNightTime);     
    }
}
