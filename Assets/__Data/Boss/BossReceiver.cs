using __Data.Script;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossReceiver : DamageReceiver
{
    [Header("Boss Receiver")]
    private static BossReceiver instance;
    public static BossReceiver Instance => instance;
    [SerializeField] protected bool isInvulnerable = false;

    protected override void Awake()
    {
        base.Awake();
        if (BossReceiver.instance != null) Debug.LogError("Only 1 BossReceiver allow to exist");
        BossReceiver.instance = this;
    }

    public bool IsInvulnerable
    {
        get => isInvulnerable;
        set => isInvulnerable = value;
    }
    public override void Deduct(int deduct)
    {
        if (isInvulnerable) return;
        base.Deduct(deduct);
    }

    protected override void Reborn()
    {
        base.Reborn();
        lifes = objController.GameObjectSo.life;
    }
    
    protected override void OnDead()
    {
        SceneManager.LoadScene(0);
    }
}
