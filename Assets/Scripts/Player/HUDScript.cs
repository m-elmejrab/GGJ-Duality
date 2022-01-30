using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

/// <summary>
/// A class for the HUD.
/// </summary>
public class HUDScript : MonoBehaviour, IListenForThePlayerDying
{

    /// <summary>
    /// The dualityManager performing all of the business logic
    /// </summary>
    public DualityManager dualityManager;

    /// <summary>
    /// The dash tokens
    /// </summary>
    public DashTokens dashTokens;

    /// <summary>
    /// Duality bar meter thingy
    /// </summary>
    public DualityBar dualityBar;

    /// <summary>
    /// Whether or not the game is currently paused
    /// </summary>
    private bool _paused = false;

    /// <summary>
    /// Publicly viewable indicator of whether or not the game is paused
    /// </summary>
    /// <value>true if the game is paused</value>
    public bool Paused
    {
        get => _paused;
    }


    [Header("Some pause menu stuff")]
    /// <summary>
    /// The text to tell everyone that this is the pause menu
    /// </summary>
    public TextMeshProUGUI pauseText;

    /// <summary>
    /// A button that resumes the game
    /// </summary>
    public Button resumeButton;

    /// <summary>
    /// A button that restarts the level
    /// </summary>
    public Button retryButton;

    /// <summary>
    /// A button that goes back to the menu
    /// </summary>
    public Button quitButton;

    public GameObject PauseBackGround;

    public bool showPauseMenuWhenThePlayerDies;

    bool playerIsAlive = true;

    private AudioSource audioSource;


    void Awake()
    {

        UnityEngine.Assertions.Assert.IsNotNull(pauseText);
        pauseText.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        PauseBackGround.SetActive(false);
        audioSource = GetComponent<AudioSource>();


        if (dualityManager == null)
        {
            dualityManager = GameObject.FindObjectOfType<DualityManager>();
        }

        if (dashTokens == null)
        {
            dashTokens = GameObject.FindObjectOfType<DashTokens>();
        }

        if (dualityBar == null)
        {
            dualityBar = GameObject.FindObjectOfType<DualityBar>();
        }

    }

    void Update()
    {

        if (Input.GetButtonDown("Cancel") && playerIsAlive)
        {
            if (_paused)
            {
                Resume();
                audioSource.Play();
            }
            else
            {
                Pause();
                audioSource.Pause();
            }
        }
    }


    /// <summary>
    /// Reset the current level
    /// </summary>
    public void ResetLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    /// <summary>
    /// Quit to menu (assumes that the scene at index 0 is the menu scene)
    /// </summary>
    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }


    /// <summary>
    /// Resume the level, hides the pause menu
    /// </summary>
    public void Resume()
    {

        if (_paused)
        {
            _paused = false;

            Time.timeScale = 1f;

            pauseText.gameObject.SetActive(false);
            resumeButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            PauseBackGround.SetActive(false);
        }
    }

    private void Pause()
    {

        _paused = true;
        Time.timeScale = 0f;

        pauseText.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        PauseBackGround.SetActive(true);
    }

    public void ThePlayerIsDead()
    {

        playerIsAlive = false;
        StartCoroutine(DelayBeforeDeath());
        
    }

    IEnumerator DelayBeforeDeath()
    {
        yield return new WaitForSeconds(1.5f);

        if (showPauseMenuWhenThePlayerDies)
        {
            Pause();
        }
    }

}