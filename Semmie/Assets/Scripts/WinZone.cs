using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") { return; }

        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("TitleScene");
    }
}
