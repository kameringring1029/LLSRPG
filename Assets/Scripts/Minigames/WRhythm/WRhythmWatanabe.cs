using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ワタナベ用クラス
 */
public class WRhythmWatanabe : MonoBehaviour
{
    public bool zaiko;

    Vector2 jump_power = new Vector2(-200000, 300000);

    // Start is called before the first frame update
    void Start()
    {        
            scroll();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void scroll()
    {
        if (zaiko)
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-150000, 0));

    }

    public void dive()
    {
        // Animationの切替
        gameObject.GetComponent<Animator>().SetTrigger("trg_dive");

        // 柵より表示を手前に
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;

        // ジャンプ
        gameObject.GetComponent<Rigidbody2D>().AddForce(jump_power);

        // 足元の判定解除
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    /**/
    public void setAct()
    {
        GetComponent<Animator>().SetTrigger("trg_act");
        StartCoroutine(syncActAnimation());
    }
    public IEnumerator syncActAnimation()
    {
        Animator animator = GetComponent<Animator>();
        yield return null;
        // animator.Play("watanabe_act",-1,Time.time);
        animator.ForceStateNormalizedTime(1f/Time.time);

        Debug.Log("watanabe:sync");
    }

    private void OnBecameInvisible()
    {
            Destroy(gameObject);

    }
}
