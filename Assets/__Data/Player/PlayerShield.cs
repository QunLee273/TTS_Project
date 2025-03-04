using System;
using System.Collections;
using __Data;
using __Data.Script;
using UnityEngine;

public class PlayerShield : GameBehaviour
{
    [SerializeField] protected GameObject shield;
    [SerializeField] protected float timeShieldBase = 8f;
    
    [SerializeField] protected bool isActive = false;
    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }
    
    private float _timeShield;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShield();
    }

    protected override void Start()
    {
        base.Start();
        UpgrateShieldDuration();
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
        yield return new WaitForSeconds(_timeShield);
        isActive = false;
        shield.SetActive(false);
        PlayerController.Instance.IsInvulnerable = false;
    }

    private void UpgrateShieldDuration()
    {
        int survivalLv = PlayerPrefs.GetInt(PlayerPrefsString.SkillLevel_ + 1);
        _timeShield = timeShieldBase;
        for (int i = 0; i <= survivalLv; i++)
        {
            if (i is 2 or 4) 
                _timeShield -= 2;
        }
    }
}
