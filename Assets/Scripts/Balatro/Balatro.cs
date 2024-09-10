using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balatro : MonoBehaviour
{
    //TESTING PURPOSES
    public List<int> values;
    public List<string> suits;
    Scoring scoring = new Scoring();
    string pokerHand;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //5 for testing
            pokerHand = scoring.GetPokerHand(5);
            Debug.Log(pokerHand);
        }
    }
}
