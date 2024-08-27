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
    [SerializeField] private PuzzleController _puzzleController;
    [SerializeField] private CameraController _cameraController;

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
                if (_puzzleController.SingleTouchBeganCallback(worldTouchPosition))
                {
                    _state = State.Puzzlepiece;
                }
                else
                {
                    _cameraController.SingleTouchBeganCallback(touchPosition);
                    _state = State.Camera;
                }
                break;
            case TouchPhase.Moved:
                if (_state == State.Puzzlepiece)
                {
                    _puzzleController.SingleTouchDrag(worldTouchPosition);
                }
                else if (_state == State.Camera)
                {
                    _cameraController.SingleTouchDrag(touchPosition);
                }
                break;
            case TouchPhase.Stationary:
                if(_state == State.Puzzlepiece)
                {
                    _puzzleController.SingleTouchDrag(worldTouchPosition);
                }
                else if (_state == State.Camera)
                {
                    _cameraController.SingleTouchDrag(touchPosition);
                }
                break;
            case TouchPhase.Ended:
                if (_state == State.Puzzlepiece)
                {
                    _puzzleController.SingleTouchEndedCallback();
                }
                break;
            case TouchPhase.Canceled:
                break;
            default:
                break;
        }
    }
}
