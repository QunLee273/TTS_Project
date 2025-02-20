using System.Collections;
using UnityEngine;

public class BossLazer : AbilityLazer
{
    [Header("Boss Lazer")]
    [SerializeField] protected Transform bossTransform;
    [SerializeField] protected Transform spark;
    
    [SerializeField] protected LineRenderer lineRenderer;
    [SerializeField] protected ParticleSystem remnants;
    [SerializeField] protected LayerMask hitLayers;
    [SerializeField] protected Transform firePoint;
    public Transform FirePoint => firePoint;
    
    [SerializeField] protected float laserDistance = 50f;
    [SerializeField] protected float laserSpeed = 50f;

    private Vector2 _targetEndPoint;

    public Vector2 TargetEndPoint
    {
        get => _targetEndPoint;
        set => _targetEndPoint = value;
    }
    private Vector2 _currentEndPoint;

    public Vector2 CurrentEndPoint
    {
        get => _currentEndPoint;
        set => _currentEndPoint = value;
    }

    protected override void Start()
    {
        base.Start();
        _currentEndPoint = firePoint.position;
    }

    protected override void Update()
    {
        Vector2 direction = bossTransform.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        _targetEndPoint = (Vector2)firePoint.position + direction * laserDistance;
        spark.localScale = direction == Vector2.right ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

        CheckCollider(direction);
        MoveLaser();
        UpdateLineRenderer();
    }

    private void CheckCollider(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, laserDistance, hitLayers);
        if (hit.collider)
            _targetEndPoint = hit.point;
        else
            _targetEndPoint = (Vector2)firePoint.position + direction * laserDistance;

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
            }
        }
    }

    private void MoveLaser()
    {
        _currentEndPoint = Vector2.MoveTowards(_currentEndPoint, _targetEndPoint, laserSpeed * Time.deltaTime);

        if (Vector2.Distance(_currentEndPoint, _targetEndPoint) < 0.1f)
        {
            remnants.transform.position = _currentEndPoint;
            remnants.gameObject.SetActive(true);
        }
        else
        {
            remnants.gameObject.SetActive(false);
        }
    }

    private void UpdateLineRenderer()
    {
        var startPointLocal = lineRenderer.transform.InverseTransformPoint(firePoint.position);
        var endPointLocal = lineRenderer.transform.InverseTransformPoint(_currentEndPoint);

        lineRenderer.SetPosition(0, startPointLocal);
        lineRenderer.SetPosition(1, endPointLocal);
    }
}
