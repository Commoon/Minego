using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    public float ResistiveForce = 40f;
    public Transform RespawnLocation;
    PolygonCollider2D area;
    List<DynamicParticle> surfaceParticles;

    private void Awake()
    {
        area = GetComponent<PolygonCollider2D>();
    }


    void Update()
    {
    }

    bool CheckPositionInArea(Vector2 v)
    {
        var p = new Vector2(transform.position.x, transform.position.y);
        for (var i = 1; i < area.points.Length; ++i)
        {
            var a = p + area.points[i] - v;
            var b = p + area.points[i - 1] - v;
            if (a.y * b.x - a.x * b.y > 0)
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var rb2d = collision.attachedRigidbody;
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.GetComponent<Player>();
                if (!player.IsDead && CheckPositionInArea(collision.transform.position))
                {
                    player.Die(RespawnLocation.position);
                }
            }
            rb2d.AddForce(-ResistiveForce * rb2d.velocity.normalized);
        }
    }
}
