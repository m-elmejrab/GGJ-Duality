using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioPlayer;

    public AudioClip playerJump;
    public AudioClip playerAttack;
    public AudioClip playerDash;
    public AudioClip playerDeath;
    public AudioClip playerHurt;

    public AudioClip groundEnemyAttack;
    public AudioClip airEnemyAttack;
    public AudioClip enemyDeath;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayPlayerSound(string soundName)
    { 
        switch(soundName)
        {
            case "jump":
                audioPlayer.PlayOneShot(playerJump);
                break;
            case "attack":
                audioPlayer.PlayOneShot(playerAttack);
                break;
            case "dash":
                audioPlayer.PlayOneShot(playerDash);
                break;
            case "hurt":
                audioPlayer.PlayOneShot(playerHurt);
                break;
            case "death":
                audioPlayer.PlayOneShot(playerDeath);
                break;
            default:
                break;
        }
    
    }

    public void PlayEnemySound(string soundName)
    {
        switch (soundName)
        {
            
            case "airAttack":
                audioPlayer.PlayOneShot(airEnemyAttack);
                break;
            case "groundAttack":
                audioPlayer.PlayOneShot(groundEnemyAttack);
                break;
            case "death":
                audioPlayer.PlayOneShot(enemyDeath);
                break;
            default:
                break;
        }

    }
}
