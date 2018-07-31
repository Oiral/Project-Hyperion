using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "Cards/Create Deck", order = 1)]
public class Deck : ScriptableObject {
    public List<Card> activeDeck;
    public List<Card> unactiveDeck;
    public List<Card> inventoryCards;



    public void GenerateActiveDeck()
    {
        if (unactiveDeck.Count == 0)
        {
            Debug.LogError("unactive deck is empty");
        }
        else
        {
            activeDeck = new List<Card>();
            foreach (Card card in unactiveDeck)
            {
                activeDeck.Add(card);
            }
            
            activeDeck.Sort(delegate(Card a, Card b)
            {
                return (a.typeOfCard.CompareTo(b.typeOfCard));
            });
        }
    }

    /// <summary>
    /// Draw a random card form the active deck
    /// </summary>
    /// <returns>the card that has been drawn</returns>
    public Card DrawRandom()
    {
        int randomNumber = UnityEngine.Random.Range(0, activeDeck.Count);
        Debug.Log(randomNumber.ToString());
        Card card = activeDeck[randomNumber];
        activeDeck.Remove(card);

        return card;
    }
}
