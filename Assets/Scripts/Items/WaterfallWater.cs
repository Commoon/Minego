using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterfallWater : MonoBehaviour
{
    Waterfall waterfall;
    Rigidbody2D rb2d;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void SetWaterfall(Waterfall w)
    {
        waterfall = w;
    }

    private void HitPlayer(Player player)
    {
        var p = rb2d.position - player.GetComponent<Rigidbody2D>().position;
        if (p.magnitude < transform.localScale.x / 2)
        {
            player.BeHitted(p, true, waterfall.RespawnPosition.position);
        }
    }

    private void FixedUpdate()
    {
        HitPlayer(waterfall.player1);
        HitPlayer(waterfall.player2);
    }
}
