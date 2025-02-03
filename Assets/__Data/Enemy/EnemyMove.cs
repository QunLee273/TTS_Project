using UnityEngine;

public class EnemyMove : ObjMovement
{
    [Header("EnemyMove")]
    [SerializeField] protected bool isGround;
    [SerializeField] protected bool isWall;
    
    public enum MoveableDirection
    {
        Right,
        Left
    }

    private MoveableDirection _moveDirection;
    private Vector2 _directionVector2;
    
    [SerializeField] protected LayerMask groundLayer;

    public MoveableDirection MoveDirection
    {
        get => _moveDirection;
        set
        {
            if (_moveDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1,
                                                              gameObject.transform.localScale.y);

                _directionVector2 = (value == MoveableDirection.Right) ? Vector2.right : Vector2.left;
            }
            
            _moveDirection = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        int randomValue = Random.Range(0, 2);
        _moveDirection = (MoveableDirection)randomValue;
        
        _directionVector2 = (_moveDirection == MoveableDirection.Right) ? Vector2.right : Vector2.left;
    }

    protected void FixedUpdate()
    {
        CheckForObstacles();
        EnemysMove();
    }

    private void CheckForObstacles()
    {
        Vector2 downDiagonal = Quaternion.Euler(0, 0, _moveDirection == MoveableDirection.Right ? 45 : -45) * Vector2.down;
        
        isGround = Physics2D.Raycast(transform.position, downDiagonal, 1f, groundLayer);
        isWall = Physics2D.Raycast(transform.position, _directionVector2, 0.5f, groundLayer);
        
        /*Debug.DrawRay(transform.position, _directionVector2 * 0.5f, Color.red);
        Debug.DrawRay(transform.position, downDiagonal * 1f, Color.green);*/
        
        if (!isGround || isWall)
        {
            MoveDirection = (_moveDirection == MoveableDirection.Right) ? MoveableDirection.Left : MoveableDirection.Right;
        }
    }

    private void EnemysMove()
    {
        if (_directionVector2 == Vector2.right)
            transform.parent.localScale = new Vector3(1, 1, 1);
        else if (_directionVector2 == Vector2.left)
            transform.parent.localScale = new Vector3(-1, 1, 1);
        
        rb.linearVelocity = new Vector2(moveSpeed * _directionVector2.x, rb.linearVelocity.y);
    }
}
