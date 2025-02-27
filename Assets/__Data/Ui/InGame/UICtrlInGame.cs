using __Data;
using UnityEngine;

public class UICtrlInGame : GameBehaviour
{
    private static UICtrlInGame _instance;
    public static UICtrlInGame Instance => _instance;

    [SerializeField] protected GameObject safeArea;
    public GameObject SafeArea => safeArea;
    
    [SerializeField] protected GameObject uiCenter;
    public GameObject UiCenter => uiCenter;

    protected override void Awake()
    {
        base.Awake();
        if (UICtrlInGame._instance != null) Debug.LogError("Only 1 UICtrl allow to exist");
        UICtrlInGame._instance = this;
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSafeArea();
        LoadUiCenter();
    }

    private void LoadSafeArea()
    {
        if (safeArea != null) return;
        safeArea = GameObject.Find("SafeAreaView");
        Debug.LogWarning(transform.name + ": LoadSafeArea", gameObject);
    }

    private void LoadUiCenter()
    {
        if (uiCenter != null) return;
        uiCenter = GameObject.Find("UICenter");
        Debug.LogWarning(transform.name + ": LoadUiCenter", gameObject);
    }
}
