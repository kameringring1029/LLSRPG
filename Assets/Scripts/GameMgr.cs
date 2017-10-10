using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameMgr : MonoBehaviour {


    public int x_mass, y_mass;
    public GameObject block_kusa;
    public GameObject unit;
    public GameObject movableArea;
    private List<GameObject> movableAreaList = new List<GameObject>();
    public GameObject reachArea;
    private List<GameObject> reachAreaList = new List<GameObject>();
    public GameObject unitMenu;
    
    public GameObject explosion;

    public GameObject[,] FieldBlocks { get; set; }

    private GameObject cursor;
    private GameObject infoWindow;
    private GameObject infoText;
    private GameObject selectedUnit;

    public enum SCENE { MY, UNIT_SELECT_MOVETO, UNIT_ACTIONING, UNIT_MENU, UNIT_SELECT_ATTACK, ENEMY };
    public SCENE gameScene = SCENE.MY;


    // Use this for initialization
    void Start ()
    {
        infoWindow = GameObject.Find("InfoWindow");
        infoText = GameObject.Find("InfoText");
        cursor = GameObject.Find("cursor");

        init();
    }

    void init()
    {

        FieldBlocks = new GameObject[x_mass * 2, y_mass * 2];
        createMap();
        positioningUnit();
        cursor.GetComponent<cursor>().moveCursorToAbs(4, 4);

        gameScene = SCENE.MY;
    }



    // Update is called once per frame
    void Update () {
		
	}


    // マップ生成
    private void createMap()
    {
        for(int x=0; x<x_mass*2; x++)
        {
            for(int y=0; y<y_mass*2; y++)
            {
                GameObject block = block_kusa;
                Vector3 position = new Vector3(x - y, y_mass - y/2.0f - x/2.0f, 0);
                                
                FieldBlocks[x,y] = Instantiate(block, position, transform.rotation);
                FieldBlocks[x, y].GetComponent<FieldBlock>().position[0] = x;
                FieldBlocks[x, y].GetComponent<FieldBlock>().position[1] = y;

                FieldBlocks[x, y].name = x + "_" + y + "_kusa";
                int distance = (abs(x) + abs(y));
                FieldBlocks[x, y].GetComponent<SpriteRenderer>().sortingOrder = distance;
            }

        }
        
    }


    // Unit配置
    private void positioningUnit()
    {
        GameObject unit1 = Instantiate(unit, new Vector3(0,0,0), transform.rotation);
        unit1.GetComponent<Unit>().init(5, 4, 1);
        GameObject enemy = Instantiate(unit, new Vector3(0, 0, 0), transform.rotation);
        enemy.GetComponent<Unit>().init(3, 5, -1);
    }

    


    // 今のBlock上のアイテムを確認し表示に反映
    public void changeInfoWindow()
    {
        int[] nowCursolPosition = new int[2];

        nowCursolPosition[0] = cursor.GetComponent<cursor>().nowPosition[0];
        nowCursolPosition[1] = cursor.GetComponent<cursor>().nowPosition[1];

        GameObject nowBlock = FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];
        GameObject groundedUnit = nowBlock.GetComponent<FieldBlock>().GroundedUnit;

        // Unitが配置されていたら
        if (groundedUnit != null)
        {
            // Infomationを更新
            infoWindow.GetComponent<Image>().sprite =
                groundedUnit.GetComponent<SpriteRenderer>().sprite;

            infoText.GetComponent<Text>().text = groundedUnit.GetComponent<Unit>().unitInfo.outputInfo();
            Debug.Log(groundedUnit.GetComponent<Unit>().unitInfo.outputInfo());

        }
        // なにもアイテムがなかったら
        else
        {
            // InfomationにBlock情報を表示
            infoWindow.GetComponent<Image>().sprite = nowBlock.GetComponent<SpriteRenderer>().sprite;
            infoText.GetComponent<Text>().text = "ブロック（草）";
            //= nowBlock.GetComponent<FieldBlock>().blockInfo.outputInfo();


        }



    }



    // Aボタンが押されたときの挙動
    public void pushA()
    {
        int[] nowCursolPosition = new int[2];

        nowCursolPosition[0] = cursor.GetComponent<cursor>().nowPosition[0];
        nowCursolPosition[1] = cursor.GetComponent<cursor>().nowPosition[1];
        GameObject nowBlock = FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];
        GameObject groundedUnit = nowBlock.GetComponent<FieldBlock>().GroundedUnit;

        switch (gameScene)
        {
            case SCENE.MY:
                // Unitが配置されていたら
                if (groundedUnit != null)
                {
                    int movable = groundedUnit.GetComponent<Unit>().unitInfo.movable[1];
                    viewArea(nowCursolPosition, nowBlock, groundedUnit, movable);

                    gameScene = SCENE.UNIT_SELECT_MOVETO;
                }
                break;

            case SCENE.UNIT_SELECT_MOVETO:
                selectMovableArea(nowCursolPosition, nowBlock, groundedUnit);
                break;

            case SCENE.UNIT_MENU:
                unitMenu.SetActive(false);
                viewArea(nowCursolPosition, nowBlock, groundedUnit, 0);

                gameScene = SCENE.UNIT_SELECT_ATTACK;
                break;

            case SCENE.UNIT_SELECT_ATTACK:
                selectAttackTarget(nowCursolPosition, nowBlock, groundedUnit);

                gameScene = SCENE.MY;
                break;

        }
    }


    // 移動/攻撃範囲の表示
    public void viewArea(int[] nowCursolPosition, GameObject nowBlock, GameObject groundedUnit, int movable)
    {
        // Unitが配置されていたら
        if (groundedUnit != null)
        {
            // 移動範囲を表示
            for (int y = -movable; y <= movable; y++)
            {
                for (int x = abs(y) - movable; x <= movable - abs(y); x++)
                {
                    // 中心以外かつマップエリア内
                    if (!(x == 0 && y == 0)
                        && (nowCursolPosition[0] + x >= 0 && nowCursolPosition[1] + y >= 0) &&
                        (nowCursolPosition[0] + x < x_mass * 2 && nowCursolPosition[1] + y < y_mass * 2))
                    {
                        Vector3 position = FieldBlocks[nowCursolPosition[0] + x, nowCursolPosition[1] + y].GetComponent<Transform>().position;
                        movableAreaList.Add(Instantiate(movableArea, position, transform.rotation));
                    }
                }
            }

            // 攻撃範囲を表示
            int reach = groundedUnit.GetComponent<Unit>().unitInfo.reach[1];
            for (int y = -(movable + reach); y <= movable + reach; y++)
            {
                for (int x = abs(y) - (movable + reach); x <= (movable + reach) - abs(y); x++)
                {
                    // 移動範囲以外かつマップエリア内
                    if ((abs(x) + abs(y) > movable) &&
                        (nowCursolPosition[0] + x >= 0 && nowCursolPosition[1] + y >= 0) &&
                        (nowCursolPosition[0] + x < x_mass * 2 && nowCursolPosition[1] + y < y_mass * 2))
                    {
                        Vector3 position = FieldBlocks[nowCursolPosition[0] + x, nowCursolPosition[1] + y].GetComponent<Transform>().position;
                        reachAreaList.Add(Instantiate(reachArea, position, transform.rotation));
                    }
                }
            }

            selectedUnit = groundedUnit;

        }
    }





    // Unitの移動範囲を決定
    private void selectMovableArea(int[] nowCursolPosition, GameObject nowBlock, GameObject groundedUnit)
    {
        //TODO 移動可能範囲

        selectedUnit.GetComponent<Unit>().changePosition(nowCursolPosition[0], nowCursolPosition[1], true);
        deleteReachArea();
    }


    // Unit移動完了時の処理 called by unit
    public void endUnitMoving()
    {
        gameScene = GameMgr.SCENE.UNIT_MENU;
        changeInfoWindow();
        unitMenu.SetActive(true);
    }


    private void selectAttackTarget(int[] nowCursolPosition, GameObject nowBlock, GameObject groundedUnit)
    {
        //TODO 攻撃可能範囲

        Vector2 targetPosition = FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<Transform>().position;

        if (groundedUnit != null)
        {
            int damage = selectedUnit.GetComponent<Unit>().unitInfo.attack_phy[1]
            - groundedUnit.GetComponent<Unit>().unitInfo.guard_phy[1];
            groundedUnit.GetComponent<Unit>().unitInfo.hp[1] -= damage;
        }
        Instantiate(explosion, targetPosition, transform.rotation);
        deleteReachArea();
    }

    // Unitの攻撃範囲/移動範囲用のパネルオブジェクトを除去
    private void deleteReachArea()
    {
        // 移動範囲オブジェクトを削除
        for (int i = 0; i < movableAreaList.Count; i++)
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


    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }

    private int vect(int a, int b)
    {
        int vecter = 1;
        if(a < x_mass || b < y_mass)
        {
            vecter = -1;
        }
        return vecter;
    }
}
