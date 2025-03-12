using UnityEngine;

public class EnemyReceiver : DamageReceiver
{
    [Header("Enemy Receiver")] 
    [SerializeField] protected EnemyCtrl enemyCtrl;
    
    protected override void Reborn()
    {
        MaxLifes = objController.GameObjectSo.life;
        base.Reborn();
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (enemyCtrl != null) return;
        enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
        Debug.Log(transform.name + ": LoadCtrl", gameObject);
    }
    
    protected override void OnDead()
    {
        Debug.Log("Enemy Dead");
        this.OnDeadDrop();
    }
    
    protected virtual void OnDeadDrop()
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        ItemDropSpawner.Instance.Drop(enemyCtrl.GameObjectSo.dropList, dropPos, dropRot);
    }
}
