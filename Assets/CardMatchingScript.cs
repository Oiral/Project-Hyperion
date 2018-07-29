using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatchingScript : MonoBehaviour {

    public Card[] playerCardMatch;
    public Card[] enemyCardMatch;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MatchCards(playerCardMatch, enemyCardMatch);
        }
    }

    public void MatchCards(Card[] playerCards, Card[] enemyCards)
    {
        matchRow(0);
        matchRow(1);
        matchRow(2);
    }

    void matchRow(int row)
    {
        Card playerCard = playerCardMatch[row];
        Card enemyCard = enemyCardMatch[row];

        if(playerCard.typeOfCard >= enemyCard.typeOfCard)
        {
            //Player goes first
            evaluateCard(playerCard);
        }
        else
        {
            //Enemy card goes first
        }
    }

    void evaluateCard(Card card)
    {

    }
}
