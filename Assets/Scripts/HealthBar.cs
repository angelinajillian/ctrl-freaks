using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Slider for health bar
    public Slider healthBar;
    // Gradient used to allow different health colors
    // public Gradient gradient;
    // // Image to change the different health colors
    public Image barColor;

    public void SetMaxHealth(int maxHealth)
    {
        // Set Max health in the beginning
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        // First color is the full value on the gradient
        // gradient.Evaluate(1f);
    }
    public void SetHealth(int healthValue)
    {
        // Reassign health value on the bar
        healthBar.value = healthValue;
        // Change color according to a normalized value
        // barColor.color = gradient.Evaluate(healthBar.normalizedValue);
    }
}