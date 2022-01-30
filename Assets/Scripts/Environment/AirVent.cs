using UnityEngine;
public class AirVent : MonoBehaviour
{

    public Vector2 airVentForce;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement thePlayer))
        {
            thePlayer.StartApplyingVentForce(airVentForce);
        }
    }


    void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement thePlayer))
        {
            thePlayer.StopApplyingVentForce();
        }

    }

}