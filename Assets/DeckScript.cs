using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour {

    public List<Card> deck;

    public GameObject cardPrefab;
    public Transform layoutParent;

    public List<GameObject> cardsVisualised;

    public void SpawnInDeck()
    {
        foreach(Card card in deck)
        {
            GameObject cardObject = Instantiate(cardPrefab, layoutParent);
            cardObject.GetComponent<PlayingCardScript>().info = card;
            cardsVisualised.Add(cardObject);
        }
    }
    public void UpdateDeck()
    {
        foreach(GameObject cardObject in cardsVisualised)
        {
            Destroy(cardObject);
        }
        cardsVisualised = new List<GameObject>();
    }

    public void RemoveCard(Card cardToRemove)
    {
        if (!deck.Contains(cardToRemove))
        {
            Debug.LogError("Trying to remove a card that does not exist in deck");
            return;
        }

        //Remove it from the deck and update the current deck
        deck.Remove(cardToRemove);
        UpdateUI();

    }

    void UpdateUI()
    {
        UpdateDeck();
        SpawnInDeck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateUI();
        }
    }

}
