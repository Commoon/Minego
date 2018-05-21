using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool IsDead = false;
    public Player Villain;
    public StageManager StageManager;
    public float InvincibleTime = 2.0f;
    
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

    PlayerController pc;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
    }

    void Update() {
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

    public void Die()
    {
        StageManager.GetPoint(Villain, 1);
        IsDead = true;
    }
}