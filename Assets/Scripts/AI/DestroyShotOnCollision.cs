using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShotOnCollision : MonoBehaviour
{


    public float MAXIMUM_TRAVEL_TIME = 10f;


    void Awake()
    {
        Destroy(this, MAXIMUM_TRAVEL_TIME);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {

            DualityManager dualityManager = collision.gameObject.GetComponent<DualityManager>();
            dualityManager.DrainLightMode(0.2f);


        }

        Destroy(gameObject);
    }
}
