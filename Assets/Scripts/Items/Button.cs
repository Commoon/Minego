using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minego;

public class Button : MonoBehaviour
{
    public GameObject Target;
    public float PressSpeed = 1f;


    IButtonControlled target;
    bool isPressing = false;
    bool pressed = false;
    float bottomY;
    float originalHeight;
    HashSet<GameObject> gameObjects;

    private void Awake()
    {
        target = Target.GetComponent<IButtonControlled>();
        gameObjects = new HashSet<GameObject>();
    }

    void Start()
    {
        originalHeight = transform.localScale.y;
        bottomY = transform.position.y - originalHeight / 2;
    }

    private void Update()
    {
        var q = transform.position;
        var s = transform.localScale;
        gameObjects.RemoveWhere(go =>
        {
            if (go == null)
            {
                return true;
            }
            var p = go.transform.position;
            return !(p.x >= q.x - s.x / 2 && p.x <= q.x + s.x / 2
                && p.y > q.y && p.y <= q.y + s.y / 2 + Player.CenterFromGround + 0.1);
        });
        isPressing = gameObjects.Count > 0;
    }

    void FixedUpdate()
    {
        if (isPressing && transform.localScale.y > 0.05f)
        {
            if (!pressed)
            {
                pressed = true;
                target.Activate();
            }
            var newScale = new Vector3(transform.localScale.x, transform.localScale.y - PressSpeed * Time.deltaTime);
            if (newScale.y <= 0.05f)
            {
                newScale.y = 0.05f;
            }
            transform.position = new Vector3(transform.position.x, bottomY + newScale.y / 2);
            transform.localScale = newScale;
        }
        else if (pressed && !isPressing)
        {
            var newScale = new Vector3(transform.localScale.x, transform.localScale.y + PressSpeed * Time.deltaTime);
            if (newScale.y >= originalHeight)
            {
                newScale.y = originalHeight;
                    target.Deactivate();
                    pressed = false;
            }
            transform.position = new Vector3(transform.position.x, bottomY + newScale.y / 2);
            transform.localScale = newScale;
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
