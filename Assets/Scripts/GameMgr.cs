using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;
using General;
using UnityEngine.UI;
using UnityEngine.Networking;

/*
 * SRPGゲーム部分のマネージャ
 * WholeMgrからinitされて開始
 */

public class GameMgr : MonoBehaviour
{

    private Map map;
    private int[] unitnums;

    public GameObject btnPref;  //ボタンプレハブ
    private List<mapinfo> mapinfos;

    public GameObject unitMenuPanel;
    public GameObject unitMenu;
    public GameObject sceneBanner;
    public GameObject cursor;
    private cursor cursorComp;
    public GameObject mapList;

    public GameObject infoPanel;


    // マップ上のユニット情報
    public GameObject selectedUnit;

    // 現在のシーン情報
    //  ユニット移動先選択中、ユニット行動中、敵ターン中など
    //  シーンに応じてボタンが押された時の処理を変更
    public CAMP gameTurn { get; private set; }
    public SCENE gameScene { get; private set; }
    private SCENE preScene;

    public bool playFirst = true;
    

    // Use this for initialization
    void Start ()
    {

        map = gameObject.GetComponent<Map>();

        cursorComp = cursor.GetComponent<cursor>();
        
    }

    public void init(int[] units)
    {
        unitnums = units;

        Debug.Log("init gm");


        mapList.SetActive(true);
        //gameObject.GetComponent<MapListUtil>().getMapsFromServer();
        gameObject.GetComponent<MapListUtil>().getMapsFromLocal();
    }


    /* 廃止、MapListUtilに移行

    // Map情報をサーバから取得
    IEnumerator getMapsFromServer()
    {

        mapinfos = new List<mapinfo>();

        Debug.Log("request maps from server");

        UnityWebRequest request = UnityWebRequest.Get("http://koke.link:3000/llsrpg/map/get/all");
        // 下記でも可
        // UnityWebRequest request = new UnityWebRequest("http://example.com");
        // methodプロパティにメソッドを渡すことで任意のメソッドを利用できるようになった
        // request.method = UnityWebRequest.kHttpVerbGET;

        // リクエスト送信
        yield return request.Send();

        // 通信エラーチェック
        if (request.isNetworkError)
        {
            Debug.Log(request.error);

            getMapsFromLocal();
        }
        else
        {
            if (request.responseCode == 200)
            {
                // UTF8文字列として取得する
                string text = request.downloadHandler.text;

                Debug.Log("success request! result:" + text);

                // jsonをパースしてListに格納
                // jsonutilityそのままだと配列をパースできないのでラッパを使用 https://qiita.com/akira-sasaki/items/71c13374698b821c4d73
                mapinfo[] maparray;
                maparray = JsonUtilityHelper.MapFromJson<mapinfo>(text);

                for (int i = 0; i < maparray.Length; i++)
                {
                    mapinfos.Add(maparray[i]);
                }
            }
        }

        // UIのMapリストを設定

        setMapList();
    }




    // Mapリストを取得してリストUIに反映
    //
    private void setMapList()
    {
        //Content取得(ボタンを並べる場所)
        RectTransform content = GameObject.Find("MapListContent").GetComponent<RectTransform>();
        
        //Contentの高さ決定
        //(ボタンの高さ+ボタン同士の間隔)*ボタン数
        float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
        float btnHeight = btnPref.GetComponent<LayoutElement>().preferredHeight;
        content.sizeDelta = new Vector2(0, (btnHeight + btnSpace) * mapinfos.Count);

        for (int no = 0; no < mapinfos.Count; no++)
        {

            //ボタン生成
            GameObject btn = (GameObject)Instantiate(btnPref);

            //ボタンをContentの子に設定
            btn.transform.SetParent(content, false);

            //ボタンのテキスト変更
            btn.transform.GetComponentInChildren<Text>().text = mapinfos[no].name.ToString();

            //ボタンのクリックイベント登録
            int tempno = no;
            btn.transform.GetComponent<Button>().onClick.AddListener(() => startGame(mapinfos[no]));


        }

    }

    // Map情報をローカルファイルから取得
    private void getMapsFromLocal()
    {
        mapinfos = new List<mapinfo>();

        
        // JSONフォルダからの読み込み
        TextAsset[] json = Resources.LoadAll<TextAsset>("JSON/");

        foreach (TextAsset mapjson in json) {
            string maptext = mapjson.text;
            mapinfo map = JsonUtility.FromJson<mapinfo>(maptext);
            mapinfos.Add(map);
        }

        // Informationmapからの読み込み
        //mapinfo map = JsonUtility.FromJson<mapinfo>(new MapPlain().mapStruct());
        //mapinfos.Add(map);
        //map = JsonUtility.FromJson<mapinfo>(new MapOtonokiProof().mapStruct());
        //mapinfos.Add(map);
        //map = JsonUtility.FromJson<mapinfo>(new MapPlainStory().mapStruct());
        //mapinfos.Add(map);
        

    }

*/





