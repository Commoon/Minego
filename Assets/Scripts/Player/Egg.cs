using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minego;

public class Egg : MonoBehaviour
{
    public float ExplosionDelay = 2f;
    public float ExplosionRadius = 1f;
    float createdTime;
    bool exploded = false;

    void Start()
    {
        createdTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (!exploded && Time.timeSinceLevelLoad - createdTime >= ExplosionDelay)
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (exploded)
        {
            return;
        }
        foreach (var playerObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (MathHelpers.DiffInRadius(playerObject.transform, transform, ExplosionRadius))
            {
                var player = playerObject.GetComponent<Player>();
                if (!player.IsDead && !player.IsInvincible)
                    player.Die();
            }
        }
        foreach (var destroyableObject in GameObject.FindGameObjectsWithTag("Destroyable"))
        {
            if (MathHelpers.DiffInRadius(destroyableObject.transform, transform, ExplosionRadius))
            {
                var destroy = destroyableObject.GetComponent<IDestroyable>();
                destroy.BeDestroyed();
            }
        }
        GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0);
        exploded = true;
        Destroy(gameObject, 0.5f);
    }
}
