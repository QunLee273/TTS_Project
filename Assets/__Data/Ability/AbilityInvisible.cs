using System.Collections;
using System.Collections.Generic;
using __Data.Script;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AbilityInvisible : AbilityAbstract
{
    [Header("Ability Invisible")]
    [SerializeField] protected GameObject logInstance;
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject objAbility;
    [SerializeField] protected CinemachineCamera vCam;

    [SerializeField] protected float logControlTimeBase = 5f;
    [SerializeField] protected float cooldownTimeBase = 14f;

    [SerializeField] protected bool canUseAbility = true;
    [SerializeField] protected bool isInvisible;

    public GameObject[] btns;
    private float _logControlTime;
    private float _cooldownTime;
    private int _invisibleLevel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayer();
        LoadObjAbility();
    }

    protected override void Start()
    {
        base.Start();
        UpdateDisguise();
    }

    private void LoadPlayer()
    {
        if (player != null) return;
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }

    private void LoadObjAbility()
    {
        if (objAbility != null) return;
        objAbility = transform.parent.gameObject;
        Debug.LogWarning(transform.name + ": LoadObjAbility", gameObject);

    }

    public void Activate()
    {
        if (isInvisible)
        {
            StopAllCoroutines();
            StartCoroutine(StarPlayer());
        }
        if (!canUseAbility) return;
        StartCoroutine(StartInvisible());
    }

    private IEnumerator StartInvisible()
    {
        canUseAbility = false;
        
        var invisUI = UIBottomRight.Instance.BtnInvisible.GetComponent<UICooldown>();
        
        ChangeLog();
        invisUI.StartActive(_logControlTime);   // Thanh active chạy
        yield return new WaitForSeconds(_logControlTime);

        ChangePlayer();
        invisUI.StartCooldown(_cooldownTime);   // Overlay hồi chiêu
        yield return new WaitForSeconds(_cooldownTime);
        canUseAbility = true;
    }

    private IEnumerator StarPlayer()
    {
        var invisUI = UIBottomRight.Instance.BtnInvisible.GetComponent<UICooldown>();
        
        invisUI.StopActive();
        ChangePlayer();
        invisUI.StartCooldown(_cooldownTime);
        yield return new WaitForSeconds(_cooldownTime);
        canUseAbility = true;
    }

    private void ChangeLog()
    {
        isInvisible = true;
        List<Collider2D> detectedAttack = objAbility.GetComponentInChildren<PlayerAttack>().DetectedAttack;
        detectedAttack.Clear();
        logInstance.transform.position = player.transform.position;
        logInstance.SetActive(true);
        AudioManager.Instance.PlaySfx("Disguise");
        vCam.Follow = logInstance.transform;
        objAbility.transform.SetParent(logInstance.transform);
        foreach (GameObject btn in btns)
            btn.GetComponent<Button>().interactable = false;

        player.SetActive(false);
    }

    private void ChangePlayer()
    {
        isInvisible = false;
        List<Collider2D> detectedAttack = objAbility.GetComponentInChildren<PlayerAttack>().DetectedAttack;
        detectedAttack.Clear();
        player.transform.position = logInstance.transform.position;
        player.SetActive(true);
        AudioManager.Instance.PlaySfx("Disguise");
        vCam.Follow = player.transform;
        objAbility.transform.SetParent(player.transform);

        foreach (GameObject btn in btns)
            btn.GetComponent<Button>().interactable = true;
        
        logInstance.SetActive(false);
    }
    
    private void UpdateDisguise()
    {
        _invisibleLevel = PlayerPrefs.GetInt(PlayerPrefsString.SkillLevel_ + 2);

        _logControlTime = logControlTimeBase;
        _cooldownTime = cooldownTimeBase;

        for (int i = 0; i <= _invisibleLevel; i++)
        {
            if (i is 1 or 3)
                _cooldownTime -= 1;
            if (i == 5)
                _cooldownTime -= 2;
            if (i is 2 or 4)
                _logControlTime += 2;
        }
    }
}
