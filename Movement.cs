using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D chel;
    public float spd;
    public float force;
    private float moveInput;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Animator anim;

    private int extraJumps;
    public int ejValue;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = ejValue;
        chel = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxis("Horizontal");
        chel.velocity = new Vector2(moveInput * spd, chel.velocity.y);
        
        if(moveInput > 0)
        {
            anim.SetInteger("state", 1);
        }
        else if (isGrounded)
        {
            anim.SetInteger("state", 0);
        }


        if(facingRight == false && moveInput > 0)
        {
            Flip();
        } else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if(isGrounded == true)
        {
            extraJumps = ejValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            chel.velocity = Vector2.up * force;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            chel.velocity = Vector2.up * force;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    
}