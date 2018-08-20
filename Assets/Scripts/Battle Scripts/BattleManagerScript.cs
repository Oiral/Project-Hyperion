using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManagerScript : MonoBehaviour {
    
    public PlayerScript enemy;
    public PlayerScript player;
    public GameObject cardPrefab;

    //UI Stuff
    public GameObject UiCardPrefab;
    public GameObject UIHandParent;
    public List<GameCard> playerHand = new List<GameCard>();

    public GameCard[] enemyCardsInPlay = new GameCard[3];
    public GameCard[] playerCardsInPlay = new GameCard[3];

    int AiXValue = 0;
    int PlayerXValue = -20;

    GameManager GM;

    public Button matchButton;

    #region testing
    //Testing Items
    public Button setupUpTurnButton;
    public Button playerAutoPlayButton;
    

    public GameObject hideCards;

    public void StartTurn()
    {
        //setupUpTurnButton.interactable = false;
        StartCoroutine(SetUpTurn());
        playerAutoPlayButton.interactable = true;
    }
    public void PlayerAutoPlay()
    {
        //playerAutoPlayButton.interactable = false;
        //StartCoroutine(PlayerSetUpTurnAuto());
        matchButton.interactable = true;
    }
    public void MatchCards()
    {
        //matchButton.interactable = false;
        if (playerCardsInPlay[2] != null)
        {
            CheckChainCards(playerCardsInPlay);
            StartCoroutine(CheckCards());
        }
    }


    private void OnDisable()
    {
        enemy.currentDeck.activeDeck.Clear();
        player.currentDeck.activeDeck.Clear();
    }

    
    IEnumerator PlayerSetUpTurnAuto()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerDraw();
    }

    #endregion

    private void Start()
    {
        //Check if there is a game manager with a deck to be loaded
        GameObject gmObject = GameObject.FindGameObjectWithTag("GameController");
        if (gmObject != null)
        {
            GM = gmObject.GetComponent<GameManager>();

            enemy.currentDeck = GM.GetDeck();
            enemy.health = GM.GetEnemyHP();

            player.health = GM.playerHealth;
        }
        

        StartCoroutine(SetUpTurn());
    }

    IEnumerator SetUpTurn()
    {
        yield return new WaitForSeconds(1f);
        //hideCards.SetActive(true);
        AiDraw();
        CheckChainCards(enemyCardsInPlay);
        PlayerDraw();
    }

    public void AiDraw()
    {
        for (int i = 0; i < 3; i++)
        {
            //Check if there are no more cards to be drawn
            if (enemy.currentDeck.activeDeck.Count == 0)
            {
                ReShuffle(enemy);
            }

            GameObject SpawnedCardObject = Instantiate(cardPrefab);
            GameCard spawnedCard = SpawnedCardObject.GetComponent<PlayingCardScript>().info = enemy.currentDeck.DrawRandom();
            spawnedCard.attachedObject = SpawnedCardObject;
            Vector3 pos = new Vector3(AiXValue, 0, i * 12);
            VisualSet(spawnedCard, pos);
            enemyCardsInPlay[i] = spawnedCard;
            //Debug.Log("spawned card", spawnedCard.attachedObject);
        }
    }

    public void PlayerDraw()
    {
        while (playerHand.Count < 5)
        {
            //Check if there is more card to draw
            if (player.currentDeck.activeDeck.Count > 0)
            {

                GameObject UISpawnedCardObject = Instantiate(UiCardPrefab, UIHandParent.transform);
                GameCard spawnedCard = UISpawnedCardObject.GetComponent<UiCardScript>().info = player.currentDeck.DrawRandom();
                spawnedCard.attachedObject = UISpawnedCardObject;
                playerHand.Add(spawnedCard);
            }
            else
            {
                if (player.discardedCards.Count == 0)
                {
                    break;
                }
                ReShuffle(player);

            }
        }
    }

    public void PlayerPlayCard(GameCard cardPlayed)
    {
        for (int i = 0; i < 3; i++)
        {
            if (playerCardsInPlay[i] == null)
            {
                //Destroy the card played
                Destroy(cardPlayed.attachedObject);
                playerHand.Remove(cardPlayed);

                GameObject SpawnedCardObject = Instantiate(cardPrefab);
                SpawnedCardObject.GetComponent<PlayingCardScript>().info = cardPlayed;
                cardPlayed.attachedObject = SpawnedCardObject;
                SpawnedCardObject.GetComponent<PlayingCardScript>().targetPos = new Vector3(PlayerXValue, 0, i * 12);
                playerCardsInPlay[i] = cardPlayed;
                break;
            }
        }
    }

    public void ReShuffle(PlayerScript person)
    {
        foreach (GameCard card in person.discardedCards)
        {
            person.currentDeck.activeDeck.Add(card);
        }
        person.discardedCards.Clear();
    }

    //Check how many chain cards there are then takes each of them and adds their value
    void CheckChainCards(GameCard[] playedCardsToCheck)
    {
        List<GameCard> chainCards = new List<GameCard>();
        //find all the chain cards in play
        foreach (GameCard card in playedCardsToCheck)
        {
            if (card.typeOfCard == CardType.Chain)
            {
                chainCards.Add(card);
            }
        }

        //set their attack values to how many chain cards are in play
        foreach (GameCard chainCard in chainCards)
        {
            chainCard.attackDamage = chainCards.Count;
        }

    }

    IEnumerator CheckCards()
    {
        matchButton.interactable = false;
        hideCards.SetActive(false);
        yield return StartCoroutine(EvaluateRow(0));
        CheckHealth();
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(EvaluateRow(1));
        CheckHealth();
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(EvaluateRow(2));
        CheckHealth();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 3; i++)
        {
            //re enable the card in case it was disabled
            playerCardsInPlay[i].enabled = true;
            enemyCardsInPlay[i].enabled = true;

            //reset the multiply value of the card
            playerCardsInPlay[i].multiplyValue = 1;
            enemyCardsInPlay[i].multiplyValue = 1;

            player.discardedCards.Add(playerCardsInPlay[i]);
            enemy.discardedCards.Add(enemyCardsInPlay[i]);
            playerCardsInPlay[i] = enemyCardsInPlay[i] = null;
        }
        StartCoroutine(EndTurn());
    }

    IEnumerator EvaluateRow(int row)
    {
        //Debug.Log(playerCardsInPlay[row].nameOfCard + " + " + enemyCardsInPlay[row].nameOfCard);
       

        //Check if the players card should go first depending on what type of card it is
        if (playerCardsInPlay[row].typeOfCard < enemyCardsInPlay[row].typeOfCard)
        {
            yield return StartCoroutine(EvaluateCard(playerCardsInPlay[row],row,true));
            Debug.Log("Running enemy match");
            yield return StartCoroutine(EvaluateCard(enemyCardsInPlay[row], row, false));
        }
        else
        {
            yield return StartCoroutine(EvaluateCard(enemyCardsInPlay[row], row, false));
            Debug.Log("Running player match");
            yield return StartCoroutine(EvaluateCard(playerCardsInPlay[row], row, true));
        }
        //playerCardsInPlay[row] = enemyCardsInPlay[row] = null;
    }

    IEnumerator EvaluateCard(GameCard cardToEval,int row,bool isPlayer)
    {
        //Check if the card is disabled
        if (cardToEval.enabled)
        {
            GameCard[] myCollumn;
            GameCard[] otherCollumn;

            PlayerScript user;
            PlayerScript opponent;

            int attackMove;
            int defendMove;

            if (isPlayer)
            {
                myCollumn = playerCardsInPlay;
                otherCollumn = enemyCardsInPlay;
                user = player;
                opponent = enemy;
                attackMove = 4;
                defendMove = -4;
            }
            else
            {
                myCollumn = enemyCardsInPlay;
                otherCollumn = playerCardsInPlay;
                user = enemy;
                opponent = player;
                attackMove = -4;
                defendMove = 4;
            }

            switch (cardToEval.typeOfCard)
            {
                case CardType.Mirror:
                    Debug.Log("Mirroring");
                    if (enemyCardsInPlay[row].relatedCard == playerCardsInPlay[row].relatedCard)//Checking for the edge case of both being a mirror
                    {
                        break;
                    }

                    //Visual stuff
                    Vector3 currpos = cardToEval.attachedObject.GetComponent<PlayingCardScript>().targetPos;

                    //spawn in the mirrored card
                    GameObject SpawnedCardObject = Instantiate(cardPrefab,otherCollumn[row].attachedObject.transform.position,Quaternion.identity);

                    //create a new game card and then copy the information over from the old one
                    GameCard spawnedCard = new GameCard(otherCollumn[row].relatedCard);
                    spawnedCard.multiplyValue = otherCollumn[row].multiplyValue;
                    spawnedCard.attackDamage = otherCollumn[row].attackDamage;


                    SpawnedCardObject.GetComponent<PlayingCardScript>().info = spawnedCard;
                    spawnedCard.attachedObject = SpawnedCardObject;
                    VisualSet(spawnedCard, currpos);
                    //myCollumn[row] = spawnedCard;

                    //move the mirror card down
                    currpos.y -= 1;
                    VisualSet(cardToEval, currpos);

                    yield return new WaitForSeconds(0.5f);
                    //Check the new spawned in card
                    yield return StartCoroutine( EvaluateCard(spawnedCard, row, isPlayer));
                    break;

                case CardType.Freeze:
                    Debug.Log("Freeze");
                    if (otherCollumn[row].extras != CardFamily.Effect)
                    {
                        otherCollumn[row].enabled = false;
                    }
                    break;

                case CardType.Thief:
                    Debug.Log("Thief");
                    if (otherCollumn[row].extras == CardFamily.Effect)//Check if opponents card is a effect
                    {
                        //If they are both thiefs disable both
                        //enemyCardsInPlay[row].enabled = playerCardsInPlay[row].enabled = false;
                        break;
                    }

                    //Swap the other cards around
                    //myCollumn[row] = otherCollumn[row];
                    //otherCollumn[row] = cardToEval;

                    //Disable this card
                    cardToEval.enabled = false;
                    VisualSet(otherCollumn[row], cardToEval.attachedObject.transform.localPosition);

                    VisualMove(cardToEval, new Vector3(0, -1, 0));


                    //visualization
                    //UpdateCardPos();


                    yield return new WaitForSeconds(0.5f);
                    yield return StartCoroutine(EvaluateCard(otherCollumn[row], row, isPlayer));
                    otherCollumn[row].enabled = false;
                    break;

                case CardType.Multiply:
                    Debug.Log("Mulitply");
                    if (row >= 2)//Catch if this multiply is the last one in the list
                    {
                        break;
                    }

                    myCollumn[row + 1].multiplyValue += 1;

                    //visualisation
                    VisualMove(cardToEval, new Vector3(0, -1, 9));
                    break;
                case CardType.Heal:
                    Debug.Log("Heal",cardToEval.attachedObject);
                    //heal the player
                    user.Heal(cardToEval.attackDamage * cardToEval.multiplyValue);

                    //Visual
                    VisualMove(cardToEval, new Vector3(defendMove, 0, 0));
                    break;

                case CardType.Block:
                    Debug.Log("Block", cardToEval.attachedObject);
                    //add block
                    user.Block(cardToEval.attackDamage * cardToEval.multiplyValue);

                    //Visual
                    VisualMove(cardToEval, new Vector3(defendMove, 0, 0));
                    break;

                case CardType.Hit:
                    Debug.Log("Hit", cardToEval.attachedObject);
                    //deal damage
                    opponent.Damage(cardToEval.attackDamage * cardToEval.multiplyValue);

                    //Visual
                    VisualMove(cardToEval, new Vector3(attackMove, 0, 0));
                    break;

                case CardType.Chain:
                    Debug.Log("Chain", cardToEval.attachedObject);
                    //deal damage
                    opponent.Damage(cardToEval.attackDamage * cardToEval.multiplyValue);

                    //Visual
                    VisualMove(cardToEval, new Vector3(attackMove, 0, 0));
                    break;
            }
        }
    }

    void VisualMove(GameCard cardToMove,Vector3 moveDir)
    {
        cardToMove.attachedObject.GetComponent<PlayingCardScript>().targetPos += moveDir;
    }

    void VisualSet(GameCard cardToMove,Vector3 targetPos)
    {
        cardToMove.attachedObject.GetComponent<PlayingCardScript>().targetPos = targetPos;
    }

    void UpdateCardPos()
    {
        foreach (GameCard curCard in playerCardsInPlay)
        {
            if (curCard != null)
            {
                Debug.Log("Updating card", curCard.attachedObject);
                Vector3 pos = curCard.attachedObject.transform.position;
                pos.x = PlayerXValue;
                curCard.attachedObject.GetComponent<PlayingCardScript>().targetPos = pos;
            }
        }
        foreach (GameCard curCard in enemyCardsInPlay)
        {
            if (curCard != null)
            {
                Vector3 pos = curCard.attachedObject.transform.position;
                pos.x = AiXValue;
                curCard.attachedObject.GetComponent<PlayingCardScript>().targetPos = pos;
            }
        }
    }

    void CheckHealth()
    {
        if (enemy.health <= 0 || player.health <= 0)
        {
            Debug.Log("End Game Early");
            GM.playerHealth = player.health;
			SceneFlow.RunScene(SceneList.MainScene);
        }
	}

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(1);
        //move cards that are in play
        foreach (GameObject cardInPlay in GameObject.FindGameObjectsWithTag("Played Card"))
        {
            cardInPlay.GetComponent<PlayingCardScript>().targetPos += new Vector3(0, -50, 0);
            Destroy(cardInPlay, 2);
        }
        player.shield = enemy.shield = 0;
        player.UpdateUI();
        enemy.UpdateUI();
        yield return new WaitForSeconds(1);

        matchButton.interactable = true;

        //ReShuffle Deck stuff
        StartCoroutine(SetUpTurn());

        /*
        //Check if there are no more cards left
        if (player.currentDeck.activeDeck.Count > 0)
        {
            
            StartCoroutine(SetUpTurn());
            //playerAutoPlayButton.interactable = true;
        }
        else
        {
            Debug.Log("End Game!");
            if (GM!= null)
            {
                GM.playerHealth = player.health;
            }
			
            SceneFlow.RunScene(SceneList.MainScene);
        }*/

        
    }

}
