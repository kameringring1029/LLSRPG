using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunfuMgr : SingletonMonoBehaviour<KunfuMgr>
{
    private bool isFiring;

    public enum ARROW { UP, DOWN, LEFT, RIGHT}

    public GameObject player;
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * Fireキーが押されたとき
     */
    public void onFire()
    {
        Debug.Log("fire!");

        //いらないかも
        switch (isFiring)
        {
            case true:
                isFiring = false;
                break;

            case false:
                isFiring = true;
                break;
        }
        

        player.GetComponent<KunfuPlayer>().actionFire(isFiring);
    }

    /*
     * 矢印キーが押されたとき
     */
    public void onArrow(ARROW arrow)
    {
        Debug.Log(arrow);
    }
}
