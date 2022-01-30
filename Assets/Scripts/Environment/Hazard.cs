using UnityEngine;


/// <summary>
/// Make sure any environmental hazards that hurt things that can get hurt implement this interface!
/// </summary>
public class Hazard : MonoBehaviour
{


    /// <summary>
    /// Is this hazard an instant death hazard?
    /// </summary>
    public bool instaDeath;

    /// <summary>
    /// If it's not an instant death hazard, how much damage does it deal?
    /// </summary>
    public float damageDealt;

    public void Awake()
    {
        if (!TryGetComponent(out Collider2D c))
        {
            UnityEngine.Debug.Log("oh no!");
        }
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject oth = other.gameObject;

        if (oth.TryGetComponent(out IGetHurt hurtyComponent))
        {
            if (this.instaDeath)
            {
                hurtyComponent.Kill();
            }
            else
            {
                hurtyComponent.TakeDamage(damageDealt);
            }
        }
    }


}