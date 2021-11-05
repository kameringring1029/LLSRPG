using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

#if !UNITY_EDITOR && UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class KunfuBtn : MonoBehaviour
{

#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern string TweetFromUnity(string rawMessage);
#endif

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
#if !UNITY_EDITOR && UNITY_WEBGL
        TweetFromUnity("Tweet Message"+ KunfuMgr.Instance.charged_power);
#endif
    }

    public void onClickRetry()
    {
        SceneManager.LoadScene("Kungfu"); // Reset
    }

    public void onClickBacktoMenu()
    {
        KunfuMgr.playmode = KunfuMgr.MODE.READY;
        SceneManager.LoadScene("Kungfu"); // Reset
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
