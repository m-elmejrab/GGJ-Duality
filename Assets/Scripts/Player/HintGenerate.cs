using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintGenerate : MonoBehaviour
{
    public GameObject BlankBlackNormal;
    public GameObject BlankBlackLong;
    public bool generateNormalHint;
    public string KeyCodeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (generateNormalHint)
            {
                GameObject a = Instantiate(BlankBlackNormal, GameObject.FindGameObjectWithTag("Player").transform);
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = KeyCodeText;
                Destroy(a, 3);
                Destroy(gameObject);
            }
            else
            {
                GameObject a = Instantiate(BlankBlackLong, GameObject.FindGameObjectWithTag("Player").transform);
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = KeyCodeText;
                Destroy(a, 3);
                Destroy(gameObject);
            }
            
        }
    }
}
