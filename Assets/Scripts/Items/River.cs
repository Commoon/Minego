using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    public float ResistiveForce = 25f;
    public Transform RespawnPosition;
    public float DieDelay = 2f;
    Vector2[] points;
    List<DynamicParticle> surfaceParticles;

    private void Awake()
    {
        var area = GetComponent<PolygonCollider2D>();
        points = new Vector2[area.points.Length];
        for (var i = 0; i < area.points.Length; ++i)
        {
            points[i] = Vector2.Scale(area.points[i], transform.localScale);
        }
    }


    void Update()
    {
    }

    bool CheckPositionInArea(Vector2 v)
    {
        var p = new Vector2(transform.position.x, transform.position.y);
        for (var i = 1; i < points.Length; ++i)
        {
            var a = p + points[i] - v;
            var b = p + points[i - 1] - v;
            if (a.y * b.x - a.x * b.y > 0)
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && CheckPositionInArea(collision.transform.position))
        {
            var rb2d = collision.attachedRigidbody;
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.GetComponent<Player>();
                if (!player.GetComponent<PlayerController>().IsGrounded || CheckPositionInArea(collision.transform.position + Vector3.up))
                {
                    player.BeHitted(RespawnPosition.position - collision.transform.position, true, RespawnPosition.position, DieDelay);
                }
            }
            rb2d.AddForce(-ResistiveForce * collision.friction * rb2d.mass * rb2d.velocity.normalized);
        }
    }
}
