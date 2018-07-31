using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

    public int health;

    public Text healthText;

    public Deck enemyDeck;

    public GameObject battleManager;

    public GameObject cardPrefab;

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
        enemyDeck.GenerateActiveDeck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject SpawnedCard = Instantiate(cardPrefab);
                SpawnedCard.GetComponent<PlayingCardScript>().info = enemyDeck.DrawRandom();
                SpawnedCard.GetComponent<PlayingCardScript>().targetPos = new Vector3(0, 0, i * 12);
            }
            
        }
    }
}
