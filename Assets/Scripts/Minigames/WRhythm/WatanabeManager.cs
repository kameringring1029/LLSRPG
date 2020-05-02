﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

/*
 * げーむまねーじゃ
 */
public class WatanabeManager : SingletonMonoBehaviour<WatanabeManager>
{

    GameObject watanabe_act;
    List<GameObject> watanabeall;

    public GameObject fence;

    GameObject pray_prefab;

    int watanabe_maxnum = 4;
    int watanabe_zanki;

    static int record = 0; //記録保持用
    int score = 0; //そのときのスコア用

    WRhythmMusicalScore ms;
    int progress = 0;

    // Start is called before the first frame update
    void Start()
    {
        init();

        StartCoroutine(createWatanabe());
        StartCoroutine(createIcon());

      //  watanabe_act = GameObject.Find("watanabe");
      //  watanabeall.Add(watanabe_act);
    }

    void init()
    {
        ms = new WRhythmMusicalScore(1);
        progress = 0;

        watanabe_zanki = 30; changeZanki(0);
        score = 0; changeScore(0);

        watanabeall = new List<GameObject>();

        pray_prefab = Resources.Load<GameObject>("Minigame/w_rhythm/prey");
    }

    // Update is called once per frame
    void Update()
    {

        // クリックされたらワタナベがダイブ
        if (Input.GetMouseButtonDown(0) && watanabeall.Count > 0 && watanabe_zanki > 0)
        {
            watanabe_act.GetComponent<WRhythmWatanabe>().dive();

            // ワタナベのいれかえ
            watanabeall.Remove(watanabe_act);
            changeZanki(1, false);
            renewWatanabeList();

            // ワタナベを動か
            for (int i=0; i<watanabeall.Count;i++)
            {
                watanabeall[i].GetComponent<WRhythmWatanabe>().scroll();
            }

        }

    }

    /* ワタナベ生産用コルーチン */
    IEnumerator createWatanabe()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f); //ワタナベ生成時間の間隔

            // fenceの動き
            fence.GetComponent<Animator>().SetBool("isMoving", false);


            if (watanabeall.Count < 4 && watanabeall.Count < watanabe_zanki) // ワタナベの数の上限
            {
                //ワタナベ生成
                GameObject watanabe = Instantiate<GameObject>(Resources.Load<GameObject>("Minigame/w_rhythm/watanabe"));
                //ワタナベ位置調整
                if(watanabeall.Count != 0)
                    watanabe.transform.position = watanabeall[watanabeall.Count-1].transform.position + new Vector3(1,0,0);
                //ワタナベ軍団なかまいり
                watanabeall.Add(watanabe);
                renewWatanabeList();

                //fence動かす
                fence.GetComponent<Animator>().SetBool("isMoving", true);
            }
        }
    }

    /* アイコンが周期的にぽこぽこ出てくるよ */
    IEnumerator createIcon()
    {
        yield return new WaitForSeconds(3f);

        while (progress < ms.score.Length)
        {

            //生成
            Instantiate(pray_prefab);


            // 長音（休符）
            yield return new WaitForSeconds(ms.spansec * ms.score[progress]);

            progress++;

            //  Debug.Log(Time.deltaTime);
        }

    }

    /* 残基の変更 */
    void changeZanki( int num, bool incre=true)
    {
        if (incre)
        {
            watanabe_zanki += num;
        }
        else
        {
            watanabe_zanki -= num;
        }

        // 終了
        if(watanabe_zanki < 0)
        {
            return;
        }

        GameObject.Find("zanki_num").GetComponent<TextMeshProUGUI>().text =  string.Format("{0:D3}",watanabe_zanki);
    }

    /* スコアの変更 */
    public void changeScore(int num, bool incre = true)
    {
        if (incre)
        {
            score += num;
        }
        else
        {
            score -= num;
        }

        if (score > record) record = score;

        GameObject.Find("score_num").GetComponent<TextMeshProUGUI>().text = string.Format("{0:D9}", score);
        GameObject.Find("record_num").GetComponent<TextMeshProUGUI>().text = string.Format("{0:D9}",record);
    }


    /* watanabe 表示順/active watanabe更新用 */
    void renewWatanabeList()
    {
        for (int i = 0; i < watanabeall.Count; i++)
        {
            watanabeall[i].GetComponent<SpriteRenderer>().sortingOrder = watanabe_maxnum - i;
        }

        if (watanabeall.Count > 0)
        {
            watanabe_act = watanabeall[0];
            watanabeall[0].GetComponent<Animator>().SetTrigger("trg_act");
        }
        else
        {
            watanabe_act = null;
        }
    }
}
