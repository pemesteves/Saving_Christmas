using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider = null;
    [SerializeField]
    private Gradient gradient = null;
    [SerializeField]
    private Image fillImage = null;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fillImage.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        fillImage.color = gradient.Evaluate(slider.normalizedValue);
    }
}
