using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "Cards/Create Deck", order = 1)]
public class Deck : ScriptableObject {
    [SerializeField]
    public List<GameCard> activeDeck;
    [SerializeField]
    public List<Card> unactiveDeck;
    [SerializeField]
    public List<Card> inventoryCards;

    public List<Card> rewardCards;

    public void GenerateActiveDeck()
    {
        if (unactiveDeck.Count == 0)
        {
            Debug.LogError("unactive deck is empty");
        }
        else
        {
            activeDeck = new List<GameCard>();
            foreach (Card card in unactiveDeck)
            {
                activeDeck.Add(new GameCard(card));
            }
            
            activeDeck.Sort(delegate(GameCard a, GameCard b)
            {
                return (a.typeOfCard.CompareTo(b.typeOfCard));
            });
        }
    }

    /// <summary>
    /// Draw a random game card form the active deck
    /// </summary>
    /// <returns>the game card that has been drawn</returns>
    public GameCard DrawRandom()
    {
        int randomNumber = Random.Range(0, activeDeck.Count);
        //Debug.Log(randomNumber.ToString());
        GameCard card = activeDeck[randomNumber];
        activeDeck.Remove(card);

        return card;
    }

    public List<GameCard> GetRewardCards()
    {
        List<GameCard> rewards = new List<GameCard>();
        //check if there are 3 or less reward cards
        if (rewardCards.Count <= 3)
        {
            foreach (Card reward in rewardCards)
            {
                rewards.Add(new GameCard(reward));
            }
        }
        else//there are more than 3
        {
            //pick a random list of 3 numbers
            for (int i = 0; i < 3; i++)
            {
                rewards.Add(new GameCard(rewardCards[Random.Range(0, rewardCards.Count)]));
            }
        }

        return rewards;

    } 
}
