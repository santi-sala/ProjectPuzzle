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

    [Header("Double touch")]
    private Vector2 _firstTouchPosition;
    private Vector2 _secondTouchPosition;
    private Vector2 _initialDelta;

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
       else if(Input.touchCount == 2)
       {
            ManageDoubleInput();
       }
    }

    private void ManageDoubleInput()
    {
        switch (_state)
        {
            case State.None:
            case State.Camera:
                ManageCameraDoubleTouchInput();
                break;
            case State.Puzzlepiece:
                break;

        }
    }

    private void ManageCameraDoubleTouchInput()
    {
        Touch[] touches = Input.touches;

        if (touches[0].phase == TouchPhase.Began)
        {
            _firstTouchPosition = touches[0].position;
        }

        if (touches[1].phase == TouchPhase.Began)
        {
            _firstTouchPosition = touches[0].position;
            _secondTouchPosition = touches[1].position;

            _initialDelta = _firstTouchPosition - _secondTouchPosition;

            _cameraController.DoubleTouchBeganCallback(_firstTouchPosition, _secondTouchPosition);
        }

        Vector2 currentDelta = touches[1].position - touches[0].position;
        float deltaMagnitude = (currentDelta.magnitude - _initialDelta.magnitude) / Screen.width;

        _cameraController.DoubleTouchDrag(deltaMagnitude);

        foreach (Touch touch in touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                _state = State.None;
            }
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
