using System;
using __Data;
using UnityEngine;

public class SawTrap : GameBehaviour
{
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float rotationSpeed = 500f;
    private Vector3 _target;

    protected override void Start()
    {
        base.Start();
        _target = pointA.position;
    }

    protected void Update()
    {
        transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
        
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _target) < 0.1f)
            _target = _target == pointA.position ? pointB.position : pointA.position;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            BulletSpawner.Instance.Despawn(other.transform);
            AudioManager.Instance.PlaySfx("Clash");
            string fxName = FXSpawner.impact1;

            Vector3 hitPos = other.transform.position;
            Transform fxImpact = FXSpawner.Instance.Spawn(fxName, hitPos, Quaternion.identity);
            fxImpact.gameObject.SetActive(true);
        }
    }
}
