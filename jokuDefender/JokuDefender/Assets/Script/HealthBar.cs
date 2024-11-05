using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    public int maxHealth;
    [SerializeField] Vector3 offset;

    public void SetHealth(int health)
    {
        slider.maxValue = maxHealth;
        gameObject.SetActive(health < maxHealth);
        slider.value = health;
        
    }

    private void Update()
    {
        slider.gameObject.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
