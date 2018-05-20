using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string PlayerNumber = "1";
    public float MoveForce = 300f;
    public Vector2 MaxSpeed = new Vector2(5, 20);
    public float JumpForce = 1000f;
    public Transform GroundCheck;
    public Egg EggPrefab;
    [HideInInspector] public Egg Egg = null;

    public bool IsGrounded { get; private set; }
    public bool FacingRight { get; private set; }

    Rigidbody2D rb2d;
    string inputHorizontal;
    string inputFire;
    string inputJump;
    bool jump;

    private void Start()
    {

    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        inputHorizontal = "P" + PlayerNumber + " Horizontal";
        inputFire = "P" + PlayerNumber + " Fire";
        inputJump = "P" + PlayerNumber + " Jump";
    }

    private void Update()
    {
        IsGrounded = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (IsGrounded && Input.GetButton(inputJump))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        var h = Input.GetAxis(inputHorizontal);
        if (Mathf.Abs(h * rb2d.velocity.x) < MaxSpeed.x)
        {
            rb2d.AddForce(Vector2.right * h * MoveForce);
        }
        if (Mathf.Abs(rb2d.velocity.x) > MaxSpeed.x)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * MaxSpeed.x, rb2d.velocity.y);
        }
        if (Mathf.Abs(rb2d.velocity.y) > MaxSpeed.y)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Sign(rb2d.velocity.y) * MaxSpeed.y);
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
            if (rb2d.velocity.y == 0f)
            {
                rb2d.AddForce(new Vector2(0f, JumpForce));
            }
            jump = false;
        }
        if (Input.GetButton(inputFire))
        {
            if (Egg == null)
            {
                LayEgg();
            }
            else
            {
                Egg.Explode();
                Egg = null;
            }
        }
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 0));
    }

    private void LayEgg()
    {
        Egg = Instantiate(EggPrefab, transform);
    }
}