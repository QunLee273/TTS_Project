using System.Collections;
using System.Collections.Generic;
using System.Linq;
using __Data;
using UnityEngine;

public class SpearsTrap : GameBehaviour
{
    [SerializeField] protected List<Transform> prefabs = new List<Transform>();
    public float riseSpeed = 10f;
    public float spearHeight = 2.5f;
    public float wobbleHeight = 1f;
    public float delayBetweenSpears = 0.2f;
    private bool _isActivated;

    protected override void Start()
    {
        base.Start();
        LoadPrefabs();
    }

    protected virtual void LoadPrefabs()
    {
        if (prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        if (prefabObj)
        {
            foreach (Transform prefab in prefabObj)
            {
                prefabs.Add(prefab);
            }
        }
        Debug.LogWarning(transform.name + ": LoadPrefabs", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isActivated) return;
        if (other.CompareTag("Player"))
        {
            _isActivated = true;
            StartCoroutine(WobbleAndRaiseSpikes());
        }
    }

    private IEnumerator WobbleAndRaiseSpikes()
    {
        Transform farthestSpike = prefabs
            .OrderByDescending(s => s.position.x)
            .FirstOrDefault();

        if (farthestSpike == null)
            yield break;

        Vector3 originalPos = farthestSpike.position;
        Vector3 wobblePos = originalPos + Vector3.up * wobbleHeight;

        const int wobbleCount = 3;
        for (int i = 0; i < wobbleCount; i++)
        {
            yield return MoveSpike(farthestSpike, wobblePos, 0.2f);
            yield return MoveSpike(farthestSpike, originalPos, 0.2f);
        }

        float targetY = farthestSpike.position.y + spearHeight;

        yield return StartCoroutine(RaiseSpear(farthestSpike, targetY));

        List<Transform> sortedSpears = prefabs
            .OrderByDescending(s => s.position.x)
            .ToList();

        foreach (Transform spear in sortedSpears)
        {
            if (spear == farthestSpike) continue;
            StartCoroutine(RaiseSpear(spear, targetY));
            yield return new WaitForSeconds(delayBetweenSpears);
        }
    }

    private IEnumerator RaiseSpear(Transform spear, float targetY)
    {
        while (spear.position.y < targetY)
        {
            spear.position += Vector3.up * (riseSpeed * Time.deltaTime);
            yield return null;
        }
        Vector3 pos = spear.position;
        pos.y = targetY;
        spear.position = pos;
    }

    private IEnumerator MoveSpike(Transform spike, Vector3 targetPos, float duration)
    {
        Vector3 startPos = spike.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            spike.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        spike.position = targetPos;
    }
}
