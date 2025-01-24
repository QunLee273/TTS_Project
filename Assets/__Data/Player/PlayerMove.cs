using __Data.Script;
using UnityEngine;

public class PlayerMove : ObjMovement
{
    [SerializeField] protected float jumpForce = 15f;
    [SerializeField] protected bool doubleJump;
    
    [SerializeField] protected bool isGround;
    public bool IsGround
    {
        get => isGround;
        private set
        {
            isGround = value;
            animator.SetBool(AnimString.isGround, value);
        }
    }
    
    private bool CanMove => animator.GetBool(AnimString.canMove);
    private bool IsAlive => animator.GetBool(AnimString.isAlive);

    protected void FixedUpdate()
    {
        if (IsAlive)
        {
            HandleMovement();
            HandleJump();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
        
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        if (CanMove)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            if (moveInput > 0)
                transform.parent.localScale = new Vector3(1, 1, 1);
            else if (moveInput < 0)
                transform.parent.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandleJump()
    {
        IsGround = Physics2D.Raycast(transform.position, Vector2.down,
            1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.green);
        
        if (IsGround) doubleJump = true;
        
        if (Input.GetButtonDown("Jump") && CanMove)
        {
            if (IsGround)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            if (doubleJump && !IsGround)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                doubleJump = false;
            }
        }
    }
    

    private void UpdateAnimation()
    {
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        animator.SetBool(AnimString.isMove, isMoving);
        animator.SetFloat(AnimString.yVelocity, rb.linearVelocity.y);
    }
}
