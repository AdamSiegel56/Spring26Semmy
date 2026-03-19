using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{


    public int doorNum;
    public bool isSuperDoor;
    public GameObject[] locks;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBus<OnCoinPickup>.OnEvent += DoorEvt;
        EventBus<AllCoinsAquired>.OnEvent += SuperDoor;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoorEvt(OnCoinPickup evt)
    {
        if(evt.coinNum == doorNum)
        {
            StartCoroutine(DoorOpen());
        }
        //Debug.Log(evt.coinNum - 1);
        if(isSuperDoor)
        {
            locks[evt.coinNum - 1].SetActive(false);
        }
    }
    public void SuperDoor(AllCoinsAquired evt)
    {
        if (!isSuperDoor) { return; }
        StartCoroutine(DoorOpen());
    }

    public IEnumerator DoorOpen()
    {
        gameObject.transform.DOScale(0, 2f);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    

}
