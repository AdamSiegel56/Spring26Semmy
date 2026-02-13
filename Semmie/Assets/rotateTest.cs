using DG.Tweening;
using UnityEngine;

public class rotateTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360);
    }

    // Update is called once per frame
    void Update()
    {
        
        
            
       
    }
}
