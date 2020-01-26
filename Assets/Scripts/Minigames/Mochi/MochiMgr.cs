using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MochiMgr : MonoBehaviour
{
    GameObject next_mikan;
    GameObject tweetbutton;

    // Start is called before the first frame update
    void Start()
    {
        tweetbutton = GameObject.Find("tweetbutton");
        next_mikan = Instantiate(Resources.Load<GameObject>("Minigame/mochi/mikan"));
    }

    // Update is called once per frame
    void Update()
    {
	}

    
    public void onClick() { 
 
        if (next_mikan.GetComponent<MochiMikan>().dropper.name == "dropper")
        {
            // みかん落として次のを補填
            next_mikan.gameObject.tag = "mochimikan_released";
            next_mikan.GetComponent<MochiMikan>().release();
            next_mikan = Instantiate(Resources.Load<GameObject>("Minigame/mochi/mikan"));
        }
      
    }

}
