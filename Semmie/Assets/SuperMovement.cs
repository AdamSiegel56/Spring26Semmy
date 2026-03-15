using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class SuperMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private Vector2 _moveDirection;
    private Vector2 rightLookDirection;
    private Vector2 leftLookDirection;
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask layerCheck;
    public LayerMask ignoreLayer;
    public GameObject landParticle;

    private bool canSpawnParticle;

    public GameObject rightAimReticle;
    public GameObject leftAimReticle;

    

    public LineRenderer leftLR;
    public LineRenderer rightLR;

    public float aimOffset;

    public float pushForce;

    [Header("Values")]
    public float acceleration;
    public float speed;
    public float jumpSpeed;
    public float groundDecay;
    public float airSpeed;

    [Header("PushPull")]
    public bool canPush;
    public bool pushLocked;
    public float pushCurrReload;
    public float pushReloadNeeded;
    public float pushReloadSpeed;
    public float pushMaxDistance;

    public bool canPull;
    public float pullCurrReload;
    public float pullReloadNeeded;
    public float pullReloadSpeed;
    public float pullMaxDistance;

    public Color fullColor;
    public Color notColor;


    public Color red;
    public Color blue;

    [Header("Controls")]
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference look;
    public InputActionReference push;
    public InputActionReference pull;


    private void OnEnable()
    {
        EventBus<LockPushEvent>.OnEvent += LockPush;
    }
    private void OnDisable()
    {
        EventBus<LockPushEvent>.OnEvent -= LockPush;
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        CheckLanding();
        LookWithReticle();
        if(!pushLocked)
        {
            Push();
        }
        Pull();
        CheckForReticleLine();
        //LineColorSet();
    }


    public void FixedUpdate()
    {
        CheckGround();
        ApplyFriction();
        UpdateValues();
    }

    public void LineColorSet()
    {
        if (canPush)
        {
            rightLR.endColor = red;
        }
        else
        {
            rightLR.endColor = Color.white;
        }
    }

    public void LookWithReticle()
    {
        //Aims reticle in direction, and rotates it away from player
        //(right stick)
        rightAimReticle.transform.localPosition = rightLookDirection * aimOffset;
        Vector2 r1_direction = rightAimReticle.transform.position - transform.position;
        float r1_angle = Mathf.Atan2(r1_direction.y, r1_direction.x) * Mathf.Rad2Deg;
        rightAimReticle.transform.rotation = Quaternion.Euler(0, 0, r1_angle - 90);

        RaycastHit2D rHit = Physics2D.Raycast(rightAimReticle.transform.position, rightLookDirection, pushMaxDistance, layerCheck);
        RaycastHit2D rIgnore = Physics2D.Raycast(rightAimReticle.transform.position, rightLookDirection, pushMaxDistance, ignoreLayer);
        
        Debug.DrawLine(rightAimReticle.transform.position, rHit.point);

        Debug.DrawLine(rightAimReticle.transform.position, rHit.point);
        

        if (rHit)
        {
            canPush = true;
            rightAimReticle.transform.GetComponentInChildren<SpriteRenderer>().color = fullColor;
            rightLR.SetPosition(1, rHit.point);
            rightLR.endColor = red;
        }
        else
        {
            canPush = false;
            rightAimReticle.transform.GetComponentInChildren<SpriteRenderer>().color = notColor;
        }

        if(rIgnore)
        {
            canPush = false;
            rightAimReticle.transform.GetComponentInChildren<SpriteRenderer>().color = notColor;
            rightLR.SetPosition(1, rIgnore.point);
            rightLR.endColor = notColor;
        }


        if(!rIgnore && !rHit)
        {
            rightLR.SetPosition(1, rightAimReticle.transform.position + new Vector3(pushMaxDistance * rightLookDirection.x, pushMaxDistance * rightLookDirection.y, 0));
            rightLR.endColor = Color.white;
        }

        //(left stick)
        leftAimReticle.transform.localPosition = leftLookDirection * aimOffset;
        Vector2 r2_direction = leftAimReticle.transform.position - transform.position;
        float r2_angle = Mathf.Atan2(r2_direction.y, r2_direction.x) * Mathf.Rad2Deg;
        leftAimReticle.transform.rotation = Quaternion.Euler(0, 0, r2_angle - 90);

        RaycastHit2D lHit = Physics2D.Raycast(leftAimReticle.transform.position, leftLookDirection, pullMaxDistance, layerCheck);
        RaycastHit2D lIgnore = Physics2D.Raycast(leftAimReticle.transform.position, leftLookDirection, pullMaxDistance, ignoreLayer);
        Debug.DrawLine(leftAimReticle.transform.position, lHit.point);
        if (lHit)
        {
            canPull = true;
            leftAimReticle.transform.GetComponentInChildren<SpriteRenderer>().color = fullColor;
            leftLR.SetPosition(1, lHit.point);
            leftLR.endColor = blue;
        }
        else
        {
            canPull = false;
            leftAimReticle.transform.GetComponentInChildren<SpriteRenderer>().color = notColor;
        }
        if (lIgnore)
        {
            canPull = false;
            leftAimReticle.transform.GetComponentInChildren<SpriteRenderer>().color = notColor;
            leftLR.SetPosition(1, lIgnore.point);
            leftLR.endColor = notColor;
        }

        if (!lIgnore && !lHit)
        {
            leftLR.SetPosition(1, leftAimReticle.transform.position + new Vector3(pullMaxDistance * leftLookDirection.x, pullMaxDistance * leftLookDirection.y, 0));
            leftLR.endColor = Color.white;
        }

    }
    private Tween PushRotateTween;
    public void Push()
    {
        if (push.action.triggered && pushCurrReload >= pushReloadNeeded && canPush)
        {
            pushCurrReload = 0f;

            Vector2 aimingDirection;
            aimingDirection = -(rightAimReticle.transform.position - gameObject.transform.position).normalized;

            PushRotateTween?.Kill();
            rightAimReticle.transform.GetChild(0).transform.localRotation = Quaternion.identity;
            PushRotateTween = rightAimReticle.transform.GetChild(0).transform.DOLocalRotate(new Vector3(0, 0, 360), pushReloadSpeed, RotateMode.FastBeyond360);

            rb2D.AddForce(aimingDirection * pushForce, ForceMode2D.Impulse);
        }
    }
    private Tween PullRotateTween;
    public void Pull()
    {
        if (pull.action.triggered && pullCurrReload >= pullReloadNeeded && canPull)
        {
            pullCurrReload = 0f;

            Vector2 aimingDirection;
            aimingDirection = (leftAimReticle.transform.position - gameObject.transform.position).normalized;

            PullRotateTween?.Kill();
            leftAimReticle.transform.GetChild(0).transform.localRotation = Quaternion.identity;
            PullRotateTween = leftAimReticle.transform.GetChild(0).transform.DOLocalRotate(new Vector3(0, 0, 360), pullReloadSpeed, RotateMode.FastBeyond360);

            rb2D.AddForce(aimingDirection * pushForce, ForceMode2D.Impulse);
        }
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
    public void UpdateValues()
    {
        if(pushCurrReload <= pushReloadNeeded)
        {
            pushCurrReload += Time.deltaTime * pushReloadSpeed;
        }
        if(pullCurrReload <= pullReloadNeeded)
        {
            pullCurrReload += Time.deltaTime * pullReloadSpeed;
        }
    }
    public void GetInput()
    {
        leftLookDirection = move.action.ReadValue<Vector2>();
        rightLookDirection = look.action.ReadValue<Vector2>();

    }
    
    public void CheckForReticleLine()
    {
        if (leftLookDirection == Vector2.zero)
        {
            leftLR.SetPosition(1, leftAimReticle.transform.position);
            leftLR.SetPosition(0, leftAimReticle.transform.position);
        }
        else
        {
            leftLR.SetPosition(0, leftAimReticle.transform.position);
        }

        if (rightLookDirection == Vector2.zero)
        {
            Debug.Log("VECZERO");
            
            rightLR.SetPosition(1, rightAimReticle.transform.position);
            rightLR.SetPosition(0, rightAimReticle.transform.position);
        }
        else
        {
            rightLR.SetPosition(0, rightAimReticle.transform.position);
        }
    }
    
    public void ApplyFriction()
    {
        if (grounded && (Mathf.Abs(_moveDirection.x) == 0))
        {
            rb2D.linearVelocity *= groundDecay;
        }
    }
    
    //Lock Push, Lock Pull

    public void LockPush(LockPushEvent evt)
    {
        pushLocked = true;
    }


}
