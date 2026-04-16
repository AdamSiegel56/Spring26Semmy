using TMPro;
using UnityEngine;

public class GetTime : MonoBehaviour
{

    private TimeManager timeManager;

    public TextMeshProUGUI tpro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tpro.text = TimeManager.Instance.timeToSave;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
