using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagerScript : MonoBehaviour {

    public List<Card> possibleCardsInShop = new List<Card>();

    public Deck playerDeck;
    public GameObject gameCamera;

    public GameObject shopPrefab;

    public GameObject shopCenter;
    public Transform camFocusOnShopPos;

    public Transform inventoryCenter;
    public Transform camFocusOnInventoryPos;

    public OfferScript playerOffering;
    public OfferScript shopOffering;

    private bool focusOnShop = true;

    List<GameObject> inventoryCards = new List<GameObject>();
    Card[] inventory;

    int startingCollumn = 0;
    public int inventoryMove = 1;

    void Start()
    {
        inventory = new Card[playerDeck.unactiveDeck.Count];

        for (int i = 0; i < playerDeck.unactiveDeck.Count; i++)
        {
            inventory[i] = playerDeck.unactiveDeck[i];
        }

        StartCoroutine(GenerateShop());
        StartCoroutine(UpdateInventory());
    }

    IEnumerator GenerateShop()
    {
        int i = 0;
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                
                GameObject CardObject = Instantiate(shopPrefab, Vector3.zero, Quaternion.identity,shopCenter.transform);

                Vector3 pos = new Vector3((r - 1) * 10, 0, (c - 1) * 12);

                ShopCardScript cardScript = CardObject.GetComponent<ShopCardScript>();

                cardScript.targetPos = pos;

                cardScript.info = DrawRandomShopCard();

                cardScript.shopScript = this;

                cardScript.isShopCard = true;

                cardScript.inventoryNum = i;

                i++;

                yield return new WaitForSeconds(0.1f);
            }
            
        }
    }

    IEnumerator UpdateShop(ShopCardScript card)
    {
        int i = 0;
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (card.inventoryNum == i)
                {
                    GameObject CardObject = Instantiate(shopPrefab, Vector3.zero, Quaternion.identity, shopCenter.transform);

                    Vector3 pos = new Vector3((r - 1) * 10, 0, (c - 1) * 12);

                    ShopCardScript cardScript = CardObject.GetComponent<ShopCardScript>();

                    cardScript.targetPos = pos;

                    cardScript.info = card.info;

                    cardScript.shopScript = this;

                    cardScript.isShopCard = true;

                    cardScript.inventoryNum = i;

                }

                i++;
            }
        }
        yield return new WaitForSeconds(0.1f);
    }

    void RegenInventory()
    {
        inventory = new Card[playerDeck.unactiveDeck.Count];

        for (int i = 0; i < playerDeck.unactiveDeck.Count; i++)
        {
            inventory[i] = playerDeck.unactiveDeck[i];
        }


        StopAllCoroutines();
        StartCoroutine(UpdateInventory());
    }

    IEnumerator UpdateInventory()
    {
        foreach (GameObject objectToDestroy in inventoryCards)
        {
            Destroy(objectToDestroy);
        }
        inventoryCards.Clear();

        int c = 0;
        int r = startingCollumn;
        for (int i = 0; i < playerDeck.unactiveDeck.Count; i++)
        {
            //Looping through getting the rows a collums set up
            if (c >= 3)
            {
                c = 0;
                r++;
            }
            //Debug.Log(c + " - " + r);

            if (r == 0 || r == 1 || r == 2)
            {
                //Check if there is a card in that slot
                if (inventory[i] != null)
                {

                    GameObject CardObject = Instantiate(shopPrefab, inventoryCenter.transform.position, Quaternion.identity, inventoryCenter.transform);

                    Vector3 pos = new Vector3((c - 1) * 10, 0, (r - 1) * 12);

                    ShopCardScript cardScript = CardObject.GetComponent<ShopCardScript>();

                    cardScript.targetPos = pos;

                    cardScript.info = DrawPlayerCard(i);

                    cardScript.shopScript = this;

                    cardScript.isShopCard = false;

                    cardScript.inventoryNum = i;

                    inventoryCards.Add(CardObject);
                    yield return new WaitForSeconds(0.1f);
                }
            }

            c++;
            
        }

        yield return new WaitForSeconds(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleShop();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NextInventory();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PrevInventory();
        }
    }

    void ToggleShop()
    {
        focusOnShop = !focusOnShop;

        if (focusOnShop)
        {
            gameCamera.GetComponent<CamShopScript>().targetPos = camFocusOnShopPos.position;
        }
        else
        {
            gameCamera.GetComponent<CamShopScript>().targetPos = camFocusOnInventoryPos.position;
            StartCoroutine(UpdateInventory());
        }

    }
    
    GameCard DrawRandomShopCard()
    {
        Card drawnCard = possibleCardsInShop[Random.Range(0, (int)possibleCardsInShop.Count)];

        return new GameCard(drawnCard);
    }

    GameCard DrawPlayerCard(int cardNum)
    {
        Card drawnCard = inventory[cardNum];

        return new GameCard(drawnCard);
    }

    public void NextInventory()
    {
        startingCollumn += inventoryMove;
        StopAllCoroutines();
        StartCoroutine(UpdateInventory());
    }
    public void PrevInventory()
    {
        startingCollumn -= inventoryMove;
        StopAllCoroutines();
        StartCoroutine(UpdateInventory());
    }


    public void GivePlayerCard(GameCard cardToGive)
    {
        playerDeck.unactiveDeck.Add(cardToGive.relatedCard);
    }

    public void OfferCard(GameObject cardOffering, bool isPlayer)
    {
        OfferScript offeringSide;
        if (isPlayer)
        {
            offeringSide = playerOffering;
            inventory[cardOffering.GetComponent<ShopCardScript>().inventoryNum] = null;
            inventoryCards.Remove(cardOffering);
        }
        else
        {
            offeringSide = shopOffering;
        }
        cardOffering.transform.parent = offeringSide.gameObject.transform;
        offeringSide.AddCard(cardOffering);
        cardOffering.GetComponent<ShopCardScript>().offerScript = offeringSide;
    }

    public void AddPlayerCard(ShopCardScript cardToAdd)
    {
        StopAllCoroutines();
        inventory[cardToAdd.inventoryNum] = cardToAdd.info.relatedCard;
        StartCoroutine(UpdateInventory());
    }

    public void AddShopCard(ShopCardScript cardToAdd)
    {
        StopAllCoroutines();
        StartCoroutine(UpdateShop(cardToAdd));
    }

    public void ExitShop()
    {
        //exit the shop maybe
    }

    public void ConfirmSelection()
    {
        //Set up some info to be used later
        List<ShopCardScript> playerOffers = new List<ShopCardScript>();
        List<ShopCardScript> shopOffers = new List<ShopCardScript>();

        int playerPoints = playerOffering.points;
        int shopPoints = shopOffering.points;

        foreach (GameObject item in playerOffering.offeredCards)
        {
            playerOffers.Add(item.GetComponent<ShopCardScript>());
        }
        foreach (GameObject item in shopOffering.offeredCards)
        {
            shopOffers.Add(item.GetComponent<ShopCardScript>());
        }


        //check if either is empty
        if (playerOffers.Count == 0 || shopOffers.Count == 0)
        {
            Debug.Log("Both parties need to offer something");
            return;
        }
        //check if the prices match
        //if they are good to go
        //make the trade
        if (playerPoints == shopPoints)//if the player has more or the same amount of points let the transfer go through
        {
            Debug.Log("Trade Confirmed");
            //remove the player cards from the player unactive deck
            for (int i = 0; i < playerOffers.Count; i++)
            {
                playerDeck.unactiveDeck.Remove(playerOffers[i].info.relatedCard);
                //remove the cards that are up for offer
                playerOffers[i].offerScript.RemoveCard(playerOffers[i].gameObject);
            }

            //add the shop cards to the players unactive deck
            for (int i = 0; i < shopOffers.Count; i++)
            {
                playerDeck.unactiveDeck.Add(shopOffers[i].info.relatedCard);
                //remove the cards that are up for offer
                shopOffers[i].offerScript.RemoveCard(shopOffers[i].gameObject);
            }
            
            RegenInventory();
        }
        else
        {
            Debug.Log("Not enough points");
            //Maybe player a error thingy?
        }

    }

}
