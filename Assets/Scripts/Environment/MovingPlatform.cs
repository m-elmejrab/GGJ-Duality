using UnityEngine;
public class MovingPlatform : MonoBehaviour
{

    /// <summary>
    /// one position
    /// </summary>
    public Vector2 positionA;

    /// <summary>
    /// other position
    /// </summary>
    public Vector2 positionB;

    /// <summary>
    /// time (seconds) taken to get between point a and point b
    /// </summary>
    public float travelTimeInSeconds;

    /// <summary>
    /// time (seconds) which the platform should wait for at each end of the path
    /// </summary>
    public float waitTimeInSeconds;

    private float timer;


    private Vector2 aToBVelocity;

    private Vector2 bToAVelocity;

    private enum MoveState
    {
        AtA,
        ToB,
        AtB,

        ToA
    }

    private MoveState moveState;

    public Rigidbody2D rb;

    void Awake()
    {

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.position = positionA;

        rb.velocity = Vector2.zero;

        moveState = MoveState.AtA;

        timer = waitTimeInSeconds;

        // Might need to be swapped.
        Vector2 aToBDist = positionB - positionA;

        aToBVelocity = aToBDist / travelTimeInSeconds;

        bToAVelocity = -aToBVelocity;
    }

    void FixedUpdate()
    {


        timer -= Time.fixedDeltaTime;

        if (timer <= 0)
        {
            switch (moveState)
            {

                case MoveState.AtA:
                    moveState = MoveState.ToB;
                    rb.velocity = aToBVelocity;
                    timer = travelTimeInSeconds;
                    break;
                case MoveState.AtB:
                    moveState = MoveState.ToA;
                    rb.velocity = bToAVelocity;
                    timer = travelTimeInSeconds;
                    break;
                case MoveState.ToA:
                    rb.velocity = Vector2.zero;
                    rb.MovePosition(positionA);
                    moveState = MoveState.AtA;
                    timer = waitTimeInSeconds;
                    break;
                case MoveState.ToB:
                    rb.velocity = Vector2.zero;
                    rb.MovePosition(positionB);
                    moveState = MoveState.AtB;
                    timer = waitTimeInSeconds;
                    break;
            }
        }

    }

}