using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Information;


public class GameMgr : MonoBehaviour {


    public GameObject fighterPrefab;
    public GameObject sagePrefab;
    public GameObject piratesPrefab;
    public GameObject ninjaPrefab;
    public GameObject singerPrefab;
    public GameObject arcangelPrefab;
    public GameObject healerPrefab;

    public GameObject block_kusaPrefab;
    public GameObject unitMenuPanel;
    public GameObject unitMenu;
    public GameObject sceneBanner;
    private GameObject cursor;
    private GameObject infoPanel;
    private GameObject infoWindow;
    private GameObject infoText;

    // マップ情報
    public int x_mass, y_mass;
    public GameObject[,] FieldBlocks { get; set; }

    // マップ上のユニット情報
    public GameObject selectedUnit;
    public List<GameObject> allyUnitList = new List<GameObject>();
    public List<GameObject> enemyUnitList = new List<GameObject>();

    // 現在のシーン情報
    //  ユニット移動先選択中、ユニット行動中、敵ターン中など
    //  シーンに応じてボタンが押された時の処理を変更
    public enum SCENE { ALLY_MAIN, UNIT_SELECT_MOVETO, UNIT_MENU, UNIT_SELECT_TARGET, ENEMY_MAIN, GAME_INEFFECT,GAME_RESULT };
    public SCENE gameScene;
    private SCENE preScene;

    

    // Use this for initialization
    void Start ()
    {
        infoPanel = GameObject.Find("InfoPanel");
        infoWindow = GameObject.Find("InfoWindow");
        infoText = GameObject.Find("InfoText");
        cursor = GameObject.Find("cursor");
        
        init();
    }

    private void init()
    {
        createMap();
        positioningUnit();

        // カーソルを味方ユニットの位置に移動
        cursor.GetComponent<cursor>().moveCursolToUnit(allyUnitList.Count-1,allyUnitList);

        gameScene = SCENE.ENEMY_MAIN;
        switchTurn();
    }

    
    // Update is called once per frame
    void Update () {
       
	}


