using __Data;
using UnityEngine;

public class ObjFly : GameBehaviour
{
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected Rigidbody2D rbFly;
    public Vector3 direction;

    void Update()
    {
        rbFly.AddForce(direction * moveSpeed);
        // transform.parent.Translate(direction * (moveSpeed * Time.deltaTime));
    }
}
