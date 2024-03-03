using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    // Slider for Mana bar
    public Slider manaBar;

    public void SetMaxMana(float maxMana)
    {
        // Set Max mana in the beginning
        manaBar.maxValue = maxMana;
        manaBar.value = maxMana;
    }
    public void SetMana(float manaValue)
    {
        // Reassign mana value on the bar
        manaBar.value = manaValue;
    }
}