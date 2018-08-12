using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

	public Foe foeType;
	public Deck deck;
	public int startHealth;

	private void Start()
	{
		startHealth = Mathf.Abs(startHealth);
	}
}
