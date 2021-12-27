using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

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
