using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * えもの用クラス
 */
public class WRhythmPrey : MonoBehaviour
{
    Vector2 jump_power = new Vector2(100, 100);

    // Start is called before the first frame update
    void Start()
    {
        randSprite();
        StartCoroutine(vanishProcess());

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
        Debug.Log("collision");
    }

    /* 一定時間後に消えるよ */
    IEnumerator vanishProcess()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
