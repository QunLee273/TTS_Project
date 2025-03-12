using __Data.Script;
using UnityEngine;

public class BossCtrl : ObjController
{
    [Header("Boss Control")]
    private static BossCtrl _instance;
    public static BossCtrl Instance => _instance;

    public bool isUsingAbility;
    
    [SerializeField] protected bool isAlive = true;
    public bool IsAlive
    {
        get => isAlive;
        private set
        {
            isAlive = value;
            ObjMovement.Animator.SetBool(AnimString.isAlive, isAlive);
            ObjMovement.Animator.SetBool(AnimString.canMove, isAlive);
        }
    }
    
    protected override bool IsDebugEnabled => true;

    protected override void Awake()
    {
        base.Awake();
        if (BossCtrl._instance != null) Debug.LogError("Only 1 BossCtrl allow to exist");
        BossCtrl._instance = this;
    }

    protected void Update()
    {
        CheckIsAlive();
    }

    private void CheckIsAlive()
    {
        if (DamageReceiver.IsDead())
        {
            IsAlive = false;
            UIMapBoss.Instance.HpBar.gameObject.SetActive(false);
        }
    }
    protected override string GetObjectTypeString()
    {
        return ObjectType.Boss.ToString();
    }
}
