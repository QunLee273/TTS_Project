using __Data;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpdate : GameBehaviour
{
    [SerializeField] protected Button btnBack;
    [SerializeField] protected Button btnUpdate;
    [SerializeField] protected Button btnShop;
    [SerializeField] protected Button btnDisguise;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnBack();
        LoadBtnUpdate();
        LoadBtnShop();
        LoadBtnDisguise();
    }

    protected override void Start()
    {
        base.Start();
        btnBack.onClick.AddListener(OnClickBack);
        btnUpdate.onClick.AddListener(OnClickUpdate);
        btnDisguise.onClick.AddListener(OnClickDisguise);
    }

    private void LoadBtnBack()
    {
        if (btnBack != null) return;
        btnBack = GameObject.Find("BtnBackSelectLV").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnBack", gameObject);
    }

    private void LoadBtnUpdate()
    {
        if (btnUpdate != null) return;
        btnUpdate = GameObject.Find("BtnUpdate").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnUpdate", gameObject);
    }

    private void LoadBtnShop()
    {
        if (btnShop != null) return;
        btnShop = GameObject.Find("BtnShop").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnShop", gameObject);
    }

    private void LoadBtnDisguise()
    {
        if (btnDisguise != null) return;
        btnDisguise = GameObject.Find("BtnDisguise").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnDisguise", gameObject);
    }

    private void OnClickBack()
    {
        UICtrlMainMenu.Instance.SelectLevel.SetActive(true);
        UICtrlMainMenu.Instance.ShopUpdate.SetActive(false);
    }
    
    private void OnClickUpdate()
    {
        UICtrlMainMenu.Instance.UpdateSkill.SetActive(true);
        UICtrlMainMenu.Instance.ShopUpdate.SetActive(false);
    }

    private void OnClickDisguise()
    {
        UICtrlMainMenu.Instance.Disguise.SetActive(true);
        UICtrlMainMenu.Instance.ShopUpdate.SetActive(false);
    }
}
