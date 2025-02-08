using __Data;
using UnityEngine;

public class BulletCtrl : GameBehaviour
{
    [SerializeField] protected BulletDamSender bulletDamSender;
    public BulletDamSender BulletDamSender => bulletDamSender;

    [SerializeField] protected BulletDespawn bulletDespawn;
    public BulletDespawn BulletDespawn => bulletDespawn;

    [SerializeField] protected Transform shooter;
    public Transform Shooter => shooter;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletDamageSender();
        this.LoadBulletDespawn();
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
