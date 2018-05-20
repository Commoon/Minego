using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool IsDead = false;
    public Player Villain;
    public StageManager StageManager;

    [HideInInspector] public int Score = 0;
    [HideInInspector] public float LastRespawned = -10;

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
}