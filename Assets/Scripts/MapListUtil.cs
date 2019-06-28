using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;
using General;
using UnityEngine.UI;
using UnityEngine.Networking;

/*
 * Map情報を取得してリスト表示するためのUtilityクラス
 *
 */

public class MapListUtil : MonoBehaviour
{

    private List<mapinfo> mapinfos; //取得したマップ情報を格納するリスト


    public void getMapsFromServer()
    {
        StartCoroutine("getMapsFromServerProcess");
    }

    // Map情報をサーバから取得
    IEnumerator getMapsFromServerProcess()
    {

        mapinfos = new List<mapinfo>();

        Debug.Log("request maps from server");

        UnityWebRequest request = UnityWebRequest.Get("http://koke.link:3000/llsrpg/map/get/all");
        // 下記でも可
        // UnityWebRequest request = new UnityWebRequest("http://example.com");
        // methodプロパティにメソッドを渡すことで任意のメソッドを利用できるようになった
        // request.method = UnityWebRequest.kHttpVerbGET;

        // リクエスト送信
        yield return request.Send();

        // 通信エラーチェック
        if (request.isNetworkError)
        {
            Debug.Log(request.error);

            getMapsFromLocal();
        }
        else
        {
            if (request.responseCode == 200)
            {
                // UTF8文字列として取得する
                string text = request.downloadHandler.text;

                Debug.Log("success request! result:" + text);

                // jsonをパースしてListに格納
                // jsonutilityそのままだと配列をパースできないのでラッパを使用 https://qiita.com/akira-sasaki/items/71c13374698b821c4d73
                mapinfo[] maparray;
                maparray = JsonUtilityHelper.MapFromJson<mapinfo>(text);

                for (int i = 0; i < maparray.Length; i++)
                {
                    mapinfos.Add(maparray[i]);
                }

                // UIのMapリストを設定
                setMapList();
            }
        }
        
    }




    // Mapリストを取得してリストUIに反映
    //
    public void setMapList()
    {
        GameObject btnPref = Resources.Load<GameObject>("Prefab/ScrollViewButtonPrefab");

        //Content取得(ボタンを並べる場所)
        RectTransform content = GameObject.Find("MapListContent").GetComponent<RectTransform>();

        //Contentの高さ決定
        //(ボタンの高さ+ボタン同士の間隔)*ボタン数
        float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
        float btnHeight = btnPref.GetComponent<LayoutElement>().preferredHeight;
        content.sizeDelta = new Vector2(0, (btnHeight + btnSpace) * mapinfos.Count);

        for (int no = 0; no < mapinfos.Count; no++)
        {

            //ボタン生成
            GameObject btn = (GameObject)Instantiate(btnPref);

            //ボタンをContentの子に設定
            btn.transform.SetParent(content, false);

            //ボタンのテキスト変更
            btn.transform.GetComponentInChildren<Text>().text = mapinfos[no].name.ToString();

            //ボタンのクリックイベント登録
            int tempno = no;
            switch (gameObject.GetComponent<WholeMgr>().wholemode)
            {
                // SRPGのときとMapエディタモードのときで分岐
                case WHOLEMODE.GAME:
                    btn.transform.GetComponent<Button>().onClick.AddListener(() => gameObject.GetComponent<GameMgr>().startGame(mapinfos[tempno]));
                    break;
                case WHOLEMODE.OTHER:
                    btn.transform.GetComponent<Button>().onClick.AddListener(() => gameObject.GetComponent<EditMapMgr>().startEditMap(mapinfos[tempno]));
                    break;
            }
 

        }

    }

    // Map情報をローカルファイルから取得
    public void getMapsFromLocal()
    {
        mapinfos = new List<mapinfo>();

        // JSONフォルダからの読み込み
        TextAsset[] json = Resources.LoadAll<TextAsset>("JSON/");

        foreach (TextAsset mapjson in json)
        {
            string maptext = mapjson.text;
            mapinfo map = JsonUtility.FromJson<mapinfo>(maptext);
            mapinfos.Add(map);
        }

        // UIのMapリストを設定
        setMapList();
    }


}
