using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using DG.Tweening;
using TMPro;

using UnityEngine.UI;

public class KunfuMgr : SingletonMonoBehaviour<KunfuMgr>
{
    private bool isControling;
    private bool flg_explain;

    public int charged_power { get; private set; }

    public enum ARROW { NULL, UP, DOWN, LEFT, RIGHT}
    ARROW nowcharge_arrow;
    ARROW cursol_arrow;

    enum HIT { NULL, HIT, MISS }

    public enum MODE { READY, CHIKA, YOU, VS }
    public GameObject canvas_m;
    public GameObject canvas_v;
    public GameObject canvas_c;
    public GameObject canvas_r;

    public static MODE playmode { get; set; }

    public GameObject chika;
    public GameObject you;
    public GameObject ruby;
    private GameObject player;
    private GameObject effect;
    private GameObject enemy;

    private FixedJoystick fixedJoystick;
    public FixedJoystick fixedJoystick_c;
    public FixedJoystick fixedJoystick_y;

    public GameObject text_explain;
    public GameObject text_vs;
    public GameObject text_winner;

    float elapsed;

    private GameObject _beamcharge;
    public GameObject _beamcharge_c;
    public GameObject _beamcharge_y;
    private GameObject gauge;
    public GameObject gauge_c;
    public GameObject gauge_y;

    public int fitness_c;
    public int fitness_y;
    private int fitness_e;

    public GameObject time_gauge;

    float orthographicSize;

    Tween tween;

