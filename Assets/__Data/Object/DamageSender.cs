using __Data;
using UnityEngine;

public class DamageSender : GameBehaviour
{
    [Header("Damage Sender")]
    [SerializeField] protected ObjController objController;
    public ObjController ObjController => objController;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadObjCtrl();
    }

    private void LoadObjCtrl()
    {
        if (objController != null) return;
        objController = transform.parent.GetComponent<ObjController>();
        Debug.LogWarning(transform.name + ": LoadObjCtrl", gameObject);
    }
}