    public void startGame(mapinfo mapinfo)
    {
        mapList.SetActive(false);

        if (mapinfo.mapscenarioarrays != null)
        {
            StartCoroutine(fortest(mapinfo));
        }
        else
        {
            settingSRPG(mapinfo);
            startSRPG();
        }

    }

    IEnumerator fortest(mapinfo mapinfo)
    {
        GameObject loadpanel = Instantiate(Resources.Load<GameObject>("Prefab/LoadingPanel"), GameObject.Find("Canvas").transform);

        //yield return new WaitForSeconds(2.5f);
        yield return new WaitForSeconds(0.5f);

        settingStory(mapinfo);

    }


    private void settingStory(mapinfo mapinfo)
    {
        setGameScene(SCENE.GAME_INEFFECT);

        //--- マップ生成 ---//
        gameObject.GetComponent<Map>().positioningBlocks(mapinfo);

        //--- map,unitのSRPG用設定 ---//
        map.settingforGame();

        gameObject.GetComponent<StoryMgr>().init(mapinfo, unitnums);
    }

    private void settingSRPG(mapinfo mapinfo)
    {
        //--- マップ生成 ---//
        gameObject.GetComponent<Map>().positioningBlocks(mapinfo);

        Debug.Log("main  " + map.FieldBlocks[0, 0]);

        //--- Unit配置 ---//
        gameObject.GetComponent<Map>().positioningAllyUnits(unitnums);
        gameObject.GetComponent<Map>().positioningEnemyUnits();

        //--- map,unitのSRPG用設定 ---//
        map.settingforGame();

        // カーソルを味方ユニットの位置に移動
        cursor.GetComponent<cursor>().moveCursolToUnit(map.allyUnitList[map.allyUnitList.Count - 1]);

    }


    public void startSRPG()
    {
        infoPanel.SetActive(true);

        // 先攻か後攻か
        switch (playFirst)
        {
            case true:
                gameTurn = CAMP.ENEMY;
                gameScene = SCENE.MAIN;
                break;
            case false:
                gameTurn = CAMP.ALLY;
                gameScene = SCENE.MAIN;
                break;
        }
        

        switchTurn();
    }
    

    //--- ターン切り替え ---//
    private void switchTurn()
    {
        //SCENE nextScene = gameScene;

        // 次のターン指定、カーソルをターンユニットに移動
        switch (gameTurn)
        {
            case CAMP.ALLY:
                gameTurn = CAMP.ENEMY;
                cursor.GetComponent<cursor>().moveCursolToUnit(map.enemyUnitList[map.enemyUnitList.Count - 1]);
                gameObject.GetComponent<EnemyMgr>().startEnemyTurn();
                break;

            case CAMP.ENEMY:
                gameTurn = CAMP.ALLY;
                cursor.GetComponent<cursor>().moveCursolToUnit(map.allyUnitList[map.allyUnitList.Count - 1]);
                break;

            case CAMP.GAMEMASTER:
                break;
        }

        // バナー表示後ターン移行
        sceneBanner.GetComponent<SceneBanner>().activate(gameTurn);
    }



    //--- ユニット除外処理 ---//
    public void removeUnitByList(GameObject unit)
    {
        if(unit.GetComponent<Unit>().camp == CAMP.ALLY)
        {
            map.allyUnitList.Remove(unit);
        }
        else if(unit.GetComponent<Unit>().camp == CAMP.ENEMY)
        {
            map.enemyUnitList.Remove(unit);
        }


        // 全滅判定
        if(map.allyUnitList.Count == 0)
        {
            Debug.Log("Lose...");
            gameTurn = CAMP.GAMEMASTER;
            switchTurn();
        }
        else if (map.enemyUnitList.Count ==0)
        {
            Debug.Log("Win!");
            gameTurn = CAMP.GAMEMASTER;
            switchTurn();
        }
    }



    //--- 今のBlock上のアイテムを確認し表示に反映 ---//
    public void changeInfoWindow()
    {
        int[] nowCursolPosition = new int[2];

        nowCursolPosition[0] = cursor.GetComponent<cursor>().nowPosition[0];
        nowCursolPosition[1] = cursor.GetComponent<cursor>().nowPosition[1];

        infoPanel.GetComponent<DisplayInfo>().displayBlockInfo(map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<FieldBlock>());
        
    }




    //--- ユニット移動完了時の処理---//
    public void endUnitMoving()
    {
        gameScene = SCENE.UNIT_MENU;
        changeInfoWindow();
        unitMenuPanel.SetActive(true);
        unitMenu.GetComponent<UnitMenu>().moveCursor(0, selectedUnit.GetComponent<Unit>());
        selectedUnit.GetComponent<Unit>().deleteReachArea();
    }

