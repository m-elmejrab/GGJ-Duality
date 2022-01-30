using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IGetHurt, IGetPushedByAir
{


    [Header("Horizontal Movement")]
    public float moveSpeed = 3f;
    public Vector2 direction;
    public static bool facingRight = true;


    [Header("Vertical Movement")]
    public float jumpSpeed;
    public float jumpDelay;
    private float jumpTimer;

    [Header("Components")]
    public Rigidbody2D rb;
    public LayerMask groundLayer;


    [Header("Physics")]
    public float maxSpeed;
    public float linearDrag = 4f;
    public float gravity = 0.75f;
    public float fallMultiplier = 5;



    [Header("Collision")]
    public bool isGrounded;
    public float groundLength = 2f;
    public Vector3 colliderOffset;


    private Vector2 currentVentForce = Vector2.zero;


    /// <summary>
    /// Cooldown between attacks
    /// </summary>
    public float attackCooldown = 0.5f;

    /// <summary>
    /// Time left until the next attack is possible
    /// </summary>
    float attacktimer = 0f;


    /// <summary>
    /// Time which must be spent standing still upon starting an attack whilst on the ground
    /// </summary>
    public float groundedAttackStopTime = 0.5f;

    /// <summary>
    /// Time left until the player is allowed to move again after starting an attack whilst on the ground
    /// </summary>
    float groundedAttackStopTimeLeftUntilCanMove = 0f;

    [Header("DualityState")]
    public DualityManager dualityManager;

    [Header("Other stuff")]

    /// <summary>
    /// Delay between form changes
    /// </summary>
    public float formChangeCooldownTime;

    float formCooldownTimer;

    private bool lightmode;

    /// <summary>
    /// How long the dashes are
    /// </summary>
    public float dashLength = 6;

    bool alive = true;



    public GameObject AttackPrefab;
    public GameObject AttackPosition;
    public GameObject[] PlayerAfterImage;
    public Slider DB;

    public Sprite whiteSprite;
    public Sprite blackSprite;

    private Animator anim;

    private float colliderWidth;

    private float downcast;

    private bool wantToJump = false;

    private bool wantToAct = false;


    // Start is called before the first frame update
    void Start()
    {



        dualityManager = GetComponent<DualityManager>();

        lightmode = dualityManager.Lightmode;

        anim = GetComponent<Animator>();

        HandleFormChange();

        colliderWidth = GetComponent<CapsuleCollider2D>().bounds.size.x;

        downcast = GetComponent<CapsuleCollider2D>().bounds.extents.y - (colliderWidth - 0.01f);
    }

    // Update is called once per frame
    void Update()
    {

        if (alive)
        {

            if (attacktimer > 0)
            {
                attacktimer -= Time.deltaTime;
            }

            // can move unless the player just attacked whilst grounded.
            if (groundedAttackStopTimeLeftUntilCanMove > 0)
            {
                groundedAttackStopTimeLeftUntilCanMove -= Time.deltaTime;
            }

            if (formCooldownTimer > 0)
            {
                formCooldownTimer -= Time.deltaTime;
            }
            else if (Input.GetButtonDown("Swap"))
            {
                UnityEngine.Debug.Log("attempted swap");
                dualityManager.AttemptSwapForm();
            }


            if (isGrounded)
            {
                if (Input.GetButtonDown("Jump") && !wantToJump)
                {
                    wantToJump = true;
                }
            }
            else
            {
                wantToJump = false;
            }



            if (lightmode ^ dualityManager.Lightmode)
            {
                UnityEngine.Debug.Log(String.Format("Swapped. Current form {0}", dualityManager.Lightmode.ToString()));
                HandleFormChange();
                // cancel any pending acts upon form change, just in case it causes an unwanted act to happen.
                wantToAct = false;
            }
            else if (Input.GetButtonDown("Action") && !wantToAct)
            {
                wantToAct = true;
            }

        }

        /*
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time - changetimer > 3)
        {
            SwapForm();

        }

        if (Input.GetKeyDown(KeyCode.J) && Time.time - attacktimer > 1 && darkform == true)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.J) && dashCharge >= 2 && darkform == false)
        {
            Dash();
        }
        */


        //TODO: get a DB

        //DB.value = (float)dualityManager.LightMode01;



        //Debug.Log(rb.velocity.x);
    }

    private void FixedUpdate()
    {


        isGrounded = Physics2D.CircleCast(transform.position, colliderWidth, Vector2.down, downcast, groundLayer).collider != null;

        if (alive)
        {

            if (isGrounded)
            {
                if (wantToJump)
                {
                    UnityEngine.Debug.Log("Jump!");

                    // jumping now happens immediately, less frustrating.
                    Jump();
                    ///jumpTimer = Time.time + jumpDelay;
                    wantToJump = false;

                }
            }
            else
            {
                wantToJump = false;
            }


            if (wantToAct)
            {
                UnityEngine.Debug.Log("Action!");
                if (dualityManager.Lightmode)
                {
                    if (dualityManager.AttemptDash())
                    {
                        Dash();
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Dash failed!");
                    }
                }
                else if (attacktimer <= 0)
                {
                    Attack();
                }
                else
                {
                    UnityEngine.Debug.Log("Attack failed!");
                }
                wantToAct = false;
            }


            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.SetBool("isRunningBlack", false);
                anim.SetBool("isRunningWhite", false);

            }
            else if (groundedAttackStopTimeLeftUntilCanMove <= 0)
            {
                moveCharacter(direction.x);
            }

            if (rb.velocity.y <= 0.01f)
            {
                anim.SetTrigger("Grounded");
            }


            if (jumpTimer > Time.time && isGrounded)
            {
                //Jump();
            }

            
            ModifyPhysics();

        }


    }


    void Attack()
    {
        if (lightmode)
        {
            anim.SetTrigger("dash");
            SoundManager.instance.PlayPlayerSound("dash");
        }
        else
        {
            anim.SetTrigger("AttackBlack");
            SoundManager.instance.PlayPlayerSound("attack");

        }
        attacktimer = attackCooldown;

        // more fun if the player can keep going through the air whilst attacking, y'know?
        if (isGrounded)
        {

            rb.velocity = Vector2.zero;
            groundedAttackStopTimeLeftUntilCanMove = groundedAttackStopTime;
        }
        // also the attack now follows the player as the player moves.
        GameObject theAttack = Instantiate(AttackPrefab, AttackPosition.transform.position, Quaternion.identity);
        theAttack.transform.parent = transform;
        if (!facingRight)
        {
            theAttack.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    void moveCharacter(float horizontal)
    {

        if ((Mathf.Sign(rb.velocity.x) != Mathf.Sign(horizontal)) || (Mathf.Abs(rb.velocity.x) < maxSpeed))
        {
            rb.AddForce(Vector2.right * horizontal * moveSpeed);
        }


        if (lightmode)
        {
            anim.SetBool("isRunningWhite", true);
        }
        else
        {
            anim.SetBool("isRunningBlack", true);
        }

        if ((facingRight && horizontal < 0) || (!facingRight && horizontal > 0))
        {
            Flip();
        }

        float absx = Mathf.Abs(rb.velocity.x);

        if (absx > maxSpeed)
        {

            float brakes = absx - maxSpeed;

            rb.AddForce(new Vector2(Mathf.Sign(rb.velocity.x) * brakes, 0));

            //rb.velocity = new Vector2(Mathf.clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
        }

        rb.AddForce(currentVentForce * Time.fixedDeltaTime);


    }

    void ModifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);



        if (isGrounded)
        {
            rb.gravityScale = 0;
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0;
            }

        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    void Jump()
    {

        //rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        //jumpTimer = 0;
        if (lightmode)
        {
            anim.SetTrigger("jumpWhite");
            SoundManager.instance.PlayPlayerSound("jump");

        }
        else
        {
            anim.SetTrigger("jumpBlack");
            SoundManager.instance.PlayPlayerSound("jump");

        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
    }


    void HandleFormChange()
    {

        formCooldownTimer = formChangeCooldownTime;

        lightmode = dualityManager.Lightmode;
        StateMovementStats cms = dualityManager.CurrentMoveStats.validate();

        UnityEngine.Debug.Log(cms.ToString());

        moveSpeed = cms.moveSpeed;
        maxSpeed = cms.maxSpeed;
        jumpSpeed = cms.jumpSpeed;

        //TODO other stuff to do with form changes

        if (lightmode)
        {
            anim.SetBool("inLightMode", true);

            AttackDetect anAttack = gameObject.GetComponentInChildren<AttackDetect>();
            if (anAttack != null)
            {
                Destroy(anAttack.gameObject);
            }
        }
        else
        {
            anim.SetBool("inLightMode", false);
        }
    }


    void Dash()
    {

        if (facingRight && !(Physics2D.Raycast(transform.position, Vector2.right, dashLength, groundLayer)))
        {


            SoundManager.instance.PlayPlayerSound("dash");


            rb.position = transform.position + Vector3.left * dashLength;
            Transform a = rb.transform;
            a.position = a.position + Vector3.right * 2;
            Instantiate(PlayerAfterImage[0], a.position, Quaternion.Euler(0, facingRight ? 0 : 180, 0));

            a.position = a.position + Vector3.right * 2f;
            Instantiate(PlayerAfterImage[1], a.position, Quaternion.Euler(0, facingRight ? 0 : 180, 0));

            a.position = a.position + Vector3.right * 2f;
            Instantiate(PlayerAfterImage[2], a.position, Quaternion.Euler(0, facingRight ? 0 : 180, 0));

        }
        else if (!facingRight && !(Physics2D.Raycast(transform.position, Vector2.left, dashLength, groundLayer)))
        {
            SoundManager.instance.PlayPlayerSound("dash");

            rb.position = transform.position + Vector3.right * dashLength;
            Transform a = transform;
            a.position = a.position + Vector3.left * 2f;
            Instantiate(PlayerAfterImage[0], a.position, Quaternion.Euler(0, facingRight ? 0 : 180, 0));

            a.position = a.position + Vector3.left * 2f;
            Instantiate(PlayerAfterImage[1], a.position, Quaternion.Euler(0, facingRight ? 0 : 180, 0));

            a.position = a.position + Vector3.left * 2f;
            Instantiate(PlayerAfterImage[2], a.position, Quaternion.Euler(0, facingRight ? 0 : 180, 0));
        }
    }



    public void TakeDamage(float damage)
    {

        // TODO this!
        dualityManager.DrainLightMode(damage);
        SoundManager.instance.PlayPlayerSound("hurt");
        Debug.Log("Player was damaged");

        if (dualityManager.LightModeRemaining <= 0)
            Kill();
        Debug.Log("Player died");

    }

    public void Kill()
    {

        this.alive = false;

        this.rb.velocity = Vector2.zero;

        anim.SetTrigger("dead");
        SoundManager.instance.PlayPlayerSound("death");

        // Telling everything that needs to know about the death of the player about the death of the player.
        foreach (var gameObj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            foreach (var obituaryConnoisseur in gameObj.GetComponentsInChildren<IListenForThePlayerDying>())
            {
                obituaryConnoisseur.ThePlayerIsDead();
            }
        }

        /*
        foreach (var obituaryConnoisseur in GameObject.FindObjectsOfType<MonoBehaviour>().ofType<IListenForThePlayerDying>())
        {
            obituaryConnoisseur.ThePlayerIsDead();
        }
        */

        // TODO perform other cleanup/autopsy work related to the untimely death of our dearly departed



        //TODO this!
        //throw new NotImplementedException("Please implement this!");
    }


    public void StartApplyingVentForce(Vector2 ventForce)
    {
        currentVentForce = ventForce;
    }

    public void StopApplyingVentForce()
    {
        currentVentForce = Vector2.zero;
    }
}
