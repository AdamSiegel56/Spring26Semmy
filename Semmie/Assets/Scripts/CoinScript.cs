using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int coinTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") { return; }

        EventBus<OnCoinPickup>.Raise(new OnCoinPickup { coinNum = coinTag});
        StartCoroutine(CoinAnimation());
    }

    IEnumerator CoinAnimation()
    {
        gameObject.transform.DORotate(new Vector3(0, 720, 0), 1f, RotateMode.FastBeyond360);


        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

}
