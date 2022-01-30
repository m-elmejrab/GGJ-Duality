using UnityEngine;
using System.Collections.Generic;
public class DoorButton : MonoBehaviour
{

    /// <summary>
    /// Every single door that this button opens
    /// </summary>
    public List<DoorScript> theDoorsThatThisOpens;

    /// <summary>
    /// Should the doors open when the player standing on them is in dark mode?
    /// Set this to false if the doors should only open if the player standing on them needs to be in light mode.
    /// </summary>
    public bool openInDarkMode;


    void OnTriggerEnter2D(Collider2D other)
    {

        IHaveDuality d = other.gameObject.GetComponentInParent<IHaveDuality>();
        if (d == null)
        {
            // i forgor the actual hierarchy, so I'm checking parents and also children for IHaveDuality.
            d = other.gameObject.GetComponentInChildren<IHaveDuality>();
        }

        if (d != null)
        {
            DualityRelatedOpenWhenOnThis(d);
        }
    }

    void DualityRelatedOpenWhenOnThis(IHaveDuality d)
    {
        if (d.IsInLightMode() ^ openInDarkMode)
        {
            foreach (DoorScript door in theDoorsThatThisOpens)
            {
                door.PleaseToOpen();
            }
        }
        else
        {
            foreach (DoorScript door in theDoorsThatThisOpens)
            {
                door.PleaseToClose();
            }
        }
    }

    void onTriggerStay2D(Collider2D other)
    {
        IHaveDuality d = other.gameObject.GetComponentInParent<IHaveDuality>();
        if (d == null)
        {
            // i forgor the actual hierarchy, so I'm checking parents and also children for IHaveDuality.
            d = other.gameObject.GetComponentInChildren<IHaveDuality>();
        }

        if (d != null)
        {
            DualityRelatedOpenWhenOnThis(d);
        }
    }


    /// <summary>
    /// does the opposite thing when the player steps off the button
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other)
    {

        IHaveDuality d = other.gameObject.GetComponentInParent<IHaveDuality>();
        if (d == null)
        {
            // i forgor the actual hierarchy, so I'm checking parents and also children for IHaveDuality.
            d = other.gameObject.GetComponentInChildren<IHaveDuality>();
        }

        if (d != null)
        {
            if (d.IsInLightMode() ^ openInDarkMode)
            {
                foreach (DoorScript door in theDoorsThatThisOpens)
                {
                    door.PleaseToClose();
                }
            }
            else
            {
                foreach (DoorScript door in theDoorsThatThisOpens)
                {
                    door.PleaseToOpen();
                }
            }
        }
    }

}