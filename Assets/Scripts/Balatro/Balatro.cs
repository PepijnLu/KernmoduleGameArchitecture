using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<PokerCard> playedHand;
    Scoring scoring = new Scoring();
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
            playedHand = new List<PokerCard>();
            //TESTING TESTING TESTING
            for(int i = 0; i < values.Count; i++)
            {
                InitializePlayedHand(values[i], suits[i]);
            }

            //5 for testing
            totalScore += scoring.Score(playedHand);

            playedHand = null;

            //Debug.Log(totalScore);
        }
    }

    void InitializePlayedHand(int rank, string suit)
    {
        PokerCard newCard = new PokerCard();
        newCard.rank = rank;
        newCard.suit = suit;
        
        if(rank > 10) {newCard.chips = 10;}
        else {newCard.chips = rank;}

        playedHand.Add(newCard);
    }


}
