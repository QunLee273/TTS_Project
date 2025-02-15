using __Data;
using UnityEngine;

public class ItemCtrl : GameBehaviour
{
    [SerializeField] protected ItemDespawn itemDespawn;
    public ItemDespawn ItemDespawn => itemDespawn;
    
    [SerializeField] protected ItemProfileSO itemProfileSo;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemDespawn();
        LoadItemProfile();
    }
    
    protected virtual void LoadItemDespawn()
    {
        if (itemDespawn != null) return;
        itemDespawn = transform.GetComponentInChildren<ItemDespawn>();
        Debug.Log(transform.name + ": LoadItemDespawn", gameObject);
    }

    private void LoadItemProfile()
    {
        if (itemProfileSo != null) return;
        ItemCode itemCode = ItemCodeParser.FromString(transform.name);
        itemProfileSo = ItemProfileSO.FindByItemCode(itemCode);
        Debug.Log(transform.name + ": LoadItemProfile", gameObject);
    }
}
