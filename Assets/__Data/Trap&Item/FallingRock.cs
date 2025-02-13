using __Data;
using UnityEngine;

public class FallingRock : GameBehaviour
{
    public Rigidbody2D rb;
    protected override void Start()
    {
        base.Start();
        rb = transform.parent.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
