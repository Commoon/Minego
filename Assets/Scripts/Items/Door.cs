using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minego;

public class Door : MonoBehaviour, IButtonControlled
{
    public float TargetAngle = 90f;
    public Transform RotateCenter;
    public bool Clockwise = true;
    public float RotateSpeed = 80f;

    float angle = 0f;

    bool _active = false;
    public bool Active
    {
        get
        {
            return _active;
        }
        private set { _active = value; }
    }

    public void Deactivate()
    {
        Active = false;
    }

    public void Activate()
    {
        Active = true;
    }

    void Rotate()
    {
        var oldAngle = angle;
        var toRotate = RotateSpeed * Time.deltaTime * (Active ^ Clockwise ? 1 : -1);
        angle = Mathf.Clamp(angle + (Active ? 1 : -1) * Mathf.Abs(toRotate), 0, TargetAngle);
        transform.RotateAround(
            RotateCenter.transform.position, Vector3.forward,
            Mathf.Sign(toRotate) * Mathf.Abs(angle - oldAngle)
        );
    }

    private void FixedUpdate()
    {
        if (Active && angle < TargetAngle || !Active && angle > 0)
        {
            Rotate();
        }
    }

}
