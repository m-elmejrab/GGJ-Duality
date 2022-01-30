using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes
/// </summary>
namespace ItGoesItGoesItGoesItGoesItGoesItGoesItGoesItGoesItGoes
{

    /// <summary>
    /// IF THE CONTENT NEVER ENDS, THE PLAYTIME NEVER ENDS
    /// 
    /// IT'S A SCIENTIFIC FACT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// </summary>
    public class InfiniteContent : MonoBehaviour, IListenForThePlayerDying
    {

        /// <summary>
        /// Every possible infinite segment.
        /// </summary>
        public List<InfiniteSegment> everyPossibleSegment;

        /// <summary>
        /// The first of many.
        /// </summary>
        public InfiniteSegment theFirstSegment;

        /// <summary>
        /// How many segments are we going to keep track of at once?
        /// </summary>
        public int maxSegments;


        /// <summary>
        /// Our infinite segments.
        /// </summary>
        /// <typeparam name="InfiniteSegment"></typeparam>
        /// <returns></returns>
        List<InfiniteSegment> loadedSegments = new List<InfiniteSegment>();

        /// <summary>
        /// They're on the road to nowhere
        /// </summary>
        PlayerMovement thePlayer;

        /// <summary>
        /// where the expedition began
        /// </summary>
        float playerStartX;


        /// <summary>
        /// where the expedition shall end
        /// </summary>
        float playerTotalXMovement = 0;


        /// <summary>
        /// are they still physically capable of moving further?
        /// </summary>
        bool playerIsntDeadYet = true;


        [Header("UI Settings")]
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI gameOverText;
        public TextMeshProUGUI newHighScoreText;

        public Button retryButton;

        public Button quitButton;


        public void Awake()
        {
            loadedSegments.Add(theFirstSegment);

            thePlayer = GameObject.FindObjectOfType<PlayerMovement>();

            playerStartX = thePlayer.transform.position.x;

            playerTotalXMovement = 0;

            scoreText.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);
            newHighScoreText.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
        }


        /// <summary>
        /// inevitability.
        /// </summary>
        public void ThePlayerIsDead()
        {
            playerIsntDeadYet = false;


            //  yeah this high score system just uses PlayerPrefs for the time being.
            float previousHighScore = PlayerPrefs.GetFloat("Highscore", 1);

            string hsText = "";

            bool newHighScore = playerTotalXMovement > previousHighScore;
            if (newHighScore)
            {
                PlayerPrefs.SetFloat("Highscore", playerTotalXMovement);
                hsText = "New best distance!";
            }
            else
            {
                hsText = $"Best distance:\n{previousHighScore:F2}";
            }
            scoreText.SetText($"Your distance:\n{playerTotalXMovement:F2}");
            scoreText.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            newHighScoreText.SetText(hsText);
            newHighScoreText.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }


        public void Update()
        {
            if (playerIsntDeadYet)
            {
                var curx = thePlayer.transform.position.x - playerStartX;

                if (curx > playerTotalXMovement)
                {
                    playerTotalXMovement = curx;
                }
            }
        }


        /// <summary>
        /// THERE IS NO BACKTRACKING, ONLY PROGRESS.
        /// </summary>
        /// <param name="doItHere">where the next segment should be instantiated</param>
        public void AnotherOne(Transform doItHere)
        {

            loadedSegments.Add(Instantiate<InfiniteSegment>(TheNextEpisode(), doItHere.position, doItHere.rotation));

            if (loadedSegments.Count > maxSegments)
            {

                Destroy(loadedSegments[0].gameObject, 1f);
                UnityEngine.Debug.Log("YEEEEEEEEEEEEEEEEEEET");
                loadedSegments.RemoveAt(0);
            }

        }


        /// <summary>
        /// The next wacky and uncharacteristic adventure
        /// (also the player won't get the same one twice in a row)
        /// </summary>
        /// <returns>the segment to instantiate next</returns>
        InfiniteSegment TheNextEpisode()
        {

            int nextIndex = UnityEngine.Random.Range(1, everyPossibleSegment.Count);

            InfiniteSegment loadThisOne = everyPossibleSegment[nextIndex];

            everyPossibleSegment[nextIndex] = everyPossibleSegment[0];
            everyPossibleSegment[0] = loadThisOne;

            return loadThisOne;
        }

        public void QuitToMenu()
        {
            // TODO create the main menu.
            SceneManager.LoadScene(0);
        }


        public void Retry()
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

    }

}