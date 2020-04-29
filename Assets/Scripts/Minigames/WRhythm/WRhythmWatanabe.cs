using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ワタナベ用クラス
 */
public class WRhythmWatanabe : MonoBehaviour
{
    Vector2 jump_power = new Vector2(-200, 200);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void dive()
    {
        // Animationの切替
        gameObject.GetComponent<Animator>().SetTrigger("trg_dive");

        // 柵より表示を手前に
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;

        // 
        gameObject.GetComponent<Rigidbody2D>().AddForce(jump_power);
    }

    private void OnBecameInvisible()
    {
        if (!gameObject.GetComponent<SpriteRenderer>().isVisible)
        {
            Instantiate<GameObject>(Resources.Load<GameObject>("Minigame/w_rhythm/watanabe"));
            Destroy(gameObject);
        }

    }
}
