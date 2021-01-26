using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool dev;
    

    private Rigidbody2D rb;
    private Animator anim;
    private float dirX;
    private bool facingRight = true;
    private bool isGrounded = true;
    private bool isJumping = false;
    private bool moveRight, moveLeft;
    private Vector3 localScale;

    PlayerAttack playerAttack;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        moveLeft = false;
        moveRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);

        if (dev)
        {
            dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        }

        MoveCharacter();

        Debug.Log(moveLeft + " move left");
        Debug.Log(moveRight + " move right");


        if ( dev && Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        if(Mathf.Abs(dirX) > 0f && isGrounded)
        {
            
            anim.SetBool("isRunning", true);
        } else
        {
            anim.SetBool("isRunning", false);
        }
        
        if(isGrounded)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }

        // Character is Jumping
        if(!isGrounded && rb.velocity.y > 0.1f)
        {
            anim.SetBool("isJumping", true);
        }

        // Character is falling
        if(rb.velocity.y < -0.1f)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
        
    }

    private void FixedUpdate()
    {
        if (playerAttack.AttackMode == true)
        {
            
            rb.velocity = new Vector2(0f,rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(dirX, rb.velocity.y);
        }

        if (isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJumping = false;
        }
        
    }

    private void LateUpdate()
    {
        if(dirX > 0)
        {
            facingRight = true;
        } else if (dirX < 0)
        {
            facingRight = false;
        }

        if(((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }

    

    public void LeftMoveOnPressedDown()
    {
        moveLeft = true;
        //dirX = Input.GetAxisRaw("Horizontal") * -moveSpeed;
    }
    public void LeftMoveOnPressedUp()
    {
        moveLeft = false;
        //dirX = Input.GetAxisRaw("Horizontal") * -moveSpeed;
    }

    public void RightMoveOnPressedDown()
    {
        moveRight = true;
        //dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }


    public void RightMoveOnPressedUp()
    {
        moveRight = false;
        //dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private void MoveCharacter()
    {

        if(moveLeft)
        {
            dirX = -moveSpeed;
        } 
        else if (moveRight)
        {
            dirX = moveSpeed;
        }
        else
        {
            dirX = 0f;
        }
    }

    public void JumpButton()
    {
        if (rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
}
