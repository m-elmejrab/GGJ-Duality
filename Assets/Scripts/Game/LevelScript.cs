using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelScript : MonoBehaviour, IListenForThePlayerDying
{


    /// <summary>
    /// THE ACTUAL PLAYER OBJECT
    /// </summary>
    public PlayerMovement theActualPlayer;


    /// <summary>
    /// The goal object
    /// </summary>
    public GameObject goalObject;

    /// <summary>
    /// The camera following script thing
    /// </summary>
    public CameraFollow theCameraFollow;

    public void Awake()
    {


        if (theActualPlayer == null)
        {
            theActualPlayer = GameObject.FindObjectOfType<PlayerMovement>();
        }


        if (theCameraFollow == null)
        {
            theCameraFollow = ((UnityEngine.GameObject)GameObject.FindObjectOfType(typeof(CameraFollow))).GetComponent<CameraFollow>();
        }

        if (theCameraFollow.Player == null)
        {
            theCameraFollow.Player = theActualPlayer.gameObject;
        }

    }

    public void ThePlayerIsDead()
    {

        // TODO stuff when the player dies
    }


    /// <summary>
    /// Completely reloads the level.
    /// </summary>
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Start()
    {

    }

    public void Update()
    {

    }


    public void ImDone()
    {
        // TODO stuff when the level is over
    }


}