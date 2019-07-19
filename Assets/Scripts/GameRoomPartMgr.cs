using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

using General;
using Information;

public class GameRoomPartMgr : MonoBehaviour
{

    private int nowCursorPosition = 0;
    private List<ACTION> actionList;

    private List<GameObject> roomSelectBtns;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    
    public void init()
    {
        roomSelectBtns = new List<GameObject>();

        List<string> roomList = new List<string>( getRoomList());

        GameObject btnPref = Resources.Load<GameObject>("Prefab/ScrollViewButtonPrefab");

        //Content取得(ボタンを並べる場所)
        RectTransform content = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<RectTransform>();

        //Contentの高さ決定
        //(ボタンの高さ+ボタン同士の間隔)*ボタン数
        float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
        float btnHeight = btnPref.GetComponent<LayoutElement>().preferredHeight;
        content.sizeDelta = new Vector2(0, (btnHeight + btnSpace) * roomList.Count);

        for (int no = 0; no < roomList.Count; no++)
        {
            //ボタン生成
            GameObject btn = (GameObject)Instantiate(btnPref, content);

            btn.GetComponent<RectTransform>().sizeDelta = new Vector2(btnPref.GetComponent<RectTransform>().sizeDelta[0], btnPref.GetComponent<RectTransform>().sizeDelta[1] * 2);

            //ボタンのテキスト変更
            btn.transform.GetComponentInChildren<Text>().text = roomList[no];
            Debug.Log("room:"+ roomList[no]);

            //色変更
            btn.GetComponent<Image>().color = new Color(92 / 255f, 92 / 255f, 128 / 255f, 255 / 255f);

            //ボタンのクリックイベント登録
            int tempno = no;
            btn.transform.GetComponent<Button>().onClick.AddListener(() => selectRoom(tempno));

            roomSelectBtns.Add(btn);
        }
    }



    string[] getRoomList()
    {
        return GameObject.Find("Main Camera").GetComponent<WebsocketAccessor>().getRoomList();
    }

    /*

    //--- Menu中のカーソルを移動 ---//
    // 
    public void moveCursor(int vector)
    {

        List<ACTION> unitActionList = selectedUnit.getActionableList();

        nowCursorPosition += vector;

        // カーソル位置がオーバーフローしたとき
        if (nowCursorPosition < 0) nowCursorPosition = unitActionList.Count - 1;
        if (nowCursorPosition > unitActionList.Count - 1) nowCursorPosition = 0;

        Debug.Log("nowcursor:" + nowCursorPosition);

        foreach (GameObject btn in actionSelectBtn)
        {
            if (btn == actionSelectBtn[nowCursorPosition])
            {
                btn.GetComponent<Image>().color = new Color(192 / 255f, 192 / 255f, 228 / 255f, 255 / 255f);
            }
            else
            {
                btn.GetComponent<Image>().color = new Color(192 / 255f, 192 / 255f, 228 / 255f, 192 / 255f);
            }
        }
    }
    */
    
    void selectRoom(int no)
    {
        GameObject.Find("Main Camera").GetComponent<WebsocketAccessor>().sendws("setPair;"+roomSelectBtns[no].transform.GetComponentInChildren<Text>().text);
    }



    public void StartCreateRoom()
    {
        GameObject.Find("Main Camera").GetComponent<WholeMgr>().startSelectMap();
    }

    public void createRoom(mapinfo map)
    {
        GameObject.Find("Main Camera").GetComponent<WebsocketAccessor>().sendws("createRoom;" + JsonUtility.ToJson(map));
    }
}
