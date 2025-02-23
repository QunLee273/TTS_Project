using System.Collections;
using System.Collections.Generic;
using __Data;
using UnityEngine;

public abstract class JunkAbstract : GameBehaviour
{
    [SerializeField] protected JunkCtrl junkCtrl;
    public JunkCtrl JunkCtrl => junkCtrl; 

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadJunkCtrl();
    }

    protected virtual void LoadJunkCtrl()
    {
        if (this.junkCtrl != null) return;
        this.junkCtrl = transform.parent.GetComponent<JunkCtrl>();
        Debug.Log(transform.name + ": LoadJunkCtrl", gameObject);
    }
}
