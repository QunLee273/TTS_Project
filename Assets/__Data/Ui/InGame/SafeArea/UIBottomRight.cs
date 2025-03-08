using __Data;
using UnityEngine;
using UnityEngine.UI;

public class UIBottomRight : GameBehaviour
{
    private static UIBottomRight _instance;
    public static UIBottomRight Instance => _instance;
    
    [SerializeField] protected GameObject btnJump;
    public GameObject BtnJump => btnJump;
    [SerializeField] protected GameObject btnDash;
    public GameObject BtnDash => btnDash;
    [SerializeField] protected GameObject btnAttack;
    public GameObject BtnAttack => btnAttack;
    [SerializeField] protected GameObject btnInvisible;
    public GameObject BtnInvisible => btnInvisible;

    protected override void Awake()
    {
        base.Awake();
        if (UIBottomRight._instance != null) Debug.LogError("Only 1 UIBottomRight allow to exist");
        UIBottomRight._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnJump();
        LoadBtnDash();
        LoadBtnAttack();
        LoadBtnInvisible();
    }

    private void LoadBtnJump()
    {
        if (btnJump != null) return;
        btnJump = transform.Find("BtnJump").gameObject;
        Debug.LogWarning(transform.name + ": LoadBtnJump", gameObject);
    }

    private void LoadBtnDash()
    {
        if (btnDash != null) return;
        btnDash = transform.Find("BtnDash").gameObject;
        Debug.LogWarning(transform.name + ": LoadBtnDash", gameObject);
    }

    private void LoadBtnAttack()
    {
        if (btnAttack != null) return;
        btnAttack = transform.Find("BtnAttack").gameObject;
        Debug.LogWarning(transform.name + ": LoadBtnAttack", gameObject);
    }

    private void LoadBtnInvisible()
    {
        if (btnInvisible != null) return;
        btnInvisible = transform.Find("BtnInvisible").gameObject;
        Debug.LogWarning(transform.name + ": LoadBtnInvisible", gameObject);
    }
}
