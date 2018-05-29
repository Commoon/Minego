using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minego;

public class Button : MonoBehaviour
{
    public GameObject[] Targets;
    public float PressSpeed = 1f;
    public float DeactivateDelay = 0f;
    public Transform Sprite;

    IButtonControlled[] targets;
    bool isPressing = false;
    bool pressed = false;
    float originalHeight;
    HashSet<GameObject> gameObjects;
    float startTime;
    Vector3 origin;

    private void Awake()
    {
        targets = new IButtonControlled[Targets.Length];
        for (var i = 0; i < Targets.Length; i++)
        {
            targets[i] = Targets[i].GetComponent<IButtonControlled>();
        }
        gameObjects = new HashSet<GameObject>();
    }

    void Start()
    {
        originalHeight = Sprite.localScale.y;
        origin = Sprite.position;
    }

    private void Update()
    {
        var s = Vector3.Scale(Sprite.localScale, new Vector3(4.25f, 1f, 1f));
        var r = Quaternion.Inverse(Sprite.rotation);
        gameObjects.RemoveWhere(go =>
        {
            if (go == null)
            {
                return true;
            }
            var p = r * (go.transform.position - Sprite.position);
            return !(p.x >= -s.x / 2 && p.x <= s.x / 2
                && p.y > 0 && p.y <= s.y / 2 + Player.CenterFromFoot + 0.2);
        });
        if (gameObjects.Count == 0 && isPressing)
        {
            startTime = Time.time;
        }
        isPressing = gameObjects.Count > 0;
    }

    public void Activate()
    {
        foreach (var target in targets)
        {
            target.Activate();
        }
    }

    public void Deactivate()
    {
        foreach (var target in targets)
        {
            target.Deactivate();
        }
    }

    void UpdateTransform(Vector3 newScale)
    {
        var t = Vector3.down * (originalHeight - newScale.y) / 2 * 0.16f;
        Sprite.position = Sprite.rotation * t + origin;
        Sprite.localScale = newScale;
    }

    void FixedUpdate()
    {
        if (isPressing && Sprite.localScale.y > 0.05f)
        {
            if (!pressed)
            {
                pressed = true;
                Activate();
            }
            var newScale = new Vector3(Sprite.localScale.x, Sprite.localScale.y - PressSpeed * Time.deltaTime, Sprite.localScale.z);
            if (newScale.y <= 0.05f || DeactivateDelay > 0)
            {
                newScale.y = 0.05f;
            }
            UpdateTransform(newScale);
        }
        else if (!isPressing && Sprite.localScale.y < originalHeight)
        {
            if (Time.time - startTime <= DeactivateDelay)
            {
                return;
            }
            if (pressed)
            {
                Deactivate();
                pressed = false;
            }
            var newScale = new Vector3(Sprite.localScale.x, Sprite.localScale.y + PressSpeed * Time.deltaTime, Sprite.localScale.z);
            if (newScale.y >= originalHeight)
            {
                newScale.y = originalHeight;
            }
            UpdateTransform(newScale);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") &&
            !gameObjects.Contains(collision.gameObject))
        {
            gameObjects.Add(collision.gameObject);
        }
    }
}
