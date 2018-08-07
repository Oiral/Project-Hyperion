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

                yield return new WaitForSeconds(0.1f);
            }
            
        }
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

    public void OfferCard(GameObject cardOffering)
    {
        inventory[cardOffering.GetComponent<ShopCardScript>().inventoryNum] = null;
        cardOffering.transform.parent = null;
        cardOffering.GetComponent<ShopCardScript>().targetPos = new Vector3(0, 0, 0);
    }

}
