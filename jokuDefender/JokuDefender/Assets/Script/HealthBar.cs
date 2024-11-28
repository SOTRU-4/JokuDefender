using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Vector3 offset;

    public void SetHealth(int maxHealth, int health)
    {
        slider.maxValue = maxHealth;
        gameObject.SetActive(health > 0 && health < maxHealth);
        slider.value = health;
    }

    private void Update()
    {
        slider.gameObject.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
