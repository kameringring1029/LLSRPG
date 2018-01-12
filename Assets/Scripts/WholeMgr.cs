using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using General;

/*
 * アプリケーション全体のマネージャ
 * スタートメニューから選択されて動作
 * Game:ユニット選択→ゲームマネージャ呼び出し
 * Room：ルームマネージャ呼び出し
 */

public class WholeMgr : MonoBehaviour {

    public WHOLEMODE wholemode = WHOLEMODE.SELECTMODE;

    private int wholecursor = 1;

    public GameObject wholecursorIcon; 
    public GameObject startMenuPanel;
    public GameObject selectUnitPanel;

    public UnitSelect unitSelect; 


    private void Start()
    {
        gameObject.GetComponent<Camera>().orthographicSize = 1.5f;

        wholecursorIcon.GetComponent<RectTransform>().position =
            GameObject.Find("StartGameButton").GetComponent<RectTransform>().position;
    }


    // メニューボタンから選択され呼び出される
    public void selectGame()
    {
        Debug.Log("selectGame()");
        startMenuPanel.SetActive(false);
        initGame();
    }
    public void selectRoom()
    {
        startMenuPanel.SetActive(false);
        initRoom();
    }




    //--- SRPGスタート ---//
    private void initGame()
    {

        selectUnitPanel.SetActive(true);
        wholemode = WHOLEMODE.SELECTUNIT;
        unitSelect = new UnitSelect(wholecursorIcon);

   }



    // ユニット選択画面を消去、選択されたユニットをGameMgrに渡す
    public void startGame()
    {
        selectUnitPanel.SetActive(false);
        wholecursorIcon.SetActive(false);

        wholemode = WHOLEMODE.GAME;
        StartCoroutine("startGM");

    }
    IEnumerator startGM()
    {
        gameObject.GetComponent<GameMgr>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<GameMgr>().init(unitSelect.selectedUnits.ToArray());
    }



    //---Roomスタート ---//
    private void initRoom()
    {
        wholecursorIcon.SetActive(false);
        wholemode = WHOLEMODE.ROOM;
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
                    wholecursorIcon.GetComponent<RectTransform>().position =
                        GameObject.Find("StartGameButton").GetComponent<RectTransform>().position;
                }
                else if (wholecursor == 2)
                {
                    GameObject.Find("StartGameButton").GetComponent<Image>().color = new Color(155.0f/255.0f, 155.0f / 255.0f, 155.0f / 255.0f, 255f);
                    GameObject.Find("StartRoomButton").GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                    wholecursorIcon.GetComponent<RectTransform>().position =
                     GameObject.Find("StartRoomButton").GetComponent<RectTransform>().position;
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
