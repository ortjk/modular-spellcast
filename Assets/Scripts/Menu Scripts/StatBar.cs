using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public Slider _slider;

    public void SetMax(float stat)
    {
        _slider.maxValue = stat;
        _slider.value = stat;
    }

    public void SetFill(float stat)
    {
        _slider.value = stat;
    }
}
