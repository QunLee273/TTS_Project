using __Data;
using UnityEngine;


public class ObjController : GameBehaviour
{
    [Header("ObjController")]
    [SerializeField] protected GameObject model;
    public GameObject Model => model;
    [SerializeField] protected ObjMovement objMovement;
    public ObjMovement ObjMovement => objMovement;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadModels();
        LoadObjMovements();
    }

    protected void LoadModels()
    {
        if (model != null) return;
        model = GameObject.Find("Model");
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    protected void LoadObjMovements()
    {
        if (objMovement != null) return;
        objMovement = GetComponentInChildren<ObjMovement>();
        Debug.LogWarning(transform.name + ": LoadObjMovement", gameObject);
    }
}
