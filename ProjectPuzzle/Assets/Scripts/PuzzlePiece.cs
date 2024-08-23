using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Configure(float scale, Vector2 tilling, Vector2 offset)
    {
        this.transform.localScale = Vector3.one * scale;

        _renderer.material.mainTextureScale = tilling;
        _renderer.material.mainTextureOffset = offset;
    }
}
