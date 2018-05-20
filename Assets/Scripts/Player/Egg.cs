using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    public void Explode()
    {
        Debug.Log("TODO: Explode");
        Destroy(this, 0.5f);
    }
}
