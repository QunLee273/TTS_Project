using __Data;
using __Data.Script;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

public class ShopManager : GameBehaviour
{
    [SerializeField] protected CodelessIAPButton removeAdsBtn;
    [SerializeField] protected Button btnCoin3000;
    [SerializeField] protected CodelessIAPButton btnCoin6000;
    [SerializeField] protected CodelessIAPButton btnCoin13000;
    [SerializeField] protected CodelessIAPButton btnCoin20000;
    private const string Coin6000 = "coin6000";
    private const string Coin13000 = "coin13000";
    private const string Coin20000 = "coin20000";
    private const string RemoveAds = "removeAds";

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnRemoveAds();
        LoadBtnCoin();
    }

    private void LoadBtnCoin()
    {
        if (btnCoin3000 != null || btnCoin6000 != null ||
            btnCoin13000 != null || btnCoin20000 != null) return;
        btnCoin3000 = GameObject.Find("Btn3000Coin").GetComponent<Button>();
        btnCoin6000 = GameObject.Find("Btn6000Coin").GetComponent<CodelessIAPButton>();
        btnCoin13000 = GameObject.Find("Btn13000Coin").GetComponent<CodelessIAPButton>();
        btnCoin20000 = GameObject.Find("Btn20000Coin").GetComponent<CodelessIAPButton>();
        Debug.LogWarning(transform.name + ": LoadBtnCoin", gameObject);
    }

    private void LoadBtnRemoveAds()
    {
        if (removeAdsBtn != null) return;
        removeAdsBtn = transform.Find("BtnRemoveAds").GetComponent<CodelessIAPButton>();
        Debug.LogWarning(transform.name + ": LoadBtnRemoveAds", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        AddEventListeners();
        if (PlayerPrefs.GetInt(PlayerPrefsString.ActiveAds, 0) == 1) 
            ShopCtrl.Instance.RemoveAdsBtn();
    }

    private void AddEventListeners()
    {
        btnCoin3000.onClick.AddListener(BuyCoinAds);
        
        btnCoin6000.onPurchaseComplete.AddListener(PurchaseComplete);
        btnCoin13000.onPurchaseComplete.AddListener(PurchaseComplete);
        btnCoin20000.onPurchaseComplete.AddListener(PurchaseComplete);
        removeAdsBtn.onPurchaseComplete.AddListener(PurchaseComplete);
        
        btnCoin6000.onPurchaseFailed.AddListener(PurchaseFailed);
        btnCoin13000.onPurchaseFailed.AddListener(PurchaseFailed);
        btnCoin20000.onPurchaseFailed.AddListener(PurchaseFailed);
        removeAdsBtn.onPurchaseFailed.AddListener(PurchaseFailed);
    }
    
    private void BuyCoinAds()
    {
        if (AdsManager.Instance.rewardedAds == null) return;
        
        AdsManager.Instance.rewardedAds.OnAdCompleted += ShopCtrl.Instance.Coin3000Btn;
        AdsManager.Instance.rewardedAds.ShowRewardedAd();
    }

    private void PurchaseComplete(Product product)
    {
        if (product.definition.id == Coin6000)
            ShopCtrl.Instance.Coin6000Btn();
        if (product.definition.id == Coin13000)
            ShopCtrl.Instance.Coin13000Btn();
        if (product.definition.id == Coin20000)
            ShopCtrl.Instance.Coin20000Btn();
        if (product.definition.id == RemoveAds)
            ShopCtrl.Instance.RemoveAdsBtn();
    }

    private void PurchaseFailed(Product product, PurchaseFailureDescription description)
    {
        Debug.Log(product.definition.id + ": Purchase failure reason" + description);
    }
}
