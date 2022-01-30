using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashTokens : MonoBehaviour
{
    public GameObject[] dashTokens;
    public Sprite[] sprites;

    public IHaveDashTokens thingWithDashTokens;

    // Start is called before the first frame update
    void Awake()
    {
        if (thingWithDashTokens == null)
        {
            thingWithDashTokens = GameObject.FindObjectOfType<DualityManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Debug.Log("12345");

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //FIXME yeet this ASAP.
            //TODO yeet this asap. 
            thingWithDashTokens.AddDashToken(1);
        }

        switch (thingWithDashTokens.GetCurrentDashes())
        {
            case 0:
                dashTokens[0].GetComponent<Image>().sprite = sprites[0];
                dashTokens[1].GetComponent<Image>().sprite = sprites[0];
                dashTokens[2].GetComponent<Image>().sprite = sprites[0];
                break;
            case 1:
                dashTokens[0].GetComponent<Image>().sprite = sprites[1];
                dashTokens[1].GetComponent<Image>().sprite = sprites[0];
                dashTokens[2].GetComponent<Image>().sprite = sprites[0];
                break;
            case 2:
                dashTokens[0].GetComponent<Image>().sprite = sprites[2];
                dashTokens[1].GetComponent<Image>().sprite = sprites[0];
                dashTokens[2].GetComponent<Image>().sprite = sprites[0];
                break;
            case 3:
                dashTokens[0].GetComponent<Image>().sprite = sprites[2];
                dashTokens[1].GetComponent<Image>().sprite = sprites[1];
                dashTokens[2].GetComponent<Image>().sprite = sprites[0];
                break;
            case 4:
                dashTokens[0].GetComponent<Image>().sprite = sprites[2];
                dashTokens[1].GetComponent<Image>().sprite = sprites[2];
                dashTokens[2].GetComponent<Image>().sprite = sprites[0];
                break;
            case 5:
                dashTokens[0].GetComponent<Image>().sprite = sprites[2];
                dashTokens[1].GetComponent<Image>().sprite = sprites[2];
                dashTokens[2].GetComponent<Image>().sprite = sprites[1];
                break;
            case 6:
                dashTokens[0].GetComponent<Image>().sprite = sprites[2];
                dashTokens[1].GetComponent<Image>().sprite = sprites[2];
                dashTokens[2].GetComponent<Image>().sprite = sprites[2];
                break;

        }


        for (int i = 0; i < 3; i++)
        {
            if (dashTokens[i].GetComponent<Image>().sprite == sprites[1])
            {
                dashTokens[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else
            {
                dashTokens[i].transform.localScale = new Vector3(1, 1, 1);
            }

        }


    }
}
