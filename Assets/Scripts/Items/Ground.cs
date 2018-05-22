using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
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
