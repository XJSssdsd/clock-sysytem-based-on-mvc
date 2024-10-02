using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class ClockTimeView : MonoBehaviour
{
    Label clockTimeLabel;

    void OnEnable()
    {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        clockTimeLabel = rootVisualElement.Q<Label>("ClockTimeLabel");
    }
    public void BindClockTimeLabel(object dataSource,string propertyPath)
    {
        clockTimeLabel.SetBinding(nameof(clockTimeLabel.text), new DataBinding
        {
            dataSource = dataSource,
            dataSourcePath = new PropertyPath(propertyPath),
            bindingMode = BindingMode.ToTarget
        });

    }
}
