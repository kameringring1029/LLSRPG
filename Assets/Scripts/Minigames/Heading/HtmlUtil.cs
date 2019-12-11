using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HtmlUtil : MonoBehaviour
{
    private void Start()
    {
        GetUrl();
    }

    public void twitter()
    {
        Debug.Log("twitter");

        Application.ExternalCall("oauth");
    }

    /// <summary>
    /// WebGLアプリが動いているURL。
    /// </summary>
    public string Url;

    /// <summary>
    /// WebGLアプリが動いているURLを取得する。
    /// </summary>
    public void GetUrl()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("GetUrl() called.");
            Application.ExternalEval("SendMessage('" + transform.name + "', 'GetUrlEnd', window.location.href)");
        }
    }

    /// <summary>
    /// WebGLアプリが動いているURLを受け取る。
    /// </summary>
    /// <param name="url">アプリのURL。</param>
    public void GetUrlEnd(string url)
    {
        Debug.Log("GetUrlEnd('" + url + "') received.");
        this.Url = url;

        GameObject.Find("htmlText").GetComponent<Text>().text = url.Split('=')[1];
    }
}
