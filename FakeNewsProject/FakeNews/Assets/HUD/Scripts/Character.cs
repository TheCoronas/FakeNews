using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //initialises the order of characters in the game
    public int characterOrder;
    public int scrollDamage; 
    // public int scrollAbilityPoints; 
    public int ScrollCoins; 

    public int getCharacterOrder()
    {
        return characterOrder;
    }

    public int getScrollDamage()
    {
        return scrollDamage;
    }

    // public int getScrollAbilityPoints()
    // {
    //     return scrollAbilityPoints;
    // }

    public int getScrollCoins()
    {
        return ScrollCoins;
    }
}