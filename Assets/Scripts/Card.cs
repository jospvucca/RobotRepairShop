using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card 
{
    public bool _isFaceUp = false;
    public bool _isMatched = false;
    public string _img;

    public int _id;

    public Card()
    {
        _img = "robot";
    }

    public Card(string cardName, int i)
    {
        _img = cardName;
        _id = i;
    }
}
