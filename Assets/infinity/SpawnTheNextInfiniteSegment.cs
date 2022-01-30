using System.Diagnostics;
using UnityEngine;


/// <summary>
/// it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes
/// </summary>
namespace ItGoesItGoesItGoesItGoesItGoesItGoesItGoesItGoesItGoes
{

    /// <summary>
    /// Class responsible for spawning the next infinite segment
    /// </summary>
    public class SpawnTheNextInfiniteSegment : MonoBehaviour
    {


        /// <summary>
        /// Did I ever tell you the definition of insanity?
        /// </summary>
        public InfiniteContent theDefinitionOfInsanity;

        /// <summary>
        /// Where to put the next one
        /// </summary>
        public NextOneGoesHere pleaseToPut;

        /// <summary>
        /// Can I summon a next one?
        /// </summary>
        bool canSummonANewOne = true;

        void Awake()
        {
            theDefinitionOfInsanity = GameObject.FindObjectOfType<InfiniteContent>();

            if (pleaseToPut == null)
            {
                pleaseToPut = GetComponentInChildren<NextOneGoesHere>();
            }
        }





        void OnTriggerEnter2D(Collider2D other)
        {
            UnityEngine.Debug.Log("okay so someone entered this collision");


            if (canSummonANewOne & other.gameObject.TryGetComponent(out PlayerMovement plr))
            {
                UnityEngine.Debug.Log("hey so uhh can u pls spawn another one in k thx");
                theDefinitionOfInsanity.AnotherOne(pleaseToPut.transform);
                canSummonANewOne = false;
            }
        }



    }

}