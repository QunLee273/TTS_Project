using __Data;
using UnityEngine;

public class UIBottomLeft : GameBehaviour
{
    [SerializeField] protected GameObject btnMoveLeft;
    [SerializeField] protected GameObject btnMoveRight;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadBtnMoveLeft();
        LoadBtnMoveRight();
    }

    private void LoadBtnMoveLeft()
    {
        if (btnMoveLeft != null) return;
        btnMoveLeft = transform.Find("BtnMoveLeft").gameObject;
        Debug.LogWarning(transform.name + ": LoadBtnMoveLeft", gameObject);
    }

    private void LoadBtnMoveRight()
    {
        if (btnMoveRight != null) return;
        btnMoveRight = transform.Find("BtnMoveRight").gameObject;
        Debug.LogWarning(transform.name + ": LoadBtnMoveRight", gameObject);
    }
}
