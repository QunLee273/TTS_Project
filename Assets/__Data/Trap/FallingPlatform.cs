using System.Collections;
using __Data;
using UnityEngine;

public class FallingPlatform : GameBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float delayBeforeFall = 1f;
    [SerializeField] protected float resetTime = 2f;
    
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(delayBeforeFall);
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(resetTime);
        ResetPlatform();
    }

    void ResetPlatform()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = 0f;
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }
}
