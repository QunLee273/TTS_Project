using System.Collections;
using UnityEngine;

public class BossLazer : AbilityAbstract
{
    public Transform bossTransform;
    public LineRenderer lineRenderer;
    public ParticleSystem remnants;
    public LayerMask hitLayers;
    public Transform firePoint;
    public float laserDistance = 10f;
    public Vector2 endPoint;

    protected void Update()
    {
        Vector2 direction = bossTransform.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        endPoint = (Vector2)firePoint.position + direction * laserDistance;
        
        CheckCollider(direction);
        UpdateLineRenderer();
    }

    private void CheckCollider(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, laserDistance, hitLayers);
        if (hit.collider != null)
        {
            endPoint = hit.point;
            remnants.transform.position = endPoint;
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
        var endPointLocal = lineRenderer.transform.InverseTransformPoint(endPoint);
        
        lineRenderer.SetPosition(0, startPointLocal);
        lineRenderer.SetPosition(1, endPointLocal);
    }
}