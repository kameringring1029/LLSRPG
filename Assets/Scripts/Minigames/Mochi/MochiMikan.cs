using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MochiMikan : MonoBehaviour
{
    public GameObject dropper;

    void Start()
    {
        dropper = gameObject;
        gameObject.GetComponent<FixedJoint2D>().connectedBody = GameObject.Find("mikanspot").GetComponent<Rigidbody2D>(); // 落とす人に接着

    }

    // Update is called once per frame
    void Update()
    {
        // 設置失敗したら消去
        if (!gameObject.GetComponent<FixedJoint2D>().enabled & !gameObject.GetComponent<Renderer>().isVisible)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("collision:" + collider.gameObject.name);

        switch (collider.gameObject.name)
        {
            case "dropper": // 落とす人に接触したら
                dropper = collider.gameObject;
                gameObject.GetComponent<FixedJoint2D>().connectedBody = dropper.GetComponent<Rigidbody2D>(); // 落とす人に接着
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f; // 重力ON

                if(gameObject.tag == "mochimikan") { // 積んで積んであるみかん
					gameObject.GetComponent<EdgeCollider2D>().enabled = false; // 他と干
					gameObject.GetComponent<CapsuleCollider2D>().enabled = false; // 他と干
				}
                return;

            default:
                return;

        }

    }

    /* 落とされたとき */
    public void release()
    {
        gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.GetComponent<FixedJoint2D>().enabled = false;
        GameObject.Find("dropper").GetComponent<MochiDropper>().releaseMikan();
    }
}
