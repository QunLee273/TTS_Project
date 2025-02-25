using System;
using __Data;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class InvisibleWall : GameBehaviour
{
    [SerializeField] protected PolygonCollider2D wallCollider;
    [SerializeField] protected Sprite wallState1;
    [SerializeField] protected Sprite wallState2;
    [SerializeField] protected Sprite wallState3;
    [SerializeField] protected GameObject blackSpace;
    [SerializeField] protected PolygonCollider2D polygonCollider2D;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    private int _currentState = 1;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWallCollider();
        LoadBlackSpace();
        LoadRendererAndCollider();
    }

    private void LoadWallCollider()
    {
        if (wallCollider != null) return;
        wallCollider = transform.parent.GetComponent<PolygonCollider2D>();
        Debug.LogWarning(transform.name + ": LoadWallCollider", gameObject);
    }
    
    private void LoadBlackSpace()
    {
        if (blackSpace != null) return;
        blackSpace = transform.Find("BlackSpace").gameObject;
        Debug.LogWarning(transform.name + ": LoadBlackSpace", gameObject);
    }

    private void LoadRendererAndCollider()
    {
        if (polygonCollider2D != null && spriteRenderer != null) return;
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadRendererAndCollider", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        
        spriteRenderer.sprite = wallState1;
        Vector2[] points = polygonCollider2D.points;
        points[0] = new Vector2(-0.35f, 1.55f);
        points[1] = new Vector2(-0.45f, 0f);
        points[2] = new Vector2(-0.35f, -1.55f);
        points[3] = new Vector2(0.9f, -1.55f);
        points[4] = new Vector2(0.9f, 1.55f);
        polygonCollider2D.points = points;
        
        UpdateWallColliderPoints(points);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Bullet")) return;
        BulletCtrl bulletCtrl = other.transform.parent.GetComponent<BulletCtrl>();
        Transform shooter = bulletCtrl.Shooter;

        if (shooter.name == "Player")
        {
            if (_currentState == 1)
            {
                _currentState = 2;
                spriteRenderer.sprite = wallState2;
                Vector2[] points = polygonCollider2D.points;
                points[0] = new Vector2(-0.34f, 1.55f);
                points[1] = new Vector2(0f, -0.15f);
                points[2] = new Vector2(-0.33f, -1.55f);
                points[3] = new Vector2(0.9f, -1.55f);
                points[4] = new Vector2(0.9f, 1.55f);
                polygonCollider2D.points = points;
                UpdateWallColliderPoints(points);
            }
            else if (_currentState == 2)
            {
                _currentState = 3;
                spriteRenderer.sprite = wallState3;
                Vector2[] points = polygonCollider2D.points;
                points[0] = new Vector2(-0.35f, 1.55f);
                points[1] = new Vector2(0.25f, -0.17f);
                points[2] = new Vector2(-0.33f, -1.55f);
                points[3] = new Vector2(0.9f, -1.55f);
                points[4] = new Vector2(0.9f, 1.55f);
                polygonCollider2D.points = points;
                UpdateWallColliderPoints(points);
            }
            else
            {
                spriteRenderer.enabled = false;
                polygonCollider2D.enabled = false;
                blackSpace.SetActive(false);
                wallCollider.enabled = false;
            }
        }
    }
    
    private void UpdateWallColliderPoints(Vector2[] points)
    {
        Vector2[] newPoints = wallCollider.points;
        for (int i = 0; i < points.Length; i++)
        {
            if (i < 3)
                newPoints[i] = new Vector2(points[i].x + 0.05f, points[i].y);
            else
                newPoints[i] = points[i];
        }
        wallCollider.points = newPoints;
    }
}
