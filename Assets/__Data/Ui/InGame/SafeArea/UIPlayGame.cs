using __Data;
using UnityEngine;

public class UIPlayGame : GameBehaviour
{
    private static UIPlayGame _instance;
    public static UIPlayGame Instance => _instance;
    [SerializeField] protected GameObject uiBottomLeft;
    public GameObject UIBottomLeft => uiBottomLeft;
    [SerializeField] protected GameObject uiBottomRight;
    public GameObject UIBottomRight => uiBottomRight;
    [SerializeField] protected GameObject uiTopLeft;
    public GameObject UITopLeft => uiTopLeft;
    [SerializeField] protected GameObject uiTopRight;
    public GameObject UITopRight => uiTopRight;

    protected override void Awake()
    {
        base.Awake();
        if (UIPlayGame._instance != null) Debug.LogError("Only 1 UIPlayGame allow to exist");
        UIPlayGame._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadUiBottomLeft();
        LoadUiBottomRight();
        LoadUiTopLeft();
        LoadUiTopRight();
    }

    private void LoadUiBottomLeft()
    {
        if (uiBottomLeft != null) return;
        uiBottomLeft = transform.Find("UIBottomLeft").gameObject;
        Debug.LogWarning(transform.name + ": LoadingUiBottomLeft", gameObject);
    }

    private void LoadUiBottomRight()
    {
        if (uiBottomRight != null) return;
        uiBottomRight = transform.Find("UIBottomRight").gameObject;
        Debug.LogWarning(transform.name + ": LoadingUiBottomRight", gameObject);
    }

    private void LoadUiTopLeft()
    {
        if (uiTopLeft != null) return;
        uiTopLeft = transform.Find("UITopLeft").gameObject;
        Debug.LogWarning(transform.name + ": LoadingUiTopLeft", gameObject);
    }

    private void LoadUiTopRight()
    {
        if (uiTopRight != null) return;
        uiTopRight = transform.Find("UITopRight").gameObject;
        Debug.LogWarning(transform.name + ": LoadingUiTopRight", gameObject);
    }
}
