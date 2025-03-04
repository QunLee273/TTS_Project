using __Data;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : GameBehaviour
{
    [SerializeField] protected Button btnSetting;
    [SerializeField] protected Button btnShopUpdate;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnSetting();
        LoadBtnShopUpdate();
    }

    protected override void Start()
    {
        base.Start();
        btnSetting.onClick.AddListener(OnClick_Setting);
        btnShopUpdate.onClick.AddListener(OnClick_ShopUpdate);
    }

    private void LoadBtnSetting()
    {
        if (btnSetting != null) return;
        btnSetting = transform.Find("BtnSetting").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnSetting", gameObject);
    }

    private void LoadBtnShopUpdate()
    {
        if (btnShopUpdate != null) return;
        btnShopUpdate = transform.Find("BtnShop&Update").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnShopUpdate", gameObject);
    }

    private void OnClick_Setting()
    {
        UICtrlMainMenu.Instance.Settings.SetActive(true);
        UICtrlMainMenu.Instance.SelectLevel.SetActive(false);
    }

    private void OnClick_ShopUpdate()
    {
        UICtrlMainMenu.Instance.ShopUpdate.SetActive(true);
        UICtrlMainMenu.Instance.SelectLevel.SetActive(false);
    }
}
