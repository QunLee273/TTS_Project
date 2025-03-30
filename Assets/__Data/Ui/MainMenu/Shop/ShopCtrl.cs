using __Data;
using __Data.Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCtrl : GameBehaviour
{
    private static ShopCtrl _instance;
    public static ShopCtrl Instance => _instance;
    [SerializeField] protected Button backBtn;
    [SerializeField] protected GameObject btnRemoveAds;
    [SerializeField] protected TMP_Text txtActive;
    [SerializeField] protected TMP_Text txtPurchased;

    protected override void Awake()
    {
        base.Awake();
        if (ShopCtrl._instance != null) Debug.LogError("Only 1 ShopCtrl allow to exist");
        ShopCtrl._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnBack();
        LoadBtnRemoveAds();
    }

    private void LoadBtnBack()
    {
        if (backBtn != null) return;
        backBtn = GameObject.Find("BtnBack").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnBack", gameObject);
    }

    private void LoadBtnRemoveAds()
    {
        if (btnRemoveAds != null) return;
        btnRemoveAds = GameObject.Find("BtnRemoveAds");
        txtActive = btnRemoveAds.transform.Find("TxtActive").GetComponent<TMP_Text>();
        txtPurchased = btnRemoveAds.transform.Find("TxtPurchased").GetComponent<TMP_Text>();
        Debug.LogWarning(transform.name + ": LoadBtnRemoveAds", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        PlayerPrefs.GetInt(PlayerPrefsString.ActiveAds, 0);
        backBtn.onClick.AddListener(OnClickBack);
    }

    public void Coin3000Btn() => AddCoin(3000);
    public void Coin6000Btn() => AddCoin(6000);
    public void Coin13000Btn() => AddCoin(13000);
    public void Coin20000Btn() => AddCoin(20000);

    public void RemoveAdsBtn()
    {
        btnRemoveAds.GetComponent<Button>().interactable = false;
        txtActive.text = "Active";
        txtPurchased.text = "";
        PlayerPrefs.SetInt(PlayerPrefsString.ActiveAds, 1);
        PlayerPrefs.Save();
    }

    private void AddCoin(int amount)
    {
        int coin = PlayerPrefs.GetInt(PlayerPrefsString.AmountCoins, 0);
        coin += amount;
        PlayerPrefs.SetInt(PlayerPrefsString.AmountCoins, coin);
        PlayerPrefs.Save();
    }

    private void OnClickBack()
    {
        UICtrlMainMenu.Instance.ShopUpdate.SetActive(true);
        UICtrlMainMenu.Instance.Shop.SetActive(false);
    }
}
