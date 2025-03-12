using __Data;
using __Data.Script;
using UnityEngine;

public class DisguiseCtrl : GameBehaviour
{
    [SerializeField] protected GameObject model;
    [SerializeField] protected DisguiseSO disguiseSo;
    [SerializeField] protected DisguiseMovement disguiseMove;
    
    private int _index;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadDisguiseSo();
        LoadDisguiseMove();
    }

    private void LoadModel()
    {
        if (model != null) return;
        model = transform.Find("Model").gameObject;
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    private void LoadDisguiseSo()
    {
        if (disguiseSo != null) return;
        disguiseSo = Resources.Load<DisguiseSO>("Disguise/DisguiseData");
        Debug.LogWarning(transform.name + ": LoadDisguiseSo", gameObject);
    }

    private void LoadDisguiseMove()
    {
        if (disguiseMove != null) return;
        disguiseMove = transform.GetComponentInChildren<DisguiseMovement>();
        Debug.LogWarning(transform.name + ": LoadDisguiseMove", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        _index = PlayerPrefs.GetInt(PlayerPrefsString.EquippedIndex, 0);
        if (_index < 0 || _index > disguiseSo.disguiseData.Count) return;
        UpdateSpriteRenderer();
    }

    private void UpdateSpriteRenderer()
    {
        for (int i = 0; i < disguiseSo.disguiseData.Count; i++)
            if (i == _index)
                model.GetComponent<SpriteRenderer>().sprite = disguiseSo.disguiseData[i].disguiseIcon;
    }
}
