using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerManager playerManager;

    public GameObject Flag1;
    public GameObject Flag2;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") { return; }

        playerManager.SetSpawnToCheckpoint(gameObject.transform.position);


        Flag1.SetActive(false);
        Flag2.SetActive(true);
    }

    
}
