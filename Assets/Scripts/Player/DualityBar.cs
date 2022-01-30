using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DualityBar : MonoBehaviour
{

    public Image sliderFill;


    /// <summary>
    /// The DualityManager performing all of the actual business logic.
    /// If undefined, will try to find one itself.
    /// </summary>
    public DualityManager dualityManager;

    /// <summary>
    /// The slider being used to show the duality bar itself.
    /// If undefined, will try to find it as a component of itself, then of its children, then of its parents
    /// </summary>
    public Slider theSlider;
    // Start is called before the first frame update
    void Awake()
    {
        if (theSlider == null)
        {
            theSlider = GetComponent<Slider>();
            if (theSlider == null)
            {
                theSlider = GetComponentInChildren<Slider>();
                if (theSlider == null)
                {
                    theSlider = GetComponentInParent<Slider>();
                }
            }
        }

        if (dualityManager == null)
        {
            dualityManager = FindObjectOfType<DualityManager>();
        }



    }

    // Update is called once per frame
    void Update()
    {
        sliderFill.color = Color.Lerp(Color.black, Color.black, theSlider.normalizedValue);


        // updates the slider to reflect the current amount of light mode remaining.
        theSlider.normalizedValue = dualityManager.LightMode01;
    }


}
