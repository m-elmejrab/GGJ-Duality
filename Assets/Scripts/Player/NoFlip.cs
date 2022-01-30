using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFlip : MonoBehaviour
{
    public GameObject mytext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.facingRight)
        {
            mytext.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            mytext.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
