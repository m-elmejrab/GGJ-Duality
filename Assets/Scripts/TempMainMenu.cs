using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class TempMainMenu : MonoBehaviour
{

    public TextMeshProUGUI title;

    public Button level1Button;

    public Button endlessModeButton;

    public Button exitButton;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void StartLevelOne()
    {
        audioSource.Play();
        Debug.Log("CLICK REGISTERED");
        SceneManager.LoadScene(1);
    }

    public void StartEndless()
    {
        audioSource.Play();
        Debug.Log("CLICK REGISTERED");

        SceneManager.LoadScene("And so it goes");

    }

    public void CloseGame()
    {
        audioSource.Play();
        Debug.Log("CLICK REGISTERED");

        Application.Quit();

    }

}