using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Information;

public class cursor : MonoBehaviour {

    private GameObject Camera;
    private GameMgr GM;
    public int[] nowPosition = new int[2];


    // Use this for initialization
    void Start () {
        Camera = GameObject.Find("Main Camera");
        GM = Camera.GetComponent<GameMgr>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    // 相対移動（現在の座標から）
    public void moveCursor(int x, int y)
    {
        // マップ上カーソルの相対移動（現在の座標から）
        gameObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(x - y, -(x / 2.0f + y / 2.0f), 0);
        Camera.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, -10);
        nowPosition[0] = nowPosition[0] + x;
        nowPosition[1] = nowPosition[1] + y;

        Camera.GetComponent<GameMgr>().changeInfoWindow();
        
        Debug.Log(nowPosition[0] + "/" + nowPosition[1]);
    }

    // 絶対座標移動
    public void moveCursorToAbs(int X, int Y)
    {
        gameObject.GetComponent<Transform>().position = 
            Camera.GetComponent<GameMgr>().FieldBlocks[X, Y].GetComponent<Transform>().position;
        Camera.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, -10);
        nowPosition[0] = X;
        nowPosition[1] = Y;

        Camera.GetComponent<GameMgr>().changeInfoWindow();
    }


    //--- カーソルを味方ユニットの位置に移動 ---//
    public void moveCursolToUnit(int unitNo, List<GameObject> unitList)
    {
        moveCursorToAbs(
            unitList[unitNo].GetComponent<Unit>().nowPosition[0],
            unitList[unitNo].GetComponent<Unit>().nowPosition[1]);
        
    }



    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }
}
