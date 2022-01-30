using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetect : MonoBehaviour
{

    public GameObject deathPrefab;
    public GameObject mc;

    /// <summary>
    /// How long should we wait before automatically destroying this?
    /// </summary>
    public float destroyTime = 2f;

    public PlayerMovement thePlayer;

    public CameraFollow theFollower;


    public DualityManager dualityManager;
    // Start is called before the first frame update
    void Awake()
    {
        mc = GameObject.FindGameObjectWithTag("MainCamera");
        thePlayer = GameObject.FindObjectOfType<PlayerMovement>();

        dualityManager = GameObject.FindObjectOfType<DualityManager>();

        theFollower = GameObject.FindObjectOfType<CameraFollow>();

        UnityEngine.Assertions.Assert.IsNotNull(dualityManager);
        UnityEngine.Assertions.Assert.IsNotNull(theFollower);

        Destroy(this, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    thePlayer.dualityManager.AddLightModeRaw(10);
        //}
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(deathPrefab, collision.gameObject.transform.position, Quaternion.identity);
            //CameraFollow.shakeDuration = 0.5f;

            theFollower.StartCameraShake(0.25f);

            dualityManager.AddLightMode(0.2f);
            //CameraFollow.oripos = mc.transform.localPosition;
            Destroy(collision.gameObject);
        }
    }
}
