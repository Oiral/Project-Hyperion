using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public int health;
    public int shield;
    public int maxHP = 15;

    public Text healthText;

    public Deck currentDeck;

    public void Damage(int amount)
    {
        //Deal damage to shield first
        shield -= amount;

        //If shield is in negative carry the damage over to health
        if (shield < 0)
        {
            health += shield;
            shield = 0;
        }
        Debug.Log("Health: " + health + " Shield: " + shield);
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHP)
        {
            health = maxHP;
        }
    }

    public void Block(int amount)
    {
        shield += amount;
    }

    private void Start()
    {
        currentDeck.GenerateActiveDeck();
    }
    private void OnDisable()
    {
        currentDeck.activeDeck.Clear();
    }
}
