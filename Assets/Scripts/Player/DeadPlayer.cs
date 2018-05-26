using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{
    public float DeadSpeed = 8f;
    public Vector2 RightTop = new Vector2(14.22f, 8);
    public bool FacingRight = false;
    public Player Player;
    public float DieDistance = 4f;
    public float DieSpeed = 15f;
    public int DieFrames = 20;

    Rigidbody2D rb2d;
    string inputHorizontal;
    string inputVertical;
    int started;
    Vector2 direction;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Init(Player player, Vector2 direction)
    {
        transform.position += Vector3.back * 5;
        var playerNumber = player.IsPlayer1 ? "1" : "2";
        Player = player;
        inputHorizontal = "P" + playerNumber + " Horizontal";
        inputVertical = "P" + playerNumber + " Vertical";
        this.direction = direction;
        started = 0;
    }

    void FixedUpdate()
    {
        if (started < DieFrames)
        {
            rb2d.velocity = direction * DieSpeed;
            started += 1;
        }
        else
        {
            var direction = new Vector2(Input.GetAxis(inputHorizontal), Input.GetAxis(inputVertical));
            rb2d.velocity = direction * DeadSpeed;
            if (rb2d.velocity.x > 0 && !FacingRight || rb2d.velocity.x < 0 && FacingRight)
            {
                Flip();
            }
        }
        rb2d.position = new Vector2(
            Mathf.Clamp(rb2d.position.x, -RightTop.x, RightTop.x),
            Mathf.Clamp(rb2d.position.y, -RightTop.y, RightTop.y)
        );
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (started < DieFrames)
        {
            return;
        }
        if (collision.gameObject == Player.gameObject)
        {
            Player.Respwan();
            Destroy(gameObject);
        }
    }
}
