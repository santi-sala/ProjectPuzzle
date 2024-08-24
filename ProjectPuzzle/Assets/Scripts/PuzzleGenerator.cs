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
    // Start is called before the first frame update
    void Start()
    {
        _puzzleController.Configure(_gridScale);
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector3 startPosition = Vector2.left * _gridScale * _gridSize / 2 + Vector2.down * _gridScale * _gridSize / 2;

        startPosition.x += _gridScale / 2;
        startPosition.y += _gridScale / 2;

        for (int x = 0; x < _gridSize; x++)
        {
            for (int y = 0; y < _gridSize; y++)
            {
                //Vector3 spawnPosition = startPosition + (Vector3.right * x + Vector3.up * y) * _gridScale;
                Vector3 spawnPosition = startPosition + new Vector3(x,y) * _gridScale;
                PuzzlePiece puzzlePieceInstance = Instantiate(_puzzlePiecePrefab, spawnPosition, Quaternion.identity, this.transform);


                Vector2 tiling = new Vector2(1f / _gridSize, 1f / _gridSize);
                Vector2 offset = new Vector2((float)x / _gridSize, (float)y / _gridSize);

               puzzlePieceInstance.Configure(_gridScale, tiling, offset);
            }
        }

    }

}
