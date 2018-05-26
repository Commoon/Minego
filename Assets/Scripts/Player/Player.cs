using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public const float CenterFromGround = 0.8624f;

    public bool IsDead = false;
    public Player Villain;
    public StageManager StageManager;
    public float InvincibleTime = 2.0f;
    public GameObject DeadPlayerPrefeb;

    public bool IsPlayer1
    {
        get { return pc.PlayerNumber == "1"; }
    }
    [HideInInspector] public int Score = 0;
    [HideInInspector] public float LastRespawned = -10;
    public bool IsInvincible
    {
        get { return Time.time - LastRespawned <= InvincibleTime; }
    }

    Vector3 respawnPosition;
    PlayerController pc;
    DeadPlayer deadPlayer;
    Vector2 deadDirection;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (IsDead)
        {
            return;
        }
    }

    void Respawn()
    {
        IsDead = false;
        LastRespawned = Time.time;
    }

    public void Die(Vector2 direction, Vector3? respawnPosition = null, float? delay = null)
    {
        direction.Normalize();
        this.respawnPosition = respawnPosition ?? transform.position;
        StageManager.GetPoint(Villain, 1);
        IsDead = true;
        deadDirection = direction;
        if (delay.HasValue)
        {
            Invoke("InitDead", delay.Value);
        }
        else
        {
            InitDead();
        }
    }
    public void InitDead()
    {
        transform.position = respawnPosition;
        var deadPlayer = Instantiate(DeadPlayerPrefeb, transform.position, Quaternion.identity).GetComponent<DeadPlayer>();
        deadPlayer.Init(this, deadDirection);
    }

    public void Respwan()
    {
        IsDead = false;
    }

    public void BeHitted(RaycastHit2D hit)
    {
        if (!IsDead && !IsInvincible)
        {
            Die(-hit.normal);
        }
    }
}