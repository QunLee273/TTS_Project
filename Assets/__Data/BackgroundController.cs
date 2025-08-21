using __Data;
using UnityEngine;

public class BackgroundController : GameBehaviour
{
    private float _posStart;
    [SerializeField] protected GameObject cam;
    [SerializeField] protected float parallaxEffect;
    
    protected override void Start()
    {
        base.Start();
        _posStart = transform.position.x;
    }
    
    protected void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        
        transform.position = new Vector3(_posStart + distance, transform.position.y, transform.position.z);
    }
}
