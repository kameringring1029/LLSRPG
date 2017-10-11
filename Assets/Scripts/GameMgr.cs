using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Information;


public class GameMgr : MonoBehaviour {


    public int x_mass, y_mass;
    public GameObject block_kusa;
    public GameObject unit;
    public GameObject unitMenuPanel;
    public GameObject unitMenu;
    
    public GameObject explosion;

    public GameObject[,] FieldBlocks { get; set; }

    private GameObject cursor;
    private GameObject infoPanel;
    private GameObject infoWindow;
    private GameObject infoText;
    public GameObject selectedUnit;

    public enum SCENE { MY, UNIT_SELECT_MOVETO, UNIT_ACTIONING, UNIT_MENU, UNIT_SELECT_TARGET, ENEMY };
    public SCENE gameScene = SCENE.MY;


    // Use this for initialization
    void Start ()
    {
        infoPanel = GameObject.Find("InfoPanel");
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
        cursor.GetComponent<cursor>().moveCursorToAbs(4, 5);

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
        unit1.GetComponent<Unit>().init(5, 4, 1, new Kanan_TT());
        GameObject enemy = Instantiate(unit, new Vector3(0, 0, 0), transform.rotation);
        enemy.GetComponent<Unit>().init(3, 5, -1, new Enemy1_Smile());
        GameObject enemy2 = Instantiate(unit, new Vector3(0, 0, 0), transform.rotation);
        enemy2.GetComponent<Unit>().init(8, 5, -1, new Enemy1_Smile());
        GameObject enemy3 = Instantiate(unit, new Vector3(0, 0, 0), transform.rotation);
        enemy3.GetComponent<Unit>().init(2, 8, -1, new Enemy1_Smile());
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


    //--- 十字ボタンが押されたときの挙動 ---//
    public void pushArrow(int x, int y)
    {
        switch (gameScene){
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
            case SCENE.MY:
                // Unitが配置されていたら
                if (groundedUnit != null)
                {
                    gameScene = SCENE.UNIT_SELECT_MOVETO;
                    groundedUnit.GetComponent<Unit>().viewArea();

                }
                break;

            case SCENE.UNIT_SELECT_MOVETO:
                selectedUnit.GetComponent<Unit>().selectMovableArea();
                break;

            case SCENE.UNIT_MENU:
                unitMenuPanel.SetActive(false);

                if(unitMenu.GetComponent<Menu>().getSelectedAction() == 0){
                    gameScene = SCENE.UNIT_SELECT_TARGET;
                    selectedUnit.GetComponent<Unit>().viewArea();
                }
                else
                {
                    gameScene = SCENE.MY;
                }

                break;

            case SCENE.UNIT_SELECT_TARGET:
                if (groundedUnit != null)
                {
                    selectedUnit.GetComponent<Unit>().targetAction(groundedUnit);
                    gameScene = SCENE.MY;
                }
                break;

        }
    }


    //--- Bボタンが ---//
    public void pushB()
    {
        switch (gameScene)
        {
            case SCENE.MY:
                // nothing to do
                break;

            case SCENE.UNIT_SELECT_MOVETO:
                gameScene = SCENE.MY;
                selectedUnit.GetComponent<Unit>().deleteReachArea();
                break;

            case SCENE.UNIT_MENU:
                unitMenuPanel.SetActive(false);
                gameScene = SCENE.UNIT_SELECT_MOVETO;
                selectedUnit.GetComponent<Unit>().returnPrePosition();
                selectedUnit.GetComponent<Unit>().viewArea();

                break;

            case SCENE.UNIT_SELECT_TARGET:
                gameScene = SCENE.UNIT_MENU;
                unitMenuPanel.SetActive(true);
                selectedUnit.GetComponent<Unit>().deleteReachArea();

                break;

        }
    }



    //--- ユニット移動完了時の処理---//
    public void endUnitMoving()
    {
        gameScene = GameMgr.SCENE.UNIT_MENU;
        changeInfoWindow();
        unitMenuPanel.SetActive(true);
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
