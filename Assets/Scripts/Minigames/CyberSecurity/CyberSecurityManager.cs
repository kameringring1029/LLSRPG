using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberSecurityManager : MonoBehaviour
{
    enum SCREEN_MODE{LANDSCAPE, PORTRAIT};

    SCREEN_MODE screen_mode;

    public int x_max, y_max; // 縦横長さ設定（向き考えてない）
    private int height, width; // 画面の向きに合わせた幅と高さ

    public GameObject web_landscape; // 横向き用のWebのSprite
    public GameObject web_portrait; // 縦向き用のWebのSprite


    void Start()
    {

        // アスペクト比によって表示を変更
        float currentAspect = (float)Screen.height / (float)Screen.width;

        if(currentAspect > 1) /* portrait */
        {
            screen_mode = SCREEN_MODE.PORTRAIT;
            height = x_max; width = y_max;
            web_landscape.SetActive(false);
            web_portrait.SetActive(true);
        }
        else /* landscape */
        {
            screen_mode = SCREEN_MODE.LANDSCAPE;
            height = y_max; width = x_max;
            web_landscape.SetActive(true);
            web_portrait.SetActive(false);
        }

        GetComponent<CameraSizeUpdater>().setHeight(height);
        GetComponent<CameraSizeUpdater>().setWidth(width);


        StartCoroutine(createBeam());
    }


    void Update()
    {
        // マウス左クリックチェック
        if (Input.GetMouseButtonDown(0))
        {
            // マウス座標取得
            Debug.Log(Input.mousePosition);
        }
        // マウス左クリックチェック
        if (Input.GetMouseButtonUp(0))
        {
            // マウス座標取得
            Debug.Log(Input.mousePosition);
        }

    }

    IEnumerator createBeam()
    {
        /* 範囲内ランダム */
        int pre_x = Random.Range(-width / 4 / 2 + 2, width / 4 / 2 - 2) * 4 + 2;
        int pre_y = Random.Range(-height / 6 / 2 + 2, height / 6 / 2 - 2) * 6 + 1;

        while (true)
        {
            float t = Random.Range(0.30f, 0.50f);
            yield return new WaitForSeconds(t);

            /* 前回からちょっと動く */
            int x = pre_x;
            int y = pre_y;
            if (exRand(-1,1,0) == 1)
            {
                x += exRand(-1, 1, 0) * 5;
            }
            else
            {
                y += exRand(-1, 1, 0) * 5;
            }

            /* ビーム作る */
            Vector3 position = new Vector3(x, y, 0);
            Instantiate(Resources.Load<GameObject>("Minigame/cybersecurity/cs_beam"), position, Quaternion.identity);

            pre_x = x; pre_y = y;
        }
    }

    /*
     * exRand  範囲内かつ一部の数字を覗いてランダムに
     * return:乱数
     * min:範囲内最小、max:範囲内最大、exclude:除外される数
     */
    private int exRand(int min, int max, int exclude)
    {
        int rand;
        do
        {
            rand = Random.Range(min, max+1);
        } while (rand == exclude);

        return rand;
    }




}
