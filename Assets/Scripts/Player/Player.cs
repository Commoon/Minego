using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const float CenterFromFoot = 0.8484f;

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

    Vector3? respawnPosition;
    PlayerController pc;
    Animator anim;
    DeadPlayer deadPlayer;
    Vector2 deadDirection;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (IsDead)
        {
            return;
        }
    }

    public void StartRespawn()
    {
        anim.SetTrigger("Respawn");
    }

    public void Respawn()
    { 
        IsDead = false;
        anim.SetBool("IsDead", false);
        LastRespawned = Time.time;
        pc.Respawn();
    }

    public void Die(Vector2 direction, Vector3? respawnPosition = null, float? delay = null)
    {
        direction.Normalize();
        this.respawnPosition = respawnPosition;
        StageManager.GetPoint(Villain, 1);
        IsDead = true;
        deadDirection = direction;
        pc.InitDead();
        anim.SetTrigger("Die");
        anim.SetBool("IsDead", true);
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
        if (respawnPosition.HasValue)
        {
            transform.position = respawnPosition.Value;
        }
        var deadPlayer = Instantiate(DeadPlayerPrefeb, transform.position, Quaternion.identity).GetComponent<DeadPlayer>();
        deadPlayer.Init(this, deadDirection);
    }

    public void BeHitted(RaycastHit2D hit, Vector3? respawnPosition = null, float? delay = null)
    {
        if (!IsDead && !IsInvincible)
        {
            Die(-hit.normal, respawnPosition, delay);
        }
    }
    public void BeHitted(Vector2 direction, bool force = false, Vector3? respawnPosition = null, float? delay = null)
    {
        if (!IsDead && (force || !IsInvincible))
        {
            Die(direction, respawnPosition, delay);
        }
    }
}