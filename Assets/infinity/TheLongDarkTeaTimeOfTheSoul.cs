using UnityEngine;

/// <summary>
/// it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes it goes
/// </summary>
namespace ItGoesItGoesItGoesItGoesItGoesItGoesItGoesItGoesItGoes
{

    /// <summary>
    /// le existential dread has arrived
    /// </summary>
    public class TheLongDarkTeaTimeOfTheSoul : MonoBehaviour, IListenForThePlayerDying
    {


        /// <summary>
        /// How much should it move by in the X axis per second?
        /// </summary>
        public float _normalLerp = 0.5f;

        public float _chaseDist = 8f;

        public float _chaseSpeed = 5f;

        private InfiniteContent metaphorForExistenceISuppose;


        private bool targetDown = false;


        private Transform unfortunate;


        public Rigidbody2D rb;


        void Awake()
        {
            metaphorForExistenceISuppose = GameObject.FindObjectOfType<InfiniteContent>();

            if (rb == null)
            {
                rb = GetComponent<Rigidbody2D>();
            }

            unfortunate = GameObject.FindObjectOfType<PlayerMovement>().transform;

        }

        void FixedUpdate()
        {

            float distRemaining = unfortunate.transform.position.x - rb.position.x;

            if (targetDown || distRemaining > _chaseDist)
            {
                rb.position = Vector2.Lerp(rb.position, unfortunate.transform.position, _normalLerp * Time.fixedDeltaTime);
            }
            else
            {
                rb.position = new Vector2(
                    rb.position.x + (_chaseSpeed * Time.fixedDeltaTime),
                    Mathf.Lerp(rb.position.y, unfortunate.transform.position.y, _normalLerp * Time.fixedDeltaTime)
                );
            }

            //rb.velocity = new Vector2(_xmovement, (unfortunate.transform.position.y - rb.position.y)) / 1.5f;

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!targetDown)
            {
                PlayerMovement m = other.gameObject.GetComponentInParent<PlayerMovement>();
                if (m != null)
                {
                    m.Kill();
                    UnityEngine.Debug.Log("The player has died.");
                }
            }
        }


        public void ThePlayerIsDead()
        {
            targetDown = true;
            unfortunate = GameObject.FindObjectOfType<CameraChaseTarget>().transform;
        }
    }

}