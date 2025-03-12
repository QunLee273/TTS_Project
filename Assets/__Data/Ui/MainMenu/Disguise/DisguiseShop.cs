using System;
using __Data;
using __Data.Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisguiseShop : GameBehaviour
{
    [SerializeField] protected Button backButton;
    [SerializeField] protected DisguiseSO disguiseSo;
    [SerializeField] protected GameObject disguisePrefab;
    [SerializeField] protected Transform disguiseContent;

    private int _goldCounter;
    private int _equippedIndex;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnBack();
        LoadDisguiseSo();
        LoadContent();
    }

    private void LoadBtnBack()
    {
        if (backButton != null) return;
        backButton = GetComponentInChildren<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnBack", gameObject);
    }

    private void LoadDisguiseSo()
    {
        if (disguiseSo != null) return;
        disguiseSo = Resources.Load<DisguiseSO>("Disguise/DisguiseData");
        Debug.LogWarning(transform.name + ": LoadDisguiseSo", gameObject);
    }

    private void LoadContent()
    {
        if (disguiseContent != null) return;
        disguiseContent = transform.Find("ScrollDisguise/Viewport/Content");
        Debug.LogWarning(transform.name + ": LoadContent", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        backButton.onClick.AddListener(BackToShopUpdate);
        _goldCounter = PlayerPrefs.GetInt(PlayerPrefsString.AmountCoins, 0);
        _equippedIndex = PlayerPrefs.GetInt(PlayerPrefsString.EquippedIndex, 0);
        LoadDisguiseUI();
    }

    private void LoadDisguiseUI()
    {
        foreach (Transform child in disguiseContent)
            Destroy(child.gameObject);

        for (int i = 0; i < disguiseSo.disguiseData.Count; i++)
            CreateDisguiseItem(i);
    }

    private void CreateDisguiseItem(int index)
    {
        var disguise = disguiseSo.disguiseData[index];
        GameObject item = Instantiate(disguisePrefab, disguiseContent);

        item.transform.Find("TxtName").GetComponent<TMP_Text>().text = disguise.disguiseName;
        item.transform.Find("Image").GetComponent<Image>().sprite = disguise.disguiseIcon;
        TMP_Text logText = item.transform.Find("TxtLog").GetComponent<TMP_Text>();
        Button selectButton = item.transform.Find("BtnSelect").GetComponent<Button>();
        TMP_Text buttonText = selectButton.GetComponentInChildren<TMP_Text>();

        selectButton.onClick.RemoveAllListeners();

        if (disguise.isPurchased)
            UpdatePurchasedDisguise(selectButton, buttonText, logText, index);
        else
            UpdateUnpurchasedDisguise(selectButton, buttonText, logText, disguise, index);
    }

    private void UpdatePurchasedDisguise(Button selectButton, TMP_Text buttonText, TMP_Text logText, int index)
    {
        if (index == _equippedIndex)
        {
            buttonText.text = "Equipped";
            selectButton.interactable = false;
        }
        else
        {
            buttonText.text = "Equip";
            selectButton.interactable = true;
            selectButton.onClick.AddListener(() => EquipDisguise(index));
        }
        logText.text = "Purchased";
    }

    private void UpdateUnpurchasedDisguise(Button selectButton, TMP_Text buttonText, TMP_Text logText, DisguiseData disguise, int index)
    {
        buttonText.text = disguise.price + " Gold";
        logText.text = (_goldCounter < disguise.price) ? "Not enough gold!" : "Not Purchased";
        selectButton.interactable = (_goldCounter >= disguise.price);
        selectButton.onClick.AddListener(() => BuyDisguise(index));
    }

    private void BuyDisguise(int index)
    {
        var disguise = disguiseSo.disguiseData[index];

        if (_goldCounter < disguise.price) return;
        _goldCounter -= disguise.price;
        disguise.isPurchased = true;
        PlayerPrefs.SetInt(PlayerPrefsString.AmountCoins, _goldCounter);
        PlayerPrefs.Save();

        LoadDisguiseUI();
    }

    private void EquipDisguise(int index)
    {
        _equippedIndex = index;
        PlayerPrefs.SetInt(PlayerPrefsString.EquippedIndex, index);
        PlayerPrefs.Save();

        LoadDisguiseUI();
    }

    private void BackToShopUpdate()
    {
        UICtrlMainMenu.Instance.ShopUpdate.SetActive(true);
        UICtrlMainMenu.Instance.Disguise.SetActive(false);
    }
}