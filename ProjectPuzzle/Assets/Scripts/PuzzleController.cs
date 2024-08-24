using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
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
    public void Configure(float gridScale)
    {
        _detectionRadius = gridScale * 1.5f;
    }

    public bool SingleTouchBecanCallback(Vector3 worldPosition)
    {
        
    }

}
