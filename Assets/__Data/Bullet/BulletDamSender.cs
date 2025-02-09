using UnityEngine;

public class BulletDamSender : DamageSender
{
    [Header("Bullet Damage Sender")]
    [SerializeField] protected BulletCtrl bulletCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }

    protected virtual void LoadBulletCtrl()
    {
        if (bulletCtrl != null) return;
        bulletCtrl = transform.parent.GetComponent<BulletCtrl>();
        Debug.Log(transform.name + ": LoadBulletCtrl", gameObject);
    }

    public override void Send(Transform obj)
    {
        base.Send(obj);
        CreateImpactFX();
    }

    protected virtual void CreateImpactFX()
    {
        string fxName = GetImpactFX();

        Vector3 hitPos = transform.position;
        Transform fxImpact = FXSpawner.Instance.Spawn(fxName, hitPos, Quaternion.identity);
        fxImpact.gameObject.SetActive(true);
    }

    protected virtual string GetImpactFX()
    {
        return FXSpawner.blood1;
    }
}
