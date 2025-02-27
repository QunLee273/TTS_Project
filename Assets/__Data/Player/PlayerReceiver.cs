using System;
using __Data.Script;
using UnityEngine;

public class PlayerReceiver : DamageReceiver
{
    [Header("Player Receiver")]
    [SerializeField] protected PlayerController playerController;
    public PlayerController PlayerController => playerController;
    
    protected float CurrentTime = 0f;
    protected float TimeRespawn = 2f;
    private int currentLife;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerCtrl();
    }

    protected override void Start()
    {
        base.Start();
        currentLife = lifes;
    }

    protected void Update()
    {
        CheckLife();
    }

    private void LoadPlayerCtrl()
    {
        if (playerController != null) return;
        playerController = transform.parent.GetComponentInChildren<PlayerController>();
        Debug.LogWarning(transform.name + "LoadPlayerCtrl", gameObject);
    }

    protected override void Reborn()
    {
        base.Reborn();
        lifes = objController.GameObjectSo.life;
    }

    protected override void OnDead()
    {
        Time.timeScale = 0;
        UICenter.Instance.YouDead.SetActive(true);
    }

    protected void CheckLife()
    {
        if (lifes >= currentLife) return;
        
        playerController.IsAlive = false;

        if (lifes > 0)
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime < TimeRespawn) return;
            playerController.Respawn();
        }
        currentLife = lifes;
        
        StartCoroutine(playerController.DamageCooldownCoroutine());
    }
}
