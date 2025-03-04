using __Data;
using __Data.Script;
using UnityEngine;

public class AutoMoveZone : GameBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected Transform point;

    private bool _isMove;
    private Vector3 _target;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayer();
        LoadPoint();
        LoadAnim();
    }

    protected override void Start()
    {
        base.Start();
        _target = point.position;
        _isMove = false;
    }

    private void LoadPlayer()
    {
        if (player != null) return;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = player.GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }
    
    private void LoadPoint()
    {
        if (point != null) return;
        point = GameObject.Find("PointMove").transform;
        Debug.LogWarning(transform.name + ": LoadPoint", gameObject);
    }

    private void LoadAnim()
    {
        if (animator != null) return;
        animator = player.GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnim", gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(transform.name + ": OnTriggerEnter2D", gameObject);
            _isMove = true;
        }
    }

    private void Update()
    {
        if (!_isMove) return;
        
        
        
        if (Vector3.Distance(player.position, _target) < 0.1f)
        {
            
        }
    }
}
