using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunfuBtn : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickFire()
    {
        //KunfuMgr.Instance.onFire();
    }

    public void onClickChika()
    {
        KunfuMgr.Instance.startPlay(KunfuMgr.MODE.CHIKA);
    }

    public void onClickYou()
    {
        KunfuMgr.Instance.startPlay(KunfuMgr.MODE.YOU);
    }

    public void onClickTweet()
    {
        Debug.Log("tweeeeeet" + KunfuMgr.Instance.charged_power);
        Application.ExternalCall("tweet", KunfuMgr.Instance.charged_power);
    }

    public void onClickRetry()
    {
        Application.LoadLevel("Kungfu"); // Reset
    }

    /*
     * Arrow Key
     
    public void onClickLeft()
    {
        KunfuMgr.Instance.onArrow(KunfuMgr.ARROW.LEFT);
    }
    public void onClickRight()
    {
        KunfuMgr.Instance.onArrow(KunfuMgr.ARROW.RIGHT);
    }
    public void onClickUp()
    {
        KunfuMgr.Instance.onArrow(KunfuMgr.ARROW.UP);
    }
    public void onClickDown()
    {
        KunfuMgr.Instance.onArrow(KunfuMgr.ARROW.DOWN);
    }
    */
}
