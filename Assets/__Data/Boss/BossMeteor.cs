using System;
using System.Collections;
using __Data;
using __Data.Script;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMeteor : AbilityAbstract
{
    [Header("Boss Meteor")]
    [SerializeField] protected GameObject meteorPrefab;
    [SerializeField] protected Transform spawnArea;
    [SerializeField] protected float spawnHeight = 15f;
    [SerializeField] protected float spawnRate = 0.3f;
    [SerializeField] protected int meteorCount = 25;
    [SerializeField] protected float minSpeed = 2f;
    [SerializeField] protected float maxSpeed = 7f;
    
    [SerializeField] protected float thresholdHp = 0.5f;
    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp;

    public float timer = 0;
    public float delay = 25f;
    
    protected override void Start()
    {
        base.Start();
        maxHp = BossReceiver.Instance.Lifes;
        timer = delay;
    }

    private void Update()
    {
        currentHp = BossReceiver.Instance.Lifes;
        float hpPercent = currentHp / maxHp;
        if (hpPercent > thresholdHp) return;
        if (timer < delay)
        {
            timer += Time.deltaTime;
            return;
        }
        if (timer >= delay)
        {
            timer = 0;
            animator.SetBool(AnimString.spells, true);
            animator.SetBool(AnimString.canMove, false);
            animator.SetBool(AnimString.atkMeteor, true);
        }
    }

    public void StartMeteorRain()
    {
        animator.SetBool(AnimString.spells, false);
        animator.SetBool(AnimString.canMove, true);
        animator.SetBool(AnimString.atkMeteor, false);
        StartCoroutine(SpawnMeteors());
    }

    private IEnumerator SpawnMeteors()
    {
        for (int i = 0; i < meteorCount; i++)
        {
            SpawnMeteor();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnMeteor()
    {
        float offsetX = Random.Range(-20f, 20f);
        Vector2 spawnPos = new Vector2(spawnArea.position.x + offsetX, spawnHeight);

        GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float angleDeg = Random.Range(190f, 350f);
            float angleRad = angleDeg * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;

            float speed = Random.Range(minSpeed, maxSpeed);
            rb.linearVelocity = direction * speed;
        }
    }

}
