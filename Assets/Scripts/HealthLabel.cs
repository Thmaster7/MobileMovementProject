using UnityEngine;
using UnityEngine.UI;

public class HealthLabel : MonoBehaviour
{
    public PlayerMove playerHealth;
    public Slider healthSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (healthSlider != null && playerHealth != null)
        {
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.playerHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider != null && playerHealth != null)
        {
            healthSlider.value = playerHealth.playerHealth;
        }
    }
}
