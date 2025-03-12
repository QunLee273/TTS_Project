using System.Collections;
using __Data.Script;
using UnityEngine;
using UnityEngine.Serialization;

public class BossHealing : AbilityAbstract
{
    [Header("Boss Healing")]
    [SerializeField] protected GameObject healingEfx;
    [SerializeField] protected float timeHealing = 10f;
    [SerializeField] protected int healingValue = 5;
    
    [SerializeField] protected float thresholdHp = 0.7f;
    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHealing();
    }

    protected override void Start()
    {
        base.Start();
        maxHp = BossReceiver.Instance.MaxLifes;
    }

    protected void Update()
    {
        currentHp = BossReceiver.Instance.Lifes;
        float hpPercent = currentHp / maxHp;
        if (hpPercent <= thresholdHp && thresholdHp > 0)
        {
            animator.SetBool(AnimString.spells, true);
            animator.SetBool(AnimString.healing, true);
            animator.SetBool(AnimString.canMove, false);
            
            thresholdHp -= 0.3f;
        }
    }
    
    private void LoadHealing()
    {
        if(healingEfx != null) return;
        healingEfx = GameObject.Find("HealingEffect");
        healingEfx.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadHealing", gameObject);
    }

    public void UseHealing()
    {
        BossCtrl.Instance.isUsingAbility = true;
        healingEfx.SetActive(true);
        StartCoroutine(HealOverTime());
    }

    IEnumerator HealOverTime()
    {
        int seconds = Mathf.RoundToInt(timeHealing);
        for (int i = 0; i < seconds; i++)
        {
            yield return new WaitForSeconds(1f);
            BossReceiver.Instance.Add(healingValue);
        }
        animator.SetBool(AnimString.spells, false);
        animator.SetBool(AnimString.healing, false);
        animator.SetBool(AnimString.canMove, true);
        healingEfx.SetActive(false);
        BossCtrl.Instance.isUsingAbility = false;
    }
}
