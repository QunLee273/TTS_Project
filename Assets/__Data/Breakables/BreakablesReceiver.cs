using UnityEngine;

public class BreakablesReceiver : DamageReceiver
{
    [Header("Breakables Receiver")] 
    [SerializeField] protected BreakablesCtrl breakablesCtrl;
    
    protected override void Reborn()
    {
        base.Reborn();
        lifes = objController.GameObjectSo.life;
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (breakablesCtrl != null) return;
        breakablesCtrl = transform.parent.GetComponent<BreakablesCtrl>();
        Debug.Log(transform.name + ": LoadCtrl", gameObject);
    }
    
    protected override void OnDead()
    {
        Debug.Log(transform.parent.name + ": Break");
        OnDeadDrop();
    }
    
    protected virtual void OnDeadDrop()
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        ItemDropSpawner.Instance.Drop(breakablesCtrl.GameObjectSo.dropList, dropPos, dropRot);
    }
}
