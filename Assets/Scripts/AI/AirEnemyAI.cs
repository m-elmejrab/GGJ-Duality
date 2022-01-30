using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyAI : MonoBehaviour
{

    public float delayBetweenShots = 3f;
    public GameObject shotPrefab;
    public Transform shotPosition;
    public float shotSpeedX = 5f;
    public float shotSpeedY = 5f;

    private float timeSinceLastShot = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= delayBetweenShots)
        {
            timeSinceLastShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject shot = Instantiate(shotPrefab, shotPosition.position, Quaternion.identity);
        Rigidbody2D shotRb2D = shot.AddComponent<Rigidbody2D>();
        shotRb2D.gravityScale = 0;
        shotRb2D.velocity = new Vector2(shotSpeedX, shotSpeedY);

    }
}
