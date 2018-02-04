using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;
using General;

public class EditMapMgr : MonoBehaviour {

    public GameObject cursor;
    private Map map;
    private int nowblocktype = 0;

    public GameObject infoPanel;

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
        Debug.Log("EditMap");

        //--- マップ生成 ---//
        gameObject.GetComponent<Map>().settingforEditMap();
        gameObject.GetComponent<Map>().positioningBlocks(new MapPlain());

        infoPanel.SetActive(true);

        nowblocktype = 2;
        cursor.GetComponent<SpriteRenderer>().sprite 
            = gameObject.GetComponent<Map>().getBlockTypebyid(nowblocktype).GetComponent<SpriteRenderer>().sprite;
        cursor.GetComponent<cursor>().moveCursorToAbs(map.x_mass, map.y_mass);


        saveMap();
    }


    public void saveMap()
    {
        LocalStorage.LoadLocalStageData();
        LocalStorage.SaveToLocal(JsonUtility.ToJson(map.mapinformation), map.mapinformation.name+".json");

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

        map.setBlock(nowblocktype, cursor.GetComponent<cursor>().nowPosition[0], cursor.GetComponent<cursor>().nowPosition[1]);
    }


    //--- Bボタンが押されたとき＝キャンセル処理 ---//
    public void pushB()
    {
        Debug.Log("pushB");

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
