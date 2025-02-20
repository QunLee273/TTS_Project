using __Data.Script;
using UnityEngine;

public class AbilityLazer : AbilityAbstract
{
    [Header("Ability Lazer")]
    [SerializeField] protected BossLazer bossLazer;
    [SerializeField] protected float timerCoolDown = 10f;
    [SerializeField] protected float timer;
    [SerializeField] protected bool spells;
    public bool Spells 
    { 
        get => spells;
        set
        {
            spells = value; 
            animator.SetBool(AnimString.spells, value);
        } 
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBossLazer();
    }

    protected virtual void Update()
    {
        if (timer < timerCoolDown) timer += Time.deltaTime;
        else
        {
            Spells = true;
            animator.SetTrigger(AnimString.atkLazerTrigger);
            animator.SetBool(AnimString.canMove, false);
        }
    }

    private void LoadBossLazer()
    {
        if (bossLazer != null) return;
        bossLazer = gameObject.GetComponentInChildren<BossLazer>();
        GameObject bossLazerObj = bossLazer.gameObject;
        bossLazerObj.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadBossLazer", gameObject);
    }

    public void StartLazing()
    {
        bossLazer.gameObject.SetActive(true);
    }

    public void EndLazing()
    {
        bossLazer.TargetEndPoint = bossLazer.FirePoint.position;
        bossLazer.CurrentEndPoint = bossLazer.FirePoint.position;
        bossLazer.gameObject.SetActive(false);
        timer = 0f;
        Spells = false;
        animator.SetBool(AnimString.canMove, true);
    }
}
