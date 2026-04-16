using UnityEngine;

public class resetOneWay : MonoBehaviour
{
    public BoxCollider2D standingGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        standingGround.enabled = false;
    }
}
