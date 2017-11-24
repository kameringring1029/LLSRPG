﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour {

    private GameObject Camera;
    private GameMgr GM;

    public int[] nowPosition = new int[2];


    // Use this for initialization
    void Start () {
        Camera = GameObject.Find("Main Camera");
        GM = Camera.GetComponent<GameMgr>();

        nowPosition[0] = 0;
        nowPosition[1] = 0;
    }
	
	// Update is called once per frame
	void Update () {

	}

    // 相対移動（現在の座標から）
    public void moveCursor(int x, int y)
    {
        // マップ上カーソルの相対移動（現在の座標から）
        gameObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3((x - y)/2.0f, -(x / 4.0f + y / 4.0f), 0);
        Camera.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, -10);
        nowPosition[0] = nowPosition[0] + x;
        nowPosition[1] = nowPosition[1] + y;

        if(GM.enabled == true)
         Camera.GetComponent<GameMgr>().changeInfoWindow();
        
        Debug.Log(nowPosition[0] + "/" + nowPosition[1]);
    }

    // 絶対座標移動
    public void moveCursorToAbs(int X, int Y)
    {
        gameObject.GetComponent<Transform>().position = 
            Camera.GetComponent<Map>().FieldBlocks[X, Y].GetComponent<Transform>().position;
        Camera.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, -10);
        nowPosition[0] = X;
        nowPosition[1] = Y;

        if (GM.enabled == true)
            Camera.GetComponent<GameMgr>().changeInfoWindow();
    }


    //--- カーソルをユニットの位置に移動 ---//
    public void moveCursolToUnit(GameObject unit)
    {
        moveCursorToAbs(
            unit.GetComponent<Unit>().nowPosition[0],
            unit.GetComponent<Unit>().nowPosition[1]);
        
    }



    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }
}
