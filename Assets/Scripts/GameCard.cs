using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameCard{
    
    [Header("General Stats")]

    public CardType typeOfCard;
    public CardFamily extras;
    public Card relatedCard;
    public GameObject attachedObject;

    [Header("Battle Stats")]
    [SerializeField]
    public int attackDamage = 0;
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

    public void UpdateMatchCardUI(Text title, Text damageNumbers, Text battleDescriptionText)
    {
        damageNumbers.text = (attackDamage * multiplyValue).ToString();
        if (multiplyValue > 1)
        {
            damageNumbers.color = Color.yellow;
        }

        title.text = relatedCard.nameOfCard;

        string desc = relatedCard.battleDescription.Description;
        desc = desc.Replace("{atk}", attackDamage.ToString());
        battleDescriptionText.text = desc;
    }
}
