using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PuzzleController puzzleController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
       if(Input.touchCount == 1)
       {
            ManageSingleInput();
       }
    }

    private void ManageSingleInput()
    {
        Vector2 touchPosition = Input.GetTouch(0).position;
        Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

        TouchPhase touchPhase = Input.touches[0].phase;

        switch (touchPhase)
        {
            case TouchPhase.Began:
                if (puzzleController.SingleTouchBeganCallback(worldTouchPosition))
                {
                }
                break;
            case TouchPhase.Moved:
                break;
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Ended:
                break;
            case TouchPhase.Canceled:
                break;
            default:
                break;
        }
    }
}
