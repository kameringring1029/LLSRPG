using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;

public class Unit : MonoBehaviour {

    private int[] nowPosition = new int[2];
    private GameMgr GM;
    private int camp;

    public Kanan_TT unitInfo;


	// Use this for initialization
	void Start () {
        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();
        nowPosition[0] = 0;
        nowPosition[1] = 0;

        unitInfo = new Kanan_TT();
    }

    public void init(int x, int y, int c)
    {

        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();

        camp = c;
        if (camp == 1) gameObject.GetComponent<SpriteRenderer>().flipX = true;

        changePosition(x, y);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void changePosition(int x, int y)
    {
        // 移動元BlockからUnit情報を削除
        GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = null;

        nowPosition[0] = x;
        nowPosition[1] = y;
        
        gameObject.GetComponent<Transform>().position
             = GM.FieldBlocks[x, y].GetComponent<Transform>().position;

        // 移動先BlockにUnit情報を設定
        GM.FieldBlocks[x, y].GetComponent<FieldBlock>().GroundedUnit = gameObject;
    }
}
