using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class getPlayerName : MonoBehaviour
{

    public Player self;
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        if (Player.loggedIn)
        {

            var values = new NameValueCollection();
            values["user_id"] = Player.userId.ToString();
            values["currentHealth"] = Player.CurrentHealth.ToString();
            values["abilityPoints"] = Player.currentAbilityPoints.ToString();
            values["activeScene"] = SceneManager.GetActiveScene().buildIndex.ToString();
            values["coins"] = Player.currentCoins.ToString();
            values["characterCount"] = Player.characterCount.ToString();

            name = Player.userId.ToString();
        } else {
            name = "Player1";
        }

        GameObject[] toAddNane = GameObject.FindGameObjectsWithTag("needName");

        foreach (GameObject nameAdd in toAddNane)
        {
            nameAdd.GetComponent<TMPro.TextMeshProUGUI>().text = name + ",";
        }




    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] toAddNane = GameObject.FindGameObjectsWithTag("needName");

        foreach (GameObject nameAdd in toAddNane)
        {
            nameAdd.GetComponent<TMPro.TextMeshProUGUI>().text = name + ",";
        }
    }
}
