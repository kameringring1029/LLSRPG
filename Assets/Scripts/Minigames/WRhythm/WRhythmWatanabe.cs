using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ワタナベ用クラス
 */
public class WRhythmWatanabe : MonoBehaviour
{
    Vector2 jump_power = new Vector2(-2000, 2000);

    // Start is called before the first frame update
    void Start()
    {
        scroll();
    }

    // Update is called once per frame
    void Update()
    {
  //      if(transform.position.x >= WatanabeManager.Instance.pos_jump.x)
  //          gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 0));


    }

    public void scroll()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2000, 0));

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

    private void OnBecameInvisible()
    {
      //  if (!gameObject.GetComponent<SpriteRenderer>().isVisible)
      //  {
          //  WatanabeManager.Instance.removeWatanabe(gameObject);
            Destroy(gameObject);
      //  }

    }
}
