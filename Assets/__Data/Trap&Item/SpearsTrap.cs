using System.Collections;
using System.Collections.Generic;
using __Data;
using UnityEngine;

public class SpearsTrap : GameBehaviour
{
    [SerializeField] protected List<Transform> prefabs;
    public float riseSpeed = 10f;
    public float spearHeight = 2.5f;
    public float wobbleHeight = 1f;
    public float delayBetweenSpears = 0.2f;
    private int _farthestIndex;
    private bool _isActivated;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPrefabs();
    }

    protected virtual void LoadPrefabs()
    {
        if (prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            prefabs.Add(prefab);
        }
        Debug.LogWarning(transform.name + ": LoadPrefabs", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isActivated && other.CompareTag("Player"))
        {
            _isActivated = true;
            StartCoroutine(WobbleAndRaiseSpikes());
        }
    }

    private IEnumerator WobbleAndRaiseSpikes()
    {
        float maxX = float.MinValue;
        for (int i = 0; i < prefabs.Count; i++)
        {
            if (prefabs[i].position.x > maxX)
            {
                maxX = prefabs[i].position.x;
                _farthestIndex = i;
            }
        }

        Transform farthestSpike = prefabs[_farthestIndex];

        Vector3 originalPos = farthestSpike.position;
        Vector3 wobblePos = originalPos + new Vector3(0, wobbleHeight, 0);

        for (int i = 0; i < 3; i++)
        {
            yield return MoveSpike(farthestSpike, wobblePos, 0.2f);
            yield return MoveSpike(farthestSpike, originalPos, 0.2f);
        }

        List<Transform> sortedSpears = new List<Transform>(prefabs);
        sortedSpears.Sort((a, b) => b.position.x.CompareTo(a.position.x));
        
        foreach (Transform spear in sortedSpears)
        {
            StartCoroutine(RaiseSpear(spear));
            yield return new WaitForSeconds(delayBetweenSpears);
        }
    }

    private IEnumerator RaiseSpear(Transform spear)
    {
        Vector3 targetPosition = spear.position + new Vector3(0, spearHeight, 0);
        
        while (spear.position.y < targetPosition.y)
        {
            spear.position += Vector3.up * (riseSpeed * Time.deltaTime);
            yield return null;
        }

        spear.position = targetPosition;
    }

    private IEnumerator MoveSpike(Transform spike, Vector3 targetPos, float duration)
    {
        Vector3 startPos = spike.position;
        float elapsed = 0;
        while (elapsed < duration)
        {
            spike.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        spike.position = targetPos;
    }
}
