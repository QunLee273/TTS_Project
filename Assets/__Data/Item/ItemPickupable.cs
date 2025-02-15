using System;
using __Data;
using UnityEngine;

public class ItemPickupable : GameBehaviour
{
    [Header("Item Pickupable")]
    [SerializeField] protected ItemCtrl itemCtrl;
    public ItemCtrl ItemCtrl => itemCtrl;
    
    [SerializeField] protected Collider2D col;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemCtrl();
        LoadCollider();
    }

    protected virtual void LoadItemCtrl()
    {
        if (this.itemCtrl != null) return;
        this.itemCtrl = transform.parent.GetComponent<ItemCtrl>();
        Debug.Log(transform.name + ": LoadItemCtrl", gameObject);
    }
    
    public static ItemCode String2ItemCode(string itemName)
    {
        try
        {
            return (ItemCode)Enum.Parse(typeof(ItemCode), itemName);
        }
        catch (ArgumentException e)
        {
            Debug.LogError(e.ToString());
            return ItemCode.NoItem;
        }
    }

    protected virtual void LoadCollider()
    {
        if (col!= null) return;
        col = transform.GetComponent<Collider2D>();
        col.isTrigger = true;
        Debug.LogWarning(transform.name + " LoadCollider", gameObject);
    }

    public virtual ItemCode GetItemCode()
    {
        return ItemPickupable.String2ItemCode(transform.parent.name);
    }

    public virtual void Picked()
    {
        ItemCtrl.ItemDespawn.DespawnObject();
    }
}
