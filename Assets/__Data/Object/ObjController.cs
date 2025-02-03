using __Data;
using UnityEngine;


public class ObjController : GameBehaviour
{
    [Header("ObjController")]
    [SerializeField] protected GameObject model;
    public GameObject Model => model;
    [SerializeField] protected ObjMovement objMovement;
    public ObjMovement ObjMovement => objMovement;
    [SerializeField] protected ObjAbility objAbility;
    public ObjAbility ObjAbility => objAbility;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadModels();
        LoadObjMovements();
        LoadObjAbility();
    }

    private void LoadModels()
    {
        if (model != null) return;
        model = GameObject.Find("Model");
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    private void LoadObjMovements()
    {
        if (objMovement != null) return;
        objMovement = GetComponentInChildren<ObjMovement>();
        Debug.LogWarning(transform.name + ": LoadObjMovement", gameObject);
    }
    
    private void LoadObjAbility()
    {
        if (objAbility != null) return;
        objAbility = GetComponentInChildren<ObjAbility>();
        Debug.LogWarning(transform.name + ": LoadObjAbility", gameObject);
    }
}
