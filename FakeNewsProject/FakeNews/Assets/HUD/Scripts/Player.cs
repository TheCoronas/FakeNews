using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyDown("1")) 
        {
            if (currentHealth > 0) {
                TakeDamage(1); 
            } else {
                currentHealth = 0; 
            }
        }

        if (Input.GetKeyDown("2"))
        {
            if (currentAbilityPoints > 0) {
                TakeAbilityPoints(1); 
            } else {
                currentAbilityPoints = 0; 
            }
        }        

        if (Input.GetKeyDown("3"))
        {
            if (currentCoins > 0) {
                TakeCoins(1); 
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
