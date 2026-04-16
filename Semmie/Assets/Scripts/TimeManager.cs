using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public string timeToSave;
    public static TimeManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
