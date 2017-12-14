using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * アプリケーション全体のマネージャ
 * スタートメニューから選択されて動作
 * Game:ユニット選択→ゲームマネージャ呼び出し
 * Room：ルームマネージャ呼び出し
 */

public class WholeMgr : MonoBehaviour {

    private enum WHOLEMODE { SELECTMODE, GAME,ROOM}
    private WHOLEMODE wholemode = WHOLEMODE.SELECTMODE;

    private int wholecursor = 0;
    List<int> selectedUnits = new List<int>();


    public GameObject startMenuPanel;
    public GameObject selectUnitPanel;
    public GameObject musePanel;
    public GameObject aqoursPanel;

    public GameObject[] unitButtons = new GameObject[18];
    public GameObject[] unitButtonsArea = new GameObject[18];
    public GameObject[] selectedUnitArea = new GameObject[3];

    private List<GameObject> selectUnitButtonListOnMuse = new List<GameObject>();
    private List<GameObject> selectUnitButtonListOnAqours = new List<GameObject>();


    private void Start()
    {
        gameObject.GetComponent<Camera>().orthographicSize = 1.5f;

    }


    // メニューボタンから選択され呼び出される
    public void selectGame()
    {
        startMenuPanel.SetActive(false);
        wholemode = WHOLEMODE.GAME;
        wholecursor = 0;
        initGame();
    }
    public void selectRoom()
    {
        startMenuPanel.SetActive(false);
        wholemode = WHOLEMODE.ROOM;
        initRoom();
    }




    //--- SRPGスタート ---//
    private void initGame()
    {
        selectUnitPanel.SetActive(true);

        for (int i = 0; i < 9; i++)
        {
            unitButtons[i] = GameObject.Find("ButtonMuse0" + (i + 1));
            unitButtons[i + 9] = GameObject.Find("ButtonAqours0" + (i + 1));

            unitButtonsArea[i] = GameObject.Find("Muse0" + (i + 1));
            unitButtonsArea[i + 9] = GameObject.Find("Aqours0" + (i + 1));


            if (unitButtons[i]) selectUnitButtonListOnMuse.Add(unitButtons[i]);
            if (unitButtons[i + 9]) selectUnitButtonListOnAqours.Add(unitButtons[i + 9]);


            if(i == 2)
            {
                selectUnitButtonListOnMuse.Add(GameObject.Find("displayMuse"));
                selectUnitButtonListOnAqours.Add(GameObject.Find("displayMuse"));
            }else if(i == 5)
            {
                selectUnitButtonListOnMuse.Add(GameObject.Find("displayAqours"));
                selectUnitButtonListOnAqours.Add(GameObject.Find("displayAqours"));

            }else if(i == 8)
            {
                selectUnitButtonListOnMuse.Add(GameObject.Find("unitSelectOk"));
                selectUnitButtonListOnAqours.Add(GameObject.Find("unitSelectOk"));
            }

        }

    }


    //+++ ユニット選択画面での挙動 +++//

    //--- 指定したユニットを選択中に ---//
    public void selectUnit(int unitid)
    {
        selectedUnits.Add(unitid);
        unitButtons[unitid - 1].GetComponent<RectTransform>().position = selectedUnitArea[selectedUnits.Count - 1].GetComponent<RectTransform>().position;
    }

    //--- 指定したユニットを非選択中に ---//
    public void unselectUnit(int unitid)
    {
        selectedUnits.Remove(unitid);
        unitButtons[unitid - 1].GetComponent<RectTransform>().position = unitButtonsArea[unitid - 1].GetComponent<RectTransform>().position;
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            unitButtons[selectedUnits[i] - 1].GetComponent<RectTransform>().position = selectedUnitArea[i].GetComponent<RectTransform>().position;
        }

    }


    //--- ユニット選択タブの切り替え ---//
    public void displayMuse()
    {
        // musepanelを親の中で最前面に
        musePanel.transform.SetAsLastSibling();
    }
    public void displayAqours()
    {
        // aqourspanelを親の中で最前面に
        aqoursPanel.transform.SetAsLastSibling();
    }


    //+++ ユニット選択画面での挙動 ここまで +++//


    // ユニット選択画面を消去、選択されたユニットをGameMgrに渡す
    public void startGame()
    {
        selectUnitPanel.SetActive(false);
        StartCoroutine("startGM");

    }
    IEnumerator startGM()
    {
        gameObject.GetComponent<GameMgr>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<GameMgr>().init(selectedUnits.ToArray());
    }



    //---Roomスタート ---//
    private void initRoom()
    {
        StartCoroutine("startRoom");
    }

    IEnumerator startRoom()
    {
        gameObject.GetComponent<RoomMgr>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<RoomMgr>().init();
    }




    public void pushArrow(int horizon, int vertical)
    {
        wholecursor += horizon + vertical;

        switch (wholemode)
        {
            case WHOLEMODE.SELECTMODE:
                // カーソルのオーバーフロー処理
                if(wholecursor < 0)
                {
                    wholecursor = 2;
                }else if(wholecursor > 2)
                {
                    wholecursor = 1;
                }

                // カーソルの移動
               if(wholecursor == 1)
                {
                    GameObject.Find("StartGameButton").GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                    GameObject.Find("StartRoomButton").GetComponent<Image>().color = new Color(155.0f / 255.0f, 155.0f / 255.0f, 155.0f / 255.0f, 255f);
                }
                else if (wholecursor == 2)
                {
                    GameObject.Find("StartGameButton").GetComponent<Image>().color = new Color(155.0f/255.0f, 155.0f / 255.0f, 155.0f / 255.0f, 255f);
                    GameObject.Find("StartRoomButton").GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                }
                break;

            case WHOLEMODE.GAME:
                // カーソルのオーバーフロー処理

                // カーソルの移動

                break;

        }
    }

    public void pushA()
    {

        switch (wholemode)
        {
            case WHOLEMODE.SELECTMODE:
                // カーソルの決定
                if (wholecursor == 1)
                {
                    selectGame();
                }
                else if (wholecursor == 2)
                {
                    selectRoom();
                }

                break;

            case WHOLEMODE.GAME:
                

                break;

        }
    }

    public void pushB()
    {

    }

    public void pushR()
    {

    }
    public void pushL()
    {

    }

}
