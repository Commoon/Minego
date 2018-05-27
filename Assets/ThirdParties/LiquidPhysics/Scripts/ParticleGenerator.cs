using UnityEngine;
using System.Collections;
/// <summary
/// Particle generator.
/// 
/// The particle generator simply spawns particles with custom values. 
/// See the Dynamic particle script to know how each particle works..
/// 
/// Visit: www.codeartist.mx for more stuff. Thanks for checking out this example.
/// Credit: Rodrigo Fernandez Diaz
/// Contact: q_layer@hotmail.com
/// </summary>

public class ParticleGenerator : MonoBehaviour
{
    public float SpawnInterval = 0.025f; // How much time until the next particle spawns
    float lastSpawnTime = float.MinValue; //The last spawn time
    public float ParticleLifetime = 3f; //How much time will each particle live
    public float ParticleScaleDelay = 3f;
    public Vector3 particleForce; //Is there a initial force particles should have?
    public DynamicParticle.STATES particlesState = DynamicParticle.STATES.WATER; // The state of the particles spawned
    public Transform particlesParent; // Where will the spawned particles will be parented (To avoid covering the whole inspector with them)
    public GameObject LiquidParticlePrefab;
    public float InitialRadius = 0.3f;
    public Transform StartPosition;

    void Start()
    {
    }

    public DynamicParticle Spawn()
    {
        if (lastSpawnTime + SpawnInterval < Time.time)
        { // Is it time already for spawning a new particle?
            var newLiquidParticle = Instantiate(LiquidParticlePrefab); //Spawn a particle
            newLiquidParticle.GetComponent<Rigidbody2D>().AddForce(particleForce); //Add our custom force
            DynamicParticle particleScript = newLiquidParticle.GetComponent<DynamicParticle>(); // Get the particle script
            particleScript.SetLifeTime(ParticleLifetime, ParticleScaleDelay); //Set each particle lifetime
            particleScript.SetState(particlesState); //Set the particle State
            particleScript.SetRadius(InitialRadius);
            newLiquidParticle.transform.position = StartPosition.position;// Relocate to the spawner position
            newLiquidParticle.transform.parent = particlesParent;// Add the particle to the parent container			
            lastSpawnTime = Time.time; // Register the last spawnTime			
            return particleScript;
        }
        return null;
    }
}
