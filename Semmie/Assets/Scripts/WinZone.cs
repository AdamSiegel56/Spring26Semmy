using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    public Timer gameTimer;

    public TextMeshPro timeText;
    private TimeManager timeManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") { return; }

        timeText.text = gameTimer.timerText.text;
        TimeManager.Instance.timeToSave = gameTimer.timerText.text;
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("TitleScene");
    }
}
