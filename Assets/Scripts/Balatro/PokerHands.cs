using System;
using System.Linq;
using System.Collections.Generic;

public class PossiblePokerHand
{
    public string name;
    public int chips;
    public int mult;
    public int amountOfCardsItScores;

    protected int firstElementCount, secondElementCount;
    protected int firstElementNumber, secondElementNumber;
    protected bool isFlush, isFullHouse, isStraight, isRoyalStraight;
    public int[] values;
    public string[] suits;

    public List<int> indexesOfScoredCardsWhenLessThanFive;
    public PossiblePokerHand(string name, int chips, int mult, int amountOfCardsItScores) 
    {
        this.name = name;
        this.chips = chips;
        this.mult = mult;
        this.amountOfCardsItScores = amountOfCardsItScores;

        if (amountOfCardsItScores < 5)
        {
            indexesOfScoredCardsWhenLessThanFive = new List<int>();
        }
    }

    public bool IsFlush(List<PokerCard> playedHand)
    {
        string prevSuit = playedHand[0].suit;
        if(playedHand.Count == 5)
        {
            foreach(PokerCard card in playedHand.Skip(1))
            {
                if(card.suit == prevSuit) {prevSuit = card.suit;}
                else{return false;}
            }
            
            return true;
        }
        return false;
    }

    public bool IsStraight(int[] values)
    {
        if(values.Length == 5)
        {
            Array.Sort(values);

            if (values.SequenceEqual(new List<int> { 2, 3, 4, 5, 14 })) {return true;}

            for (int i = 0; i < values.Length - 1; i++)
            {
                if (values[i] + 1 != values[i + 1]) {return false;}
            }
            
            return true;
        }
        return false;
    }

    public virtual PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        values = new int[playedHand.Count];
        suits = new string[playedHand.Count]; 
        
        for(int index = 0; index < playedHand.Count; index++)
        {
            values[index] = playedHand[index].rank;
            suits[index] = playedHand[index].suit;
        }
 
        var counts = values.GroupBy(n => n)
                           .Select(g => new { Number = g.Key, Count = g.Count() })
                           .OrderByDescending(x => x.Count) // Order by the Count property
                           .ToList();

        int i = 0;
        foreach (var entry in counts)
        {
            if(i == 0) 
            {
                firstElementCount = entry.Count;
                firstElementNumber = entry.Number;
            }
            if(i == 1) 
            {
                secondElementCount = entry.Count;
                secondElementNumber = entry.Number;
            }
            if ((i == 2) || firstElementCount >= 4) {break;}
            i++;
        }

        isFlush = IsFlush(playedHand);
        isFullHouse = firstElementCount == 3 && secondElementCount == 2;
        isStraight = IsStraight(values);
        isRoyalStraight = values.SequenceEqual(new List<int>{10, 11, 12, 13, 14});

        return null;
    }
}

public class FlushFive : PossiblePokerHand
{
    public FlushFive(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand);
        if (isFlush && firstElementCount == 5) {return this;} 
        else return null;
    }

}

public class FlushHouse : PossiblePokerHand
{
    public FlushHouse(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand);
        if (isFlush && isFullHouse) {return this;} 
        else return null;
    }

}

public class FiveOfAKind : PossiblePokerHand
{
    public FiveOfAKind(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    { 
        base.IsThisHand(playedHand);
        if (firstElementCount == 5) {return this;} 
        else return null;
    }

}

public class RoyalFlush : PossiblePokerHand
{
    public RoyalFlush(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand);
        if (isFlush && isRoyalStraight) {return this;} 
        else return null;
    }

}

public class StraightFlush : PossiblePokerHand
{
    public StraightFlush(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand);
        if (isStraight && isFlush) {return this;} 
        else return null;
    }

}

public class FourOfAKind : PossiblePokerHand
{
    public FourOfAKind(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand); 
        if (firstElementCount == 4) 
        {   
            return this;
        } 
        else return null;
    }

}

public class FullHouse : PossiblePokerHand
{
    public FullHouse(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand);
        if (isFullHouse) {return this;} 
        else return null;
    }

}

public class Flush : PossiblePokerHand
{
    public Flush(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand);
        if (isFlush) {return this;} 
        else return null;
    }

}

public class Straight : PossiblePokerHand
{
    public Straight(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand);
        if (isStraight) {return this;} 
        else return null;
    }

}

public class ThreeOfAKind : PossiblePokerHand
{
    public ThreeOfAKind(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand); 
        if (firstElementCount == 3) 
        {
            foreach(PokerCard card in playedHand)
            {
                if(card.rank == firstElementNumber)
                {
                    indexesOfScoredCardsWhenLessThanFive.Add(playedHand.IndexOf(card));
                }
            }
            return this;
        } 
        else return null;
    }

}

public class TwoPair : PossiblePokerHand
{
    public TwoPair(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand); 
        if (firstElementCount == 2 && secondElementCount == 2) 
        {
            foreach(PokerCard card in playedHand)
            {
                if((card.rank == firstElementNumber) || (card.rank == secondElementNumber))
                {
                    indexesOfScoredCardsWhenLessThanFive.Add(playedHand.IndexOf(card));
                }
            }
            return this;
        } 
        else return null;
    }

}

public class OnePair : PossiblePokerHand
{
    public OnePair(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        base.IsThisHand(playedHand); 
        if (firstElementCount == 2) 
        {
            foreach(PokerCard card in playedHand)
            {
                if(card.rank == firstElementNumber)
                {
                    indexesOfScoredCardsWhenLessThanFive.Add(playedHand.IndexOf(card));
                }
            }
            return this;
        } 
        else return null;
    }

}

public class HighCard : PossiblePokerHand
{
    public HighCard(string name, int chips, int mult, int amountOfCardsItScores) : base(name, chips, mult, amountOfCardsItScores) {}

    public override PossiblePokerHand IsThisHand(List<PokerCard> playedHand)
    {
        indexesOfScoredCardsWhenLessThanFive.Add(0);
        return this;
    }
}

