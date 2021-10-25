using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KunfuMgr : SingletonMonoBehaviour<KunfuMgr>
{
    private bool isFiring;

    public enum ARROW { UP, DOWN, LEFT, RIGHT, NULL}
    ARROW nowcharge;
    ARROW cursol;

    public GameObject player;
    public GameObject effect;

    public FixedJoystick fixedJoystick;


    public GameObject _beamcharge;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
        nowcharge = ARROW.NULL;
        cursol = ARROW.NULL;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        /* JoyStick */
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;

        //Debug.Log(direction.x);

        if (direction.x > 0.9 && cursol == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge == ARROW.RIGHT) // Hitしたとき
            {
                nowcharge = ARROW.NULL;
                player.GetComponent<KunfuPlayer>().actionCharge(nowcharge);
            }
            cursol = ARROW.RIGHT;
        }
        else if (direction.x <-0.9 && cursol == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge == ARROW.LEFT) // Hitしたとき
            {
                nowcharge = ARROW.NULL;
                player.GetComponent<KunfuPlayer>().actionCharge(nowcharge);

            }
            cursol = ARROW.LEFT;
        }
        else if (direction.z > 0.9 && cursol == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge == ARROW.UP) // Hitしたとき
            {
                nowcharge = ARROW.NULL;
                player.GetComponent<KunfuPlayer>().actionCharge(nowcharge);
            }
            cursol = ARROW.UP;
        }
        else if (direction.z < -0.9 && cursol == ARROW.NULL)
        {
            Debug.Log(direction);
            if (nowcharge == ARROW.DOWN) // Hitしたとき
            {
                nowcharge = ARROW.NULL;
                player.GetComponent<KunfuPlayer>().actionCharge(nowcharge);
            }
            cursol = ARROW.DOWN;
        }
        else if(cursol != ARROW.NULL && // joystickのNewtralセット
            direction.x < 0.8 && direction.x > -0.8 && direction.z < 0.8 && direction.z > -0.8)
        {
            cursol = ARROW.NULL;
        }



        /* 新しいchargeの生成 */
        if (nowcharge == ARROW.NULL)
        {
            int rand = UnityEngine.Random.Range(0, 4);
            ARROW rand_a = (ARROW)Enum.ToObject(typeof(ARROW), rand);

            GameObject beamcharge = Instantiate<GameObject>(_beamcharge, player.transform);
            Vector3 pos = beamcharge.transform.position;

            switch (rand_a)
            {
                case ARROW.UP:
                    pos.y += 128; pos.x += 64;
                    nowcharge = ARROW.UP;
                    break;
                case ARROW.DOWN:
                    pos.y -= 128; pos.x += 64;
                    nowcharge = ARROW.DOWN;
                    break;
                case ARROW.RIGHT:
                    pos.x += 192;
                    nowcharge = ARROW.RIGHT;
                    break;
                case ARROW.LEFT:
                    pos.x -= 64;
                    nowcharge = ARROW.LEFT;
                    break;

            }

            beamcharge.transform.position = pos;

        }


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
