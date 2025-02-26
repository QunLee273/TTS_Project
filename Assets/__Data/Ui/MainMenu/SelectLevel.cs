using __Data;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : GameBehaviour
{
    [SerializeField] protected Button btnSetting;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnSetting();
    }

    protected override void Start()
    {
        base.Start();
        btnSetting.onClick.AddListener(OnClick_Setting);
    }

    private void LoadBtnSetting()
    {
        if (btnSetting != null) return;
        btnSetting = transform.Find("BtnSetting").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnSetting", gameObject);
    }

    public void OnClick_Setting()
    {
        UICtrl.Instance.Settings.SetActive(true);
        UICtrl.Instance.SelectLevel.SetActive(false);
    }
}
