using UnityEngine;

/// <summary>
/// it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes
/// </summary>
namespace ItGoesItGoesItGoesItGoesItGoesItGoesItGoesItGoesItGoes
{

    public class InfiniteSegment : MonoBehaviour
    {

        /// <summary>
        /// When the player gets here, it spawns in the next area
        /// </summary>
        SpawnTheNextInfiniteSegment spawnNextArea;


        void Awake()
        {
            spawnNextArea = GetComponentInChildren<SpawnTheNextInfiniteSegment>();
        }


        void OnDestroy()
        {
            UnityEngine.Debug.Log("I have been yeeted.");
        }


    }

}