    //--- マップ生成 ---//
    private void createMap()
    {
        FieldBlocks = new GameObject[x_mass * 2, y_mass * 2];

        for (int x=0; x<x_mass*2; x++)
        {
            for(int y=0; y<y_mass*2; y++)
            {
                GameObject block = block_kusaPrefab;
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


    //--- Unit配置 ---//
    private void positioningUnit()
    {
        allyUnitList.Add(Instantiate(piratesPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[0].GetComponent<Unit>().init(4, 2, 1, new Eli_DS());
        allyUnitList.Add(Instantiate(fighterPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[1].GetComponent<Unit>().init(3, 3, 1, new Kanan_TT());
        allyUnitList.Add(Instantiate(ninjaPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[2].GetComponent<Unit>().init(5, 2, 1, new Rin_HN());
        allyUnitList.Add(Instantiate(singerPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[3].GetComponent<Unit>().init(2, 3, 1, new Hanayo_LB());
        allyUnitList.Add(Instantiate(healerPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[4].GetComponent<Unit>().init(3, 2, 1, new Riko_SN());
        allyUnitList.Add(Instantiate(arcangelPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[5].GetComponent<Unit>().init(4, 3, 1, new Yohane_JA());

        enemyUnitList.Add( Instantiate(fighterPrefab, new Vector3(0, 0, 0), transform.rotation));
        enemyUnitList[0].GetComponent<Unit>().init(5, 5, -1, new Enemy1_Smile());
        enemyUnitList.Add(Instantiate(fighterPrefab, new Vector3(0, 0, 0), transform.rotation));
        enemyUnitList[1].GetComponent<Unit>().init(8, 5, -1, new Enemy1_Smile());
        enemyUnitList.Add(Instantiate(fighterPrefab, new Vector3(0, 0, 0), transform.rotation));
        enemyUnitList[2].GetComponent<Unit>().init(11, 8, -1, new Enemy1_Smile());
    }


    //--- シーン切り替え ---//
    public void switchTurn()
    {
        //SCENE nextScene = gameScene;

        // 次のシーン指定、カーソルをターンユニットに移動
        switch (gameScene)
        {
            case SCENE.ALLY_MAIN:
                gameScene = SCENE.ENEMY_MAIN;
                cursor.GetComponent<cursor>().moveCursolToUnit(enemyUnitList.Count - 1, enemyUnitList);
                gameObject.GetComponent<EnemyMgr>().startEnemyTurn();
                break;

            case SCENE.ENEMY_MAIN:
                gameScene = SCENE.ALLY_MAIN;
                cursor.GetComponent<cursor>().moveCursolToUnit(allyUnitList.Count - 1, allyUnitList);
                break;

            case SCENE.GAME_RESULT:
                break;
        }

        // バナー表示後シーン移行
        sceneBanner.GetComponent<SceneBanner>().activate(gameScene);
    }



    //--- ユニット除外処理 ---//
    public void removeUnitByList(GameObject unit)
    {
        if(unit.GetComponent<Unit>().camp == 1)
        {
            allyUnitList.Remove(unit);
        }
        else if(unit.GetComponent<Unit>().camp == -1)
        {
            enemyUnitList.Remove(unit);
        }


        // 全滅判定
        if(allyUnitList.Count == 0)
        {
            Debug.Log("Lose...");
        }
        else if (enemyUnitList.Count ==0)
        {
            Debug.Log("Win!");
            gameScene = SCENE.GAME_RESULT;
            switchTurn();
        }
    }



    //--- 今のBlock上のアイテムを確認し表示に反映 ---//
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

            if(groundedUnit.GetComponent<Unit>().camp == 1)
            {
                infoPanel.GetComponent<Image>().color = new Color(220.0f/255.0f, 220.0f / 255.0f, 255.0f / 255.0f, 220.0f / 255.0f);
            }
            else if (groundedUnit.GetComponent<Unit>().camp == -1)
            {
                infoPanel.GetComponent<Image>().color = new Color(255.0f / 255.0f, 220.0f / 255.0f, 220.0f / 255.0f, 220.0f / 255.0f);

            }

            infoText.GetComponent<Text>().text = groundedUnit.GetComponent<Unit>().unitInfo.outputInfo();
            Debug.Log(groundedUnit.GetComponent<Unit>().unitInfo.outputInfo());

        }
        // なにもアイテムがなかったら
        else
        {
            // InfomationにBlock情報を表示
            infoPanel.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 220.0f / 255.0f);
            infoWindow.GetComponent<Image>().sprite = nowBlock.GetComponent<SpriteRenderer>().sprite;
            infoText.GetComponent<Text>().text = "ブロック（草）";


        }
        
    }




    //--- ユニット移動完了時の処理---//
    public void endUnitMoving()
    {
        gameScene = GameMgr.SCENE.UNIT_MENU;
        changeInfoWindow();
        unitMenuPanel.SetActive(true);
    }

    //--- ユニット行動完了時の処理---//
    public void endUnitActioning()
    {
        gameScene = SCENE.ALLY_MAIN;

        // 全ユニットの行動権確認
        bool allUnitActioned = true;
        for (int i=0; i<allyUnitList.Count; i++)
        {
            if (!(allyUnitList[i].GetComponent<Unit>().isActioned))
            {
                allUnitActioned = false;
                cursor.GetComponent<cursor>().moveCursolToUnit(i, allyUnitList);

            }           
        }

        // 全ユニットが行動完了したらターン移行
        if (allUnitActioned)
        {
            switchTurn();
            for (int i = 0; i < allyUnitList.Count; i++)
            {
                allyUnitList[i].GetComponent<Unit>().restoreActionRight();
            }
        }
    }



    public void setGameScene(SCENE gameScene)
    {
        this.gameScene = gameScene;
    }


    //--- 演出フラグの設定 ---//
    // trueの場合、演出中
    // falseの場合、演出解除
    public void setInEffecting(bool inEffecting)
    {
        if (inEffecting)
        {
            preScene = gameScene;
            gameScene = SCENE.GAME_INEFFECT;
        }
        else
        {
            gameScene = preScene;
        }
    }



    //+++ 以下ボタン処理 +++//

    //--- 十字ボタンが押されたときの挙動 ---//
    public void pushArrow(int x, int y)
    {
        switch (gameScene)
        {
            // 演出中につき操作不可
            case SCENE.GAME_INEFFECT:
                break;

            case SCENE.UNIT_MENU:
                unitMenu.GetComponent<Menu>().moveCursor(y);
                break;

            default:
                cursor.GetComponent<cursor>().moveCursor(x, y);
                break;
        }
    }




    //--- Aボタンが押されたときの挙動 ---//
    public void pushA()
    {
        int[] nowCursolPosition = { cursor.GetComponent<cursor>().nowPosition[0], cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];
        GameObject groundedUnit = nowBlock.GetComponent<FieldBlock>().GroundedUnit;

        switch (gameScene)
        {
            // 演出中につき操作不可
            case SCENE.GAME_INEFFECT:
                break;

            case SCENE.ALLY_MAIN:
                // Unitが配置されていたら&&Unitが未行動だったら
                if (groundedUnit != null && !groundedUnit.GetComponent<Unit>().isActioned)
                {
                    gameScene = SCENE.UNIT_SELECT_MOVETO;
                    groundedUnit.GetComponent<Unit>().viewMovableArea();
                }
                break;

            case SCENE.UNIT_SELECT_MOVETO:
                // 移動可能範囲であれば移動
                if (selectedUnit.GetComponent<Unit>().canMove(cursor.transform.position)) 
                {
                    selectedUnit.GetComponent<Unit>().selectMovableArea();
                }
                break;

            case SCENE.UNIT_MENU:
                unitMenuPanel.SetActive(false);

                if(unitMenu.GetComponent<Menu>().getSelectedAction() == 0){
                    gameScene = SCENE.UNIT_SELECT_TARGET;
                    selectedUnit.GetComponent<Unit>().viewTargetArea();
                }
                else
                {
                    selectedUnit.GetComponent<Unit>().endAction();
                }

                break;

            case SCENE.UNIT_SELECT_TARGET:
                // ユニットがいる＆対象可能範囲であれば移動
                if (groundedUnit != null && selectedUnit.GetComponent<Unit>().canReach(cursor.transform.position))
                {
                    selectedUnit.GetComponent<Unit>().targetAction(groundedUnit);
                }
                break;

        }
    }


    //--- Bボタンが押されたとき＝キャンセル処理 ---//
    public void pushB()
    {
        switch (gameScene)
        {
            // 演出中につき操作不可
            case SCENE.GAME_INEFFECT:
                break;

            case SCENE.ALLY_MAIN:
                // nothing to do
                break;

            case SCENE.UNIT_SELECT_MOVETO:
                gameScene = SCENE.ALLY_MAIN;
                selectedUnit.GetComponent<Unit>().deleteReachArea();
                break;

            case SCENE.UNIT_MENU:
                unitMenuPanel.SetActive(false);
                gameScene = SCENE.UNIT_SELECT_MOVETO;
                selectedUnit.GetComponent<Unit>().returnPrePosition();
                selectedUnit.GetComponent<Unit>().viewMovableArea();

                break;

            case SCENE.UNIT_SELECT_TARGET:
                gameScene = SCENE.UNIT_MENU;
                unitMenuPanel.SetActive(true);
                selectedUnit.GetComponent<Unit>().deleteReachArea();

                break;

        }
    }
    
    //+++ 以上ボタン処理 +++//






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
