using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [Header("Elements")]
    private PuzzleGenerator _puzzleGenerator;

    [Header("Settings")]
    private float _detectionRadius;

    [Header("Piece movement")]
    private Vector3 _clickedPosition;
    private PuzzlePiece _currentPiece;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Configure(PuzzleGenerator puzzleGenerator, float gridScale)
    {
        _puzzleGenerator = puzzleGenerator;
        _detectionRadius = gridScale * 1.5f;
    }

    public bool SingleTouchBeganCallback(Vector3 worldPosition)
    {
        PuzzlePiece[] puzzlePieces = _puzzleGenerator.GetPuzzlePieces();
        _currentPiece = GetClosestPuzzlePiece(worldPosition, puzzlePieces);

        if (_currentPiece == null)
        {
            return false;
        }

        ManagePiecesOrder(puzzlePieces);

        _clickedPosition = worldPosition;

        _currentPiece.StartMoving();
        return true;
    }

    private void ManagePiecesOrder(PuzzlePiece[] puzzlePieces)
    {
        float highestZ = puzzlePieces.Length * Constants.PIECE_Z_OFFSET;
        float currentPieceZ = _currentPiece.transform.position.z;

        _currentPiece.transform.position = new Vector3(_currentPiece.transform.position.x, _currentPiece.transform.position.y, -highestZ);

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (puzzlePieces[i] == _currentPiece)
            {
                continue;
            }

            if (puzzlePieces[i].transform.position.z < currentPieceZ)
            {
                puzzlePieces[i].transform.position += Vector3.forward * Constants.PIECE_Z_OFFSET;
            }
        }
    }

    private PuzzlePiece GetClosestPuzzlePiece(Vector3 worldPosition, PuzzlePiece[] puzzlePieces)
    {
        float minDistance = float.MaxValue;
        int closestIndex = -1;

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            float distance = Vector3.Distance(worldPosition, (Vector2)puzzlePieces[i].transform.position);

            if (distance > _detectionRadius)
            {
                continue;
            }

            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        if (closestIndex < 0)
        {
            return null;
        }

        return puzzlePieces[closestIndex];
    }

    public void SingleTouchDrag(Vector3 worldTouchPosition)
    {
        Vector3 moveDelta = worldTouchPosition - _clickedPosition;

        if (_currentPiece != null)
        {
            _currentPiece.Move(moveDelta);
        }
    }
}
