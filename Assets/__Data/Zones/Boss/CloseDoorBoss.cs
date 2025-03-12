using __Data;
using UnityEngine;

public class CloseDoorBoss : DoorBoss
{
    private Vector3 _closedPosition;
    private bool _isClosing = false;

    

    protected override void Start()
    {
        base.Start();
        _closedPosition = door.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isClosing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            _isClosing = false;
    }

    private void Update()
    {
        if (_isClosing)
            door.position = Vector3.MoveTowards(door.position, _closedPosition, speed * Time.deltaTime);
    }
}
