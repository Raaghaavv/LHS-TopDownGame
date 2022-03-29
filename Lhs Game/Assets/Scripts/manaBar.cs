using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manaBar : MonoBehaviour
{
    public Slider slider;

    //public int maxMana;
    //public int currentMana;

    public void setMaxMana(int max){
        slider.maxValue = max; // maximum value
        //maxMana = max;
        setMana(max); 
    }

    public void setMana(int mana){
        slider.value = mana; // sets the health value whenever we need it to be set
        //currentMana = mana;
    }
}
