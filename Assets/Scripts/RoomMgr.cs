using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMgr : MonoBehaviour {

    public GameObject cursor;
    private Map map;

    // Use this for initialization
    void Start() {

        map = gameObject.GetComponent<Map>();
    }

    public void init()
    {
        Debug.Log("init room");

        //--- マップ生成 ---//
        gameObject.GetComponent<Map>().positioningBlocks();


        //--- Unit配置 ---//
        gameObject.GetComponent<Map>().positioningAllyUnits(new int[0]);

        // カーソルを味方ユニットの位置に移動
        cursor.GetComponent<cursor>().moveCursolToUnit(map.allyUnitList[map.allyUnitList.Count - 1]);


        moveUnits();
    }


    private void moveUnits()
    {

        // map.allyUnitList[0].GetComponent<Unit>().viewMovableArea();
        // map.allyUnitList[0].GetComponent<Unit>().changePosition(5, 5, true);
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


    //++++++++++++++++++++++//
    //+++ 以上ボタン処理 +++//
    //++++++++++++++++++++++//

}
