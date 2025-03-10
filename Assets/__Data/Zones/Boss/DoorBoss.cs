using __Data;
using UnityEngine;

public class DoorBoss : GameBehaviour
{
    [SerializeField] protected Transform door;
    
    [SerializeField] protected float speed = 4f;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDoor();
    }

    private void LoadDoor()
    {
        if (door != null) return;
        door = transform.parent;
        Debug.LogWarning(transform.name + ": LoadDoor", gameObject);
    }
}
