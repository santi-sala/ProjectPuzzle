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
    
}
