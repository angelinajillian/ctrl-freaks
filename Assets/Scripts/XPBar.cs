using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    // Slider for health bar
    public Slider xpBar;
    // Gradient used to allow different health colors
    public Gradient gradient;
    // Image to change the different health colors
    public Image barColor;
    


    //public void SetMaxXP(int maxXP)
    //{
    //    // Set Max health in the beginning
    //    xpBar.maxValue = Max;
    //    xpBar.value = maxHealth;
    //    // First color is the full value on the gradient
    //    gradient.Evaluate(1f);
    //}
    

    public void SetXP(float xpValue)
    {
        // Reassign health value on the bar
        xpBar.value = xpValue;
        // Change color according to a normalized value
        barColor.color = gradient.Evaluate(xpBar.normalizedValue);
    }
}