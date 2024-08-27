using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PuzzlePiece : MonoBehaviour, IComparable<PuzzlePiece>
{
    [Header("Elements")]
    [SerializeField] private Renderer _renderer;

    [Header("Movement")]
    private Vector3 _startMovePosition;

    [Header("Validation")]
    private Vector3 _correctPosition;

    [Header("Neighbors")]
    private PuzzlePiece[] _neighbours;
    public bool PieceIsInValidPosition { get; private set; }
    public void Configure(float scale, Vector2 tilling, Vector2 offset, Vector3 correctPosition)
    {
        this.transform.localScale = Vector3.one * scale;

        _renderer.material.mainTextureScale = tilling;
        _renderer.material.mainTextureOffset = offset;

        _correctPosition = correctPosition;
    }

    public void StartMoving()
    {
        _startMovePosition = transform.position;

        foreach (PuzzlePiece piece in _neighbours)
        {
            if (piece != null)
            {
                Debug.Log("Neighbour: " + piece.name);
            }
        }
    }

    public void Move(Vector3 moveDelta)
    {
        Vector3 targetPosition = _startMovePosition + moveDelta;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 60 * 0.3f);
    }        

    public void StopMoving() 
    {
        bool isValid = CheckForValidation();

        if(isValid)
        {
            return;
        }

        CheckForNeighbors();
    }

    private void CheckForNeighbors()
    {
    }

    private bool CheckForValidation()
    {
        if (PieceIsCloseToCorrectPosition())
        {
            ValidatePiece();
            return true;
        }

        return false;
    }


    private bool PieceIsCloseToCorrectPosition()
    {
        return Vector3.Distance((Vector2)transform.position, (Vector2)_correctPosition) < GetMinimumValidDistance();
    }

    private float GetMinimumValidDistance()
    {
        return Mathf.Max(0.05f, transform.localScale.x / 5);
    }

    private void ValidatePiece()
    {
        _correctPosition.z = 0;
        transform.position = _correctPosition;

        PieceIsInValidPosition = true;

        Debug.LogWarning("Piece placed correctly: " + name);
    }

    public int CompareTo(PuzzlePiece otherPiece)
    {
        return transform.position.z.CompareTo(otherPiece.transform.position.z);
    }

    internal void SetNeighbours(params PuzzlePiece[] puzzlePieces)
    {
        _neighbours = puzzlePieces;
    }
}
