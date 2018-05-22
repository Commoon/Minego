using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatingPlatform : MonoBehaviour
{
    public Transform Platform;
    public Transform[] Stops;
    public float[] StayTimes;
    public float Speed = 5f;

    float lastStay;
    int now;
    bool positive;

    private void Start()
    {
        lastStay = Time.timeSinceLevelLoad;
        now = 0;
        positive = true;
        Platform.position = Stops[0].position;
    }

    private void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad - lastStay <= StayTimes[now])
        {
            return;
        }
        var next = positive ? 1 : -1;
        var distance = Stops[now + next].position - Platform.position;
        var toMove = Speed * Time.deltaTime;
        if (distance.magnitude <= toMove)
        {
            toMove = distance.magnitude;
            now += next;
            lastStay = Time.timeSinceLevelLoad;
            if (now == Stops.Length - 1 || now == 0)
            {
                positive = !positive;
            }
        }
        Platform.position = Platform.position + distance.normalized * toMove;
    }
}
