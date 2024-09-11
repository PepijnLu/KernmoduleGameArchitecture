using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class TestingButtons : MonoBehaviour
{
    public int buttonIndex;
    public Balatro balatro;
    public TextMeshProUGUI rankText, suitText, handText, scoreText;

    public List<TextMeshProUGUI> rankTexts, suitTexts;

    List<int> ranksInOrder = new List<int>(){2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0};
    List<string> suitsinOrder = new List<string>(){"Spades", "Clubs", "Diamonds", "Hearts"};
    
    void Start()
    {
        SetText();
    }
    public void ButtonRankUp()
    {
        balatro.rankIndex[buttonIndex]++;
        SetText();
    }

    public void ButtonRankDown()
    {
        balatro.rankIndex[buttonIndex]--;
        SetText();
    }

    public void ButtonSuitUp()
    {
        balatro.suitIndex[buttonIndex]++;
        SetText();
    }

    public void ButtonSuitDown()
    {
        balatro.suitIndex[buttonIndex]--;
        SetText();
    }

    void SetText()
    {
        if(balatro.rankIndex[buttonIndex] == 14) {balatro.rankIndex[buttonIndex] = 0;}
        else if(balatro.rankIndex[buttonIndex] == -1) {balatro.rankIndex[buttonIndex] = 13;}
        if(balatro.suitIndex[buttonIndex] == 4) {balatro.suitIndex[buttonIndex] = 0;}
        else if(balatro.suitIndex[buttonIndex] == -1) {balatro.suitIndex[buttonIndex] = 3;}

        if(rankText != null) {rankText.text = ranksInOrder[balatro.rankIndex[buttonIndex]].ToString();}
        if(suitText != null) {suitText.text = suitsinOrder[balatro.suitIndex[buttonIndex]];}
        Debug.Log(gameObject.name);
    }

    public void SubmitScore()
    {
        balatro.playedHand.Clear();
        for(int i = 0; i < 5; i++)
        {
            int val = int.Parse(rankTexts[i].text);
            if(val != 0)
            {
                balatro.InitializePlayedHand(val, suitTexts[i].text);
                // Debug.Log($"Card value: {val}, card suit: {suitTexts[i].text}");
            }
        }

        int newScore = balatro.scoring.Score(balatro.playedHand);
        handText.text = TestData.handName;
        scoreText.text = newScore.ToString();
    }
}
