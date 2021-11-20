using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/*
 * えもの用クラス
 */
public class WRhythmPrey : MonoBehaviour
{
    Vector2 jump_power = new Vector2(10000, 40000);

    enum EVAL { MISS, GOOD, PERFECT}
    EVAL eval = EVAL.MISS;

    bool touching = false;

    // Start is called before the first frame update
    void Start()
    {
        randSprite();
        //StartCoroutine(vanishProcess());

        gameObject.GetComponent<Rigidbody2D>().AddForce(jump_power);
        //gameObject.GetComponent<Rigidbody2D>().angularVelocity
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,0.100f));
    }

    /* ランダムにSprite変更するよ */
    void randSprite()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Minigame/w_rhythm/prey");
        int rand = Random.Range(0, sprites.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[rand];
    }

    /* 衝突した時の処理 */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!WatanabeManager.Instance.catching) { return; }

        Debug.Log("pray collision:" + collision.gameObject.name);

        // ワタナベに結合
        gameObject.GetComponent<FixedJoint2D>().connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
        gameObject.GetComponent<FixedJoint2D>().enabled = true;

        // ダブルで判定しちゃうの避け
        collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

        // 接触箇所からスコアを決定
        int score = 0;

        foreach(ContactPoint2D point in collision.contacts)
        {
            if(point.point.y < -1.0 && point.point.y > -1.2)
            {
                eval = EVAL.PERFECT;
                score = 200;
            }
            else
            {
                eval = EVAL.GOOD;
                score = 100;
            }
            Debug.Log("hit:" + point.point);

        }

        instantiateEvaluate();

        // スコア設定
        WatanabeManager.Instance.changeScore(score);
    }

    /* 一定時間後に消えるよ */
    IEnumerator vanishProcess()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


    private void OnBecameInvisible()
    {
        if(eval == EVAL.MISS)
        {
         //   instantiateEvaluate();
        }

          Destroy(gameObject);     

    }

    void instantiateEvaluate()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Minigame/w_rhythm/Effect_eval"), GameObject.Find("Canvas").transform);

        obj.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Emotion/surprised")[(int)eval];

        
    }
}
