using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardFamily { Effect, Defend, Attack };
public enum CardType { Mirror, MassFreeze, Freeze, Thief, Multiply, AttackFreeze, Heal, Block,/*ATTACK DEFEND,*/ Hit, SuperChain, Chain }

[CreateAssetMenu(fileName = "Card Data", menuName = "Cards/Create Card", order = 1)]
public class Card : ScriptableObject {

	[Header("UniqueCardTemplateID")]
	public string uniqueCardTemplateID = "";

    [Header("General Stats")]

    public CardType typeOfCard;
    public CardFamily extras;

    [Header("Battle Stats")]
    [SerializeField]
    public int attackDamage = 0;
    [SerializeField]
    public int multiplyValue = 1;

    [Header("Shop Info")]
    public int price = 1;

    [Header("General Info")]
    public string nameOfCard;
    public Sprite image;
    [Multiline]
    public string description;
    public CardBattleDescriptions battleDescription;

	public void OnValidate()
	{
		uniqueCardTemplateID = "";
		Debug.Log(uniqueCardTemplateID);
	}

	public string GetStringID()
	{
		return uniqueCardTemplateID;
	}
}
