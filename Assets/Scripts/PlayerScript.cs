using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public int health;

    public Text healthText;

    public Deck currentDeck;

    public void ChangeHealth(int healthToChange)
    {
        health += healthToChange;
        if (health <= 0)
        {
            Debug.Log("Im dead");
            health = 0;
        }
        healthText.text = health + "/10";
    }

    private void Start()
    {
        currentDeck.GenerateActiveDeck();
    }
    private void OnDisable()
    {
        Debug.Log("Test");
    }
}
