using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCardScript : MonoBehaviour {

    public Card info;

    public Text title;
    public Text desc;
    public Text number;

    private void Start()
    {
        info.SpawnInMatchCard(title, number, desc);
    }

    public void ClickedOn()
    {
        Debug.Log(info.nameOfCard);
        DeckScript deckMan = GameObject.FindGameObjectWithTag("DeckManager").GetComponent<DeckScript>();
        deckMan.RemoveCard(info);
    }

}
