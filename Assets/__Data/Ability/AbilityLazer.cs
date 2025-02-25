using __Data.Script;
using UnityEngine;

public class AbilityLazer : AbilityAbstract
{
    [Header("Ability Lazer")]
    [SerializeField] protected BossLazer bossLazer;
    [SerializeField] protected float timerCoolDown = 10f;
    [SerializeField] protected float timer;

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
            animator.SetBool(AnimString.spells, true);
            animator.SetBool(AnimString.atkLazer, true);
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
        animator.SetBool(AnimString.spells, false);
        animator.SetBool(AnimString.atkLazer, false);
        animator.SetBool(AnimString.canMove, true);
    }
}
