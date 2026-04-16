using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level2");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
