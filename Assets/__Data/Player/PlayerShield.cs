using System;
using System.Collections;
using __Data;
using UnityEngine;

public class PlayerShield : GameBehaviour
{
    [SerializeField] protected GameObject shield;
    [SerializeField] protected float timeShield = 10f;
    
    [SerializeField] protected bool isActive = false;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShield();
    }

    protected void Update()
    {
        if (isActive == false) return;
        shield.SetActive(isActive);
        PlayerController.Instance.IsInvulnerable = true;
        StartCoroutine(ActivateShield());
        
    }

    private void LoadShield()
    {
        if(shield != null) return;
        shield = GameObject.Find("ShieldEffect");
        shield.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadShield", gameObject);
    }

    IEnumerator ActivateShield()
    {
        yield return new WaitForSeconds(timeShield);
        isActive = false;
        shield.SetActive(false);
        PlayerController.Instance.IsInvulnerable = false;
    }
}
