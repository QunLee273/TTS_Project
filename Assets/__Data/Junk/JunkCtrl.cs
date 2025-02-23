using System.Collections;
using System.Collections.Generic;
using __Data;
using UnityEngine;

public class JunkCtrl : GameBehaviour
{
    [SerializeField] protected Transform model;
    public Transform Model => model;

    [SerializeField] protected JunkSO junkSo;
    public JunkSO JunkSo => junkSo;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadJunkSo();
    }

    protected virtual void LoadModel()
    {
        if (model != null) return;
        model = transform.Find("Model");
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }
    
    protected virtual void LoadJunkSo()
    {
        if (junkSo != null) return;
        string resPath = "GameObject/Junk/" + transform.name;
        junkSo = Resources.Load<JunkSO>(resPath);
        Debug.LogWarning(transform.name + ": LoadJunkSO " + resPath, gameObject);
    }
}
