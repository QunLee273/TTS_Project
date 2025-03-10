using UnityEngine;
using UnityEngine.Serialization;

public class SliderHpBoss : BaseSlider
{
    [Header("HP")]
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected float currentHp = 100;

    protected override void FixedUpdate()
    {
        HpShowing();
    }

    protected virtual void HpShowing()
    {
        float hpPercent = currentHp / maxHp;
        slider.value = hpPercent;
    }

    protected override void OnChanged(float newValue)
    {
        //Debug.Log("newValue: " + newValue);
    }

    public virtual void SetMaxHp(float maxHP)
    {
        maxHp = maxHP;
    }

    public virtual void SetCurrentHp(float currentHP)
    {
        currentHp = currentHP;
    }
}
