using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _cols = 4;
    private int _rows = 4;
    private int _totalCards = 16;
    private int _matchesNeededToWin = 8;
    private int _matchesMade = 0;
    private int _cardW = 100;
    private List<Card> _aCards;
    private Card[,] _aGrid;
    private List<Card> _aCardsFlipped;
    private bool _playerCanClick;
    private bool _playerHasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerCanClick = true;
        _aCards = new List<Card>();
        _aGrid = new Card[_rows, _cols];
        _aCardsFlipped = new List<Card>();

        BuildDeck();

        for (int i = 0; i < _rows; i++) 
        {

            for (int j = 0; j < _cols; j++) 
            {
                int randomNumber = Random.Range(0, _aCards.Count);
                _aGrid[i, j] = _aCards[randomNumber];
                _aCards.RemoveAt(randomNumber);
            }
            /*
            for (int j = 0; j < _cols; j++)
            {
                _aGrid[i, j] = new Card();
            }*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerHasWon == true)
        {
            Debug.Log("Game Over!");
        }
    }

    void OnGUI()
    {
        Rect playfield = new Rect(0, 0, Screen.width, Screen.height);
        GUILayout.BeginArea(playfield);
        BuildGrid();
        GUILayout.EndArea();
    }

    void BuildGrid()
    {
        string img = "";

        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        for (int i = 0; i < _rows; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            for (int j = 0; j < _cols; j++)
            {
                Card card = _aGrid[i, j];

                if (card._isMatched)
                {
                    img = "blank";
                }
                else if (card._isFaceUp) 
                {
                    img = card._img;
                }
                else
                {
                    img = "wrench";
                }

                GUI.enabled = !card._isMatched;

                /*if(card._isFaceUp)
                {
                    img = card._img;
                }
                else
                {
                    img = "wrench";
                }*/
                //mozda kopirat ovu doli u if 
                if (GUILayout.Button(Resources.Load(img) as Texture, GUILayout.Width(_cardW)))
                {
                    if (_playerCanClick)
                    {
                        FlipCardFaceUp(card);
                    }
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
    }

    void BuildDeck()
    {
        int id = 0;
        int totalRobots = 4;
        Card card;

        for (int i = 0; i < totalRobots; i++) 
        {
            List<string> aRobotParts = new List<string>();
            aRobotParts.Add("Head");
            aRobotParts.Add("Arm");
            aRobotParts.Add("Leg");

            for (int j = 0; j < 2; j++)
            {
                int randomNumber = Random.Range(0, aRobotParts.Count);
                string theMissingPart = aRobotParts[randomNumber];

                aRobotParts.RemoveAt(randomNumber);

                card = new Card("robot" + (i + 1) + "Missing" + theMissingPart, id);
                _aCards.Add(card);

                card = new Card("robot" + (i + 1) + theMissingPart, id);
                _aCards.Add(card);

                id++;
            }
        }
    }

    void FlipCardFaceUp(Card card)
    {
        card._isFaceUp = true;

        if (_aCardsFlipped.IndexOf(card) < 0) 
        {
            _aCardsFlipped.Add(card);
        }

        if(_aCardsFlipped.Count == 2)
        {
            _playerCanClick = false;
            StartCoroutine(CheckCards());
        }
    }

    IEnumerator CheckCards()
    {
        yield return new WaitForSeconds(1);

        if (_aCardsFlipped[0]._id == _aCardsFlipped[1]._id)
        {
            _aCardsFlipped[0]._isMatched = true;
            _aCardsFlipped[1]._isMatched = true;

            _matchesMade++;
        }
        else
        {
            _aCardsFlipped[0]._isFaceUp = false;
            _aCardsFlipped[0]._isFaceUp = false;
        }

        if (_matchesMade == _matchesNeededToWin)
        {
            _playerHasWon = true;
        }

        _aCardsFlipped = new List<Card>();
        _playerCanClick = true;
    }
}

//NEGDI SE UBAGALO NEZ DI
