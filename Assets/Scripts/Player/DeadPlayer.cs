using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{
    public float DeadSpeed = 8f;
    public Vector2 RightTop = new Vector2(11.22f, 7);
    public bool FacingRight = true;
    public bool FacingUp = true;
    public Player Player;
    public float DieDistance = 4f;
    public float DieSpeed = 15f;
    public float DieAnglularSpeed = 3f;
    public int DieFrames = 20;

    Rigidbody2D rb2d;
    Animator anim;
    string inputHorizontal;
    string inputVertical;
    bool started = false;
    int frames = 0;
    Vector2 direction;
    Vector2 origin;
    Vector2 targetCorner;
    bool disappearing = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    public void Init(Player player, Vector2 direction)
    {
        transform.position += Vector3.back * 5;
        var playerNumber = player.IsPlayer1 ? "1" : "2";
        Player = player;
        inputHorizontal = "P" + playerNumber + " Horizontal";
        inputVertical = "P" + playerNumber + " Vertical";
        this.direction = direction;
        frames = 0;
        origin = rb2d.position;
        started = false;
        disappearing = false;
        targetCorner.x = origin.x < 0 ? 14 : -14;
        targetCorner.y = origin.y < 0 ? 7 : -7;
    }

    bool CheckCorner()
    {
        return rb2d.position.x * targetCorner.x >= 0 && rb2d.position.y * targetCorner.y >= 0;
    }

    void FixedUpdate()
    {
        if (disappearing)
        {
            return;
        }
        if (!started)
        {
            var distance = rb2d.position - origin;
            direction = Quaternion.Euler(0, 0, DieAnglularSpeed) * direction;
            rb2d.position = origin + (distance.magnitude + Time.deltaTime * DeadSpeed) * direction;
            frames += 1;
            if (frames > DieFrames && CheckCorner())
            {
                started = true;
            }
        }
        else
        {
            var direction = new Vector2(Input.GetAxis(inputHorizontal), Input.GetAxis(inputVertical));
            rb2d.velocity = direction * DeadSpeed;
            if (rb2d.velocity.x > 0 && !FacingRight || rb2d.velocity.x < 0 && FacingRight)
            {
                FlipHorizontal();
            }
            if (rb2d.velocity.y > 0 && !FacingUp || rb2d.velocity.y < 0 && FacingUp)
            {
                FlipVertical();
            }
            rb2d.position = new Vector2(
                Mathf.Clamp(rb2d.position.x, -RightTop.x, RightTop.x),
                Mathf.Clamp(rb2d.position.y, -RightTop.y, RightTop.y)
            );
        }
    }

    void FlipHorizontal()
    {
        FacingRight = !FacingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 0));
    }

    void FlipVertical()
    {
        FacingUp = !FacingUp;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1, -1, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!started || disappearing)
        {
            return;
        }
        if (collision.gameObject == Player.gameObject)
        {
            Player.StartRespawn();
            disappearing = true;
            rb2d.velocity = Vector2.zero;
            anim.SetTrigger("Disappear");
        }
    }
}
