using __Data.Script;
using UnityEngine;

public class PlayerMove : ObjMovement
{
    [SerializeField] protected float jumpForce = 15f;
    
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected bool isGround;
    [SerializeField] protected bool doubleJump;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        HandleJump();
        UpdateAnimation();
    }

    protected override void Move()
    {
        // Di chyuển
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        
        // Lật ảnh
        if (moveInput > 0) transform.parent.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.parent.localScale = new Vector3(-1, 1, 1);
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
        
        animator.SetBool(AnimString.isRun, isRunning);
        animator.SetBool(AnimString.isJump, isJumping);
        animator.SetBool(AnimString.isFall, isFalling);
    }
}
