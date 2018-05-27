using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minego;

public class Trap : MonoBehaviour, IButtonControlled
{
    public Transform RespawnPosition = null;
    public Transform TrapSprite;
    public float TargetHeight = 0.25f;
    public float ShowSpeed = 0.5f;
    public float DieDelay = 0.5f;

    bool _active;
    public bool Active
    {
        get { return _active; }
        private set { _active = value; }
    }

    public void Activate()
    {
        Active = true;
    }

    public void Deactivate()
    {
        Active = false;
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Active && TrapSprite.localPosition.y < TargetHeight)
        {
            var y = Mathf.Min(TrapSprite.localPosition.y + ShowSpeed * Time.deltaTime, TargetHeight);
            TrapSprite.localPosition = new Vector3(TrapSprite.localPosition.x, y, TrapSprite.localPosition.z);
        }
        else if (!Active && TrapSprite.localPosition.y > 0)
        {
            var y = Mathf.Max(TrapSprite.localPosition.y - ShowSpeed * Time.deltaTime, 0);
            TrapSprite.localPosition = new Vector3(TrapSprite.localPosition.x, y, TrapSprite.localPosition.z);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Active)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            player.BeHitted(transform.up, false, RespawnPosition.position, DieDelay);
        }
    }
}
