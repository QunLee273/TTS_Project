using UnityEngine;

public class ItemDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        ItemDropSpawner.Instance.Despawn(transform.parent);
    }
}
