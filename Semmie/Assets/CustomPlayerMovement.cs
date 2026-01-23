using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CustomPlayerMovement : MonoBehaviour
{

    private Vector2 _moveDirection;

    [Header("Values")]
    public float speed;
    

    [Header("Controls")]
    public InputActionReference move;
    public InputActionReference jump;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

    }

    private void FixedUpdate()
    {
        MoveWithInput();
    }

    public void GetInput()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
    }

    public void MoveWithInput()
    {
        if (Mathf.Abs(_moveDirection.x) > 0)
        {
            transform.Translate(_moveDirection);
        }
    }

}
