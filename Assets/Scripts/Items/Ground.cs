using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    const float GrassWidthUnits = 9.02946f;
    const float GrassHeightUnits = 0.335f;
    const float GrassWidth = 0.21595f;
    const float GrassHeight = 0.2158742f;

    public bool HasGrass = false;
    public GameObject GrassPrefab;
    public GameObject GrassMaskPrefab;

    private void Awake()
    {
        var r = GetComponent<Renderer>();
        if (transform.localScale.x < transform.localScale.y)
        {
            r.material.mainTextureScale = new Vector2(1, transform.localScale.y / transform.localScale.x);
        }
        else
        {
            r.material.mainTextureScale = new Vector2(transform.localScale.x / transform.localScale.y, 1);
        }
        if (HasGrass)
        {
            var grassMask = Instantiate(GrassMaskPrefab, transform);
            var l = Mathf.FloorToInt(transform.localScale.x / GrassWidthUnits);
            var newScale = new Vector2(
                GrassWidth / transform.localScale.x,
                GrassHeight / transform.localScale.y
            );
            var width = 1 / transform.localScale.x * GrassWidthUnits;
            var height = 0.5f + 1 / transform.localScale.y * GrassHeightUnits / 2;
            var now = -0.5f - UnityEngine.Random.Range(0, width / 2);
            for (var i = 0; i <= l + 1; ++i)
            {
                var grass = Instantiate(GrassPrefab, transform);
                grass.transform.localScale = newScale;
                grass.transform.localPosition = new Vector2(now, height);
                now += width;
            }
        }
    }
}
