using System;
using UnityEngine;
public class DoorScript : MonoBehaviour
{




    /// <summary>
    /// How long (in seconds) does it take for the door to fully close?
    /// </summary>
    public float doorClosingTime;


    /// <summary>
    /// How long (in seconds) does it take for the door to fully open?
    /// </summary>
    public float doorOpeningTime;

    /// <summary>
    /// The rigidbody connected to this door.
    /// </summary>
    public Rigidbody2D rb;

    /// <summary>
    /// Current state of the door.
    /// </summary>
    [SerializeField]
    public DoorState currentState;


    /// <summary>
    /// Where the door should be when open
    /// </summary>
    public Vector2 openPos;

    /// <summary>
    /// Where the door should be when closed
    /// </summary>
    public Vector2 closedPos;

    /// <summary>
    /// velocity when opening
    /// </summary>
    private Vector2 openingVel;

    /// <summary>
    /// velocity when closing
    /// </summary>
    private Vector2 closingVel;

    private bool openingGreaterThanClosing;

    private float timer;

    private float totalDist;



    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        switch (currentState)
        {
            case DoorState.OPEN:
                rb.position = openPos;
                break;
            case DoorState.CLOSED:
                rb.position = closedPos;
                break;
        }

        Vector2 closedToOpen = openPos - closedPos;

        totalDist = closedToOpen.magnitude;

        openingVel = closedToOpen / doorOpeningTime;

        closingVel = -closedToOpen / doorClosingTime;

    }

    public void PleaseToOpen()
    {
        switch (currentState)
        {
            case DoorState.CLOSED:
                currentState = DoorState.OPENING;
                rb.velocity = openingVel;
                timer = (Vector2.Distance(rb.position, openPos) / totalDist) * doorClosingTime;
                break;
            case DoorState.CLOSING:
                currentState = DoorState.OPENING;
                rb.velocity = openingVel;
                timer = doorOpeningTime;
                break;
        }
    }

    public void PleaseToClose()
    {
        switch (currentState)
        {
            case DoorState.OPENING:
                currentState = DoorState.CLOSING;
                rb.velocity = closingVel;
                timer = (Vector2.Distance(rb.position, closedPos) / totalDist) * doorClosingTime;
                break;
            case DoorState.OPEN:
                currentState = DoorState.CLOSING;
                rb.velocity = closingVel;
                timer = doorClosingTime;
                break;
        }
    }

    public void FixedUpdate()
    {
        switch (currentState)
        {
            case DoorState.OPENING:
                timer -= Time.fixedDeltaTime;
                if (timer <= 0)
                {
                    currentState = DoorState.OPEN;
                    timer = 0;
                    rb.position = openPos;
                }
                break;
            case DoorState.CLOSING:
                timer -= Time.fixedDeltaTime;
                if (timer <= 0)
                {
                    currentState = DoorState.CLOSED;
                    timer = 0;
                    rb.position = closedPos;
                }
                break;
        }
    }



}