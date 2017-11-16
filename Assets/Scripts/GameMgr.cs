using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Information;


public class GameMgr : MonoBehaviour {

    private Map map;

    public GameObject unitMenuPanel;
    public GameObject unitMenu;
    public GameObject sceneBanner;
    public GameObject cursor;

    private GameObject infoPanel;
    private GameObject infoWindow;
    private GameObject infoText;


    // マップ上のユニット情報
    public GameObject selectedUnit;

    // 現在のシーン情報
    //  ユニット移動先選択中、ユニット行動中、敵ターン中など
    //  シーンに応じてボタンが押された時の処理を変更
    public enum TURN { ALLY, ENEMY, RESULT}
    public enum SCENE { MAIN, UNIT_SELECT_MOVETO, UNIT_MENU, UNIT_SELECT_TARGET, GAME_INEFFECT };
    public TURN gameTurn { get; private set; }
    public SCENE gameScene { get; private set; }
    private SCENE preScene;

    

    // Use this for initialization
    void Start ()
    {
        infoPanel = GameObject.Find("InfoPanel");
        infoWindow = GameObject.Find("InfoWindow");
        infoText = GameObject.Find("InfoText");
        cursor = GameObject.Find("cursor");

        map = gameObject.GetComponent<Map>();
        
        init();
    }

    private void init()
    {

        //--- マップ生成 ---//
        gameObject.GetComponent<Map>().positioningBlocks();

        Debug.Log("main  " + map.FieldBlocks[0, 0]);

        //--- Unit配置 ---//
        gameObject.GetComponent<Map>().positioningAllyUnits();
        gameObject.GetComponent<Map>().positioningEnemyUnits();


        // カーソルを味方ユニットの位置に移動
        cursor.GetComponent<cursor>().moveCursolToUnit(map.allyUnitList[map.allyUnitList.Count - 1]);

        gameTurn = TURN.ENEMY;
        gameScene = SCENE.MAIN;
        switchTurn();
    }

    
    // Update is called once per frame
    void Update () {
       
	}

    

    //--- シーン切り替え ---//
    private void switchTurn()
    {
        //SCENE nextScene = gameScene;

        // 次のシーン指定、カーソルをターンユニットに移動
        switch (gameTurn)
        {
            case TURN.ALLY:
                gameTurn = TURN.ENEMY;
                cursor.GetComponent<cursor>().moveCursolToUnit(map.enemyUnitList[map.enemyUnitList.Count - 1]);
                gameObject.GetComponent<EnemyMgr>().startEnemyTurn();
                break;

            case TURN.ENEMY:
                gameTurn = TURN.ALLY;
                cursor.GetComponent<cursor>().moveCursolToUnit(map.allyUnitList[map.allyUnitList.Count - 1]);
                break;

            case TURN.RESULT:
                break;
        }

        // バナー表示後シーン移行
        sceneBanner.GetComponent<SceneBanner>().activate(gameTurn);
    }



    //--- ユニット除外処理 ---//
    public void removeUnitByList(GameObject unit)
    {
        if(unit.GetComponent<Unit>().camp == 1)
        {
            map.allyUnitList.Remove(unit);
        }
        else if(unit.GetComponent<Unit>().camp == -1)
        {
            map.enemyUnitList.Remove(unit);
        }


        // 全滅判定
        if(map.allyUnitList.Count == 0)
        {
            Debug.Log("Lose...");
        }
        else if (map.enemyUnitList.Count ==0)
        {
            Debug.Log("Win!");
            gameTurn = TURN.RESULT;
            switchTurn();
        }
    }



