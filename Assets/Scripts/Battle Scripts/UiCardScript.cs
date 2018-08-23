using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCardScript : MonoBehaviour {

    public GameCard info;

    public Text title;
    public Text number;
    public Text desc;
    public Image background;

    public bool hover = false;

    private void Update()
    {
        info.UpdateMatchCardUI(title, number, desc, background);
    }

    public void PlayCard()
    {
        GameObject.FindGameObjectWithTag("Battle Manager").GetComponent<BattleManagerScript>().PlayerPlayCard(info);
            
    }

    public void RewardCard()
    {
        Debug.Log("Adding");
        //GetComponentInParent<RewardsScript>().AddCard(info);
    }

    public void OnHover()
    {
        hover = true;
        GetComponent<Button>().Select();
    }
    public void LeaveHover()
    {
        hover = false;
    }
    public void OnSelect()
    {
        //Debug.Log("Test");
        //set up something to do with the movement of selection thingy
        GameObject.FindGameObjectWithTag("SelectionBox").GetComponent<MoveScript>().targetPos = GetComponent<RectTransform>().transform.position;
    }
}
