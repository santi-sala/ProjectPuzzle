using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 1f;
    private Vector3 _cameraStartMovePosition;
    private Vector2 _firstTouchScreenPosition;

    [Header("Zoom")]
    [SerializeField]private float _zoomMultiplier;
    [SerializeField]private Vector2 _minMaximumOrthographicSize;
    [SerializeField] private float _zoomSpeed;
    private Vector3 _zoomInitialPosition;
    private Vector3 _zoomCenter;
    private float _currenOrthographicSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SingleTouchBeganCallback(Vector2 screenPosition)
    {
        _cameraStartMovePosition = transform.position;
        _firstTouchScreenPosition = screenPosition;
    }

    public void SingleTouchDrag(Vector2 screenPostion)
    {
        Vector3 moveDelta = (screenPostion - _firstTouchScreenPosition) / Screen.width;
        Vector3 targetPostiion = _cameraStartMovePosition - moveDelta * _moveSpeed;
        transform.position = targetPostiion;
    }

    public void DoubleTouchBeganCallback(Vector2 firstTouchPosition, Vector2 secondTouchPosition)
    {
        _currenOrthographicSize = Camera.main.orthographicSize;

        _firstTouchScreenPosition = firstTouchPosition;

        _zoomInitialPosition = transform.position;

        _zoomCenter = ((Camera.main.ScreenToWorldPoint(firstTouchPosition) + Camera.main.ScreenToWorldPoint(secondTouchPosition)) / 2);
        _zoomCenter.z = -10;
    }

    public void DoubleTouchDrag(float deltaMagnitude)
    {
        SetOrtographicSize(deltaMagnitude);

        MoveTowardsZoomCenter();
    }
    private void SetOrtographicSize(float deltaMagnitude)
    {
        float targetOrthographicSize = _currenOrthographicSize - deltaMagnitude * _zoomMultiplier;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, _minMaximumOrthographicSize.x, _minMaximumOrthographicSize.y);

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetOrthographicSize, Time.deltaTime * 60 * 0.3f);
    }
    private void MoveTowardsZoomCenter()
    {
        float percent = Mathf.InverseLerp(_minMaximumOrthographicSize.x, _minMaximumOrthographicSize.y, _currenOrthographicSize - Camera.main.orthographicSize);

        percent *= _zoomSpeed;

        Vector3 targetPosition = Vector3.Lerp(_zoomInitialPosition, _zoomCenter, percent);

        transform.position = targetPosition;

        _cameraStartMovePosition = targetPosition;
    }
}
