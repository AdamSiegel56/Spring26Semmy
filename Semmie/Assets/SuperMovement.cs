using UnityEngine;
using UnityEngine.InputSystem;

public class SuperMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private Vector2 _moveDirection;
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask layerCheck;
    public GameObject landParticle;

    private bool canSpawnParticle;

    [Header("Values")]
    public float acceleration;
    public float speed;
    public float jumpSpeed;
    public float groundDecay;
    public float airSpeed;

    [Header("Controls")]
    public InputActionReference move;
    public InputActionReference jump;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleJump();
        CheckLanding();
    }


    public void FixedUpdate()
    {
        MoveWithInput();
        CheckGround();
        ApplyFriction();
    }

    public void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, layerCheck).Length > 0;
    }
    public void CheckLanding()
    {
        if(!grounded)
        {
            canSpawnParticle = true;
        }

        if(grounded && canSpawnParticle)
        {
            GameObject particle = Instantiate(landParticle);
            particle.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - .5f);
            canSpawnParticle = false;
        }

    }

    public void GetInput()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
    }
    public void MoveWithInput()
    {
        if(!grounded && Mathf.Abs(_moveDirection.x) > 0)
        {

            rb2D.linearVelocity = new Vector2(_moveDirection.x*airSpeed, rb2D.linearVelocity.y);
        }

        if (Mathf.Abs(_moveDirection.x) > 0)
        {
            float increment = _moveDirection.x * acceleration;
            float newSpeed = Mathf.Clamp(rb2D.linearVelocity.x + increment, -speed, speed);

            rb2D.linearVelocity = new Vector2(newSpeed, rb2D.linearVelocity.y);
        }
    }  
    public void HandleJump()
    {
        if (jump.action.triggered && grounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpSpeed);
        }
    }
    public void ApplyFriction()
    {
        
        if (grounded && (Mathf.Abs(_moveDirection.x) == 0))
        {
            rb2D.linearVelocity *= groundDecay;
        }
    }
}
