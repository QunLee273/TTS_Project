using System.Collections;
using System.Linq;
using __Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLooter : GameBehaviour
{
    [SerializeField] protected Collider2D col;
    [SerializeField] protected float timerInvulnerable = 10f;
    [SerializeField] protected long coinAmount;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadColliderTrigger();
    }

    private void LoadColliderTrigger()
    {
        if (col != null) return;
        col = transform.GetComponent<Collider2D>();
        col.isTrigger = true;
        Debug.LogWarning(transform.name + ": LoadColliderTrigger", gameObject);
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            ItemPickupable itemPickupable = other.GetComponent<ItemPickupable>();
            if (itemPickupable == null) return;
        
        
            ItemCode itemCode = itemPickupable.GetItemCode();
            ItemProfileSO itemProfile = GetItemProfile(itemCode);

            if (itemProfile.itemCode is ItemCode.Coin or ItemCode.BagCoin)
            {
                AddItem(itemProfile);
            }
            else if (itemProfile.itemCode == ItemCode.Life)
            {
                PlayerReceiver receiver = transform.parent.GetComponentInChildren<PlayerReceiver>();
                receiver.Add(1);
            }
            else if (itemProfile.itemCode == ItemCode.Shield)
            {
                PlayerShield shield = transform.parent.GetComponentInChildren<PlayerShield>();
                shield.IsActive = true;
            }
            
            itemPickupable.Picked();
        }
    }

    private void AddItem(ItemProfileSO itemProfile)
    {
        if (itemProfile == null) return;

        coinAmount += itemProfile.itemCount;
        Debug.Log("Đã nhận " + itemProfile.itemCount + " coin. Tổng coin hiện tại: " + coinAmount);
    }
    
    protected virtual ItemProfileSO GetItemProfile(ItemCode itemCode)
    {
        var profiles = Resources.LoadAll("Item", typeof(ItemProfileSO));
        foreach (ItemProfileSO profile in profiles.Cast<ItemProfileSO>())
        {
            if (profile.itemCode != itemCode) continue;
            return profile;
        }
        return null;
    }
}
