using Unity.VisualScripting;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public BoxCollider2D standingGround;
    public BoxCollider2D triggerFromBottom;

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
        standingGround.enabled = true;
    }

}
