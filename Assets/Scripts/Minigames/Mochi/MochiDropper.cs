using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MochiDropper : MonoBehaviour
{
    static float speed = 0.1f;

    static float x_max = 5f; // がめんはし
    float x_min;

    int move_vect;

    void Start()
    {
        x_min = x_max * -1f;
        move_vect = +1;
    }

    // Update is called once per frame
    void Update()
    {
        /* ほうこうてんかん */
        if(gameObject.GetComponent<Transform>().position.x >= x_max && move_vect == +1)
        {
            move_vect = -1;
            gameObject.GetComponent<Animator>().SetInteger("moveVector", -1);
        }
        else if(gameObject.GetComponent<Transform>().position.x <= x_min && move_vect == -1)
        {
            move_vect = +1;
            gameObject.GetComponent<Animator>().SetInteger("moveVector", 1);
        }

        // いどう
        gameObject.GetComponent<Transform>().position += new Vector3(move_vect * speed, 0, 0);

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("collision:" + collider.gameObject.name);

        switch (collider.gameObject.name)
        {
            case "mikan(Clone)": // 落とす人に接触したら
                gameObject.GetComponent<Animator>().SetBool("isHolding", true);
                return;

            default:
                return;

        }

    }
}
