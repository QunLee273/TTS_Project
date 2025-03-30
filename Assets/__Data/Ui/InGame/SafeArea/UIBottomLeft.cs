using __Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBottomLeft : GameBehaviour
{
    [SerializeField] protected GameObject btnMoveLeft;
    [SerializeField] protected GameObject btnMoveRight;
    
    [SerializeField] protected EventTrigger moveLeft;
    [SerializeField] protected EventTrigger moveRight;
    
    [SerializeField] protected PlayerMove playerMove;
    [SerializeField] protected DisguiseMovement disguiseMovement;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadBtnMoveLeft();
        LoadBtnMoveRight();
        LoadPlayerMove();
        LoadDisguiseMovement();
    }

    private void LoadBtnMoveLeft()
    {
        if (btnMoveLeft != null) return;
        btnMoveLeft = transform.Find("BtnMoveLeft").gameObject;
        moveLeft = btnMoveLeft.GetComponent<EventTrigger>();
        Debug.LogWarning(transform.name + ": LoadBtnMoveLeft", gameObject);
    }

    private void LoadBtnMoveRight()
    {
        if (btnMoveRight != null) return;
        btnMoveRight = transform.Find("BtnMoveRight").gameObject;
        moveRight = btnMoveRight.GetComponent<EventTrigger>();
        Debug.LogWarning(transform.name + ": LoadBtnMoveRight", gameObject);
    }

    private void LoadPlayerMove()
    {
        if (playerMove != null) return;
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerMove>();
        Debug.LogWarning(transform.name + ": LoadPlayerMove", gameObject);
    }

    private void LoadDisguiseMovement()
    {
        if (disguiseMovement != null) return;
        disguiseMovement = GameObject.Find("LogInvisible").GetComponentInChildren<DisguiseMovement>();
        Debug.LogWarning(transform.name + ": LoadDisguiseMovement", gameObject);
    }
    
    protected override void Start()
    {
        base.Start();
        SetupEventTrigger();
    }

    private void SetupEventTrigger()
    {
        AddEventTrigger(moveLeft, EventTriggerType.PointerDown, (_) => playerMove.OnPointerDownLeft());
        AddEventTrigger(moveRight, EventTriggerType.PointerDown, (_) => playerMove.OnPointerDownRight());
        
        AddEventTrigger(moveLeft, EventTriggerType.PointerUp, (_) => playerMove.OnPointerUpLeft());
        AddEventTrigger(moveRight, EventTriggerType.PointerUp, (_) => playerMove.OnPointerUpRight());
        
        AddEventTrigger(moveLeft, EventTriggerType.PointerDown, (_) => disguiseMovement.OnPointerDownLeft());
        AddEventTrigger(moveRight, EventTriggerType.PointerDown, (_) => disguiseMovement.OnPointerDownRight());
        
        AddEventTrigger(moveLeft, EventTriggerType.PointerUp, (_) => disguiseMovement.OnPointerUpLeft());
        AddEventTrigger(moveRight, EventTriggerType.PointerUp, (_) => disguiseMovement.OnPointerUpRight());
    }

    private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((data) => action(data));
        trigger.triggers.Add(entry);
    }
}
