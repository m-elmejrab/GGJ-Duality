using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyVision : MonoBehaviour
{
    public event Action playerSpotted;
    public event Action playerDisappeared;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerSpotted.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerDisappeared.Invoke();
        }
    }


}
