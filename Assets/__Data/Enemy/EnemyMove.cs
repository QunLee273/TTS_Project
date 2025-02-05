using UnityEngine;

public class EnemyMove : ObjMovement
{
    [Header("EnemyMove")]
    [SerializeField] protected EnemyAttack enemyAttack;
    [SerializeField] protected bool isGround;
    [SerializeField] protected bool isWall;
    
    [SerializeField] protected float moveDetection = 5f;
    [SerializeField] protected float detectionDistance = 5f;
    [SerializeField] protected float walkRate = 0.05f;
    
    public enum MoveableDirection
    {
        Right,
        Left
    }

    private MoveableDirection _moveDirection;
    private Vector2 _directionVector2;
    
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask detectionLayer;

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

    protected override void LoadComponents()
    {
        base.LoadComponents();
        int randomValue = Random.Range(0, 2);
        _moveDirection = (MoveableDirection)randomValue;
        _directionVector2 = (_moveDirection == MoveableDirection.Right) ? Vector2.right : Vector2.left;

        LoadEnemyAttack();
    }

    protected void FixedUpdate()
    {
        CheckMove();
        EnemysMove();
    }

    private void LoadEnemyAttack()
    {
        if (enemyAttack != null) return;
        GameObject obj = GameObject.Find("AbilityAttack");
        enemyAttack = obj.GetComponent<EnemyAttack>();
        Debug.LogWarning(transform.name + ": LoadEnemyAttack", gameObject);
    }

    private void CheckMove()
    {
        Vector2 downDiagonal = Quaternion.Euler(0, 0, _moveDirection == MoveableDirection.Right ? 45 : -45) * Vector2.down;
        
        isGround = Physics2D.Raycast(transform.position, downDiagonal, 1f, groundLayer);
        isWall = Physics2D.Raycast(transform.position, _directionVector2, 0.5f, groundLayer);
        
        /*Debug.DrawRay(transform.position, _directionVector2 * 0.5f, Color.red);
        Debug.DrawRay(transform.position, downDiagonal * 1f, Color.green);*/
        
        if (!isGround || isWall)
            MoveDirection = (_moveDirection == MoveableDirection.Right) ? MoveableDirection.Left : MoveableDirection.Right;
    }

    protected virtual void EnemysMove()
    {
        if (_directionVector2 == Vector2.right) transform.parent.localScale = new Vector3(1, 1, 1);
        else if (_directionVector2 == Vector2.left) transform.parent.localScale = new Vector3(-1, 1, 1);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _directionVector2, detectionDistance, detectionLayer);
        Debug.DrawRay(transform.position, _directionVector2 * 5f, Color.blue);
        
        float speed = (hit.collider) ? moveDetection : moveSpeed;
        
        if (enemyAttack.CanMove)
            rb.linearVelocity = new Vector2(speed * _directionVector2.x, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkRate), rb.linearVelocity.y);
    }
}
