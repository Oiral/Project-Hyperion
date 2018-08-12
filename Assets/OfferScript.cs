using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferScript : MonoBehaviour {

    public List<GameObject> offeredCards;

    public int points;

    public ShopManagerScript shopMan;

    public void AddCard(GameObject cardToOffer)
    {
        offeredCards.Add(cardToOffer);

        UpdateMoney();
        UpdateCardPos();
    }

    public void RemoveCard(GameObject cardToRemove)
    {
        //if its the player card add back to the referenced deck
        offeredCards.Remove(cardToRemove);
        ShopCardScript shopCard = cardToRemove.GetComponent<ShopCardScript>();
        if (shopCard.isShopCard)
        {
            //add back into the shop
            shopMan.AddShopCard(shopCard);
            Destroy(cardToRemove);
        }
        else
        {
            //add back into the player inventory
            shopMan.AddPlayerCard(shopCard);
            Destroy(cardToRemove);
        }
        UpdateMoney();
        UpdateCardPos();
    }

    void UpdateMoney()
    {
        //Debug.Log("Updating Money");
        points = 0;
        
        for (int i = 0; i < offeredCards.Count; i++)
        {
            ShopCardScript shopCard = offeredCards[i].GetComponent<ShopCardScript>();
            points += shopCard.info.relatedCard.price;
        }
    }

    void UpdateCardPos()
    {
        for (int i = 0; i < offeredCards.Count; i++)
        {
            ShopCardScript shopCard = offeredCards[i].GetComponent<ShopCardScript>();
            shopCard.targetPos = new Vector3(i, i, i);
        }
    }
}
