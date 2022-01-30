using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAI : MonoBehaviour
{

    public float movementSpeed = 6f;
    public GroundEnemyVision vision;
    public Transform player;
    public float attackDelay = 1f;
    private float timeSinceLastAttack = 0f;
    private bool canAttack = true;

    enum EnemyState
    {
        Roam,
        Seek,
        Return,
        Search,
    }

    private EnemyState state;
    private Vector3 startPosition;
    private Vector3 roamPosition;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool isFacingRight = true;
    private bool playerInSight = false;
    private bool isSearchingForPlayer = false;

    private void Awake()
    {
        startPosition = transform.position;
        state = EnemyState.Roam;
        roamPosition = CreateRoamPosition();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindObjectsOfType<PlayerMovement>()[0].transform;

    }
    // Start is called before the first frame update
    void Start()
    {
        vision.playerSpotted += Vision_playerSpotted;
        vision.playerDisappeared += Vision_playerDisappeared;
    }

    private void Vision_playerDisappeared()
    {
        playerInSight = false;
        isSearchingForPlayer = true;
        state = EnemyState.Search;
        Debug.Log("Player DISAPPEARED");


    }

    private void Vision_playerSpotted()
    {
        playerInSight = true;
        state = EnemyState.Seek;
        Debug.Log("Player SPOTTED");
        StopAllCoroutines();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Roam:
                Vector3 direction = (roamPosition - transform.position).normalized;
                MoveTowards(direction);
                if (Vector3.Distance(transform.position, roamPosition) <= 0.1f)
                    roamPosition = CreateRoamPosition();
                break;
            case EnemyState.Seek:
                Vector3 playerDirection = (player.position - transform.position).normalized;
                if (Vector3.Distance(transform.position, player.position) <= 1.5f && canAttack)
                {
                    anim.SetTrigger("attack");
                    PlayerMovement pMovement = player.gameObject.GetComponent<PlayerMovement>();
                    pMovement.TakeDamage(0.1f);
                    canAttack = false;
                }
                else
                {
                    MoveTowards(playerDirection);
                }

                break;
            case EnemyState.Return:
                Vector3 returnDirection = (startPosition - transform.position).normalized;
                MoveTowards(returnDirection);
                if (Vector3.Distance(transform.position, startPosition) <= 0.1f)
                    state = EnemyState.Roam;
                break;

            case EnemyState.Search:
                if (isSearchingForPlayer)
                {
                    isSearchingForPlayer = false;
                    StartCoroutine(SearchForPlayer(4));
                }
                break;
            default:
                break;
        }

        if (!canAttack)
        {
            timeSinceLastAttack += Time.fixedDeltaTime;
            if (timeSinceLastAttack >= attackDelay)
            {
                timeSinceLastAttack = 0;
                canAttack = true;
            }
        }
    }

    private void MoveTowards(Vector3 direction)
    {
        Vector3 force = direction * movementSpeed;
        if (rb2d.velocity.magnitude < 2f)
        {
            rb2d.AddForce(force, ForceMode2D.Force);
        }

        anim.SetBool("isWalking", true);
        if (force.x < -0.01f && isFacingRight)
        {
            SwitchDirection();
        }
        if (force.x > 0.01f && !isFacingRight)
        {
            SwitchDirection();
        }

    }

    private void SwitchDirection()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
        isFacingRight = !isFacingRight;
    }


    private Vector3 CreateRoamPosition()
    {
        Vector3 pos = new Vector3(Random.Range(-1f, 1f), 0, 0);
        return startPosition + pos;
    }

    IEnumerator SearchForPlayer(int numberOfSearches)
    {

        for (int i = 0; i <= numberOfSearches; i++)
        {
            yield return new WaitForSeconds(1f);
            SwitchDirection();

        }
        state = EnemyState.Return;


    }
}
