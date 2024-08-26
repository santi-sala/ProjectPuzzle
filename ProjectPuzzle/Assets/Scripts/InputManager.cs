using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    enum State
    {
        None,
        Puzzlepiece,
        Camera
    }
    private State _state;


    [Header("Elements")]
    [SerializeField] private PuzzleController puzzleController;

    // Start is called before the first frame update
    void Start()
    {
        _state = State.None;
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
        worldTouchPosition.z = 0;

        TouchPhase touchPhase = Input.touches[0].phase;

        switch (touchPhase)
        {
            case TouchPhase.Began:
                if (puzzleController.SingleTouchBeganCallback(worldTouchPosition))
                {
                    _state = State.Puzzlepiece;
                    return;
                }
                break;
            case TouchPhase.Moved:
                if (_state == State.Puzzlepiece)
                {
                    puzzleController.SingleTouchDrag(worldTouchPosition);
                }
                else if (_state == State.Camera)
                {
                    // Move Camera
                }
                break;
            case TouchPhase.Stationary:
                if(_state == State.Puzzlepiece)
                {
                    puzzleController.SingleTouchDrag(worldTouchPosition);
                }
                break;
            case TouchPhase.Ended:
                if (_state == State.Puzzlepiece)
                {
                    puzzleController.SingleTouchEndedCallback();
                }
                break;
            case TouchPhase.Canceled:
                break;
            default:
                break;
        }
    }
}
