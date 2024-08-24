using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Renderer _renderer;

    [Header("Movement")]
    private Vector3 _startMovePosition;
    public void Configure(float scale, Vector2 tilling, Vector2 offset)
    {
        this.transform.localScale = Vector3.one * scale;

        _renderer.material.mainTextureScale = tilling;
        _renderer.material.mainTextureOffset = offset;
    }

    public void StartMoving()
    {
        _startMovePosition = transform.position;
    }

    public void Move(Vector3 moveDelta)
    {
        Vector3 targetPosition = _startMovePosition + moveDelta;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 60 * 0.3f);
    }        

    public void StopMoving() 
    {

    }
}
