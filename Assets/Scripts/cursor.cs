using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Information;

public class cursor : MonoBehaviour {

    private GameObject Camera;
    private GameObject infoWindow;
    private GameObject infoText;
    private int[] nowPosition = new int[2];

    public GameObject movableArea;
    private List<GameObject> movableAreaList = new List<GameObject>();
    public GameObject reachArea;
    private List<GameObject> reachAreaList = new List<GameObject>();

    // Use this for initialization
    void Start () {
        Camera = GameObject.Find("Main Camera");
        infoWindow = GameObject.Find("InfoWindow");
        infoText = GameObject.Find("InfoText");
    }
	
	// Update is called once per frame
	void Update () {

	}

    // 相対移動（現在の座標から）
    public void moveCursor(int x, int y)
    {
        gameObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(x-y,  - (x/2.0f + y / 2.0f), 0);
        Camera.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, -10);
        nowPosition[0] = nowPosition[0] + x;
        nowPosition[1] = nowPosition[1] + y;

        changeInfoWindow();
    }

    // 絶対座標移動
    public void moveCursorToAbs(int X, int Y)
    {
        gameObject.GetComponent<Transform>().position = 
            Camera.GetComponent<GameMgr>().FieldBlocks[X, Y].GetComponent<Transform>().position;
        Camera.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, -10);
        nowPosition[0] = X;
        nowPosition[1] = Y;

        changeInfoWindow();
    }


    // 今のBlock上のアイテムを確認し表示に反映
    private void changeInfoWindow()
    {
        GameObject nowBlock = Camera.GetComponent<GameMgr>().FieldBlocks[nowPosition[0], nowPosition[1]];
        GameObject groundedUnit = nowBlock.GetComponent<FieldBlock>().GroundedUnit;

        // Unitが配置されていたら
        if(groundedUnit != null)
        {
            // Infomationを更新
            infoWindow.GetComponent<Image>().sprite =
                groundedUnit.GetComponent<SpriteRenderer>().sprite;

            infoText.GetComponent<Text>().text = groundedUnit.GetComponent<Unit>().unitInfo.outputInfo();
            Debug.Log(groundedUnit.GetComponent<Unit>().unitInfo.outputInfo());


            // 移動範囲を表示
            int movable = groundedUnit.GetComponent<Unit>().unitInfo.movable;
            for(int y = -movable; y <= movable; y++)
            {
                for(int x=abs(y)-movable; x<=movable - abs(y); x++)
                {
                    Vector3 position = Camera.GetComponent<GameMgr>().FieldBlocks[nowPosition[0] + x, nowPosition[1] + y].GetComponent<Transform>().position;
                    movableAreaList.Add( Instantiate(movableArea, position, transform.rotation));
                }
            }

            // 攻撃範囲を表示
            int reach = groundedUnit.GetComponent<Unit>().unitInfo.reach;
            for (int y = -(movable+reach); y <= movable+reach; y++)
            {
                for (int x = abs(y) - (movable+reach); x <= (movable + reach) - abs(y); x++)
                {
                    if (abs(x)+abs(y)> movable) {
                        Vector3 position = Camera.GetComponent<GameMgr>().FieldBlocks[nowPosition[0] + x, nowPosition[1] + y].GetComponent<Transform>().position;
                        reachAreaList.Add(Instantiate(reachArea, position, transform.rotation));
                    }
                }
            }


        }
        // なにもアイテムがなかったら
        else
        {
            // InfomationにBlock情報を表示
            infoWindow.GetComponent<Image>().sprite = nowBlock.GetComponent<SpriteRenderer>().sprite;
            infoText.GetComponent<Text>().text = "ブロック（草）";
            //= nowBlock.GetComponent<FieldBlock>().blockInfo.outputInfo();

            // 移動範囲オブジェクトを削除
            for (int i=0; i<movableAreaList.Count; i++)
            {
                Destroy(movableAreaList[i]);
            }
            movableAreaList.Clear();

            // 攻撃範囲オブジェクトを削除
            for (int i = 0; i < reachAreaList.Count; i++)
            {
                Destroy(reachAreaList[i]);
            }
            reachAreaList.Clear();

        }


        Debug.Log(nowPosition[0]+"/"+nowPosition[1]);
    }



    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }
}
