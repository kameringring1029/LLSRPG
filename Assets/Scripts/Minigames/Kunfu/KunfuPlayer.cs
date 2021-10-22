﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunfuPlayer : MonoBehaviour
{
    GameObject effect;
    bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        effect = transform.GetChild(0).gameObject;
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * アクションを制御
     */
    public void actionFire(bool flg)
    {
        isFiring = flg;
        gameObject.GetComponent<Animator>().SetBool("isFiring", isFiring);
        if (isFiring == false) fireBeam();
    }

    /*
     * 子オブジェクトのビームを制御
     */
    public void fireBeam()
    {
        Debug.Log("fire");
        effect.GetComponent<Animator>().SetBool("isFiring", isFiring);
    }
}