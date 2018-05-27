using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomLimit : MonoBehaviour
{
    public Transform RespawnPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player.IsDead)
            {
                player.transform.position = RespawnPosition.position;
            }
            else
            {
                player.BeHitted(-collision.relativeVelocity, true, RespawnPosition.position);
            }
        }
        else if (collision.gameObject.CompareTag("DynamicParticle"))
        {
            Destroy(collision.gameObject);
        }
    }
}
