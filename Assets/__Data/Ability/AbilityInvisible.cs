using System.Collections;
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

    [SerializeField] protected float logControlTime = 7f;
    [SerializeField] protected float cooldownTime = 5f;

    [SerializeField] protected bool canUseAbility = true;
    [SerializeField] protected bool isInvisible;

    public GameObject[] btns;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayer();
        LoadObjAbility();
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
        objAbility = player.transform.GetChild(3).gameObject;
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
        
        ChangeLog();

        yield return new WaitForSeconds(logControlTime);

        ChangePlayer();
        
        yield return new WaitForSeconds(cooldownTime);
        canUseAbility = true;
    }

    IEnumerator StarPlayer()
    {
        ChangePlayer();
        yield return new WaitForSeconds(cooldownTime);
        canUseAbility = true;
    }

    private void ChangeLog()
    {
        isInvisible = true;
        logInstance.transform.position = player.transform.position;
        logInstance.SetActive(true);
        vCam.Follow = logInstance.transform;
        objAbility.transform.SetParent(logInstance.transform);
        foreach (GameObject btn in btns)
            btn.GetComponent<Button>().interactable = false;

        player.SetActive(false);
    }

    private void ChangePlayer()
    {
        isInvisible = false;
        player.transform.position = logInstance.transform.position;
        player.SetActive(true);
        vCam.Follow = player.transform;
        objAbility.transform.SetParent(player.transform);

        foreach (GameObject btn in btns)
            btn.GetComponent<Button>().interactable = true;
        
        logInstance.SetActive(false);
    }
}
