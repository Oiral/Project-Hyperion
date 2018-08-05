using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCardScript : MonoBehaviour {

    public GameCard info;

    public Text title;
    public Text number;
    public Text desc;

    public bool hover = false;

    private void Update()
    {
        info.UpdateMatchCardUI(title, number, desc);
    }

    public void PlayCard()
    {
        GameObject.FindGameObjectWithTag("Battle Manager").GetComponent<BattleManagerScript>().PlayerPlayCard(info);
            
    }

    public void OnHover()
    {
        hover = true;
    }
    public void LeaveHover()
    {
        hover = false;
    }
}
