using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region PC Inputs

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }

        #endregion

        #region Mobile Inputs

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            } else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }

        #endregion
        
        // Calculate distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            } else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2) Input.mousePosition - startTouch;
            }
        }
        
        // Is deadzone crossed?
        if (swipeDelta.magnitude > 10) 
        {
            // Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or right
                if (x < 0)
                {
                    swipeLeft = true;
                    Debug.Log("left");
                }
                else
                {
                    swipeRight = true;
                    Debug.Log("right");
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                    Debug.Log("Down");
                }
                else
                {
                    swipeUp = true;
                    Debug.Log("Up");
                }
            }
            
            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }


    public Vector2 SwipeDelta => swipeDelta;

    public bool Tap => tap;
    public bool SwipeLeft => swipeLeft;
    public bool SwipeRight => swipeRight;
    public bool SwipeUp => swipeUp;
    public bool SwipeDowm => swipeDown;
}
