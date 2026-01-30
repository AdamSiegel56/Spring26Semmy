using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CustomPlayerMovement : MonoBehaviour
{

    private Vector2 _moveDirection;
    private bool _jump;
    public Transform groundCheckTransform, wallCheckTransform;
    public LayerMask groundLayer;
    public bool isGrounded;
    public bool isWallAhead;

    [Header("Values")]
    public float horizontralSpeed;
    public float verticalSpeed;
    public float movementSpeed;
    public Vector2 movement;

    [Header("Jumping")]
    public float gravityStrength;
    public float gravityFloatiness;
    public float jumpHeight;


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
        CheckGround();
        CheckJumping();
        GetInput();
        MoveWithInput();
    }

    private void FixedUpdate()
    {
        
        
    }

    public void GetInput()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
       
    }

    public void MoveWithInput()
    {
        movement = new Vector2(_moveDirection.x, verticalSpeed);
        movement.x *= movementSpeed;
        transform.Translate(movement * Time.deltaTime);


    }

    public void CheckGround()
    {
        RaycastHit2D _hit = Physics2D.Raycast(groundCheckTransform.position, -Vector2.up, 0.2f, groundLayer);
        if(_hit.collider)
        {

            verticalSpeed = 0;
            isGrounded = true;

        }
        else
        {

            //verticalSpeed = Mathf.Lerp(verticalSpeed, gravityStrength, gravityFloatiness * Time.deltaTime);
            verticalSpeed -= gravityStrength * Time.deltaTime;
        }
    }

    public void CheckWall()
    {
        RaycastHit2D _hit = Physics2D.Raycast(wallCheckTransform.position, Vector2.right, 0.2f, groundLayer);
        if (_hit.collider)
        {
            isWallAhead = true;
            movement.x = 0;
        }
        else
        {
            isWallAhead = false;
        }
    }

    public void CheckJumping()
    {
        if(isGrounded && jump.action.WasPressedThisFrame())
        {
            Debug.Log("CUM");
            verticalSpeed = jumpHeight;
            isGrounded = false;
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(groundCheckTransform.position, -Vector2.up * 0.2f);
        Gizmos.DrawRay(wallCheckTransform.position, Vector2.right * 0.2f);
    }

}
