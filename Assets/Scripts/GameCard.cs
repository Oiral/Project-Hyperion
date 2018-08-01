using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameCard{
    
    [Header("General Stats")]

    public CardType typeOfCard;
    public CardFamily extras;
    public Card relatedCard;


    [Header("Battle Stats")]
    [SerializeField]
    public float attackDamage = 0;
    public int multiplyValue = 1;
    public bool enabled = true;

    public GameCard(Card cardToCopy)
    {
        typeOfCard = cardToCopy.typeOfCard;

        extras = cardToCopy.extras;


        attackDamage = cardToCopy.attackDamage;
        multiplyValue = cardToCopy.multiplyValue;
        enabled = true;
        relatedCard = cardToCopy;
    }
}
