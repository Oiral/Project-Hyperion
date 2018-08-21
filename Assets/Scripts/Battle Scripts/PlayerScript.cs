using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public int health;
    public int shield;
    public int maxHP = 15;

    public Text healthText;
    public Text shieldText;
    public Image bust;
    public Text playerName;

    public Deck currentDeck;
    public List<GameCard> discardedCards = new List<GameCard>();

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
		if(health < 0)
		{
			health = 0;
		}
        Debug.Log("Health: " + health + " Shield: " + shield);
        UpdateUI();
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHP)
        {
            health = maxHP;
        }
        UpdateUI();
    }

    public void Block(int amount)
    {
        shield += amount;
        UpdateUI();
    }

    private void Start()
    {
        currentDeck.GenerateActiveDeck();
        UpdateUI();
    }
    private void OnDisable()
    {
        currentDeck.activeDeck.Clear();
    }
    public void UpdateUI()
    {
        healthText.text = health.ToString();
        shieldText.text = shield.ToString();

        if (playerName != null)
        {
            playerName.text = GameManager.instance.enemyName;
        }

        if (bust != null)
        {
            bust.sprite = GameManager.instance.enemyBust;
        }
    }
}
