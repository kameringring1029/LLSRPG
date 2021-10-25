using System.Collections;
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
    public void actionFire(int charged)
    {
        isFiring = true;
        actionCharge(KunfuMgr.ARROW.NULL);
        gameObject.GetComponent<Animator>().SetBool("isFiring", isFiring);
        //fireBeam();
        //if (isFiring == false) fireBeam();
    }

    /*
     * 子オブジェクトのビームを制御
     */
    public void fireBeam()
    {
        effect.GetComponent<Animator>().SetBool("isFiring", isFiring);
        KunfuMgr.Instance.changeGauge();
    }

    /*
     * 
     */
    public void actionCharge(KunfuMgr.ARROW arrow)
    {
        // beamのAnimationを
        transform.GetChild(transform.childCount - 1).GetComponent<Animator>().SetTrigger("trgCharge");
        
    }

}
