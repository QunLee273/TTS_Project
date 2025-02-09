using __Data;
using UnityEngine;

public class ObjFly : GameBehaviour
{
    [SerializeField] protected float moveSpeed = 1f;
    public Vector3 direction;

    void Update()
    {
        transform.parent.Translate(direction * (moveSpeed * Time.deltaTime));
    }
}
