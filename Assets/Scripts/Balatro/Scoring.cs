using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public struct PossiblePokerHand
{
    public string name;
    public int chips;
    public int mult;
    public int amountOfCardsItScores;
}
public class Scoring
{
    List<PossiblePokerHand> possiblePokerHands = new List<PossiblePokerHand>();

    public Scoring()
    {
        InitializePokerHand("High Card", 5, 1, 1);
        InitializePokerHand("One Pair", 10, 2, 2);
        InitializePokerHand("Two Pair", 20, 2, 4);
        InitializePokerHand("Three of a Kind", 30, 3, 3);
        InitializePokerHand("Straight", 30, 4, 5);
        InitializePokerHand("Flush", 5, 1, 1);
        InitializePokerHand("Full House", 5, 1, 1);
        InitializePokerHand("Four of a Kind", 60, 7, 4);
        InitializePokerHand("Straight Flush", 100, 8, 5);
        InitializePokerHand("Royal Flush", 100, 8, 5);
        InitializePokerHand("Five of a Kind", 120, 12, 5);
        InitializePokerHand("Flush House", 140, 14, 5);
        InitializePokerHand("Flush Five", 160, 16, 5);
    }

    public int Score(List<PokerCard> playedHand)
    {
        string pokerHand = GetPokerHand(playedHand);
        PossiblePokerHand scoredHand = PlayedPokerHand(pokerHand);
        List<PokerCard> scoredCards = GetScoredCards(scoredHand, playedHand);
        
        int score = CalculateBaseScore(scoredCards, scoredHand);
        return score;
    }

    void InitializePokerHand(string name, int chips, int mult, int amountOfCardsItScores)
    {
        PossiblePokerHand newHand = new PossiblePokerHand();
        newHand.name = name;
        newHand.chips = chips;
        newHand.mult = mult;
        newHand.amountOfCardsItScores = amountOfCardsItScores;
        possiblePokerHands.Add(newHand);
    }
    public string GetPokerHand(List<PokerCard> playedHand)
    {       
        int[] values = new int[playedHand.Count];
        string[] suits = new string[playedHand.Count]; 
        
        for(int index = 0; index < playedHand.Count; index++)
        {
            values[index] = playedHand[index].rank;
            suits[index] = playedHand[index].suit;
        }
 

        var counts = values.GroupBy(n => n)
                           .Select(g => new { Number = g.Key, Count = g.Count() })
                           .OrderByDescending(x => x.Count) // Order by the Count property
                           .ToList();

        int firstElementCount = 0, secondElementCount = 0, i = 0;
        foreach (var entry in counts)
        {
            if(i == 0) {firstElementCount = entry.Count;}
            if(i == 1) {secondElementCount = entry.Count;}
            if ((i == 2) || firstElementCount >= 4) {break;}
            i++;
        }

        //#1; Flush Five
        bool isFlush = IsFlush(playedHand);
        if(isFlush && firstElementCount == 5) {return "Flush Five";}

        //#2: Flush House
        bool isFullhouse = firstElementCount == 3 && secondElementCount == 2;
        if(isFlush && isFullhouse) {return "Flush House";}

        //#3: Five of a Kind
        if(firstElementCount == 5) {return "Five of a Kind";}

        //#4: Royal Flush
        bool isStraight = IsStraight(values);
        bool isRoyalStraight = values.SequenceEqual(new List<int>{10, 11, 12, 13, 14});
        if(isStraight && isFlush && isRoyalStraight) {return "Royal Flush";}

        //#5: Straight Flush
        if(isStraight && isFlush) {return "Straight Flush";}

        //#6: Four of a Kind
        if(firstElementCount == 4) {return "Four of a Kind";}

        //#7: Full house
        if(isFullhouse) {return "Full House";}

        //#8: Flush
        if(isFlush) {return "Flush";}

        //#9: Straight
        if(isStraight) {return "Straight";}

        //10: Three of a Kind
        if(firstElementCount == 3) {return "Three of a Kind";}

        //#11: Two Pair
        if(firstElementCount == 2 && secondElementCount == 2) {return "Two Pair";}

        //#12: One Pair
        if(firstElementCount == 2) {return "One Pair";}

        //#13: High Card
        return "High Card";
    }

    public PossiblePokerHand PlayedPokerHand(string pokerHand)
    {
        foreach(PossiblePokerHand hand in possiblePokerHands)
        {
            if(hand.name == pokerHand) 
            {
                Debug.Log(hand.name);
                return hand;
            }
        }

        throw new Exception("POSSIBLE HAND NOT FOUND");
    }

    public List<PokerCard> GetScoredCards(PossiblePokerHand playedPokerHand, List<PokerCard> playedHand)
    {
        List<PokerCard> scoredCards = new List<PokerCard>();
        
        int amountOfScoredCards = playedPokerHand.amountOfCardsItScores;
        // Debug.Log("amount of scored cards " + amountOfScoredCards);

        switch(amountOfScoredCards)
        {
            case 5:
                scoredCards = playedHand;
                break;
        }

        // foreach(PokerCard card in scoredCards)
        // {
        //     Debug.Log($"Value: {card.rank} Suit: {card.suit}" );
        // }

        return scoredCards;
    }

    public int CalculateBaseScore(List<PokerCard> scoredCards, PossiblePokerHand pokerHand)
    {
        int baseChips = 0;
        int baseMult = 0;

        foreach(PokerCard card in scoredCards)
        {
            baseChips += card.chips; 
            baseMult += card.mult;
        }

        baseChips += pokerHand.chips;
        baseMult += pokerHand.mult;

        int score = baseChips * baseMult;
        Debug.Log("Hand Score: " + score);
        return score;
    }
    private bool IsStraight(int[] values)
    {
        Array.Sort(values);
        
        for (int i = 0; i < values.Length - 1; i++)
        {
            if (values[i] + 1 != values[i + 1]) {return false;}
        }

        // Special case for Ace-low straights (Ace, 2, 3, 4, 5)
        if (values.SequenceEqual(new List<int> { 2, 3, 4, 5, 14 })) {return true;}

        return true;
    }

    private bool IsFlush(List<PokerCard> playedHand)
    {
        string prevSuit = playedHand[0].suit;
        foreach(PokerCard card in playedHand.Skip(1))
        {
            if(card.suit == prevSuit) {prevSuit = card.suit;}
            else{return false;}
        }
        
        return true;
    }
}
