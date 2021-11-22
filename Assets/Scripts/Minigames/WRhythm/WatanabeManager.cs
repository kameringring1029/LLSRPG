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

    public GameObject watanabe_act;
    List<GameObject> watanabeall;

    public GameObject fence;

    GameObject thrower;

    GameObject watanabe_prefab_z; // for zaiko
    GameObject watanabe_prefab_d; // for dive
    GameObject pray_prefab;

    int watanabe_maxnum = 4;
    int watanabe_zanki;

    static int record = 0; //記録保持用
    int score = 0; //そのときのスコア用

    WRhythmMusicalScore ms;
    int progress = 0;

    float elapsedTime = 0f; // 経過時間
    static float pauseTime = 0.15f; // ワタナベ生成時間の間隔,クリックしてから次のクリックまでの時間制限

    public bool catching { private set; get; }

    public AudioClip sound_base;
    public AudioClip sound_dive;
    public AudioClip sound_catch;

    AudioSource audiosource;


    // Start is called before the first frame update
    void Start()
    {
        init();

        StartCoroutine(createWatanabe());
        StartCoroutine(createNotes());


      //  watanabe_act = GameObject.Find("watanabe");
      //  watanabeall.Add(watanabe_act);
    }

    void init()
    {
        ms = new WRhythmMusicalScore(1);
        progress = 0;

        watanabe_zanki = 40; changeZanki(0);
        score = 0; changeScore(0);

        watanabeall = new List<GameObject>();

        pray_prefab = Resources.Load<GameObject>("Minigame/w_rhythm/prey");
        thrower = GameObject.Find("thrower");
        pray_prefab.transform.position = thrower.transform.position;

        watanabe_prefab_d = Resources.Load<GameObject>("Minigame/w_rhythm/watanabe_z");
        watanabe_prefab_d.GetComponent<WRhythmWatanabe>().zaiko = false;
        watanabe_prefab_d.layer = 0;
        watanabe_prefab_d.transform.position = fence.transform.position + new Vector3(0,-20,0);
        // アニメの速さを譜面に合わせる
        watanabe_prefab_d.GetComponent<Animator>().speed = 0.5f / ms.spansec;

        watanabe_prefab_z = Resources.Load<GameObject>("Minigame/w_rhythm/watanabe_d");
        watanabe_prefab_z.GetComponent<WRhythmWatanabe>().zaiko = true;
        watanabe_prefab_z.layer = 8;
        watanabe_prefab_z.transform.position = fence.transform.position + new Vector3(0, -20, 0);
        // アニメの速さを譜面に合わせる
        watanabe_prefab_z.GetComponent<Animator>().speed = 0.5f / ms.spansec;


        thrower.GetComponent<Animator>().speed = 0.5f / ms.spansec;
        thrower.GetComponent<WRhythmThrower>().animhash = 0;

        audiosource = GetComponent<AudioSource>();

        StartCoroutine(waitAnimate(ms.spansec));

        catching = false;
    }


    // Update is called once per frame
    void Update()
    {
        // 前回のクリックよりpauseTime後まで無反応
        if(elapsedTime != 0)
        {
            if (elapsedTime > pauseTime)
            {
                elapsedTime = 0f;
            }
            else
            {
                elapsedTime += Time.deltaTime;
                return;
            }
        }


        // 複数タッチ判定

        int touchCount = Input.touchCount;

        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (touch.position.x > Screen.width / 2) // 画面右側なら
                    {
                        // ワタナベがダイブ
                        if (watanabeall.Count > 0 && watanabe_zanki > 0)
                        {
                            // sound
                            audiosource.PlayOneShot(sound_dive);

                            //
                            elapsedTime = -0.01f;

                            GameObject watanabe = Instantiate(watanabe_prefab_d);

                            watanabe.GetComponent<WRhythmWatanabe>().dive();

                            watanabeall.Remove(watanabe_act);
                            Destroy(watanabe_act);

                            // ワタナベのいれかえ
                            changeZanki(1, false);
                            renewWatanabeList();

                            // ワタナベを動かす
                            for (int j = 0; j < watanabeall.Count; j++)
                            {
                                watanabeall[j].GetComponent<WRhythmWatanabe>().scroll();
                            }

                        }
                    }

                    else // 画面左側なら
                    {
                        // sound
                        audiosource.PlayOneShot(sound_catch);
                        
                        // キャッチのトリガー
                        StartCoroutine(trgCatching());
                    }

                    break;
            }
        }

        
