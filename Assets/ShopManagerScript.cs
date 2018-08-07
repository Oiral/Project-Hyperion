using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagerScript : MonoBehaviour {

    public List<Card> possibleCardsInShop = new List<Card>();

    public Deck playerDeck;

    public GameObject shopPrefab;

    void Start()
    {
        StartCoroutine(GenerateShop());
    }

    IEnumerator GenerateShop()
    {
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                GameObject CardObject = Instantiate(shopPrefab, Vector3.zero, Quaternion.identity);

                Vector3 pos = new Vector3(r * 10, 0, c * 12);

                ShopCardScript cardScript = CardObject.GetComponent<ShopCardScript>();

                cardScript.targetPos = pos;

                cardScript.info = DrawRandomShopCard();

                cardScript.shopScript = this;

                yield return new WaitForSeconds(0.1f);
            }
            
        }
    }

    GameCard DrawRandomShopCard()
    {
        Card drawnCard = possibleCardsInShop[Random.Range(0, (int)possibleCardsInShop.Count)];

        return new GameCard(drawnCard);

    }

    public void GivePlayerCard(GameCard cardToGive)
    {
        playerDeck.unactiveDeck.Add(cardToGive.relatedCard);
    }

}
