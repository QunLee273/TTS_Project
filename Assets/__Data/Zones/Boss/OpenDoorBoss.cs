using __Data;
using UnityEngine;

public class OpenDoorBoss : DoorBoss
{
    [SerializeField] private float openHeight = 3f;
    private bool _isOpening = false;
    private Vector3 _closedPosition;
    private Vector3 _openPosition;

    protected override void Start()
    {
        base.Start();
        _closedPosition = door.position;
        _openPosition = _closedPosition + new Vector3(0, openHeight, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isOpening = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isOpening = false;
    }

    private void Update()
    {
        if (_isOpening)
        {
            door.position = Vector3.MoveTowards(door.position, _openPosition, speed * Time.deltaTime);
        }
    }
}
