using __Data;
using UnityEngine;

public class HpBossBar : GameBehaviour
{
    [Header("HP Bar")]
    [SerializeField] protected ObjController objController;
    [SerializeField] protected SliderHpBoss sliderHp;

    private int maxHp;
    private int hp;
    protected virtual void FixedUpdate()
    {
        HpShowing();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSliderHp();
    }
    
    protected virtual void LoadSliderHp()
    {
        if (sliderHp != null) return;
        sliderHp = transform.GetComponentInChildren<SliderHpBoss>();
        Debug.LogWarning(transform.name + ": LoadSliderHp", gameObject);
    }
    
    protected override void Start()
    {
        base.Start();
        maxHp = objController.DamageReceiver.MaxLifes;
        sliderHp.SetMaxHp(maxHp);
    }

    protected virtual void HpShowing()
    {
        if (objController == null) return;

        hp = objController.DamageReceiver.Lifes;
        if (hp > maxHp) hp = maxHp;
        sliderHp.SetCurrentHp(hp);
    }
}
