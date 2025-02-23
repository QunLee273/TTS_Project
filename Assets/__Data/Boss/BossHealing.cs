using System.Collections;
using __Data;
using __Data.Script;
using UnityEngine;
using UnityEngine.Serialization;

public class BossHealing : GameBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject healing;
    [SerializeField] protected float timeHealing = 10f;
    [SerializeField] protected int healingValue = 5;
    
    [SerializeField] protected float thresholdHp = 0.5f;
    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHealing();
        LoadAnimation();
    }

    protected override void Start()
    {
        base.Start();
        maxHp = BossReceiver.Instance.Lifes;
    }

    protected void FixedUpdate()
    {
        currentHp = BossReceiver.Instance.Lifes;
        float hpPercent = currentHp / maxHp;
        if (hpPercent <= thresholdHp && thresholdHp > 0)
        {
            animator.SetBool(AnimString.spells, true);
            animator.SetBool(AnimString.healing, true);
            animator.SetBool(AnimString.canMove, false);
            
            thresholdHp -= 0.25f;
        }
    }
    
    private void LoadHealing()
    {
        if(healing != null) return;
        healing = GameObject.Find("HealingEffect");
        healing.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadHealing", gameObject);
    }
    
    private void LoadAnimation()
    {
        if(animator != null) return;
        animator = transform.parent.parent.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimation", gameObject);
    }

    public void UseHealing()
    {
        healing.SetActive(true);
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
        healing.SetActive(false);
    }
}
