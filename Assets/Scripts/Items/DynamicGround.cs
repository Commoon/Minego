using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minego;

public class DynamicGround : MonoBehaviour, IButtonControlled
{
    public Transform ForcePoint;
    public float Force;
    public float MinRotation = -180f;
    public float MaxRotation = 180f;
    Rigidbody2D rb2d;
    float originalRotation;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalRotation = rb2d.rotation;
    }

    private void Start()
    {
        rb2d.centerOfMass = Vector2.zero;
    }

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

    private void FixedUpdate()
    {
        if (Active && rb2d.rotation != originalRotation)
        {
            var position = (Vector2)ForcePoint.position;
            var t = position - rb2d.position;
            var direction = new Vector2(-t.y, t.x);
            rb2d.AddForceAtPosition(direction.normalized * Force * Mathf.Sign(originalRotation - rb2d.rotation), position);
        }
        var rotation = rb2d.rotation + rb2d.angularVelocity * Time.deltaTime;
        if (((int)rotation) % 90 == 0)
        {
            rb2d.rotation = rotation;
            rb2d.angularVelocity = 0f;
        }
        if (rb2d.rotation < MinRotation)
        {
            rb2d.rotation = MinRotation;
            rb2d.angularVelocity = 0f;
        }
        if (rb2d.rotation > MaxRotation)
        {
            rb2d.rotation = MaxRotation;
            rb2d.angularVelocity = 0f;
        }
    }
}
