using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * げーむまねーじゃ
 */
public class WatanabeManager : MonoBehaviour
{

    GameObject watanabe_act;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(createIcon());

        watanabe_act = GameObject.Find("watanabe");
    }

    // Update is called once per frame
    void Update()
    {
        
        // ワタナベのいれかえ
        if (watanabe_act == null)
        {
            watanabe_act = GameObject.FindGameObjectWithTag("wrhythm_watanabe");
        }
        

        // クリックされたらワタナベがダイブ
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(watanabe_act);
            watanabe_act.GetComponent<WRhythmWatanabe>().dive();
        }

    }

    /* アイコンが周期的にぽこぽこ出てくるよ */
    IEnumerator createIcon()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(Resources.Load<GameObject>("Minigame/w_rhythm/prey"));
        }

    }
}
