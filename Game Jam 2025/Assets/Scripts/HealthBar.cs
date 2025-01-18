using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void UpdateHealthBar(float curentValue, float maxValue)
    {
        slider.value = curentValue / maxValue;
    }
    void Update()
    {
        
    }
}
