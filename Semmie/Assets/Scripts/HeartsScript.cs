using UnityEngine;
using UnityEngine.UI;

public class HeartsScript : MonoBehaviour
{
    [SerializeField] GameObject[] heartObjects;

    public GameObject fullHeart;
    public PlayerManager playerManagerRef;
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void UpdateHearts()
    {
        switch(playerManagerRef.currentHealth)
        {
            case 0:
                heartObjects[0].GetComponent<Animator>().SetBool("Empty", true);
                heartObjects[1].GetComponent<Animator>().SetBool("Empty", true);
                heartObjects[2].GetComponent<Animator>().SetBool("Empty", true);
                break;
            case 1:
                heartObjects[0].GetComponent<Animator>().SetBool("Empty", false);
                heartObjects[1].GetComponent<Animator>().SetBool("Empty", true);
                heartObjects[2].GetComponent<Animator>().SetBool("Empty", true);
                break;
            case 2:

                heartObjects[0].GetComponent<Animator>().SetBool("Empty", false);
                heartObjects[1].GetComponent<Animator>().SetBool("Empty", false);
                heartObjects[2].GetComponent<Animator>().SetBool("Empty", true);
                break;
            case 3:
                heartObjects[0].GetComponent<Animator>().SetBool("Empty", false);
                heartObjects[1].GetComponent<Animator>().SetBool("Empty", false);
                heartObjects[2].GetComponent<Animator>().SetBool("Empty", false);
                break;
        }

        for(int i = 0; i< heartObjects.Length; i++)
        {
            
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHearts();
    }
}
