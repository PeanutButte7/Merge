using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public Controls controls;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    
    public Image[] squares;

    public Sprite squareN2;
    public Sprite squareN4;
    public Sprite squareN8;
    public Sprite squareN16;
    public Sprite squareN32;
    public Sprite squareN64;
    public Sprite squareN128;

    private void Start()
    {
        // Spawn squares
        for (int i = 0; i < squares.Length; i++)
        {
            int randomNumber = Random.Range(1, 11);
            
            if (randomNumber <= 4)
            {
                squares[i].sprite = squareN4;
            }
            else
            {
                squares[i].sprite = squareN2;
            }
        }
    }

    void Update()
    {
        if (controls.SwipeRight)
        {
            MergeSquaresRight();
        } else if (controls.SwipeLeft)
        {
            MergeSquaresLeft();
        } else if (controls.SwipeUp)
        {
            MergeSquaresUp();
        } else if (controls.SwipeDowm)
        {
            MergeSquaresDown();
        }
    }

    void SpawnSquare()
    {
        ArrayList unsignedSquares = new ArrayList();
        
        for (int i = 0; i < squares.Length; i++)
        {
            if (!squares[i].sprite)
            {
                unsignedSquares.Add(i);
            }
        }

        int randomImage = Random.Range(0, unsignedSquares.Count);
        
        int randomNumber = Random.Range(1, 11);

        if (randomNumber <= 4)
        {
            squares[randomImage].sprite = squareN4;
        }
        else
        {
            squares[randomImage].sprite = squareN2;
        }
    }

    void MergeSquaresRight()
    {
        int squareToRight;
        bool merged = false;

        for (int i = 0; i < squares.Length; i++)
        {
            if (i != 3 && i != 7 && i != 11 && i != 15)
            {
                squareToRight = i + 1;
            }
            else
            {
                continue;
            }
            
            if (squares[i].sprite == squares[squareToRight].sprite)
            {
                merged = true;
                MergeSquares(i, squareToRight);
            }
            else if (!squares[squareToRight].sprite && squares[i].sprite)
            {
                squares[squareToRight].sprite = squares[i].sprite;
                squares[i].sprite = null;
            }
        }

        if (merged)
        {
            SpawnSquare();
        }
    }
    
    void MergeSquaresLeft()
    {
        int squareToLeft;
        bool merged = false;

        for (int i = 0; i < squares.Length; i++)
        {
            if (i != 0 && i != 4 && i != 8 && i != 12)
            {
                squareToLeft = i + -1;
            }
            else
            {
                continue;
            }
            
            if (squares[i].sprite == squares[squareToLeft].sprite)
            {
                merged = true;
                MergeSquares(i, squareToLeft);
            }
            else if (!squares[squareToLeft].sprite && squares[i].sprite)
            {
                squares[squareToLeft].sprite = squares[i].sprite;
                squares[i].sprite = null;
            }
        }
        
        if (merged)
        {
            SpawnSquare();
        }
    }
    
    void MergeSquaresUp()
    {
        int squareToUp;
        bool merged = false;

        for (int i = 0; i < squares.Length; i++)
        {
            if (i != 0 && i != 1 && i != 2 && i != 3)
            {
                squareToUp = i - 4;
            }
            else
            {
                continue;
            }
            
            if (squares[i].sprite == squares[squareToUp].sprite)
            {
                merged = true;
                MergeSquares(i, squareToUp);
            }
            else if (!squares[squareToUp].sprite && squares[i].sprite)
            {
                squares[squareToUp].sprite = squares[i].sprite;
                squares[i].sprite = null;
            }
        }
        
        if (merged)
        {
            SpawnSquare();
        }
    }
    
    void MergeSquaresDown()
    {
        int squareToDown;
        bool merged = false;

        for (int i = 0; i < squares.Length; i++)
        {
            if (i != 12 && i != 13 && i != 14 && i != 15)
            {
                squareToDown = i + 4;
            }
            else
            {
                continue;
            }
            
            if (squares[i].sprite == squares[squareToDown].sprite)
            {
                merged = true;
                MergeSquares(i, squareToDown);
            }
            else if (!squares[squareToDown].sprite && squares[i].sprite)
            {
                squares[squareToDown].sprite = squares[i].sprite;
                squares[i].sprite = null;
            }
        }
        
        if (merged)
        {
            SpawnSquare();
        }
    }

    void MergeSquares(int currentSquare, int squareToMerge)
    {
        // Debug.Log(currentSquare + " should be merged");
        if (squares[currentSquare].sprite == squareN2)
        {
            squares[squareToMerge].sprite = squareN4;
            squares[currentSquare].sprite = null;

            UpdateScore(4);
        } 
        else if (squares[currentSquare].sprite == squareN4)
        {
            squares[squareToMerge].sprite = squareN8;
            squares[currentSquare].sprite = null;
            
            UpdateScore(8);
        } 
        else if (squares[currentSquare].sprite == squareN8)
        {
            squares[squareToMerge].sprite = squareN16;
            squares[currentSquare].sprite = null;
            
            UpdateScore(16);
        }
        else if (squares[currentSquare].sprite == squareN16)
        {
            squares[squareToMerge].sprite = squareN32;
            squares[currentSquare].sprite = null;
            
            UpdateScore(32);
        }
        else if (squares[currentSquare].sprite == squareN32)
        {
            squares[squareToMerge].sprite = squareN64;
            squares[currentSquare].sprite = null;
            
            UpdateScore(64);
        }
        else if (squares[currentSquare].sprite == squareN64)
        {
            squares[squareToMerge].sprite = squareN128;
            squares[currentSquare].sprite = null;
            
            UpdateScore(128);
        }
    }
    
    public void UpdateScore(int score)
    {
        Score.CurrentScore += score;
        scoreText.SetText(Score.CurrentScore.ToString());

        if (Score.CurrentScore > Score.HighScore)
        {
            Score.HighScore = Score.CurrentScore;
            highScoreText.SetText(Score.HighScore.ToString());
        }
    }
}
