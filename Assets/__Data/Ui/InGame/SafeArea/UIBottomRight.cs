using __Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIBottomRight : GameBehaviour
{
    private static UIBottomRight _instance;
    public static UIBottomRight Instance => _instance;
    
    [Header("Game Objects")]
    [SerializeField] protected GameObject btnJump;
    public GameObject BtnJump => btnJump;
    [SerializeField] protected GameObject btnDash;
    public GameObject BtnDash => btnDash;
    [SerializeField] protected GameObject btnAttack;
    public GameObject BtnAttack => btnAttack;
    [SerializeField] protected GameObject btnInvisible;
    public GameObject BtnInvisible => btnInvisible;
    
    [Header("Script")]
    [SerializeField] protected PlayerMove playerMove;
    [SerializeField] protected PlayerAttack playerAttack;
    [SerializeField] protected AbilityDash abilityDash;
    [SerializeField] protected AbilityInvisible abilityInvisible;
    
    [Header("Button")]
    [SerializeField] protected Button jumpBtn;
    [SerializeField] protected Button dashBtn;
    [SerializeField] protected Button attackBtn;
    [SerializeField] protected Button invisibleBtn;

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
        LoadPlayerAbility();
    }

    private void LoadBtnJump()
    {
        if (btnJump != null) return;
        btnJump = transform.Find("BtnJump").gameObject;
        jumpBtn = btnJump.GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnJump", gameObject);
    }

    private void LoadBtnDash()
    {
        if (btnDash != null) return;
        btnDash = transform.Find("BtnDash").gameObject;
        dashBtn = btnDash.GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnDash", gameObject);
    }

    private void LoadBtnAttack()
    {
        if (btnAttack != null) return;
        btnAttack = transform.Find("BtnAttack").gameObject;
        attackBtn = btnAttack.GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnAttack", gameObject);
    }

    private void LoadBtnInvisible()
    {
        if (btnInvisible != null) return;
        btnInvisible = transform.Find("BtnInvisible").gameObject;
        invisibleBtn = btnInvisible.GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnInvisible", gameObject);
    }

    private void LoadPlayerAbility()
    {
        if (playerMove != null || playerAttack != null ||
            abilityDash != null || abilityInvisible != null) return;
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerMove>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>();
        abilityDash = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AbilityDash>();
        abilityInvisible = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AbilityInvisible>();
        Debug.LogWarning(transform.name + ": LoadPlayerAbility", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        jumpBtn.onClick.AddListener(() => playerMove.OnClickJump());
        attackBtn.onClick.AddListener(() => playerAttack.OnClickAtk());
        dashBtn.onClick.AddListener(() => abilityDash.OnClickDash());
        invisibleBtn.onClick.AddListener(() => abilityInvisible.Activate());
    }
}
