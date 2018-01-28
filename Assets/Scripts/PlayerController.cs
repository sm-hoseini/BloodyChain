using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool canControllPlayer = true;
    public bool canAim = true;
    public static bool isPlayerAlive = true;
    public LayerMask IgnoreLayer;

    public int maxLife = 3;
    public int currentLife = 0;
    public float damageCoolDown = 0.5f;
    public static bool canRecieveDamage = true;

    public float moveForce = 10;
    public float maxMoveSpeed = 10;

    public float jumpForce = 300;
    public float maxJumpSpeed = 15;

    public float hookForce = 2;
    public float hookThrowSpeed = 2000;
    public float maxHookLenth = 20;

    Rigidbody2D myRigid;

    bool facingRight = false;

    public Transform weapon;
    public Transform firePlace;

    public Transform groundCheck;
    public LayerMask groundCheckLayerMask;
    public Transform bullet;
    public Rigidbody2D hookRb;

    public bool grounded = false;
    float groundRadious = 0.2f;

    float fixedY;
    bool isHooKing = false;

    bool hookHasTarget = false;
    public ChainCreator Chain;

    int playerVerticalAxis = 0;
    private DistanceJoint2D distanceJoint;
    Vector3 hookedPoint;
    Vector3 hookedPos;
    public AudioClip walk,shoothook;
    Vector3 mousePosition;

    Animator anim;

    bool isRunningOnGround = false;
    void Awake()
    {
        isPlayerAlive = true;
        myRigid = GetComponent<Rigidbody2D>();
        fixedY = transform.position.y;
        currentLife = maxLife;
        distanceJoint = gameObject.GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
    }


    void OnEnable()
    {
        ActionManager.OnPlayerHit += RecieveDamage;
        ActionManager.OnGamePaused += OnGamePaused;
        ActionManager.OnGameResumed += OnGameResumed;
        ActionManager.OnEnvironmentChange += OnEnvironmentChange;
    }

    void OnDisable()
    {
        ActionManager.OnPlayerHit -= RecieveDamage;
        ActionManager.OnGamePaused -= OnGamePaused;
        ActionManager.OnGameResumed -= OnGameResumed;
        ActionManager.OnEnvironmentChange -= OnEnvironmentChange;
    }

    void Start()
    {
        Statics.Emmet = this.transform;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (canControllPlayer)
        {

            float moveAxis = Input.GetAxis("Horizontal");
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                myRigid.AddForce(new Vector3(moveAxis * moveForce, 0, 0));
            }

            if (Physics2D.OverlapCircle(groundCheck.position, groundRadious, groundCheckLayerMask))
            {
                if (!grounded)
                {
                    ActionManager.OnPlayerHitGround();
                    ActionManager.OnPlayerIsInGround(true);
                    grounded = true;
                }
            }
            else
            {
                if (grounded)
                {
                    ActionManager.OnPlayerIsInGround(false);
                    grounded = false;
                }
            }

            if (grounded && Mathf.Abs(myRigid.velocity.x) > 0.1f)
            {
                if (!isRunningOnGround)
                {
                    ActionManager.OnPlayerStartRun();
                    isRunningOnGround = true;
                }
            }
            else
            {
                if (isRunningOnGround)
                {
                    ActionManager.OnPlayerEndRun();
                    isRunningOnGround = false;
                }
            }
            //print(Mathf.Abs(myRigid.velocity.z));

            if (Mathf.Abs(myRigid.velocity.y) < 0.1f)
            {
                if (playerVerticalAxis != 0)
                {
                    playerVerticalAxis = 0;
                    ActionManager.PlayerAirCondition(0);
                }
            }
            else
            {
                if (myRigid.velocity.y > 0)
                {
                    if (playerVerticalAxis != 1)
                    {
                        playerVerticalAxis = 1;
                        ActionManager.PlayerAirCondition(1);
                    }
                }
                else
                {
                    if (playerVerticalAxis != -1)
                    {
                        playerVerticalAxis = -1;
                        ActionManager.PlayerAirCondition(-1);
                    }
                }
            }

        }
    }
    Vector3 lastHookPose;
    public GameObject remainigLight;
    void Update()
    {
        if (canControllPlayer)
        {
            if (canAim)
            {
                Aiming();
            }

            if (grounded && Input.GetKeyDown(KeyCode.Space))
            {
                myRigid.AddForce(new Vector3(0, jumpForce, 0));
                ActionManager.OnPlayerJump();
            }

            if (isHooKing && Input.GetKeyDown(KeyCode.Space))
            {
                ReleaseHook();
                myRigid.AddForce(new Vector3(0, jumpForce, 0));
            }


            if (Input.GetMouseButtonDown(1))
            {
                hookedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hookedPos.z = 0;
                StartCoroutine("Hooking");
                ActionManager.OnPlayerHookStart();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                ReleaseHook();
            }

            if (Input.GetMouseButtonDown(0))
            {
               // Shoot();
            }


            if (isHooKing)
            {
                Hook();
            }

            myRigid.velocity = new Vector3(Mathf.Clamp(myRigid.velocity.x, -maxMoveSpeed, maxMoveSpeed), myRigid.velocity.y, Mathf.Clamp(myRigid.velocity.y, -maxJumpSpeed, maxJumpSpeed));
        }
    }


    void ReleaseHook()
    {
        if (isHooKing)
        {
            GameObject obj =Instantiate(remainigLight, lastHookPose+Vector3.forward*-.91f, Quaternion.identity) as GameObject;
            obj.SetActive(true);
        }

        StopCoroutine("Hooking");
        distanceJoint.enabled = false;
        Chain.DisableChain();
        hookRb.gameObject.SetActive(false);
        myRigid.gravityScale = 1;
        isHooKing = false;
        ActionManager.OnPlayerHookEnd();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Hook()
    {
        if (Input.GetMouseButton(1))
        {
            //myRigid.gravityScale = 0;
            Vector3 forceAxis = (hookedPoint - transform.position);
           // float distance = Vector3.Distance(hookedPoint, transform.position);
          //  myRigid.AddForce(forceAxis * hookForce * Mathf.Clamp((distance / 2), 1, 100));
        }
        /*
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            myRigid.AddForce(new Vector3(hAxis * airMoveForce, 0, 0));
        }


        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            myRigid.AddForce(new Vector3(0, 0, vAxis * airMoveForce));
        }*/
    }

    IEnumerator Hooking()
    {
        hookHasTarget = false;

        hookedPos.z = 0;

        hookRb.velocity = Vector3.zero;

        hookRb.transform.position = transform.position - new Vector3(0, 0.5f, 0);

        Vector3 rightPos;
        if (hookedPos.x > transform.position.x)
        {
            rightPos = Vector3.right;
        }
        else
        {
            rightPos = Vector3.right*-1;
        }


        hookRb.gameObject.SetActive(true);
        Vector3 forcePos = (hookedPos - transform.position).normalized;
        print("forcePos " + forcePos);
        forcePos.z = 0;
        hookRb.AddForce(forcePos * hookThrowSpeed);

        Ray2D ray = new Ray2D(transform.position, forcePos*100);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, forcePos*100,100,IgnoreLayer);
        if (hit && hit.transform.tag == "hookable")
        {

            lastHookPose = hit.point;
            hookedPoint = new Vector3(hit.point.x, hit.point.y, 0);
            hookHasTarget = true;
        }
        float initdistance = 0;
        while (Vector3.Distance(hookRb.transform.position, transform.position) < maxHookLenth)
        {
            if (hookHasTarget && Vector2.Distance(new Vector2(hookRb.position.x, hookRb.position.y), new Vector2(hookedPoint.x, hookedPoint.y)) < 1)
            {
                if (!isHooKing)
                {
                    distanceJoint.connectedAnchor = hit.point;
                    distanceJoint.enabled = true;
                    initdistance=  Vector3.Distance(hookRb.transform.position, transform.position) / 2;

                    hookRb.transform.position = hookedPoint;
                    hookRb.velocity = Vector3.zero;
                    hookRb.gravityScale = 0;
                    isHooKing = true;
                    ActionManager.OnPlayerHooked();
                }
                else {
                    distanceJoint.distance = Mathf.Lerp(distanceJoint.distance, initdistance, Time.deltaTime *2f);
                }
                
            }

            Chain.Create(hookRb.transform.position, transform.position, fixedY);
            yield return null;
        }

        ReleaseHook();
    }

    void Aiming()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 objectPos = transform.position;
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        mousePos.z = 0;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //if (angle > 90)
        //{
        //    weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (angle )));
        //}
        //else
        //{
        //    weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0,(angle )));
        //}
        weapon.LookAt(mousePos);
        if (angle < 90 && !facingRight)
            Flip();

        else if (angle > 90 && facingRight)
            Flip();
    }

    public void RecieveDamage()
    {
        if (canRecieveDamage)
        {
            currentLife--;
            canRecieveDamage = false;

            StopCoroutine("ResetDamageCoolDown");
            StartCoroutine("ResetDamageCoolDown");

            if (currentLife <= 0)
            {
                Die();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.layer+ "     " + groundCheckLayerMask);

       print((collision.gameObject.layer == groundCheckLayerMask )+ "  " + !isHooKing + "  " + (collision.relativeVelocity.y > 18));
        if (collision.gameObject.layer == 8 && !isHooKing && collision.relativeVelocity.y > 18)
        {
            Die();
        }
    } 
    IEnumerator ResetDamageCoolDown()
    {
        yield return new WaitForSeconds(damageCoolDown);
        canRecieveDamage = true;
    }

    void Die()
    {
        //TODO GameOver
        //TODO Play DeadAnimation
        //myRigid.freezeRotation = false;
        //myRigid.AddForce(new Vector3(100, 60, -90)*100);

        ReleaseHook();
        canRecieveDamage = false;
        isPlayerAlive = false;
        ActionManager.OnPlayerDie();
        canControllPlayer = false;
    }

    void Shoot()
    {
        Instantiate(bullet, firePlace.position - new Vector3(0, 0.5f, 0), firePlace.rotation);
        ActionManager.OnPlayerShoot();
    }

    void OnGamePaused()
    {
        canControllPlayer = false;
    }

    void OnGameResumed()
    {
        canControllPlayer = true;
    }

    void OnEnvironmentChange()
    {
        ReleaseHook();
    }
}
