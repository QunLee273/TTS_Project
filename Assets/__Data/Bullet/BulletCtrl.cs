using System;
using __Data;
using UnityEngine;

public class BulletCtrl : GameBehaviour
{
    [SerializeField] protected ObjFly objFly;
    public ObjFly ObjFly => objFly;
    
    [SerializeField] protected BulletDamSender bulletDamSender;
    public BulletDamSender BulletDamSender => bulletDamSender;

    [SerializeField] protected BulletDespawn bulletDespawn;
    public BulletDespawn BulletDespawn => bulletDespawn;

    [SerializeField] protected Transform shooter;
    public Transform Shooter => shooter;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBulletDamageSender();
        LoadBulletDespawn();
        LoadObjFly();
    }

    protected void Update()
    {
        transform.localScale = (ObjFly.direction == Vector3.right) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    protected virtual void LoadObjFly()
    {
        if (objFly != null) return;
        objFly = transform.GetComponentInChildren<ObjFly>();
        Debug.Log(transform.name + ": LoadObjFly", gameObject);
    }
    
    protected virtual void LoadBulletDamageSender()
    {
        if (bulletDamSender != null) return;
        bulletDamSender = transform.GetComponentInChildren<BulletDamSender>();
        Debug.Log(transform.name + ": LoadBulletDamageSender", gameObject);
    }

    protected virtual void LoadBulletDespawn()
    {
        if (this.bulletDespawn != null) return;
        this.bulletDespawn = transform.GetComponentInChildren<BulletDespawn>();
        Debug.Log(transform.name + ": LoadBulletDespawn", gameObject);
    }

    public virtual void SetShooter(Transform shooter)
    {
        this.shooter = shooter;
    }
}
