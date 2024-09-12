using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardsHandout : MonoBehaviour
{
    private readonly float amountOfCards = 52;
    private readonly int maxHandSize = 8; 
    [SerializeField] private int currentHandSize;
    private readonly int maxAmountOfSelectedCards = 5;

    [SerializeField] private PokerCard cardPrefab;
    [SerializeField] private List<PokerCard> deck = new(); 
    [SerializeField] private List<PokerCard> playerHand = new(); // Player's hand
    private List<PokerCard> selectedCards = new();
    public Transform deckLocation;
    [SerializeField] private List<List<Sprite>> spriteListList;

    [SerializeField] private List<Sprite> clubsList, spadesList, heartsList, diamondsList;
    public List<Sprite> usedSpriteList;
    void Start()
    {
        GenerateStandardDeck();
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.S)) { ShuffleDeck(); }

        if(Input.GetKeyDown(KeyCode.H)) { HandoutCard(); }
    }

    void GenerateStandardDeck()
    {
        spriteListList = new List<List<Sprite>>() {clubsList, spadesList, heartsList, diamondsList};
        foreach (List<Sprite> list in spriteListList)
        {
            Debug.Log(list.Count);
        }
        string suit = "";

        for (int i = 0; i < 4; i++)
        {
            switch(i)
            {
                case 0:
                    suit = "Clubs";
                    break;
                case 1:
                    suit = "Spades";
                    break;
                case 2:
                    suit = "Hearts";
                    break;
                case 3:
                    suit = "Diamonds";
                    break;
            }
            for(int index = 0; index < 13; index++)
            {

                PokerCard newCard = Instantiate(cardPrefab, deckLocation);
                newCard.SetValues(index + 2, suit, spriteListList[i][index]);
                deck.Add(newCard);
            }
        }
    }
   
    private void ShuffleDeck() 
    {
        System.Random random = new();
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = random.Next(i + 1);

            // (deck[randomIndex], deck[i]) = (deck[i], deck[randomIndex]);
            
            PokerCard temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        Debug.Log("Cards are shuffled....");

        //deck = deck.OrderBy(x => Random.value).ToList();
    }

    private void HandoutCard() 
    {
        // Check how many cards are in the deck
        // If there are enough cards in the deck, then give the player the cards
        if(deck.Count > 0 && deck.Count <= amountOfCards) 
        {
            currentHandSize = ReturnHandSize();
            
            // Check how many cards the player has  
            if(playerHand.Count < maxHandSize) 
            {
                //Fill the remaining spots in the playableCards list from the cards of the deck
                for(int i = 0; i < maxHandSize - currentHandSize; i++)
                {
                    playerHand.Add(deck[0]);
                    Debug.Log($"Added a card to the player's hand: {deck[0]} : {currentHandSize}");
                    
                    deck.RemoveAt(0);
                    Debug.Log($"Removed the card from the deck: {deck[0]}");
                }
            }
        }     
    }

    private int ReturnHandSize() 
    {
        return playerHand.Count;
    }

    private void PlaySelectedCards()
    {
        // Behavior to play the selected cards
        if(Input.GetKeyDown(KeyCode.Alpha0)) 
        {
            // Get all the selected cards
            PokerCard[] cardArr = selectedCards.ToArray(); 

            // And lay them down on the 'table'
            Debug.Log("Cards have been played...");   
        }

        currentHandSize -= selectedCards.Count;

        selectedCards.Clear();
    }

    public void SelectCard(PokerCard card)
    {
        if(selectedCards.Count <= 5)
        {
            if(selectedCards.Contains(card))
            {   
                selectedCards.Remove(card);
                card.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                selectedCards.Add(card);
                card.gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
        }

        foreach(PokerCard selected in selectedCards)
        {
            Debug.Log($"Selected card: {selected.rank} of {selected.suit}");
        }
    }

    // private void SelectCard() 
    // {
    //     // Iterate through the cards in the player's hand
    //     for (int i = 0; i < playerHand.Count; i++)
    //     {
    //         var card = playerHand[i].GetComponent<PokerCard>();

    //         if(card.selected) 
    //         {
    //             if(selectedCards.Contains(card)) 
    //             {
    //                 selectedCards.Remove(card);
    //                 card.selected = false;
    //                 card.canSelect = true;
    //                 continue;
    //             }
    //         }
    //         else 
    //         {
    //             card.canSelect = true;

    //             if(card.canSelect && selectedCards.Count < maxAmountOfSelectedCards) 
    //             {
    //                 selectedCards.Add(card);
    //                 card.selected = true;
    //                 card.canSelect = false;
    //             }
    //         }
    //     }
    // }

    // On mouse down
    // Collide with the card (raycast)
}