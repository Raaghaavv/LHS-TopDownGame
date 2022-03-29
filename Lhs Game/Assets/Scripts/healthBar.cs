using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider slider;

    public void setMaxHealth(int health){
        slider.maxValue = health; // maximum value
        slider.value = health; // we do this to set the default value at the start
    }

    public void setHealth(int health){
        slider.value=health; // sets the health value whenever we need it to be set
    }
}
