using UnityEngine;
public class PlayerTrigger : MonoBehaviour
{
    PlayerMovement thePlayerItself;

    void Awake()
    {
        if (thePlayerItself == null)
        {
            thePlayerItself = gameObject.GetComponentInParent<PlayerMovement>();
        }
    }

    public PlayerMovement getThePlayer()
    {
        return thePlayerItself;
    }

}