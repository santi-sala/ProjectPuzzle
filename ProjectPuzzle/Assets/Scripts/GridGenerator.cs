using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject _spherePrefab;

    [Header("Settings")]
    [SerializeField] private int _gridSize;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector3 startPosition = Vector2.left * (float)_gridSize / 2 + Vector2.down * (float)_gridSize / 2;

        startPosition.x += 0.5f;
        startPosition.y += 0.5f;

        for (int x = 0; x < _gridSize; x++)
        {
            for (int y = 0; y < _gridSize; y++)
            {
                Vector3 spawnPosition = startPosition + Vector3.right * x + Vector3.up * y;
                Instantiate(_spherePrefab, spawnPosition, Quaternion.identity, this.transform);
            }
        }

        //Instantiate(_spherePrefab, startPosition, Quaternion.identity, this.transform);
    }

}
