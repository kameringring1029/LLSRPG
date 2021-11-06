using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


// Tweet用： https://blog.gigacreation.jp/entry/2020/10/04/223712
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


    public void onClickRetry()
    {
        SceneManager.LoadScene("Kungfu"); // Reset
    }

    public void onClickBacktoMenu()
    {
        KunfuMgr.playmode = KunfuMgr.MODE.READY;
        SceneManager.LoadScene("Kungfu"); // Reset
    }

    public void onClickTweet()
    {
        KunfuMgr.MODE mode = KunfuMgr.playmode;
        KunfuMgr.MODE winner = KunfuMgr.Instance.winner;

        string message = "気功を";
        message += KunfuMgr.Instance.getScore() + "ぱわー集めて";

        switch (mode)
        {
            case KunfuMgr.MODE.CHIKA:
                message += "ヨウに";
                break;
            case KunfuMgr.MODE.YOU:
                message += "チカに";
                break;
        }

        if(mode == winner)
        {
            if(mode == KunfuMgr.MODE.CHIKA && KunfuMgr.Instance.charged_power > 24
                || mode == KunfuMgr.MODE.YOU && KunfuMgr.Instance.charged_power > 100)
            {
                message += "倒した！！！！";
            }
            else
            {
                message += "勝った！";
            }
        }
        else
        {
            message += "負けた…";
        }

        message += " https://koke.link/wp/?p=627";

#if !UNITY_EDITOR && UNITY_WEBGL
        TweetFromUnity(message);
#endif

        Debug.Log("tweeeeeet : " + message);
    }

}
