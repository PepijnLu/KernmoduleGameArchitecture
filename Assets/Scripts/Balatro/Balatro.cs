using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
BUGS:
-SCORE KEEPS ADDING TO ITSELF WHEN SAME HAND TWICE IN A ROW
-NO LOW STRAIGHTS WITH ACE
-0 IN TESTING DOESNT ALWAYS WORK PROPERLY (RANDOM STRAIGHTS/FLUSHES?)
-
*/
public struct PokerCard
{
    public int rank;
    public string suit;
    public int chips, mult;
}

public class Balatro : MonoBehaviour
{
    //TESTING PURPOSES
    public List<int> values;
    public List<string> suits;
    public List<int> rankIndex, suitIndex;

    public List<PokerCard> playedHand = new List<PokerCard>();
    public TextMeshProUGUI handText, scoreText;
    public Scoring scoring = new Scoring();
    int totalScore;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var card in playedHand)
            {
                Debug.Log($"Card value: {card.rank}, card suit: {card.suit}");
            }
        }
    }

    public void InitializePlayedHand(int rank, string suit)
    {
        PokerCard newCard = new PokerCard();
        newCard.rank = rank;
        newCard.suit = suit;
        
        if(rank > 10) {newCard.chips = 10;}
        else {newCard.chips = rank;}

        playedHand.Add(newCard);
    }


}
