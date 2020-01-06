using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MochiMgr : MonoBehaviour
{
    GameObject next_mikan;

    // Start is called before the first frame update
    void Start()
    {
        next_mikan = Instantiate(Resources.Load<GameObject>("Minigame/mochi/mikan"));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && next_mikan.GetComponent<MochiMikan>().dropper.name == "dropper")
        {
            // みかん落として次のを補填
            next_mikan.GetComponent<MochiMikan>().release();
            next_mikan = Instantiate(Resources.Load<GameObject>("Minigame/mochi/mikan"));
        }
    }
}
