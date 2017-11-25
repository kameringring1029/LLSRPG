using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * アプリケーション全体のマネージャ
 * スタートメニューから選択されて動作
 * Game:ユニット選択→ゲームマネージャ呼び出し
 * Room：ルームマネージャ呼び出し
 */

public class WholeMgr : MonoBehaviour {

    public enum WHOLEMODE { GAME,ROOM}
    public WHOLEMODE wholemode;

    public GameObject startMenuPanel;

    public GameObject selectUnitPanel;
    public GameObject musePanel;
    public GameObject aqoursPanel;

    public GameObject[] unitButtons = new GameObject[18];
    public GameObject[] unitButtonsArea = new GameObject[18];
    public GameObject[] selectedUnitArea = new GameObject[3];
    List<int> selectedUnits = new List<int>();


    private void Start()
    {
        /*
        switch (wholemode)
        {
            case WHOLEMODE.GAME:
                initGame();
                break;
            case WHOLEMODE.ROOM:
                initRoom();
                break;
        }
        */
        
    }


    // メニューボタンから選択され呼び出される
    public void selectGame()
    {
        startMenuPanel.SetActive(false);
        initGame();
    }
    public void selectRoom()
    {
        startMenuPanel.SetActive(false);
        initRoom();
    }



    //--- 指定したユニットを選択中に ---//
    public void selectUnit(int unitid)
    {
        selectedUnits.Add(unitid);
        unitButtons[unitid-1].GetComponent<RectTransform>().position = selectedUnitArea[selectedUnits.Count-1].GetComponent<RectTransform>().position;
    }

    //--- 指定したユニットを非選択中に ---//
    public void unselectUnit(int unitid)
    {
        selectedUnits.Remove(unitid);
        unitButtons[unitid-1].GetComponent<RectTransform>().position =unitButtonsArea[unitid - 1].GetComponent<RectTransform>().position;
        for(int i =0; i<selectedUnits.Count; i++)
        {
            unitButtons[selectedUnits[i]-1].GetComponent<RectTransform>().position = selectedUnitArea[i].GetComponent<RectTransform>().position;
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
        }

    }
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

}
