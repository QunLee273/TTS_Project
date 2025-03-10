using __Data;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSlider : GameBehaviour
{
    [Header("Base Slider")]
    [SerializeField] protected Slider slider;

    protected override void Start()
    {
        base.Start();
        AddOnClickEvent();
    }

    protected virtual void FixedUpdate()
    {
        //For override
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSlider();
    }

    protected virtual void LoadSlider()
    {
        if (slider != null) return;
        slider = GetComponent<Slider>();
        Debug.LogWarning(transform.name + ": LoadSlider", gameObject);
    }

    protected virtual void AddOnClickEvent()
    {
        slider.onValueChanged.AddListener(OnChanged);
    }

    protected abstract void OnChanged(float newValue);
}
