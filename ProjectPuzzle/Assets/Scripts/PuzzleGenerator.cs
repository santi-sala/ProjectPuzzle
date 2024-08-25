using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PuzzleController _puzzleController;
    [SerializeField] private PuzzlePiece _puzzlePiecePrefab;

    [Header("Settings")]
    [SerializeField] private int _gridSize;
    [SerializeField] private float _gridScale;
    private List<PuzzlePiece> _puzzlePieces = new List<PuzzlePiece>();


    // Start is called before the first frame update
    void Start()
    {
        _puzzleController.Configure(this ,_gridScale);
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _puzzlePieces.Clear();

        Vector3 startPosition = Vector2.left * _gridScale * _gridSize / 2 + Vector2.down * _gridScale * _gridSize / 2;

        startPosition.x += _gridScale / 2;
        startPosition.y += _gridScale / 2;

        for (int x = 0; x < _gridSize; x++)
        {
            for (int y = 0; y < _gridSize; y++)
            {
                //Vector3 spawnPosition = startPosition + (Vector3.right * x + Vector3.up * y) * _gridScale;
                Vector3 spawnPosition = startPosition + new Vector3(x,y) * _gridScale;

                spawnPosition.z -= Constants.PIECE_Z_OFFSET * GridIndexFromposition(x, y);

                PuzzlePiece puzzlePieceInstance = Instantiate(_puzzlePiecePrefab, spawnPosition, Quaternion.identity, this.transform);

                _puzzlePieces.Add(puzzlePieceInstance);

                Vector2 tiling = new Vector2(1f / _gridSize, 1f / _gridSize);
                Vector2 offset = new Vector2((float)x / _gridSize, (float)y / _gridSize);

               puzzlePieceInstance.Configure(_gridScale, tiling, offset);
            }
        }
    }

    private int GridIndexFromposition(int x, int y)
    {
        return y + _gridSize * x;
    }

    public PuzzlePiece[] GetPuzzlePieces()
    {
        return _puzzlePieces.ToArray();
    }

}
