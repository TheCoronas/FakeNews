using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    //public PopUpMenus popUpMenu; 
    public static int maxHealth = 10; 
    public static int maxCoins = 20; 
    public static int maxAbilityPoints = 3;
    private static int currentHealth;
    public static int currentCoins; 
    public static int currentAbilityPoints;
    public static int userId;
    public static int characterCount;
    public static bool loggedIn = false;
    
    // player's current progress in game
    public static int latestScene;
    
    // player's current latest character count
    public static int latestCharacterCount;

    public HealthBar healthBar; 
    public CoinBar coinBar; 
    public AbilityPointsBar abilityPointsBar;
    public static int activeScene;

    public static int health;
    public static int expense; 
    public int points; 

    public static int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        // currentHealth = maxHealth;
        // currentCoins = maxCoins; // todo change 
        // currentAbilityPoints = maxAbilityPoints; 
        
        // set max values
        healthBar.SetMaxHealth(maxHealth); 
        coinBar.SetMaxCoins(maxCoins); 
        abilityPointsBar.SetMaxAbilityPoints(maxAbilityPoints);    
        
       
    }

    // Update is called once per frame
    void Update()
    {
        // set current values
        healthBar.SetHealth(CurrentHealth);
        coinBar.SetCoins(currentCoins);
        abilityPointsBar.SetAbilityPoints(currentAbilityPoints);
         
        health = SelectObject.scrollDamage; 
        expense = SelectObject.scrollCoins;
        //points = SelectObject.scrollCoins;

        if (PopUpMenus.displayCorrectExplanation) 
        {
            PopUpMenus.characterCount += 1;
            characterCount += 1;
            latestCharacterCount = characterCount;
            currentHealth = currentHealth + health;
            //currentAbilityPoints = currentAbilityPoints + expense; 
            currentCoins = currentCoins + expense;

            if ((currentHealth) < maxHealth) { 
                healthBar.SetHealth(currentHealth); 
            } else {
                healthBar.SetHealth(maxHealth); 
                currentHealth = maxHealth; 
            }

            if ((currentCoins) < maxCoins) {  
                coinBar.SetCoins(currentCoins);
            } else {
                coinBar.SetCoins(maxCoins); 
                currentCoins = maxCoins;  
            }

        } else if (PopUpMenus.displayIncorrectExplanation) {
            PopUpMenus.characterCount += 1;
            characterCount += 1;

            latestCharacterCount = characterCount;
            currentHealth = currentHealth - health;
            //currentAbilityPoints = currentAbilityPoints - expense; 
            currentCoins = currentCoins - expense;

            if ((currentHealth) > 0) { 
                healthBar.SetHealth(currentHealth); 
            } else {
                healthBar.SetHealth(0); 
                currentHealth = 0; 
            }

            if ((currentCoins) > 0) {  
                coinBar.SetCoins(currentCoins);
            } else {
                coinBar.SetCoins(0);  
                currentCoins = 0;
            }
        }
        PopUpMenus.displayCorrectExplanation = false; 
        PopUpMenus.displayIncorrectExplanation = false;
    }

    public void updateAbilityPoints(int points) {
        currentAbilityPoints = currentAbilityPoints - points;
        if ((currentAbilityPoints) >= 0) { 
            abilityPointsBar.SetAbilityPoints(currentAbilityPoints); 
        } else {
            PopUpMenus.displayNotEnoughPoints = true;
            abilityPointsBar.SetAbilityPoints(0); 
            currentAbilityPoints = 0;
        }
    }
}