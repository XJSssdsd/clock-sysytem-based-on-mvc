using UnityEngine;

public class ClockTimePresenter : MonoBehaviour
{
    ClockTimeModel clockTimeModel;
    ClockTimeView  clockTimeView;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        clockTimeModel = GetComponent<ClockTimeModel>();
        clockTimeView = GetComponent<ClockTimeView>();  
    }
    void Start()
    {
        clockTimeView.BindClockTimeLabel(clockTimeModel,nameof(clockTimeModel.CurrentTimeString));     
    }
}
