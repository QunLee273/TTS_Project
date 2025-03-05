using System;
using __Data;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BulletImpart : GameBehaviour
{
    [Header("Bullet Impart")]
    [SerializeField] protected BulletCtrl bulletCtrl;
    public BulletCtrl BulletCtrl => bulletCtrl;
    
    protected bool IsDestroyed = false;
    
    [SerializeField] protected CircleCollider2D circleCollider2D;
    [SerializeField] protected Rigidbody2D rb;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBulletCtrl();
        LoadCollider();
        LoadRigibody();
    }

    protected virtual void LoadBulletCtrl()
    {
        if (this.bulletCtrl != null) return;
        this.bulletCtrl = transform.parent.GetComponent<BulletCtrl>();
        Debug.Log(transform.name + ": LoadBulletCtrl", gameObject);
    }
    
    protected virtual void LoadCollider()
    {
        if (circleCollider2D != null) return;
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = 0.09f;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }
    
    protected virtual void LoadRigibody()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        Debug.Log(transform.name + ": LoadRigibody", gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == bulletCtrl.Shooter) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") 
            || other.gameObject.layer == LayerMask.NameToLayer("Shield"))
        {
            BulletSpawner.Instance.Despawn(transform.parent);
            return;
        }
        bulletCtrl.BulletDamSender.Send(other.transform);
        
        Debug.Log("Send: " +other.transform.name);
    }
}
