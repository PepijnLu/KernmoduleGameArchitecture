using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerCard : MonoBehaviour
{
    public int rank;
    public string suit;
    public int chips, mult;
    public PokerCard scriptRef;
    public Sprite sprite;
    public Image image;

    private CardsHandout drawer;

    public PokerCard(int rank, string suit, CardsHandout drawer)
    {
        // this.rank = rank;
        // this.suit = suit;
        // this.drawer = drawer;
        // scriptRef = this;

        // if(rank == 14) {chips = 11;}
        // else if(rank > 10) {chips = 10;}
        // else {chips = rank;}
    }

    public void SetValues(int _rank, string _suit, Sprite _sprite)
    {
        rank = _rank;
        suit = _suit;
        sprite = _sprite;
        image = GetComponent<Image>();

        if(rank == 14) {chips = 11;}
        else if(rank > 10) {chips = 10;}
        else {chips = rank;}
    }

    public void Select()
    {
        CardsHandout.instance.SelectCard(this);
    }
}