    // Start is called before the first frame update
    void Start()
    {
        /* 変数init */
        isControling = false;
        flg_explain = false;

        nowcharge_arrow = ARROW.NULL;
        cursol_arrow = ARROW.NULL;
        charged_power = 1;
        elapsed = 0f;

        time_gauge.GetComponent<Image>().fillAmount = 1;


        /* カメラ調整 */
        orthographicSize = GetComponent<Camera>().orthographicSize;

        canvas_m.SetActive(true);
        canvas_r.SetActive(false);
        canvas_v.SetActive(false);
        canvas_c.SetActive(false);

        if(playmode != MODE.READY) // 再実行時
        {
            startPlay(playmode);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * mode selectから選択される
     */
    public void startPlay(MODE mode)
    {
        playmode = mode;
        setMode(mode);
        changeCamera(MODE.READY);

        canvas_m.SetActive(false);
        canvas_v.SetActive(true);

        StartCoroutine(readyPerformance(mode));
    }
    IEnumerator readyPerformance(MODE mode)
    {

        /*  */
        player.GetComponent<KunfuPlayer>().setReady();
        enemy.GetComponent<KunfuPlayer>().setReady();


        yield return new WaitForSeconds(1f);
        /* are */

        ruby.GetComponent<Animator>().SetInteger("pointer", (int)mode);

        yield return new WaitForSeconds(1f);

        changeCamera(mode);


        yield return new WaitForSeconds(1f);


        /* 開始 */
        setCursol(ARROW.NULL);
        isControling = true;
    }

    /*
     * Update
     */
    private void FixedUpdate()
    {
        if (!isControling) return;

        elapsed += Time.deltaTime;
        time_gauge.GetComponent<Image>().fillAmount = 1f - elapsed / 10f;
        if (elapsed > 10.0)
        {
            onFire();
            return;
        }
        else if (elapsed > 3.0f && charged_power <= 2 && !flg_explain) // Help
        {
            flg_explain = true;
            StartCoroutine(setExplain());
        }

        /* JoyStick */
        catchJoystick();

        /* Fire　or　新しいchargeの生成 */
        if (nowcharge_arrow == ARROW.NULL)
            nextCharge();

    }



    /*
     * JoyStick制御 
     */
    private void catchJoystick()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;

        HIT hit = HIT.NULL;

        /* 上下左右にStickを倒しきったときの処理 */
        if (direction.x > 0.9 && cursol_arrow == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge_arrow == ARROW.RIGHT) // Hitしたとき
            {
                hit = HIT.HIT;
            }
            else if(playmode == MODE.YOU)
            {
                hit = HIT.MISS;
            }
            setCursol(ARROW.RIGHT);
        }
        else if (direction.x < -0.9 && cursol_arrow == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge_arrow == ARROW.LEFT) // Hitしたとき
            {
                hit = HIT.HIT;
            }
            else if (playmode == MODE.YOU)
            {
                hit = HIT.MISS;
            }
            setCursol(ARROW.LEFT);
        }
        else if (direction.z > 0.9 && cursol_arrow == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge_arrow == ARROW.UP) // Hitしたとき
            {
                hit = HIT.HIT;
            }
            else if (playmode == MODE.YOU)
            {
                hit = HIT.MISS;
            }
            setCursol(ARROW.UP);
        }
        else if (direction.z < -0.9 && cursol_arrow == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge_arrow == ARROW.DOWN) // Hitしたとき
            {
                hit = HIT.HIT;
            }
            else if (playmode == MODE.YOU)
            {
                hit = HIT.MISS;
            }
            setCursol(ARROW.DOWN);
        }
        /* Newtralリセット */
        else if (cursol_arrow != ARROW.NULL &&
            direction.x < 0.8 && direction.x > -0.8 && direction.z < 0.8 && direction.z > -0.8)
        {
            setCursol(ARROW.NULL);
        }


        /* hitによって処理 */
        switch (hit)
        {
            case HIT.HIT:
                nowcharge_arrow = ARROW.NULL;
                player.GetComponent<KunfuPlayer>().actionCharge(nowcharge_arrow);

                if (playmode == MODE.YOU) elapsed -= 0.3f; // ボーナス

                /*演出*/
                fixedJoystick.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 1f), 0.3f).SetLoops(2, LoopType.Yoyo);
                time_gauge.GetComponent<Image>().DOColor(Color.yellow, 0.3f).SetLoops(2, LoopType.Yoyo);

                break;

            case HIT.MISS:
                elapsed += 0.7f;

                /*演出*/
                fixedJoystick.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(1f, 0.2f, 0.2f, 1f), 0.3f).SetLoops(2, LoopType.Yoyo);
                time_gauge.GetComponent<Image>().DOColor(Color.red, 0.3f).SetLoops(2, LoopType.Yoyo);

                break;
        }
    }


    /*
     * カーソル移動時のアクション 
     */
    private void setCursol(ARROW arrow){
        cursol_arrow = arrow;
        player.GetComponent<KunfuPlayer>().setArrow(arrow);
    }
        

    /*
     * 次回Chargeの準備
     */
    private void nextCharge() {

            charged_power += 1;

            int rand = UnityEngine.Random.Range(1, 5);
            ARROW rand_a = (ARROW)Enum.ToObject(typeof(ARROW), rand);

            GameObject beamcharge = Instantiate<GameObject>(_beamcharge, player.transform);
            Vector3 pos = beamcharge.transform.position;

            switch (rand_a)
            {
                case ARROW.UP:
                    pos.y += 128; pos.x += 96;
                    nowcharge_arrow = ARROW.UP;
                    break;
                case ARROW.DOWN:
                    pos.y -= 128; pos.x += 96;
                    nowcharge_arrow = ARROW.DOWN;
                    break;
                case ARROW.RIGHT:
                    pos.x += 192;
                    nowcharge_arrow = ARROW.RIGHT;
                    break;
                case ARROW.LEFT:
                    pos.x -= 32;
                    nowcharge_arrow = ARROW.LEFT;
                    break;

            }

            beamcharge.transform.position = pos;

        
    }


    /*
     * Fire!
     */
    private void onFire()
    {
        isControling = false;

        // beamのサイズ変更と位置調整
        Vector3 scale = effect.transform.localScale;
        //scale.x += charged_power * 0.1f;
        scale.y += charged_power * 0.1f;
        effect.transform.localScale = scale;

        //Vector3 pos = effect.transform.position;
        //pos += new Vector3(0, (charged_power / 10.0f) * 32.0f, 0);
        //effect.transform.position = pos;

        effect.SetActive(true);
        enemy.GetComponent<KunfuPlayer>().setGuard();

        // animation
        setCursol(ARROW.NULL);
        player.GetComponent<KunfuPlayer>().actionFire(charged_power, true);

    }

    /*
     * ゲージ減らす処理
     */
    public void changeGauge()
    {
        // まずカメラ調整
        changeCamera(MODE.VS);


        StartCoroutine(gaugereduce());
    }
    IEnumerator gaugereduce()
    {
        float damage = charged_power;
        bool flg = false;

        while (damage > 0)
        {
            yield return new WaitForSeconds(0.1f);
            gauge.GetComponent<Slider>().value += 0.02f;
            damage = damage - 0.5f;

            // enemyのグラグラ
            switch (flg)
            {
                case false:
                    enemy.transform.position += new Vector3(8, 0, 0);
                    flg = true;
                    break;
                case true:
                    enemy.transform.position -= new Vector3(8, 0, 0);
                    flg = false;
                    break;
            }
        }


        /* ゲージ削り終わったらリザルト処理 */
        player.GetComponent<KunfuPlayer>().actionFire(charged_power, false);
        yield return new WaitForSeconds(1.0f);
        setResult();
    }


    /*
     * けっかはっぴょーーーー
     */
    void setResult()
    {
        StartCoroutine(setResultCoroutine());
    }
    IEnumerator setResultCoroutine()
    {
        yield return new WaitForSeconds(1f);


        /* 勝敗Animator設定と勝者変数のせってい */
        MODE winner = MODE.VS;

        if (charged_power > fitness_e)
        {
            player.GetComponent<KunfuPlayer>().setWin();
            enemy.GetComponent<KunfuPlayer>().setLose();
            winner = playmode;
        }
        else
        {
            player.GetComponent<KunfuPlayer>().setLose();
            enemy.GetComponent<KunfuPlayer>().setWin();
            switch (playmode)
            {
                case MODE.CHIKA:
                    winner = MODE.YOU;
                    break;
                case MODE.YOU:
                    winner = MODE.CHIKA;
                    break;
            }
        }

        /* 勝者によってUIを変更 */
        ruby.GetComponent<Animator>().SetInteger("pointer", (int)winner);
        switch (winner)
        {
            case MODE.CHIKA:
                text_winner.GetComponent<TextMeshProUGUI>().text = "チカ";
                break;
            case MODE.YOU:
                text_winner.GetComponent<TextMeshProUGUI>().text = "ヨウ";
                break;
        }

        yield return new WaitForSeconds(1f);

        /* おしまい */
        canvas_v.SetActive(false);
        canvas_r.SetActive(true);

    }


    /*
     * ヘルプ表示
     */
    IEnumerator setExplain()
    {
        yield return new WaitForSeconds(0.1f);

        // joystickをわかりやすく
        fixedJoystick.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 0.8f), 1f).SetLoops(-1, LoopType.Yoyo);
        // textをアクティブに
        text_explain.GetComponent<KunfuText>().setActive();

    }

    /*
     * mode関係の変数セット
     */
    void setMode(MODE mode)
    {
        switch (mode)
        {
            case MODE.CHIKA:

                /* Gameobject設定 */
                player = chika;
                effect = chika.transform.GetChild(0).gameObject;
                enemy = you;
                _beamcharge = _beamcharge_c;
                fitness_e = fitness_y;

                break;

            case MODE.YOU:

                /* Gameobject設定 */
                player = you;
                effect = you.transform.GetChild(0).gameObject;
                enemy = chika;
                _beamcharge = _beamcharge_y;
                fitness_e = fitness_c;

                break;
        }
    }

    /*
     * カメラの寄りとCamvasを変える
     */
    void changeCamera(MODE mode)
    {
        switch (mode)
        {
            case MODE.READY: // ready


                /* カメラとUI設定 */
                GetComponent<Camera>().orthographicSize = orthographicSize;
                transform.DOMove(new Vector3(0, 0, -10), 0.2f).SetEase(Ease.OutQuart);

                canvas_v.SetActive(true);
                canvas_c.SetActive(false);

                text_vs.GetComponent<TextMeshProUGUI>().text = "FIGHT";
                tween.Kill(); text_vs.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                tween = text_vs.GetComponent<TextMeshProUGUI>().DOColor(Color.red, 0.3f).SetLoops(-1, LoopType.Yoyo);

                break;

            case MODE.VS: // 全体

                /* カメラとUI設定 */
                GetComponent<Camera>().orthographicSize = orthographicSize;
                transform.DOMove(new Vector3(0, 0, -10),0.2f).SetEase(Ease.OutQuart);

                canvas_v.SetActive(true);
                canvas_c.SetActive(false);

                switch (playmode)
                {
                    case MODE.CHIKA:
                        text_vs.GetComponent<TextMeshProUGUI>().text = "チカ";
                        tween.Kill(); text_vs.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                        tween = text_vs.GetComponent<TextMeshProUGUI>().DOColor(new Color(255/255f,157/255f,107/255f), 0.3f).SetLoops(-1, LoopType.Yoyo);
                        break;
                    case MODE.YOU:
                        text_vs.GetComponent<TextMeshProUGUI>().text = "ヨウ";
                        tween.Kill(); text_vs.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                        tween = text_vs.GetComponent<TextMeshProUGUI>().DOColor(new Color(73 / 255f, 196 / 255f, 255 / 255f), 0.3f).SetLoops(-1, LoopType.Yoyo);
                        break;
                }

                break;

            case MODE.CHIKA: // ちか

                /* カメラとUI設定 */
                GetComponent<Camera>().orthographicSize = 280;
                transform.DOMove(new Vector3(300, 0, -10), 0.2f).SetEase(Ease.OutQuart);

                canvas_v.SetActive(false);
                canvas_c.SetActive(true);

                fixedJoystick_c.gameObject.SetActive(true);
                fixedJoystick_y.gameObject.SetActive(false);
                fixedJoystick = fixedJoystick_c;

                gauge = gauge_y;

                break;

            case MODE.YOU: // よう

                /* カメラとUI設定 */
                GetComponent<Camera>().orthographicSize = 280;
                transform.DOMove(new Vector3(-300, 0, -10), 0.2f).SetEase(Ease.OutQuart);

                canvas_v.SetActive(false);
                canvas_c.SetActive(true);

                fixedJoystick_c.gameObject.SetActive(false);
                fixedJoystick_y.gameObject.SetActive(true);
                fixedJoystick = fixedJoystick_y;

                gauge = gauge_c;

                break;
        }
    }



    /*
     * 矢印キーが押されたとき
     
    public void onArrow(ARROW arrow)
    {
        Debug.Log(arrow);
    }
    */

}
