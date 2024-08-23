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
    [SerializeField] private float _gridScale;
    // Start is called before the first frame update
    void Start()
    {
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
                GameObject sphereInstance = Instantiate(_spherePrefab, spawnPosition, Quaternion.identity, this.transform);

                sphereInstance.transform.localScale = Vector3.one * _gridScale;
            }
        }

    }

}
