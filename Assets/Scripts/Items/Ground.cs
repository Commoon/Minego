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
    }
}