    //--- ユニット行動完了時の処理---//
    // 全操作ユニットの行動完了状態を確認し、完了済みだったらターン移行
    // forceend : 強制ターン終了時にTrueで呼ぶとよい
    public void endUnitActioning(bool forceend = false)
    {
        gameScene = SCENE.MAIN;

        List<GameObject> unitList = map.allyUnitList;
        if(gameTurn == CAMP.ALLY)
        {
            unitList = map.allyUnitList;
        }else if(gameTurn == CAMP.ENEMY)
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
        if (allUnitActioned || forceend)
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


    //--- 演出の設定 ---//
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
        /* for websocket

    // 自分のターンならコマンド発信
    if (gameTurn == CAMP.ALLY)
    {
        if (x == 0 && y == 1)
        {
            gameObject.GetComponent<WebsocketAccessor>().sendws("U");
        }
        else if (x == 0 && y == -1)
        {
            gameObject.GetComponent<WebsocketAccessor>().sendws("D");
        }
        else if (x == 1 && y == 0)
        {
            gameObject.GetComponent<WebsocketAccessor>().sendws("R");
        }
        else if (x == -1 && y == 0)
        {
            gameObject.GetComponent<WebsocketAccessor>().sendws("L");
        }
    }
    */


        Debug.Log("pushArrow");

        switch (gameScene)
        {
            // 演出中につき操作不可
            case SCENE.GAME_INEFFECT:
                break;

            case SCENE.UNIT_MENU:
                unitMenu.GetComponent<UnitMenu>().moveCursor(y, selectedUnit.GetComponent<Unit>());
                break;

            case SCENE.UNIT_ACTION_FORECAST:
                break;

            default:
                cursorComp.moveCursor(x, y);
                break;
        }

    }




    //--- Aボタンが押されたときの挙動 ---//
    public void pushA()
    {

    /* for websocket
    // 自分のターンならコマンド発信
    if (gameTurn == CAMP.ALLY)
    {
        gameObject.GetComponent<WebsocketAccessor>().sendws("A");
    }
    */

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
                

                // 自軍ユニットであり、選択先が移動可能範囲であれば移動
                if (selectedUnit.GetComponent<Unit>().camp == gameTurn) {
                    if (selectedUnit.GetComponent<Unit>().canMove(cursor.transform.position))
                    {
                        selectedUnit.GetComponent<Unit>().changePosition(nowCursolPosition[0], nowCursolPosition[1], true);
                    }
                }
                // 他軍ユニットだったら移動範囲表示を終了
                else
                {
                    gameScene = SCENE.MAIN;
                    selectedUnit.GetComponent<Unit>().deleteReachArea();
                }
                break;

            case SCENE.UNIT_MENU:
                unitMenuPanel.SetActive(false);

                // 待機の場合
                if(unitMenu.GetComponent<UnitMenu>().getSelectedAction()  == ACTION.WAIT)
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
                    gameScene = SCENE.UNIT_ACTION_FORECAST;
                    infoPanel.GetComponent<DisplayInfo>().displayBattleInfo(selectedUnit, groundedUnit, unitMenu.GetComponent<UnitMenu>().getSelectedAction());
                }
                break;


            case SCENE.UNIT_ACTION_FORECAST:
                // ユニットがいる＆対象可能範囲であれば
                if (groundedUnit != null && selectedUnit.GetComponent<Unit>().canReach(cursor.transform.position))
                {
                    cursor.GetComponent<cursor>().moveCursolToUnit(selectedUnit);
                    selectedUnit.GetComponent<Unit>().doAction(groundedUnit, unitMenu.GetComponent<UnitMenu>().getSelectedAction());
                }
                break;

        }

    }


    //--- Bボタンが押されたとき＝キャンセル処理 ---//
    public void pushB()
    {

                /* for websocket
        // 自分のターンならコマンド発信
        if (gameTurn == CAMP.ALLY)
        {
            gameObject.GetComponent<WebsocketAccessor>().sendws("B");
        }
        */


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

            case SCENE.UNIT_ACTION_FORECAST:
                gameScene = SCENE.UNIT_SELECT_TARGET;
                changeInfoWindow();
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
                

            case SCENE.UNIT_ACTION_FORECAST:
                if (cursor.GetComponent<cursor>().nowPosition[0] == x
                    && cursor.GetComponent<cursor>().nowPosition[1] == y)
                {
                    pushA();
                }
                break;

            default:
                cursor.GetComponent<cursor>().moveCursorToAbs(x, y);
                pushA();
                break;

        }
    }


    public void pushR()
    {
        if (gameScene == SCENE.MAIN && gameTurn == CAMP.ALLY)
            endUnitActioning(true);
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
