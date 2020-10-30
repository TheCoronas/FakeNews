using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/**
* Sets max coins and slider value. 
* Author: Madison Beare
*/
public class CoinBar : MonoBehaviour
{
    public Slider slider; 

    public void SetMaxCoins(int coins) 
    {
        slider.maxValue = coins; 
        slider.value = coins; 
    }

    public void SetCoins(int coins) 
    {
        slider.value = coins; 
    }

}