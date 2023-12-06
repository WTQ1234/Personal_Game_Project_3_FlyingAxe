using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HRL;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doulbJumpSpeed;
    public float climbSpeed;
    public float restoreTime;
    public float rushSpeed;

    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;

    public bool isRush;
    public bool isGround;
    public bool canDoubleJump;
    public bool isOneWayPlatform;

    private bool isLadder;
    private bool isClimbing;

    private bool isJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;

    private float playerGravity;

    private PlayerInputActions controls;
    public Vector2 move;

    public CapsuleCollider2D capsuleCollider;
    public ParticleSystem particleSystem;

    void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.Move.performed += ctx => { 
            move = ctx.ReadValue<Vector2>(); 
            if (move.x == 0)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    move.x = -1;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    move.x = 1;
                }
            }
        };
        controls.GamePlay.Move.canceled += ctx => { move = Vector2.zero;
            if (move.x == 0)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    move.x = -1;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    move.x = 1;
                }
            }
        };

        InputManager.Instance.RegisterKeyboardAction(InputOccasion.Update, KeyCode.Space, ButtonType.Down, Jump);
        InputManager.Instance.RegisterMouseAction(InputOccasion.Update, 1, ButtonType.Down, RushReady);
        InputManager.Instance.RegisterMouseAction(InputOccasion.Update, 1, ButtonType.Up, Rush);
    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerGravity = myRigidbody.gravityScale;
    }

    private void FixedUpdate()
    {
        if (GameController.isGameAlive)
        {
            CheckAirStatus();
            Flip();
            Run();
            CheckGrounded();
            OneWayPlatformCheck();
        }
    }

    void Update()
    {
        if(GameController.isGameAlive)
        {
            
            //Flip();
            //Jump();
            //Climb();
            //Attack();
            //CheckGrounded();
            //CheckLadder();
            SwitchAnimation();
            //OneWayPlatformCheck();
        }
    }

    void Rush()
    {
        Time.timeScale = 1f;
        if (isRush)
        {
            return;
        }
        Player.Instance.screenZoomBlur.FadeOut();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        worldPos.z = 0;
        var dir = (worldPos - transform.position);
        var subDir2 = new Vector2(dir.x, dir.y).normalized;
        myRigidbody.velocity = subDir2 * rushSpeed;
        isRush = true;
        canDoubleJump = true;
        capsuleCollider.isTrigger = true;
        myRigidbody.gravityScale = 0f;
        Invoke("RestorePlayerRush", restoreTime);
        //particleSystem.Play();
    }

    void RushReady()
    {
        if (isRush)
        {
            return;
        }
        Time.timeScale = 0.5f;
        Player.Instance.screenZoomBlur.FadeIn();
    }

    void RestorePlayerRush()
    {
        isRush = false;
        capsuleCollider.isTrigger = false;
        myRigidbody.gravityScale = 1f;
    }

    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("DestructibleLayer")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }

    void CheckLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
        //Debug.Log("isLadder:" + isLadder);
    }

    void Flip()
    {
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(plyerHasXAxisSpeed)
        {
            if(myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        if (!isRush)
        {
            Vector2 playerVelocity = new Vector2(Mathf.Lerp(move.x * runSpeed, myRigidbody.velocity.x, 0.8f), myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
            bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnim.SetBool("Run", playerHasXAxisSpeed);
        }
    }

    void Jump()
    {
        if (isGround)
        {
            myAnim.SetBool("Jump", true);
            Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
            myRigidbody.velocity = Vector2.up * jumpVel;
            canDoubleJump = true;
            particleSystem.Play();
        }
        else
        {
            if (canDoubleJump)
            {
                myAnim.SetBool("DoubleJump", true);
                Vector2 doubleJumpVel = new Vector2(0.0f, doulbJumpSpeed);
                myRigidbody.velocity = Vector2.up * doubleJumpVel;
                canDoubleJump = false;
                particleSystem.Play();
            }
        }
    }

    void Climb()
    {
        float moveY = Input.GetAxis("Vertical");

        if(isClimbing)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveY * climbSpeed);
            canDoubleJump = false;
        }

        if (isLadder)        
        {            
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("Climbing", true);
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveY * climbSpeed);
                myRigidbody.gravityScale = 0.0f;
            }
            else
            {
                if (isJumping || isFalling || isDoubleJumping || isDoubleFalling)
                {
                    myAnim.SetBool("Climbing", false);
                }
                else
                {
                    myAnim.SetBool("Climbing", false);
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0.0f);
                    
                }
            }
        }
        else
        {
            myAnim.SetBool("Climbing", false);
            myRigidbody.gravityScale = playerGravity;
        }

        if (isLadder && isGround)
        {
            myRigidbody.gravityScale = playerGravity;
        }

        //Debug.Log("myRigidbody.gravityScale:"+ myRigidbody.gravityScale);
    }

    //void Attack()
    //{
    //    if(Input.GetButtonDown("Attack"))
    //    {
    //        myAnim.SetTrigger("Attack");
    //    }
    //}

    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if(myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if(isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }

    void OneWayPlatformCheck()
    {
        if (isGround && capsuleCollider.isTrigger && !isRush)
        {
            capsuleCollider.isTrigger = false;
        }
        float moveY = Input.GetAxis("Vertical");
        if (isOneWayPlatform && moveY < -0.1f)
        {
            capsuleCollider.isTrigger = true;
            Invoke("RestorePlayerLayer", restoreTime);
        }
    }

    void RestorePlayerLayer()
    {
        capsuleCollider.isTrigger = false;
    }

    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("Jump");
        isFalling = myAnim.GetBool("Fall");
        isDoubleJumping = myAnim.GetBool("DoubleJump");
        isDoubleFalling = myAnim.GetBool("DoubleFall");
        isClimbing = myAnim.GetBool("Climbing");
        //Debug.Log("isJumping:" + isJumping);
        //Debug.Log("isFalling:" + isFalling);
        //Debug.Log("isDoubleJumping:" + isDoubleJumping);
        //Debug.Log("isDoubleFalling:" + isDoubleFalling);
        //Debug.Log("isClimbing:" + isClimbing);
    }
}
