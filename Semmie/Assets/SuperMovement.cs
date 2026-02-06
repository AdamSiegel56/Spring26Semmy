using UnityEngine;
using UnityEngine.InputSystem;

public class SuperMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
    private Vector2 _lookDirection2;
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask layerCheck;
    public GameObject landParticle;

    private bool canSpawnParticle;

    public GameObject aimReticle;
    public GameObject aimReticle2;
    public float aimOffset;

    public float pushForce;

    [Header("Values")]
    public float acceleration;
    public float speed;
    public float jumpSpeed;
    public float groundDecay;
    public float airSpeed;

    [Header("Controls")]
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference look;
    public InputActionReference push;
    public InputActionReference pull;

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
        FunnyLook();
        Push();
        Pull();
        Debug.Log(_lookDirection);

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
        _lookDirection2 = move.action.ReadValue<Vector2>();
        _lookDirection = look.action.ReadValue<Vector2>();

    }
    public void MoveWithInput()
    {
        if(!grounded && Mathf.Abs(_moveDirection.x) > 0)
        {
            rb2D.linearVelocity = new Vector2(_moveDirection.x*airSpeed, rb2D.linearVelocity.y);
        }
        else if(!grounded && Mathf.Abs(_moveDirection.x) == 0)
        {
            //rb2D.linearVelocity = new Vector2(0f, rb2D.linearVelocity.y);
        }

        if (Mathf.Abs(_moveDirection.x) > 0)
        {
            float increment = _moveDirection.x * acceleration;
            float newSpeed = Mathf.Clamp(rb2D.linearVelocity.x + increment, -speed, speed);

            rb2D.linearVelocity = new Vector2(newSpeed, rb2D.linearVelocity.y);
        }
    }  
    public void FunnyLook()
    {
        aimReticle.transform.localPosition = _lookDirection * aimOffset;
        aimReticle2.transform.localPosition = _lookDirection2 * aimOffset;
    }

    public void HandleJump()
    {
        if (jump.action.triggered && grounded)
        {
            Debug.Log("JUMP TRY");
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
    public void Push()
    {
        if(push.action.triggered)
        {
            Vector2 aimingDirection;
            aimingDirection = -(aimReticle.transform.position - gameObject.transform.position).normalized;

            rb2D.AddForce(aimingDirection * pushForce, ForceMode2D.Impulse);
        }
    }
    public void Pull()
    {
        if(pull.action.triggered)
        {
            Vector2 aimingDirection;
            aimingDirection = (aimReticle2.transform.position - gameObject.transform.position).normalized;

            rb2D.AddForce(aimingDirection * pushForce, ForceMode2D.Impulse);
        }
    }

}
