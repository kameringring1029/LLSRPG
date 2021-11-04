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

        gameObject.GetComponent<Animator>().SetInteger("arrow", (int)KunfuMgr.ARROW.NULL);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * アクションを制御
     */
    public void actionFire(int charged, bool isfiring)
    {
        Debug.Log("actionFire");

        isFiring = isfiring;

        actionCharge(KunfuMgr.ARROW.NULL);

        gameObject.GetComponent<Animator>().SetBool("isFiring", isFiring);
        gameObject.GetComponent<Animator>().SetInteger("arrow", (int)KunfuMgr.ARROW.NULL);
        //fireBeam();
        if (isFiring == false) fireBeam();
    }

    /*
     * 子オブジェクトのビームを制御
     */
    public void fireBeam()
    {
        effect.GetComponent<Animator>().SetBool("isFiring", isFiring);
        if (isFiring == true) KunfuMgr.Instance.changeGauge();
    }

    /*
     * チャージのAinmation
     */
    public void actionCharge(KunfuMgr.ARROW arrow)
    {
        // beamのAnimation
        transform.GetChild(transform.childCount - 1).GetComponent<Animator>().SetTrigger("trgCharge");
        
    }

    public void setReady()
    {
        StartCoroutine(setReadyCoroutine());
    }
    IEnumerator setReadyCoroutine()
    {
        GetComponent<Animator>().SetBool("isReading", true);
        yield return new WaitForSeconds(3f);
        GetComponent<Animator>().SetBool("isReading", false);
    }

    public void setArrow(KunfuMgr.ARROW arrow)
    {
        GetComponent<Animator>().SetInteger("arrow", (int)arrow);
    }

    public void setGuard()
    {

        GetComponent<Animator>().SetBool("isGuarding", true);
    }

    public void setWin()
    {
        GetComponent<Animator>().SetBool("isGuarding", false);
        GetComponent<Animator>().SetInteger("result", 1);
    }

    public void setLose()
    {
        GetComponent<Animator>().SetBool("isGuarding", false);
        GetComponent<Animator>().SetInteger("result", 2);
    }

}
