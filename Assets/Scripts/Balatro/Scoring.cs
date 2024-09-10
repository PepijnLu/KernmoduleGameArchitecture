using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring
{

    MathUtil mathUtil = new MathUtil();
    List<PlayingCard> scoredCards = new List<PlayingCard>();

    Dictionary<string, int> pokerHandValues = new Dictionary<string, int>();

    public Scoring()
    {
        pokerHandValues.Add("High Card", 0);
        pokerHandValues.Add("One Pair", 1);
        pokerHandValues.Add("Two Pair", 2);
        pokerHandValues.Add("Three of a Kind", 3);
        pokerHandValues.Add("Straight", 4);
        pokerHandValues.Add("Flush", 5);
        pokerHandValues.Add("Full House", 6);
        pokerHandValues.Add("Four of a Kind", 7);
        pokerHandValues.Add("Straight Flush", 8);
        pokerHandValues.Add("Royal Flush", 9);
        pokerHandValues.Add("Five of a Kind", 10);
        pokerHandValues.Add("Flush House", 11);
        pokerHandValues.Add("Flush Five", 12);

    }
    public string GetPokerHand(int cardsScored)
    {
        int[] values = new int[cardsScored];
        string[] suits = new string[cardsScored];
        string pokerHand = "";
        bool isFlush = false;
        Dictionary<int, int> valuesTimesInList = new Dictionary<int, int>();

        for(int i = 0; i < scoredCards.Count; i++)
        {
            values[i] = scoredCards[i].value;
            suits[i] = scoredCards[i].suit;
        }

        isFlush = IsFlush(suits);
        valuesTimesInList = CheckForSameNumbers(values);

        //#1: Flush Five
        if(isFlush && valuesTimesInList.Count == 1 && valuesTimesInList[0] == 5) {pokerHand = "Flush Five";}

        return pokerHand;
    }

    bool IsFlush(string[] suits)
    {
        if(mathUtil.AreAllElementsSame(suits)) {return true;}
        else {return false;}
    }

    Dictionary<int, int> CheckForSameNumbers(int[] values)
    {
        Dictionary<int, int> valuesTimesInList = new Dictionary<int, int>();

        for(int i = 0; i < values.Length; i++)
        {
            int newCount = mathUtil.HowManyInList(values, i);
            if(newCount > 1)
            {
                valuesTimesInList.Add(values[i], newCount);
            }
        }

        return valuesTimesInList;
    }
}
