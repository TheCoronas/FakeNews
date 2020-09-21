using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int maxHealth = 10; 
    public int maxCoins = 20; 
    public int maxAbilityPoints = 3;
    private static int currentHealth;
    public int currentCoins; 
    public int currentAbilityPoints; 

    public HealthBar healthBar; 
    public CoinBar coinBar; 
    public AbilityPointsBar abilityPointsBar;

    public static int damage;
    public static int expense; 
    public static int points; 

    public static int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentCoins = maxCoins; 
        currentAbilityPoints = maxAbilityPoints; 

        healthBar.SetMaxHealth(maxHealth); 
        coinBar.SetMaxCoins(maxCoins); 
        abilityPointsBar.SetMaxAbilityPoints(maxAbilityPoints); 

        
    }

    // Update is called once per frame
    void Update()
    {
        if (PopUpMenus.displayCorrectExplanation) 
        {
            if (currentHealth  > 0) 
            {
                damage = SelectObject.scrollDamage; 
                TakeDamage(damage); 
                PopUpMenus.displayCorrectExplanation = false; 
            } else {
                currentHealth = 0; 
            }

            if (currentAbilityPoints > 0) {
                expense = SelectObject.scrollAbilityPoints; 
                TakeAbilityPoints(expense); 
                PopUpMenus.displayCorrectExplanation = false; 
            } else {
                currentAbilityPoints = 0; 
            }       
            
            if (currentCoins > 0) {
                points = SelectObject.scrollCoins; 
                TakeCoins(points); 
                PopUpMenus.displayCorrectExplanation = false; 
            } else {
                currentCoins = 0; 
            }
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage; 

        healthBar.SetHealth(currentHealth); 
    }

    void TakeCoins(int expense)
    {
        currentCoins -= expense; 

        coinBar.SetCoins(currentCoins); 
    }

    void TakeAbilityPoints(int points)
    {
        currentAbilityPoints -= points; 

        abilityPointsBar.SetAbilityPoints(currentAbilityPoints); 
    }
}
