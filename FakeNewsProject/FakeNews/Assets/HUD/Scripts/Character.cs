using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Madison Beare
 * Returns the characterOrder, scrollDamage and scrollCoins assigned to each 
 * in game character. 
 */
public class Character : MonoBehaviour
{
    //initialises the order of characters in the game
    public int characterOrder;
    public int scrollDamage; 
    public int ScrollCoins; 

    public int getCharacterOrder()
    {
        return characterOrder;
    }

    public int getScrollDamage()
    {
        return scrollDamage;
    }

    public int getScrollCoins()
    {
        return ScrollCoins;
    }
}