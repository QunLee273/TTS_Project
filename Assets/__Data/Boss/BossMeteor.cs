using System;
using System.Collections;
using __Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMeteor : GameBehaviour
{
    public GameObject meteorPrefab;
    public Transform spawnArea;
    public float spawnHeight = 15f;
    public float spawnRate = 0.5f;
    public int meteorCount = 15;
    public float minSpeed = 5f;
    public float maxSpeed = 10f;

    public float timer = 0;
    public float delay = 10f;

    private void Update()
    {
        if (timer < delay)
        {
            timer += Time.deltaTime;
            return;
        }
        if (timer >= delay)
        {
            timer = 0;
            StartMeteorRain();
        }
    }

    public void StartMeteorRain()
    {
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
        float offsetX = Random.Range(-30f, 30f);
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
