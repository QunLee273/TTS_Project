using System;
using System.Collections.Generic;
using __Data;
using __Data.Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisguiseShop : GameBehaviour
{
    public List<DisguiseData> disguises;
    public GameObject disguisePrefab;
    public Transform disguiseContent;
    public int goldCounter;

    private DisguiseData _equippedDisguise = null;

    protected override void Start()
    {
        
        goldCounter = PlayerPrefs.GetInt(PlayerPrefsString.AmountCoins);
        LoadDisguiseUI();
    }

    void LoadDisguiseUI()
    {
        foreach (Transform child in disguiseContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var disguise in disguises)
        {
            GameObject item = Instantiate(disguisePrefab, disguiseContent);
            item.transform.Find("TxtName").GetComponent<TMP_Text>().text = disguise.disguiseName;
            item.transform.Find("Image").GetComponent<Image>().sprite = disguise.disguiseIcon;
            TMP_Text logText = item.transform.Find("TxtLog").GetComponent<TMP_Text>();
            Button selectButton = item.transform.Find("BtnSelect").GetComponent<Button>();
            TMP_Text buttonText = selectButton.GetComponentInChildren<TMP_Text>();

            if (disguise.isPurchased)
            {
                buttonText.text = "Equip";
                logText.text = "Purchased";
                selectButton.onClick.AddListener(() => EquipDisguise(disguise, logText));
            }
            else
            {
                buttonText.text = disguise.price + " Gold";
                logText.text = "Not Purchased";
                selectButton.onClick.AddListener(() => BuyDisguise(disguise, buttonText, logText, selectButton));
            }
        }
    }

    void BuyDisguise(DisguiseData disguise, TMP_Text buttonText, TMP_Text logText, Button button)
    {
        if (goldCounter >= disguise.price)
        {
            goldCounter -= disguise.price;
            disguise.isPurchased = true;

            buttonText.text = "Equip";
            logText.text = "Purchased";
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => EquipDisguise(disguise, logText));
        }
    }

    void EquipDisguise(DisguiseData disguise, TMP_Text logText)
    {
        _equippedDisguise = disguise;
        logText.text = "Equipped";
        Debug.Log("Equipped: " + disguise.disguiseName);
    }
}

[Serializable]
public class DisguiseData
{
    public string disguiseName;
    public Sprite disguiseIcon;
    public int price;
    public bool isPurchased;
}
