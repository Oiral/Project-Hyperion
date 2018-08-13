using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardScript : MonoBehaviour {

    public GameCard info;

    public Text title;
    public Text desc;
    public Text number;
    public Text cost;

    public Vector3 targetPos;
    public float moveSpeed = 10;

    public ShopManagerScript shopScript;
    public int inventoryNum;

    public bool isShopCard;

    public OfferScript offerScript;

    private void Start()
    {
        info.UpdateMatchCardUI(title, number, desc, cost);
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * moveSpeed);
        
    }

    private void OnMouseOver()
    {
        if (offerScript != null)//if the card has been offered up to exchange
        {
            if (Input.GetMouseButtonDown(0) && isShopCard)
            {
                Debug.Log("Remove Shop Card");
                offerScript.RemoveCard(gameObject);
            }
            if (Input.GetMouseButtonDown(0) && !isShopCard)
            {
                Debug.Log("Remove Player Card");
                offerScript.RemoveCard(gameObject);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && isShopCard)
            {
                //Debug.Log(title.text);
                shopScript.OfferCard(gameObject, false);
            }
            if (Input.GetMouseButtonDown(0) && !isShopCard)
            {
                shopScript.OfferCard(gameObject,true);
            }
        }

        
    }
}
