using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardFamily { Effect, Defend,Attack};
public enum CardType { Mirror, Freeze, Thief, Multiply, Heal, Shield, Hit, Chain }

[CreateAssetMenu(fileName = "Card Data", menuName = "Cards/Create Card", order = 1)]
public class Card : ScriptableObject {
    
    [Header("General Stats")]
    public CardType typeOfCard;
    public CardFamily extras;

    [Header("Battle Stats")]
    public float attackDamage = 0;
    public bool enabled = true;

    [Header("Shop Info")]
    public int price = 1;

    [Header("General Info")]
    public string nameOfCard;
    public Sprite image;
    [Multiline]
    public string description;
    public CardBattleDescriptions battleDescription;

    public void SpawnInMatchCard(Text title, Text damageNumbers, Text battleDescriptionText)
    {
        damageNumbers.text = attackDamage.ToString();
        title.text = nameOfCard;

        string desc = battleDescription.Description;
        desc = desc.Replace("{atk}", attackDamage.ToString());
        battleDescriptionText.text = desc;
    }
}
