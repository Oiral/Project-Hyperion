﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameCard{
    
    [Header("General Stats")]

    public CardType typeOfCard;
    public CardFamily extras;
    public Card relatedCard;
    public GameObject attachedObject;
    public Sprite cardBackground;

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
        cardBackground = cardToCopy.image;
    }

    public void UpdateMatchCardUI(Text title, Text damageNumbers)
    {
        if (extras != CardFamily.Effect)//if the card is not an effect
        {
            damageNumbers.text = (attackDamage * multiplyValue).ToString();
        }
        else
        {
            damageNumbers.text = "";
        }

        if (multiplyValue > 1)
        {
            damageNumbers.color = Color.yellow;
        }

        title.text = relatedCard.nameOfCard;
    }

    public void UpdateMatchCardUI(Text title, Text damageNumbers, Text battleDescriptionText)
    {
        if (extras != CardFamily.Effect)//if the card is not an effect
        {
            damageNumbers.text = (attackDamage * multiplyValue).ToString();
        }
        else
        {
            damageNumbers.text = "";
        }

        if (multiplyValue > 1)
        {
            damageNumbers.color = Color.yellow;
        }

        title.text = relatedCard.nameOfCard;

        string desc = relatedCard.battleDescription.Description;
        desc = desc.Replace("{atk}", attackDamage.ToString());
        battleDescriptionText.text = desc;
    }

    public void UpdateMatchCardUI(Text title, Text damageNumbers, Text battleDescriptionText, Text cost)
    {
        if (extras != CardFamily.Effect)//if the card is not an effect
        {
            damageNumbers.text = (attackDamage * multiplyValue).ToString();
        }
        else
        {
            damageNumbers.text = "";
        }

        if (multiplyValue > 1)
        {
            damageNumbers.color = Color.yellow;
        }

        title.text = relatedCard.nameOfCard;

        string desc = relatedCard.battleDescription.Description;
        desc = desc.Replace("{atk}", attackDamage.ToString());
        battleDescriptionText.text = desc;

        cost.text = relatedCard.price.ToString();
    }

    public void UpdateMatchCardUI(Text title, Text damageNumbers, Text battleDescriptionText, Image background)
    {
        if (extras != CardFamily.Effect)//if the card is not an effect
        {
            damageNumbers.text = (attackDamage * multiplyValue).ToString();
        }
        else
        {
            damageNumbers.text = "";
        }

        if (multiplyValue > 1)
        {
            damageNumbers.color = Color.yellow;
        }

        title.text = relatedCard.nameOfCard;

        string desc = relatedCard.battleDescription.Description;
        desc = desc.Replace("{atk}", attackDamage.ToString());
        battleDescriptionText.text = desc;


        background.sprite = relatedCard.image;
        //cost.text = relatedCard.price.ToString();
    }
}
