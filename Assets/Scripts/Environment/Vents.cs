using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vents : MonoBehaviour
{
    public GameObject target;
    Vector3 direction;

    public int stay_force = 5;
    public int enter_force = 75;
    public float maxV = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            direction = (target.transform.position - collision.gameObject.transform.position);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction* stay_force);
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > maxV)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized * maxV;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            direction = (target.transform.position - collision.gameObject.transform.position);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction* enter_force);
        }
    }
}
