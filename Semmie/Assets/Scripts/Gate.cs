using UnityEngine;

enum GateType
{
    LockPush,
    LockPull
}

public class Gate : MonoBehaviour
{
    [SerializeField] private GateType gate;

    private void OnEnable()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") { return; }
        
        switch (gate)
        {
            case GateType.LockPush:
                EventBus<LockPushEvent>.Raise(new LockPushEvent());
                break;
            case GateType.LockPull:
                EventBus<LockPullEvent>.Raise(new LockPullEvent());
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag != "Player") { return; }
        switch (gate)
        {
            case GateType.LockPush:
                EventBus<UnlockPushEvent>.Raise(new UnlockPushEvent());
                break;
            case GateType.LockPull:
                EventBus<UnlockPullEvent>.Raise(new UnlockPullEvent());
                break;
        }
    }

}

