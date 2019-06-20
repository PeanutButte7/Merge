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
    
    public Image[] board1;
    public Image[] board2;
    public Image[] board3;
    public Image[] board4;
    public Image[] boards = new Image[3];
 
    public Sprite squareN2;
    public Sprite squareN4;
    public Sprite squareN8;
    public Sprite squareN16;
    public Sprite squareN32;
    public Sprite squareN64;
    public Sprite squareN128;

    private void Start()
    {
        boards = {board1, board2, board3, board4};
        
        // Spawn squares
        for (int i = 0; i < board1.Length; i++)
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
        // Checks for control inputs
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
        
        
        // Sets alpha to 0 if image is not set 
        Color alphaZero = new Color(1F,1F,1F,0F);
        Color alphaOne = new Color(1F,1F,1F,1F);

        for (int i = 0; i < squares.Length; i++)
        {

            if (!squares[i].sprite)
            {
                squares[i].color = alphaZero;
            }
            else
            {
                squares[i].color = alphaOne;
            }
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
