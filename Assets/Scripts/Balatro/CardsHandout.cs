using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardsHandout : MonoBehaviour
{
    public static CardsHandout instance;
    [SerializeField] private List<List<Sprite>> spriteListList;
    [SerializeField] private List<Sprite> clubsList, spadesList, heartsList, diamondsList;
    [SerializeField] private List<Transform> cardInHandTransforms;
    [SerializeField] private Transform deckLocation; // Deck loc
    [SerializeField] private TextMeshProUGUI scorePerRound_Text, overalScore_Text, handText; // Score and poker text
    [SerializeField] private PokerCard cardPrefab;

    [SerializeField] private List<PokerCard> deck = new(); 
    [SerializeField] private List<PokerCard> playerHand = new(); // Player's hand
    [SerializeField] private List<PokerCard> selectedCards = new();
    [SerializeField] private Transform playerHandTransform;

    private readonly Scoring scoring = new();
    private readonly int maxHandSize = 8; 
    private readonly float deckSize = 52;

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        GenerateStandardDeck();
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Tab)) {SubmitHand();}
        if(Input.GetKeyDown(KeyCode.D)) {DiscardHand();}
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

        ShuffleDeck();

    }
   
    private void ShuffleDeck() 
    {
        System.Random random = new();
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = random.Next(i + 1);
            (deck[randomIndex], deck[i]) = (deck[i], deck[randomIndex]);
        }

        HandoutCard();
    }

    private void HandoutCard() 
    {
        int handIndex = playerHand.Count;

        for (int i = 0; i < maxHandSize && deck.Count > 0 && deck.Count <= deckSize; i++)
        {
            if(playerHand.Count < maxHandSize) 
            {
                // Add card from deck to hand
                playerHand.Add(deck[0]);
                deck.RemoveAt(0);

                // Transform and sprite assigment for the newly added card
                playerHand[handIndex].transform.position = cardInHandTransforms[handIndex].position;
                playerHand[handIndex].image.sprite = playerHand[handIndex].sprite;

                // Parent assignment and status update
                playerHand[handIndex].transform.SetParent(playerHandTransform);
                playerHand[handIndex].isInHand = true;

                handIndex++;
            }
        }
    }

    public void SelectCard(PokerCard card)
    {
        if(!card.isInHand) {return;}
            
        if(selectedCards.Contains(card))
        {   
            selectedCards.Remove(card);
            card.gameObject.transform.localScale = new Vector3(0.57f, 0.84f, 0.57f);
        }

        else if(selectedCards.Count < 5)
        {
            if(!selectedCards.Contains(card))
            {
                selectedCards.Add(card);
                card.gameObject.transform.localScale = new Vector3(0.8f, 1.1f, 0.8f);
            }
        }
    }

    void SubmitHand()
    {
        if(selectedCards.Count <= 0) { return; }

        int newScore = scoring.Score(selectedCards);

        scorePerRound_Text.text = newScore.ToString();
        overalScore_Text.text = "Points: " + ScoreData.score.ToString();
        
        handText.text = ScoreData.handName;

        DestroyCards();
        HandoutCard();
    }

    void DiscardHand()
    {
        if(selectedCards.Count <= 0) { return; }

        DestroyCards();
        HandoutCard();
    }

    void DestroyCards() 
    {
        int currentListSize = selectedCards.Count;

        for(int i = 0; i < currentListSize; i++)
        {   
            PokerCard cardToRemove = selectedCards[currentListSize - 1 - i];
            selectedCards.Remove(cardToRemove);
            playerHand.Remove(cardToRemove);
            Destroy(cardToRemove.gameObject);
        }

        for(int i = 0; i < playerHand.Count; i++)
        {
            playerHand[i].gameObject.transform.position = cardInHandTransforms[i].position;
        }
    }
}