using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D Prb;
    private Collider2D Pcoll;

    public float speed, jumpforce;
    public Transform groundCheck;
    public LayerMask ground;

    public bool isGround, isJump;

    bool jumpPressed;
    int jumpCount;

    void Start()
    {
        Prb = GetComponent<Rigidbody2D>();
        Pcoll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        GroundMovement();

        Jump();
    }

    void GroundMovement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");

        /*if (horizontalMove == -1) 
        {
            transform.Rotate(0f, 180f, 0f); 
        }
        else
        {
            transform.Rotate(0f, 0f, 0f);
        }*/

        Prb.velocity = new Vector2(horizontalMove * speed, Prb.velocity.y);

        if(horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    void Jump()
    {
        if (isGround)
        {   
            jumpCount = 1;
            isJump = false;
        }
        if(jumpPressed && isGround)
        {
            isJump = true;
            Prb.velocity = new Vector2(Prb.velocity.x, jumpforce);
            jumpCount--;
            jumpPressed = false;
        }
    }
}
