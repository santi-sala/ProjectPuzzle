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
        PuzzlePiece closestPuzzlePiece = GetClosestPuzzlePiece(worldPosition, puzzlePieces);

        if (closestPuzzlePiece == null)
        {
            return false;
        }
        //closestPuzzlePiece.transform.position += Vector3.up;
        closestPuzzlePiece.gameObject.SetActive(false);
        //Destroy(closestPuzzlePiece.gameObject);
        return true;
    }

    private PuzzlePiece GetClosestPuzzlePiece(Vector3 worldPosition, PuzzlePiece[] puzzlePieces)
    {
        float minDistance = float.MaxValue;
        int closestIndex = -1;

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            float distance = Vector3.Distance(worldPosition, puzzlePieces[i].transform.position);

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
}
