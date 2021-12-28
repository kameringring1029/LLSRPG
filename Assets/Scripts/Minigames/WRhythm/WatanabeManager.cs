using System.Collections;
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
    public GameObject djdia;

    GameObject watanabe_prefab_z; // for zaiko
    GameObject watanabe_prefab_d; // for dive
    GameObject pray_prefab;

    int watanabe_maxnum = 4;
    int watanabe_zanki;

    static int record = 0; //記録保持用
    public int score = 0; //そのときのスコア用

    WRhythmMusicalScore ms;
    int progress = 0;

    float elapsedTime = 0f; // 経過時間
    float pauseTime = 0.3f; // ワタナベ生成時間の間隔,クリックしてから次のクリックまでの時間制限

    public bool catching { private set; get; }

    public AudioClip sound_base;
    public AudioClip sound_dive;
    public AudioClip sound_catch;

    AudioSource audiosource;

    bool flgstop = false;
    public GameObject panel_end;

    public GameObject help;
    public GameObject text_eval;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.2f; // ゲームスピード

        init();

        StartCoroutine(createWatanabe());

        StartCoroutine(readyAndStart());

    }

    void init()
    {
        panel_end.SetActive(false);

        ms = new WRhythmMusicalScore(1);

        progress = 0;

        watanabe_zanki = 5; changeZanki(0);
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

        catching = false;

        pauseTime = ms.spansec / 2;

    }

    IEnumerator readyAndStart()
    {
        // ready
        yield return new WaitForSeconds(1.0f);

        // start
        StartCoroutine(createNotes());
        StartCoroutine(waitAnimate(ms.spansec));

    }


    // Update is called once per frame
    private void Update()    
    {
        // ゲーム終了判定
        if (flgstop) return;

        // chattering判定
        if (checkChattering()) return;

        // @mobile 複数タッチ判定

        int touchCount = Input.touchCount;

        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if(touch.phase == TouchPhase.Began) {

                if (elapsedTime < 0f) return; // chattering

                if (touch.position.x > Screen.width / 2) // 画面右側なら
                {
                    // ワタナベがダイブ
                    diveAction();
                }
                else // 画面左側なら
                {
                    catchAction();
                }

            }
        }

        // @PC

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)){ // inputされたら

            if (elapsedTime < 0f) return; // chattering


            /**/
            //audiosource.PlayOneShot(sound_dive);



            if (Input.GetKeyDown(KeyCode.X)) // Xなら
            {
                diveAction();
            }

            if (Input.GetKeyDown(KeyCode.Z)) // Zなら
            {
                catchAction();
            }
        }

    }


    bool checkChattering()
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
                return true;
            }
        }

        return false;
    }


    /*
     * dive入力時の処理
     */
    void diveAction()
    {
        // ワタナベがダイブ
        if (watanabeall.Count > 0/* && watanabe_zanki > 0*/)
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
            //changeZanki(1, false);
            renewWatanabeList();

            // ワタナベを動か
            for (int i = 0; i < watanabeall.Count; i++)
            {
                watanabeall[i].GetComponent<WRhythmWatanabe>().scroll();
            }

        }
    }


    /*
     * catch入力時の処理
     */
    void catchAction()
    {
        //
        //elapsedTime = -0.01f;

        // sound
        audiosource.PlayOneShot(sound_catch);

        // キャッチのトリガー
        StartCoroutine(trgCatching());

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
        //yield return new WaitForSeconds(ms.spansec * 4);

        // loop
        while (progress < ms.score.Length)
        {
            int createnum = ms.score[progress]; //楽譜から今回の小節のノーツ数を拾う

            /*
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
            */

            // 2進数化(4桁＝4分音符のフラグに)
            string createnumstr = System.Convert.ToString(createnum, 2).PadLeft(4);
            Debug.Log(createnumstr);

            // 1小節分のloop
            for (int i = 0; i < 4; i++)
            {
                // 2進数上1ならノーツ製造
                if (createnumstr[i] == '1')
                  StartCoroutine(createNotesSingle());                

                // 次へ
                yield return new WaitForSeconds(ms.spansec);                
            }

            progress++;

            /*譜面一周*/
            if(progress == ms.score.Length && !flgstop)
            {
                // 譜面の進捗をリセット
                progress = 0;

                // 次の譜面を設定
                int nextfumen = ms.number + 1;

                // 全譜面を一周したらスピードアップ
                if (nextfumen > 3)
                {
                    nextfumen = 1;
                    Time.timeScale = Time.timeScale * 1.2f;
                }

                ms = new WRhythmMusicalScore(nextfumen);

            }
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



    /*
     * サウンドとWatanabe・ThrowerのWaitアニメーション同期
     */
    public IEnumerator waitAnimate(float sec)
    {
        // はじまり
        yield return new WaitForSeconds(0.0f);

        bool sit = false; // アニメーションが座っているかどうか

        Animator animator_thrower = thrower.GetComponent<Animator>();
        Animator animator_djdia = djdia.GetComponent<Animator>();

        while (true)
        {
            // 引数ぶんだけWaitしてループ
            yield return new WaitForSeconds(sec);

            // アニメーション同期
            Animator animator_watanabe = watanabe_act.GetComponent<Animator>();
            animator_thrower.SetBool("flg_sit", sit);
            animator_djdia.SetBool("flg_sit", sit);
            animator_watanabe.SetBool("flg_sit", sit);

            // animation状況スイッチ
            sit = !sit;

            // ベースの音声
            audiosource.PlayOneShot(sound_base);

        }
    }


    /* 残基の変更 */
    public void changeZanki( int num, bool incre=true)
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
            flgstop = true;
            panel_end.SetActive(true);
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

        //GameObject.Find("score_num").GetComponent<TextMeshProUGUI>().text = string.Format("{0:D9}", score);
        //GameObject.Find("record_num").GetComponent<TextMeshProUGUI>().text = string.Format("{0:D9}",record);
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


    /* help panelをON⇔OFF */
    public void switchHelp() {
        switch (help.active)
        {
            case true:
                help.SetActive(false);
                break;
            case false:
                help.SetActive(true);
                break;
        }
    }

}
