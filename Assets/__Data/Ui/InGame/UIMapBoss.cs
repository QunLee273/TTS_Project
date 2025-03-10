using __Data;
using UnityEngine;

public class UIMapBoss : GameBehaviour
{
    private static UIMapBoss _instance;
    public static UIMapBoss Instance => _instance;

    [SerializeField] protected GameObject creditPanel;
    public GameObject CreditPanel => creditPanel;
    [SerializeField] protected GameObject hpBar;
    public GameObject HpBar => hpBar;

    protected override void Awake()
    {
        base.Awake();
        if (UIMapBoss._instance != null) Debug.LogError("Only 1 UIMapBoss allow to exist");
        UIMapBoss._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCredit();
        LoadHpBar();
    }

    private void LoadCredit()
    {
        if (creditPanel != null) return;
        creditPanel = transform.Find("CreditPanel").gameObject;
        creditPanel.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadCredit", gameObject);
    }

    private void LoadHpBar()
    {
        if (hpBar != null) return;
        hpBar = transform.Find("HpBar").gameObject;
        hpBar.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadHpBar", gameObject);
    }
}
