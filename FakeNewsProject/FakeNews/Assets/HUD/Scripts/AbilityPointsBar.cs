using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/**
* Sets max ability points and slider value. 
* Author: Madison Beare
*/
public class AbilityPointsBar : MonoBehaviour
{
    public Slider slider; 

    public void SetMaxAbilityPoints(int abilityPoints) 
    {
        slider.maxValue = abilityPoints; 
        slider.value = abilityPoints; 
    }

    public void SetAbilityPoints(int abilityPoints) 
    {
        slider.value = abilityPoints; 
    }

}