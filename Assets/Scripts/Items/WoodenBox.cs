using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBox : MonoBehaviour, IDestroyable
{
    [HideInInspector] public bool IsGrounded = false;

    Rigidbody2D rb2d;
    bool destroying = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void BeDestroyed()
    {
        if (destroying)
        {
            return;
        }
        destroying = true;
        Destroy(gameObject, 0.5f);
    }
    
    void Start()
    {
    }

    void Update()
    {
    }
}
