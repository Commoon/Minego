using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string PlayerNumber = "1";
    public float MoveForce = 300f;
    public Vector2 MaxSpeed = new Vector2(5, 20);
    public float JumpForce = 1000f;
    public Transform GroundCheck1;
    public Transform GroundCheck2;
    public Egg EggPrefab;
    public bool FacingRight;
    [HideInInspector] public Egg Egg = null;

    public bool IsGrounded { get; set; }

    Player player;
    Rigidbody2D rb2d;
    Animator anim;
    string inputHorizontal;
    string inputVertical;
    string inputFire;
    string inputJump;
    bool jump = false;
    bool layEgg = false;
    bool isLayingEgg = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        inputHorizontal = "P" + PlayerNumber + " Horizontal";
        inputVertical = "P" + PlayerNumber + " Vertical";
        inputFire = "P" + PlayerNumber + " Fire";
        inputJump = "P" + PlayerNumber + " Jump";
    }

    private void Update()
    {
        if (player.IsDead)
        {
            return;
        }
        if (IsGrounded && Input.GetButton(inputJump))
        {
            jump = true;
        }
        if (Input.GetButtonDown(inputFire))
        {
            if (Egg == null && !isLayingEgg)
            {
                var h = Input.GetAxis(inputHorizontal);
                if (h == 0 || Input.GetAxis(inputVertical) < 0)
                {
                    LayEgg();
                }
                else
                {
                    ThrowEgg(h > 0);
                }
            }
            else
            {
                Egg.Explode();
            }
        }
    }

    private void FixedUpdate()
    {
        if (player.IsDead)
        {
            return;
        }
        if (layEgg)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            isLayingEgg = true;
            layEgg = false;
        }
        if (isLayingEgg)
        {
            return;
        }
        var h = Input.GetAxis(inputHorizontal);
        if (Mathf.Abs(h * rb2d.velocity.x) < MaxSpeed.x)
        {
            rb2d.AddForce(Vector2.right * h * MoveForce);
        }
        if (h > 0 && !FacingRight)
        {
            Flip();
        }
        else if (h < 0 && FacingRight)
        {
            Flip();
        }
        if (jump)
        {
            rb2d.AddForce(new Vector2(0f, JumpForce));
            IsGrounded = false;
            jump = false;
        }
        if (Mathf.Abs(rb2d.velocity.x) > MaxSpeed.x)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * MaxSpeed.x, rb2d.velocity.y);
        }
        if (Mathf.Abs(rb2d.velocity.y) > MaxSpeed.y)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Sign(rb2d.velocity.y) * MaxSpeed.y);
        }
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 0));
    }

    private void LayEgg()
    {
        layEgg = true;
        anim.SetTrigger("LayingEgg");
    }

    public void CompleteLayingEgg()
    {
        Egg = Instantiate(EggPrefab, transform.position, Quaternion.identity);
        isLayingEgg = false;
    }

    private void ThrowEgg(bool toRight)
    {
        Egg = Instantiate(EggPrefab, transform.position, Quaternion.identity);
        Egg.BeThrown(rb2d.velocity, toRight);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            IsGrounded = Physics2D.Linecast(GroundCheck1.position, GroundCheck2.position, (
                (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Obstacle"))));
        }
    }
}