#if UNITY_EDITOR
        
        if (Input.GetMouseButtonDown(0)){ //タッチされたら

            if(Input.mousePosition.x > Screen.width / 2) // 画面右側なら
            {
                // ワタナベがダイブ
                if (watanabeall.Count > 0 && watanabe_zanki > 0)
                {
                            // sound
                            audiosource.PlayOneShot(sound_dive);

                            //
                    elapsedTime = -0.01f;

                    GameObject watanabe = Instantiate(watanabe_prefab_d);

                    watanabe.GetComponent<WRhythmWatanabe>().dive();

                    watanabeall.Remove(watanabe_act);
                    Destroy(watanabe_act);

                    // ワタナベのいれかえ
                    changeZanki(1, false);
                    renewWatanabeList();

                    // ワタナベを動か
                    for (int i = 0; i < watanabeall.Count; i++)
                    {
                        watanabeall[i].GetComponent<WRhythmWatanabe>().scroll();
                    }

                }
            }

            else // 画面左側なら
            {
                        // sound
                        audiosource.PlayOneShot(sound_catch);
                        
                // キャッチのトリガー
                StartCoroutine(trgCatching());
            }
        }
        
#endif
        

    }


    /*
     * サウンドとWatanabe・ThrowerのWaitアニメーション同期
     */
    public IEnumerator waitAnimate(float sec)
    {
        bool sit = false;

        Animator animator_thrower = thrower.GetComponent<Animator>();

        while (true)
        {
            yield return new WaitForSeconds(sec);

            Animator animator_watanabe = watanabe_act.GetComponent<Animator>();
            animator_thrower.SetBool("flg_sit", sit);
            animator_watanabe.SetBool("flg_sit", sit);

            audiosource.PlayOneShot(sound_base);

            sit = !sit;
        }
    }


    /* ワタナベ生産用コルーチン */
    IEnumerator createWatanabe()
    {
        while (true)
        {
            yield return new WaitForSeconds(pauseTime); //ワタナベ生成時間の間隔

            // fenceの動き
            fence.GetComponent<Animator>().SetBool("isMoving", false);


            if (watanabeall.Count < 4 && watanabeall.Count < watanabe_zanki) // ワタナベの数の上限
            {
                //ワタナベ生成
                GameObject watanabe = Instantiate<GameObject>(watanabe_prefab_z);

                //ワタナベ位置調整
                if (watanabeall.Count != 0)
                    watanabe.transform.position = watanabeall[watanabeall.Count-1].transform.position + new Vector3(64,0,0);
                //ワタナベ軍団なかまいり
                watanabeall.Add(watanabe);
                renewWatanabeList();

                //fence動かす
                fence.GetComponent<Animator>().SetBool("isMoving", true);
            }
        }
    }

    // キャッチフラグの設定
    IEnumerator trgCatching()
    {
        catching = true;

        yield return new WaitForSeconds(ms.spansec / 2);

        catching = false;
    }


    // アイコンが楽譜に沿ってぽこぽこ出てくるよ 
    IEnumerator createNotes()
    {
        // はじまり
        yield return new WaitForSeconds(3f);

        // loop
        while (progress < ms.score.Length)
        {
            int createnum = ms.score[progress]; //楽譜から今回の小節のノーツ数を拾う

            // 1小節分のloop
            while (createnum > 0)
            {
                StartCoroutine(createNotesSingle());

                yield return new WaitForSeconds(ms.spansec);
                createnum--;
            }

            // 休符
            if(ms.score[progress] < 4)
            {
                yield return new WaitForSeconds(ms.spansec * (4 - ms.score[progress]));
            }

            progress++;
        }

    }
    IEnumerator createNotesSingle()
    {
        // throwerのアニメーションready → 指定秒数待機 → throw
        thrower.GetComponent<Animator>().SetTrigger("trg_ready");

        yield return new WaitForSeconds(ms.spansec);

        thrower.GetComponent<Animator>().SetTrigger("trg_throw");

        // 生成
        Instantiate(pray_prefab);

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
            watanabeall[0].GetComponent<WRhythmWatanabe>().setAct();
        }
        else
        {
            watanabe_act = null;
        }
    }
}
