using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpawner : Spawner
{
    private static ItemDropSpawner _instance;
    public static ItemDropSpawner Instance => _instance;

    [SerializeField] protected float gameDropRate = 1;

    protected override void Awake()
    {
        base.Awake();
        if (ItemDropSpawner._instance != null) Debug.LogError("Only 1 ItemDropSpawner allow to exist");
        ItemDropSpawner._instance = this;
    }

    public virtual List<ItemDropRate> Drop(List<ItemDropRate> dropList, Vector3 pos, Quaternion rot)
    {
        List<ItemDropRate> dropItems = new List<ItemDropRate>();

        if (dropList.Count < 1) return dropItems;

        dropItems = this.DropItems(dropList);
        foreach (ItemDropRate itemDropRate in dropItems)
        {
            ItemCode itemCode = itemDropRate.itemSo.itemCode;
            Transform itemDrop = this.Spawn(itemCode.ToString(), pos, rot);
            if (itemDrop == null) continue;
            itemDrop.gameObject.SetActive(true);
        }

        return dropItems;
    }

    protected virtual List<ItemDropRate> DropItems(List<ItemDropRate> items)
    {
        float totalRate = 0f;
        float gameDropRateValue = this.GameDropRate();

        foreach (ItemDropRate item in items)
            totalRate += (item.dropRate / 100f) * gameDropRateValue;

        float rand = Random.Range(0f, totalRate);

        foreach (ItemDropRate item in items)
        {
            float currentRate = (item.dropRate / 100f) * gameDropRateValue;
            rand -= currentRate;
            if (rand <= 0)
            {
                return new List<ItemDropRate> { item };
            }
        }

        return new List<ItemDropRate>();
    }

    protected virtual float GameDropRate()
    {
        float dropRateFromItems = 0f;

        return gameDropRate + dropRateFromItems;
    }
}
