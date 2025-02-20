using __Data;
using UnityEngine;

public abstract class ObjController : GameBehaviour
{
    [Header("ObjController")]
    [SerializeField] protected GameObject model;
    public GameObject Model => model;
    [SerializeField] protected GameObjectSO gameObjectSo;
    public GameObjectSO GameObjectSo => gameObjectSo;
    [SerializeField] protected ObjMovement objMovement;
    public ObjMovement ObjMovement => objMovement;
    [SerializeField] protected DamageReceiver damageReceiver;
    public DamageReceiver DamageReceiver => damageReceiver;
    [SerializeField] protected ObjAbility objAbility;
    public ObjAbility ObjAbility => objAbility;
    
    protected virtual bool IsDebugEnabled => true;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadModels();
        LoadSo();
        LoadObjMovements();
        LoadDameReciver();
        LoadObjAbility();
    }

    private void LoadModels()
    {
        if (model != null) return;
        model = GameObject.Find("Model");
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    private void LoadSo()
    {
        if (gameObjectSo != null) return;
        string resPath = "GameObject/" + GetObjectTypeString() + "/" + transform.name;
        gameObjectSo = Resources.Load<GameObjectSO>(resPath);
        Debug.LogWarning(transform.name + ": LoadSO " + resPath, gameObject);
    }

    private void LoadDameReciver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = transform.GetComponentInChildren<DamageReceiver>();
        Debug.LogWarning(transform.name + ": LoadDamageReceiver", gameObject);
    }

    private void LoadObjMovements()
    {
        if (objMovement != null || !IsDebugEnabled) return;
        objMovement = GetComponentInChildren<ObjMovement>();
        if (IsDebugEnabled) Debug.LogWarning(transform.name + ": LoadObjMovement", gameObject);
    }
    
    private void LoadObjAbility()
    {
        if (objAbility != null || !IsDebugEnabled) return;
        objAbility = GetComponentInChildren<ObjAbility>();
        Debug.LogWarning(transform.name + ": LoadObjAbility", gameObject);
    }
    
    protected abstract string GetObjectTypeString();
}
