using __Data;
using UnityEngine;
using UnityEngine.Serialization;

public class ZoneActiveBoss : GameBehaviour
{
    [SerializeField] protected GameObject boss;
    [SerializeField] protected GameObject wallBoss;
    [SerializeField] protected Collider2D colliderActive;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWallBoss();
        LoadColliderActive();
    }

    protected override void Start()
    {
        base.Start();
        boss.SetActive(false);
    }

    private void LoadWallBoss()
    {
        if (wallBoss != null) return;
        wallBoss = transform.Find("WallBoss").gameObject;
        wallBoss.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadWallBoss", gameObject);
    }
    
    private void LoadColliderActive()
    {
        if (colliderActive != null) return;
        colliderActive = transform.GetComponent<Collider2D>();
        Debug.LogWarning(transform.name + ": LoadColliderActive", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            boss.SetActive(true);
            UIMapBoss.Instance.HpBar.gameObject.SetActive(true);
            wallBoss.SetActive(true);
            colliderActive.enabled = false;
        }
    }
}
