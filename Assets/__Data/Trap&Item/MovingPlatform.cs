using __Data;
using UnityEngine;

public class MovingPlatform : GameBehaviour
{
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;
    [SerializeField] protected float speed = 2f;
    private Vector3 _target;
    private Transform _playerParent;

    protected override void Start()
    {
        base.Start();
        _target = pointA.position;
    }

    protected void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _target) < 0.1f)
            _target = _target == pointA.position ? pointB.position : pointA.position;
    }
    
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerParent = other.gameObject.transform.parent;
            other.transform.SetParent(transform);
        }
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.transform.SetParent(_playerParent);
    }
}
