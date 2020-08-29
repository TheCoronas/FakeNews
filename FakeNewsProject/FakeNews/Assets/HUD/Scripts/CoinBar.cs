using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

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