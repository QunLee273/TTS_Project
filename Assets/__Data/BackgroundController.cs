using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float _posStart;
    [SerializeField] protected GameObject cam;
    [SerializeField] protected float parallaxEffect;
    
    void Start()
    {
        _posStart = transform.position.x;
    }
    
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        
        transform.position = new Vector3(_posStart + distance, transform.position.y, transform.position.z);
    }
}