    //--- 今のBlock上のアイテムを確認し表示に反映 ---//
    public void changeInfoWindow()
    {
        int[] nowCursolPosition = new int[2];

        nowCursolPosition[0] = cursor.GetComponent<cursor>().nowPosition[0];
        nowCursolPosition[1] = cursor.GetComponent<cursor>().nowPosition[1];

        GameObject nowBlock = map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];
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
            infoText.GetComponent<Text>().text = nowBlock.GetComponent<FieldBlock>().outputInfo();


        }
        
    }




    //--- ユニット移動完了時の処理---//
    public void endUnitMoving()
    {
        gameScene = GameMgr.SCENE.UNIT_MENU;
        changeInfoWindow();
        unitMenuPanel.SetActive(true);
        unitMenu.GetComponent<UnitMenu>().moveCursor(0, selectedUnit.GetComponent<Unit>());
        selectedUnit.GetComponent<Unit>().deleteReachArea();
    }

    //--- ユニット行動完了時の処理---//
    public void endUnitActioning()
    {
        gameScene = SCENE.MAIN;

        List<GameObject> unitList = map.allyUnitList;
        if(gameTurn == TURN.ALLY)
        {
            unitList = map.allyUnitList;
        }else if(gameTurn == TURN.ENEMY)
        {
            unitList = map.enemyUnitList; 
        }

        // 全ユニットの行動権確認
        bool allUnitActioned = true;
        for (int i=0; i< unitList.Count; i++)
        {
            if (!(unitList[i].GetComponent<Unit>().isActioned))
            {
                allUnitActioned = false;
                cursor.GetComponent<cursor>().moveCursolToUnit(unitList[i]);

            }           
        }

        // 全ユニットが行動完了したらターン移行
        if (allUnitActioned)
        {
            switchTurn();
            for (int i = 0; i < unitList.Count; i++)
            {
                unitList[i].GetComponent<Unit>().restoreActionRight();
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
            if(gameScene != SCENE.GAME_INEFFECT)
            {
                preScene = gameScene;
                gameScene = SCENE.GAME_INEFFECT;

            }
        }
        else
        {
            gameScene = preScene;
        }
    }


    //++++++++++++++++++++++//
    //+++ 以下ボタン処理 +++//
    //++++++++++++++++++++++//

    //--- 十字ボタンが押されたときの挙動 ---//
    public void pushArrow(int x, int y)
    {
        Debug.Log("pushArrow");

        switch (gameScene)
        {
            // 演出中につき操作不可
            case SCENE.GAME_INEFFECT:
                break;

            case SCENE.UNIT_MENU:
                unitMenu.GetComponent<UnitMenu>().moveCursor(y, selectedUnit.GetComponent<Unit>());
                break;

            default:
                cursor.GetComponent<cursor>().moveCursor(x, y);
                break;
        }
    }




    //--- Aボタンが押されたときの挙動 ---//
    public void pushA()
    {

        Debug.Log("pushA");

        int[] nowCursolPosition = { cursor.GetComponent<cursor>().nowPosition[0], cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];
        GameObject groundedUnit = nowBlock.GetComponent<FieldBlock>().GroundedUnit;

        switch (gameScene)
        {
            // 演出中につき操作不可
            case SCENE.GAME_INEFFECT:
                break;

            case SCENE.MAIN:
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
                    selectedUnit.GetComponent<Unit>().changePosition(nowCursolPosition[0], nowCursolPosition[1], true);
                }
                break;

            case SCENE.UNIT_MENU:
                unitMenuPanel.SetActive(false);

                // 待機の場合
                if(unitMenu.GetComponent<UnitMenu>().getSelectedAction() 
                    == selectedUnit.GetComponent<Unit>().getActionableList().Count - 1)
                {
                    selectedUnit.GetComponent<Unit>().endAction();
                }
                // 待機ではない場合（対象アクション実施）
                else
                {
                    gameScene = SCENE.UNIT_SELECT_TARGET;
                    selectedUnit.GetComponent<Unit>().viewTargetArea();
                }

                break;

            case SCENE.UNIT_SELECT_TARGET:
                // ユニットがいる＆対象可能範囲であれば
                if (groundedUnit != null && selectedUnit.GetComponent<Unit>().canReach(cursor.transform.position))
                {
                    selectedUnit.GetComponent<Unit>().doAction(groundedUnit, unitMenu.GetComponent<UnitMenu>().getSelectedAction());
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

            case SCENE.MAIN:
                // nothing to do
                break;

            case SCENE.UNIT_SELECT_MOVETO:
                gameScene = SCENE.MAIN;
                selectedUnit.GetComponent<Unit>().deleteReachArea();
                cursor.GetComponent<cursor>().moveCursolToUnit(selectedUnit);
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


    //--- フィールドブロックが選択されたとき ---//
    public void pushBlock(int x, int y)
    {
        Debug.Log("pushBlock");


        switch (gameScene)
        {
            // 演出中につき操作不可
            case SCENE.GAME_INEFFECT:
                break;

            case SCENE.UNIT_MENU:
                pushA();
                break;

            case SCENE.UNIT_SELECT_TARGET:
                if (cursor.GetComponent<cursor>().nowPosition[0] == x
                    && cursor.GetComponent<cursor>().nowPosition[1] == y)
                {
                    pushA();
                }
                else
                {
                    cursor.GetComponent<cursor>().moveCursorToAbs(x, y);
                }
                break;

            default:
                cursor.GetComponent<cursor>().moveCursorToAbs(x, y);
                pushA();
                break;

        }
    }


    //++++++++++++++++++++++//
    //+++ 以上ボタン処理 +++//
    //++++++++++++++++++++++//



        
}
