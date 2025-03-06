using System;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            PlayerController.Instance.TakeDamage();
    }
}
