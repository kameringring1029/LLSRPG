using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

// Tweet用： https://blog.gigacreation.jp/entry/2020/10/04/223712
#if !UNITY_EDITOR && UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class WRhythmButtonMgr : MonoBehaviour
{

#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern string TweetFromUnity(string rawMessage);
#endif

    public void onClickRestart()
    {
        // 現在のScene名を取得する
        Scene loadScene = SceneManager.GetActiveScene();
        // Sceneの読み直し
        SceneManager.LoadScene(loadScene.name);
    }


    public void onClickTweet()
    {

        string message = "りずむ！";
        message += WatanabeManager.Instance.score + "点";

        message += " https://koke.link/wp/?p=627";

#if !UNITY_EDITOR && UNITY_WEBGL
        TweetFromUnity(message);
#endif

        Debug.Log("tweeeeeet : " + message);
    }

}
