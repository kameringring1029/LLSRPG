using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;
using General;
using UnityEngine.UI;

public class EditMapMgr : MonoBehaviour {

    public GameObject cursor;
    private Map map;
    private int nowblocktype = 0;

    public GameObject infoPanel;
    public GameObject mapList;
    public GameObject btnPref;  //ボタンプレハブ

    private List<mapinfo> maps;


    // Use this for initialization
    void Start() {

        cursor = GameObject.Find("cursor");
        map = gameObject.GetComponent<Map>();
    }

	
	// Update is called once per frame
	void Update () {
		
	}


    public void init()
    {
        mapList.SetActive(true);

        setMapList();
    }



    // Mapリストを取得してリストUIに反映
    //
    private void setMapList()
    {
        //Content取得(ボタンを並べる場所)
        RectTransform content = GameObject.Find("MapListContent").GetComponent<RectTransform>();

        getMapsFromLocal();

        //Contentの高さ決定
        //(ボタンの高さ+ボタン同士の間隔)*ボタン数
        float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
        float btnHeight = btnPref.GetComponent<LayoutElement>().preferredHeight;
        content.sizeDelta = new Vector2(0, (btnHeight + btnSpace) * maps.Count);

        for (int no = 0; no < maps.Count; no++)
        {        

            //ボタン生成
            GameObject btn = (GameObject)Instantiate(btnPref);

            //ボタンをContentの子に設定
            btn.transform.SetParent(content, false);

            //ボタンのテキスト変更
            btn.transform.GetComponentInChildren<Text>().text = maps[no].name.ToString();

            //ボタンのクリックイベント登録
            int tempno = no;
            btn.transform.GetComponent<Button>().onClick.AddListener(() => startEditMap(tempno));

            
        }

    }

    // Map情報をローカルファイルから取得
    private void getMapsFromLocal()
    {
        maps = new List<mapinfo>();

        mapinfo map = JsonUtility.FromJson<mapinfo>(new MapPlain().mapStruct());
        maps.Add(map);
        map = JsonUtility.FromJson<mapinfo>(new MapOtonokiProof().mapStruct());
        maps.Add(map);

        /*
        List<string> mapjsons = new List<string>();
        mapjsons = LocalStorage.GetFileNames(LocalStorage.GetPath(), "json");


        for (int i=0; i<mapjsons.Count; i++)
        {
            maps.Add(JsonUtility.FromJson<mapinfo>(LocalStorage.LoadFromLocal( mapjsons[i])));
        }

    */

    }



    private void startEditMap(int mapid)
    {
        mapList.SetActive(false);
        Debug.Log("EditMap");

        Debug.Log(mapid);

        //--- マップ生成 ---//
        gameObject.GetComponent<Map>().settingforEditMap();
        gameObject.GetComponent<Map>().positioningBlocks(maps[mapid]);

        infoPanel.SetActive(true);

        nowblocktype = 2;
        cursor.GetComponent<SpriteRenderer>().sprite
            = gameObject.GetComponent<Map>().getBlockTypebyid(nowblocktype).GetComponent<SpriteRenderer>().sprite;
        cursor.GetComponent<cursor>().moveCursorToAbs(map.x_mass, map.y_mass);


        //saveMap();
    }


    public void saveMap()
    {
        // LocalStorage.LoadLocalStageData();
        map.mapinformation.name = map.mapinformation.name + System.DateTime.Now.ToString("yyyy-MM-dd_hh-mm");
        LocalStorage.SaveToLocal(JsonUtility.ToJson(map.mapinformation),
           map.mapinformation.name + ".json");

    }



    //--- 今のBlock上のアイテムを確認し表示に反映 ---//
    public void changeInfoWindow()
    {
        int[] nowCursolPosition = new int[2];

        nowCursolPosition[0] = cursor.GetComponent<cursor>().nowPosition[0];
        nowCursolPosition[1] = cursor.GetComponent<cursor>().nowPosition[1];

        infoPanel.GetComponent<DisplayInfo>().displayBlockInfo(map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<FieldBlock>());
        
    }





    //++++++++++++++++++++++//
    //+++ 以下ボタン処理 +++//
    //++++++++++++++++++++++//

    //--- 十字ボタンが押されたときの挙動 ---//
    public void pushArrow(int x, int y)
    {
        Debug.Log("pushArrow");

        cursor.GetComponent<cursor>().moveCursor(x, y);
    }


    //--- Aボタンが押されたときの挙動 ---//
    public void pushA()
    {
        Debug.Log("pushA");

        int x = cursor.GetComponent<cursor>().nowPosition[0];
        int y = cursor.GetComponent<cursor>().nowPosition[1];

        map.mapinformation.mapstruct[y * map.y_mass * 2 + x] = nowblocktype;

        map.setBlock(nowblocktype, x, y);
    }


    //--- Bボタンが押されたとき＝キャンセル処理 ---//
    public void pushB()
    {
        Debug.Log("pushB");
        saveMap();
        Application.LoadLevel("Main"); // Reset
    }


    //--- フィールドブロックが選択されたとき ---//
    public void pushBlock(int x, int y)
    {

        Debug.Log("pushBlock");

        cursor.GetComponent<cursor>().moveCursorToAbs(x, y);
        pushA();
        
    }


    public void pushR()
    {

        nowblocktype++;
        if (nowblocktype > 4) nowblocktype = 1;
        cursor.GetComponent<SpriteRenderer>().sprite 
            = gameObject.GetComponent<Map>().getBlockTypebyid(nowblocktype).GetComponent<SpriteRenderer>().sprite;

    }

    public void pushL()
    {
        float nowCameraSize = gameObject.GetComponent<Camera>().orthographicSize;

        if (nowCameraSize == 1.5f)
        {
            gameObject.GetComponent<Camera>().orthographicSize = 3;
        }
        else
        {
            gameObject.GetComponent<Camera>().orthographicSize = 1.5f;
        }

    }

    //++++++++++++++++++++++//
    //+++ 以上ボタン処理 +++//
    //++++++++++++++++++++++//


}
