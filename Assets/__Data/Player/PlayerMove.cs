using __Data.Script;
using UnityEngine;

public class PlayerMove : ObjMovement
{
    [Header("PlayerMove")]
    [SerializeField] protected float jumpForce = 15f;
    [SerializeField] protected bool isJumping;
    [SerializeField] protected bool doubleJump;
    private bool _isMoveLeft, _isMoveRight;

    [SerializeField] protected bool isGround;
    protected RaycastHit2D[] hits = new RaycastHit2D[7];
    [SerializeField] protected ContactFilter2D contactFilter;
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
    
    private bool _wasGrounded;

    protected void FixedUpdate()
    {
        if (IsAlive)
        {
            if (_isMoveLeft || Input.GetAxis("Horizontal") < 0) HandleMovement(-1);
            else if (_isMoveRight|| Input.GetAxis("Horizontal") > 0) HandleMovement(1);
            else HandleMovement(0);
            
            HandleJump();
            if (!_wasGrounded && IsGround)
                AudioManager.Instance.PlaySfx("Fall");

            _wasGrounded = IsGround;
        }
        else
            rb.linearVelocity = Vector2.zero;
        
        UpdateAnimation();
    }

    private void HandleMovement(float direction)
    {
        if (CanMove)
        {
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

            if (direction > 0)
                transform.parent.localScale = new Vector3(1, 1, 1);
            else if (direction < 0)
                transform.parent.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    private void HandleJump()
    {
        IsGround = col.Cast(Vector2.down, contactFilter, hits, 0.1f) > 0;

        Debug.DrawRay(col.bounds.center, Vector2.down * 0.5f, IsGround ? Color.green : Color.red);

        if (IsGround) doubleJump = true;

        bool jumpPress = isJumping;

        if (jumpPress && CanMove)
        {
            if (IsGround)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                AudioManager.Instance.PlaySfx("Jump");
            }
            else if (doubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                AudioManager.Instance.PlaySfx("Jump");
                doubleJump = false;
            }

            isJumping = false;
        }
    }
    
    public void OnPointerDownLeft()  { _isMoveLeft = true; }
    public void OnPointerUpLeft()    { _isMoveLeft = false; }
    public void OnPointerDownRight() { _isMoveRight = true; }
    public void OnPointerUpRight()   { _isMoveRight = false; }
    public void OnClickJump() { isJumping = true; }

    private void UpdateAnimation()
    {
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        if (isMoving && IsGround)
            AudioManager.Instance.PlayLoopSfx("Running");
        else
            AudioManager.Instance.StopLoopSfx("Running");
        
        animator.SetBool(AnimString.isMove, isMoving);
        animator.SetFloat(AnimString.yVelocity, rb.linearVelocity.y);
    }
}
