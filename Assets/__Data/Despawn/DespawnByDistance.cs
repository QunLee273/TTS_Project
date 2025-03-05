using UnityEngine;

public class DespawnByDistance : Despawn
{
    [SerializeField] protected float disLimit = 15f;
    [SerializeField] protected float distance = 0f;
    [SerializeField] protected Vector3 spawnPosition;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        spawnPosition = transform.position;
    }

    protected override bool CanDespawn()
    {
        distance = Vector3.Distance(transform.position, spawnPosition);
        if (distance > disLimit) return true;
        return false;
    }
}
