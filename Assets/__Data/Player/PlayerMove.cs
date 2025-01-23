using __Data.Script;
using UnityEngine;

public class PlayerMove : ObjMovement
{
    [SerializeField] protected float jumpForce = 15f;
    [SerializeField] protected bool isGround;
    public bool IsGround {
        get => isGround;
        private set
        {
            isGround = value;
            animator.SetBool(AnimString.isGround, value);
        } 
        
    }
    [SerializeField] protected bool doubleJump;
    [SerializeField] protected ContactFilter2D contactFilter;
    [SerializeField] protected CapsuleCollider2D touchCol;
    private readonly RaycastHit2D[] _groundHit = new RaycastHit2D[5];

    private bool CanMove => animator.GetBool(AnimString.canMove);
    public bool IsAlive => animator.GetBool(AnimString.isAlive);

    protected override void Awake()
    {
        base.Awake();
        touchCol = GetComponentInParent<CapsuleCollider2D>();
    }

    protected void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
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
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    
    private void HandleJump()
    {
        IsGround = touchCol.Cast(Vector2.down, contactFilter, _groundHit, 0.2f) > 0;
        
        if (isGround && Input.GetButtonDown("Jump"))
            doubleJump = false;
        
        if (Input.GetButtonDown("Jump") && CanMove)
        {
            if (isGround || doubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                doubleJump = !doubleJump;
            }
        }
    }

    private void UpdateAnimation()
    {
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        
        animator.SetBool(AnimString.isMove, isMoving);
        animator.SetFloat(AnimString.yVelocity, rb.linearVelocity.y);
        animator.SetBool(AnimString.isAlive, PlayerController._isAlive);
    }
}
