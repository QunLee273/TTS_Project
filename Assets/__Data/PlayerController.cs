using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 15f;
    
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected bool isGround;
    [SerializeField] protected bool doubleJump;
    
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D rb;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        // Di chyuển
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        
        // Lật ảnh
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump()
    {
        if (isGround && Input.GetButtonDown("Jump"))
            doubleJump = false;
        
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround || doubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                
                doubleJump = !doubleJump;
            }
        }
        
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = rb.linearVelocity.y > 0.1f && !isGround;
        bool isFalling = rb.linearVelocity.y < -0.1f && !isGround;
        
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFalling", isFalling);
    }
}
