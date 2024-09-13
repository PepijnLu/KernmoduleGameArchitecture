using UnityEngine;
using UnityEngine.UI;

public class PokerCard : MonoBehaviour
{
    public Sprite sprite;
    public Image image;
    
    public int rank;
    public string suit;
    public int chips, mult;
    public bool isInHand;

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

    public void Select() // Attached to the button component
    {
        CardsHandout.instance.SelectCard(this);
    }
}