using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minego;

public class Blower : MonoBehaviour, IButtonControlled
{
    public float BlowForce = 24f;
    bool _active = false;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Active && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            rb2d.AddForce(new Vector2(0, BlowForce));
            if (collision.gameObject.CompareTag("Player"))
            {
                var pc = collision.gameObject.GetComponent<PlayerController>();
                pc.IsGrounded = false;
            }
        }
    }
}
