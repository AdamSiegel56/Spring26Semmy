using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeInGame;
    public TextMeshProUGUI timerText; // Link your UI text here

    void Update()
    {
        timeInGame += Time.deltaTime;
        UpdateDisplay(timeInGame);
    }

    void UpdateDisplay(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{00}:{1:00}", minutes, seconds);
    }

}
