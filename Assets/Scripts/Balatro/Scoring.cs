using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class Scoring
{
    List<PossiblePokerHand> possiblePokerHands = new List<PossiblePokerHand>();
    public Scoring()
    {
        possiblePokerHands.Add(new FlushFive("Flush Five", 160, 16, 5));
        possiblePokerHands.Add(new FlushHouse("Flush House", 140, 14, 5));
        possiblePokerHands.Add(new FiveOfAKind("Five of a Kind", 120, 12, 5));
        possiblePokerHands.Add(new RoyalFlush("Royal Flush", 100, 8, 5));
        possiblePokerHands.Add(new StraightFlush("Straight Flush", 100, 8, 5));
        possiblePokerHands.Add(new FourOfAKind("Four of a Kind", 60, 7, 4));
        possiblePokerHands.Add(new FullHouse("Full House", 40, 5, 5));
        possiblePokerHands.Add(new Flush("Flush", 35, 4, 5));
        possiblePokerHands.Add(new Straight("Straight", 30, 4, 5));
        possiblePokerHands.Add(new ThreeOfAKind("Three of a Kind", 30, 3, 3));
        possiblePokerHands.Add(new TwoPair("Two Pair", 20, 2, 4));
        possiblePokerHands.Add(new OnePair("One Pair", 10, 2, 2));
        possiblePokerHands.Add(new HighCard("High Card", 5, 1, 1));
    }

    public int Score(List<PokerCard> playedHand)
    {
        PossiblePokerHand scoredHand = GetPokerHand(playedHand);
        Debug.Log(scoredHand.name);
        List<PokerCard> scoredCards = GetScoredCards(scoredHand, playedHand);
        int score = CalculateBaseScore(scoredCards, scoredHand);
        
        //TEST DATA
        TestData.score = score;
        TestData.handName = scoredHand.name;

        return score;
    }
    public PossiblePokerHand GetPokerHand(List<PokerCard> playedHand)
    {       
        foreach(PossiblePokerHand possibleHand in possiblePokerHands)
        {
            if(possibleHand.IsThisHand(playedHand) != null)
            {
                return possibleHand;
            }
        }

        throw new Exception("POSSIBLE HAND NOT FOUND");
    }

    public List<PokerCard> GetScoredCards(PossiblePokerHand playedPokerHand, List<PokerCard> playedHand)
    {
        List<PokerCard> scoredCards = new List<PokerCard>();
        if (playedPokerHand.amountOfCardsItScores == 5) {scoredCards = playedHand;}
        else
        {
            for(int i = 0; i < playedPokerHand.indexesOfScoredCardsWhenLessThanFive.Count; i++)
            {
                scoredCards.Add(playedHand[playedPokerHand.indexesOfScoredCardsWhenLessThanFive[i]]);
            }
        }
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
}
