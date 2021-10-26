using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;

public class KunfuMgr : SingletonMonoBehaviour<KunfuMgr>
{
    private bool isFiring;

    public enum ARROW { UP, DOWN, LEFT, RIGHT, NULL}
    ARROW nowcharge_arrow;
    ARROW cursol_arrow;

    public GameObject player;
    public GameObject effect;

    public FixedJoystick fixedJoystick;

    float elapsed;


    public GameObject _beamcharge;
    int charged_power;
    public GameObject gauge;

    enum CAMMODE { VS, CH, YO}
    public GameObject canvas_v;
    public GameObject canvas_c;

    public GameObject time_gauge;

    // Start is called before the first frame update
    void Start()
    {
        /* 変数init */
        effect.SetActive(false);

        isFiring = false;
        nowcharge_arrow = ARROW.NULL;
        cursol_arrow = ARROW.NULL;
        charged_power = 1;
        elapsed = 0f;

        time_gauge.GetComponent<Image>().fillAmount = 1;

        /* カメラ調整 */
        changeCamera(CAMMODE.CH);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isFiring) return;

        elapsed += Time.deltaTime;
        time_gauge.GetComponent<Image>().fillAmount = 1f - elapsed/10f;
        if (elapsed > 10.0)
        {
            onFire();
        }

        /* JoyStick */
        catchJoystick();

        /* Fire　or　新しいchargeの生成 */
        if (nowcharge_arrow == ARROW.NULL)
            nextCharge();
        
    }

    /*
     * Fire!
     */
    private void onFire()
    {
        isFiring = true;

        // beamのサイズ変更と位置調整
        Vector3 scale = effect.transform.localScale;
        //scale.x += charged_power * 0.1f;
        scale.y += charged_power * 0.1f;
        effect.transform.localScale = scale;

        //Vector3 pos = effect.transform.position;
        //pos += new Vector3(0, (charged_power / 10.0f) * 32.0f, 0);
        //effect.transform.position = pos;

        effect.SetActive(true);

        // animation
        player.GetComponent<KunfuPlayer>().actionFire(charged_power);
    }


    /*
     * JoyStick制御 
     */
    private void catchJoystick()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;

        /* 上下左右にStickを倒しきったときの処理 */
        if (direction.x > 0.9 && cursol_arrow == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge_arrow == ARROW.RIGHT) // Hitしたとき
            {
                nowcharge_arrow = ARROW.NULL;
                player.GetComponent<KunfuPlayer>().actionCharge(nowcharge_arrow);
            }
            cursol_arrow = ARROW.RIGHT;
        }
        else if (direction.x < -0.9 && cursol_arrow == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge_arrow == ARROW.LEFT) // Hitしたとき
            {
                nowcharge_arrow = ARROW.NULL;
                player.GetComponent<KunfuPlayer>().actionCharge(nowcharge_arrow);

            }
            cursol_arrow = ARROW.LEFT;
        }
        else if (direction.z > 0.9 && cursol_arrow == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge_arrow == ARROW.UP) // Hitしたとき
            {
                nowcharge_arrow = ARROW.NULL;
                player.GetComponent<KunfuPlayer>().actionCharge(nowcharge_arrow);
            }
            cursol_arrow = ARROW.UP;
        }
        else if (direction.z < -0.9 && cursol_arrow == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge_arrow == ARROW.DOWN) // Hitしたとき
            {
                nowcharge_arrow = ARROW.NULL;
                player.GetComponent<KunfuPlayer>().actionCharge(nowcharge_arrow);
            }
            cursol_arrow = ARROW.DOWN;
        }
        /* Newtralリセット */
        else if (cursol_arrow != ARROW.NULL &&
            direction.x < 0.8 && direction.x > -0.8 && direction.z < 0.8 && direction.z > -0.8)
        {
            cursol_arrow = ARROW.NULL;
        }
    }

    /*
     * 次回Chargeの準備
     */
    private void nextCharge() {

            charged_power += 1;

            int rand = UnityEngine.Random.Range(0, 4);
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
     * ゲージ減らす処理
     */
    public void changeGauge()
    {
        // まずカメラ調整
        changeCamera(CAMMODE.VS);

        StartCoroutine(gaugereduce());
    }
    IEnumerator gaugereduce()
    {
        int damage = charged_power;
        while (damage > 0)
        {
            yield return new WaitForSeconds(0.1f);
            gauge.GetComponent<Slider>().value += 0.01f;
            damage = damage - 1;
        }
    }


    /*
     * カメラの寄りとCamvasを変える
     */
    void changeCamera(CAMMODE mode)
    {
        switch (mode)
        {
            case CAMMODE.VS: // 全体
                GetComponent<Camera>().fieldOfView = 178.5f;
                GetComponent<Transform>().position = new Vector3(0, 0, -10);

                canvas_v.SetActive(true);
                canvas_c.SetActive(false);

                break;

            case CAMMODE.CH: // ちか
                GetComponent<Camera>().fieldOfView = 176;
                GetComponent<Transform>().position = new Vector3 (300, 0, -10);

                canvas_v.SetActive(false);
                canvas_c.SetActive(true);

                break;

            case CAMMODE.YO: // よう
                GetComponent<Camera>().fieldOfView = 176;
                GetComponent<Transform>().position = new Vector3(-300, 0, -10);

                canvas_v.SetActive(false);
                canvas_c.SetActive(true);

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
