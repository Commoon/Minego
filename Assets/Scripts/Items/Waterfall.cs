using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    public Transform RespawnPosition;
    public Player player1;
    public Player player2;

    ParticleGenerator pg;

    private void Awake()
    {
        pg = GetComponent<ParticleGenerator>();
    }

    void Start()
    {

    }
    
    void Update()
    {
        var p = pg.Spawn();
        if (p == null)
        {
            return;
        }
        var water = p.GetComponent<WaterfallWater>();
        water.SetWaterfall(this);
    }
}
