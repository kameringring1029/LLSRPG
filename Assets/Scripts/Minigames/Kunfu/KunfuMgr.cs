using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunfuMgr : SingletonMonoBehaviour<KunfuMgr>
{
    private bool isFiring;

    public enum ARROW { UP, DOWN, LEFT, RIGHT}

    public GameObject player;
    public GameObject effect;

    public float speed;
    public FixedJoystick fixedJoystick;
    public Rigidbody rb;

    private Vector3 pre_direction;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;

        //Debug.Log(direction.x);

        if (direction.x > 0.99 && direction.x > pre_direction.x)
        {
            player.GetComponent<KunfuPlayer>().actionFire(true);
            Debug.Log(direction);
        }
        if (direction.x <-0.99 && direction.x < pre_direction.x)
        {
            player.GetComponent<KunfuPlayer>().actionFire(false);
            Debug.Log(direction);
        }

        pre_direction = direction;
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
