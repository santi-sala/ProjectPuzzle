using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PuzzleController _puzzleController;
    [SerializeField] private PuzzlePiece _puzzlePiecePrefab;

    [Header("Settings")]
    [SerializeField] private int _gridSize;
     private float _gridScale;
    private List<PuzzlePiece> _puzzlePieces = new List<PuzzlePiece>();


    // Start is called before the first frame update
    void Start()
    {
        _gridScale = Constants.PUZZLE_WORLD_SIZE / _gridSize;
        
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
                Vector3 correctSpawnPosition = startPosition + new Vector3(x,y) * _gridScale;
                correctSpawnPosition.z -= Constants.PIECE_Z_OFFSET * GridIndexFromposition(x, y);

                Vector3 randomPosition = Random.onUnitSphere * 2f;
                randomPosition.z = correctSpawnPosition.z;

                PuzzlePiece puzzlePieceInstance = Instantiate(_puzzlePiecePrefab, randomPosition, Quaternion.identity, this.transform);

                _puzzlePieces.Add(puzzlePieceInstance);

                Vector2 tiling = new Vector2(1f / _gridSize, 1f / _gridSize);
                Vector2 offset = new Vector2((float)x / _gridSize, (float)y / _gridSize);

               puzzlePieceInstance.Configure(_gridScale, tiling, offset, correctSpawnPosition);
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
