using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    public float ParticleRadius = 0.1f;
    public GameObject LiquidParticlePrefab;
    public float BrownForce = 10f;

    PolygonCollider2D area;
    List<DynamicParticle> surfaceParticles;

    private void Awake()
    {
        area = GetComponent<PolygonCollider2D>();
    }

    // Use this for initialization
    void Start()
    {
        FillWater();
    }

    void FillWater()
    {
        var step = ParticleRadius * 2;
        var k1 = (area.points[3].x - area.points[0].x) / (area.points[2].y - area.points[0].y);
        var k2 = (area.points[2].x - area.points[1].x) / (area.points[3].y - area.points[1].y);
        var b1 = area.points[0].x - k1 * area.points[0].y;
        var b2 = area.points[1].x - k2 * area.points[1].y;
        surfaceParticles = new List<DynamicParticle>();
        for (var i = area.points[3].y; i < area.points[0].y; i += step)
        {
            var isSurface = i + step >= area.points[0].y;
            var right = k2 * i + b2;
            for (var j = k1 * i + b1; j < right; j += step)
            {
                var particle = Instantiate(LiquidParticlePrefab, transform)
                    .GetComponent<DynamicParticle>();
                particle.SetLifeTime(0f);
                particle.SetState(DynamicParticle.STATES.WATER);
                particle.transform.localScale = new Vector2(step, step);
                particle.transform.position = transform.position + new Vector3(j, i);
                particle.gameObject.layer = LayerMask.NameToLayer("Liquids");
                if (isSurface)
                {
                    surfaceParticles.Add(particle);
                }
            }
        }
    }
    
    void Update()
    {
        print(surfaceParticles.Count);
        foreach (var particle in surfaceParticles)
        {
            particle.MoveBrown(BrownForce);
        }
    }
}
